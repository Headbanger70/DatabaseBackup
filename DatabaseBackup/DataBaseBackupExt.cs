using KeePass.Plugins;
using KeePass.Forms;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Net.Cache;
using DatabaseBackup.UI;

namespace DatabaseBackup
{
    /// <summary>
    /// This is the main plugin class. It must be named exactly the same as the
    /// namespace and must be derived from the KeePass Plugin class.
    /// </summary>
    public sealed class DatabaseBackupExt : Plugin
    {
        // Host that we use to gain access to the KeePass GUI and all the good
        // database pieces.
        private IPluginHost m_host = null;

        private ToolStripSeparator m_toolsSeparator = null;
        private ToolStripMenuItem m_mainPopup = null;
        private ToolStripMenuItem m_backupNowToolItem = null;

        // This flag makes it so we only make backups when the database has
        // actually been modified.
        private bool m_databaseModified = false;

        /// <summary>
        /// Specifies any kind of backup errors that were critical to the
        /// operation of the backup.
        /// </summary>
        enum BackupError
        {
            /// <summary>
            /// Backup completed successfully.
            /// </summary>
            OK,

            /// <summary>
            /// No database was open when the backup was requested.
            /// </summary>
            DATABASE_CLOSED,

            /// <summary>
            /// No folders are configured for backup.
            /// </summary>
            NO_BACKUP_FOLDERS,

            /// <summary>
            /// None of the configured folders were available for backup.
            /// </summary>
            NO_VALID_FOLDERS,
        }

        /// <summary>
        /// The <c>Initialize</c> function is called by KeePass when
        /// you should initialize your plugin (create menu items, etc.).
        /// </summary>
        /// <param name="host">Plugin host interface. By using this
        /// interface, you can access the KeePass main window and the
        /// currently opened database.</param>
        /// <returns>You must return <c>true</c> in order to signal
        /// successful initialization. If you return <c>false</c>,
        /// KeePass unloads your plugin (without calling the
        /// <c>Terminate</c> function of your plugin).</returns>
        public override bool Initialize(IPluginHost host)
        {
            Debug.Assert(host != null);

            if(host == null) return false;
            m_host = host;

            AddMenuItems();
            ConnectEvents();

            return true;
        }

        /// <summary>
        /// The <c>Terminate</c> function is called by KeePass when
        /// you should free all resources, close open files/streams,
        /// etc. It is also recommended that you remove all your
        /// plugin menu items from the KeePass menu.
        /// </summary>
        public override void Terminate()
        {
            RemoveMenuItems();
            DisconnectEvents();
        }

        /// <summary>
        /// Adds all of the menu items associated with this plugin.
        /// </summary>
        private void AddMenuItems()
        {
            // Get a reference to the 'Tools' menu item container
            var toolsMenu = m_host.MainWindow.ToolsMenu.DropDownItems;

            // Add a separator at the bottom
            m_toolsSeparator = new ToolStripSeparator();
            toolsMenu.Add(m_toolsSeparator);

            // Add the popup menu item
            m_mainPopup = new ToolStripMenuItem();
            m_mainPopup.Text = "Database Backup";
            toolsMenu.Add(m_mainPopup);

            // Add menu item 'Backup now'
            m_backupNowToolItem = new ToolStripMenuItem();
            m_backupNowToolItem.Text = "Backup Now";
            m_backupNowToolItem.Click += OnMenuBackupNow;
            m_backupNowToolItem.Enabled = false;
            m_mainPopup.DropDownItems.Add(m_backupNowToolItem);

            // Add a separator
            m_toolsSeparator = new ToolStripSeparator();
            m_mainPopup.DropDownItems.Add(m_toolsSeparator);

            // Add menu item 'Auto Backup'
            var autoBackup = new ToolStripMenuItem();
            autoBackup.Text = "Automatically Backup DB";
            autoBackup.Checked = Properties.Settings.Default.AutoBackup;
            autoBackup.Click += OnMenuAutomaticBackup;
            autoBackup.Enabled = true;
            m_mainPopup.DropDownItems.Add(autoBackup);

            // Add menu item 'Configure'
            var configure = new ToolStripMenuItem();
            configure.Text = "Configure...";
            configure.Click += OnMenuConfig;
            configure.Enabled = true;
            m_mainPopup.DropDownItems.Add(configure);
        }

        /// <summary>
        /// Removes all of the menu items that we had added to the main windows.
        /// </summary>
        private void RemoveMenuItems()
        {
            ToolStripItemCollection tsMenu = m_host.MainWindow.ToolsMenu.DropDownItems;

            tsMenu.Remove(m_toolsSeparator);
            tsMenu.Remove(m_mainPopup);
        }

        /// <summary>
        /// Connects all of the events that we want to keep an eye on.
        /// </summary>
        private void ConnectEvents()
        {
            m_host.MainWindow.FileOpened += OnFileOpened;
            m_host.MainWindow.FileSaving += OnFileSaving;
            m_host.MainWindow.FileSaved += OnFileSaved;
            m_host.MainWindow.FileClosingPre += OnFileClosing;
            m_host.MainWindow.FileCreated += OnFileCreated;
        }

        /// <summary>
        /// Disconnects all of the events that we were listening for.
        /// </summary>
        private void DisconnectEvents()
        {
            m_host.MainWindow.FileOpened -= OnFileOpened;
            m_host.MainWindow.FileSaving -= OnFileSaving;
            m_host.MainWindow.FileSaved -= OnFileSaved;
            m_host.MainWindow.FileClosingPre -= OnFileClosing;
            m_host.MainWindow.FileCreated -= OnFileCreated;
        }

        /// <summary>
        /// Rename a "*_log" file into a ".log" file for backward compatibility
        /// </summary>
        private void RenameLogFile(string SourceFilename, string BackupFolder) {
            string oldLogFile = Path.Combine(BackupFolder, SourceFilename + "_log");
            string newLogFile = Path.Combine(BackupFolder, SourceFilename + ".log");

            if (File.Exists(oldLogFile) && !File.Exists(newLogFile)) {
                File.Move(oldLogFile, newLogFile);
            }
        }

        /// <summary>
        /// Delete a directory, if it is empty
        /// </summary>
        private void DelEmptyDirectory(string DirName) {
            if (Directory.GetFileSystemEntries(DirName).Length == 0) {
                // If empty, delete it
                Directory.Delete(DirName);
            }
        }

        /// <summary>
        /// Moves a backup file (*.kdbx, *.log, *_log) from one directory (BaseDir/SourceDir) to another
        /// directory (BaseDir/DestDir) and deletes the source directory if it becomes empty
        /// </summary>
        private void MoveBackupFile(string Filename, string BaseDir, string SourceDir, string DestDir) {
            string SourcePath = Path.Combine(BaseDir, SourceDir);
            string DestPath = Path.Combine(BaseDir, DestDir);
            string oldFile = Path.Combine(SourcePath, Filename); // Overload with 3 and 4 arguments is only possible since .NET 4 :-/
            string newFile = Path.Combine(DestPath, Filename);

            if (File.Exists(oldFile)) {
                if (File.Exists(newFile)) {
                    // If it is already there, delete the source file
                    File.Delete(oldFile);
                } else {
                    File.Move(oldFile, newFile);
                }

                // Check whether the directory not equals the base directory (which never should be removed)
                if (Properties.Settings.Default.AutoDelSubDirs &&
                    !Path.GetFullPath(SourcePath).Equals(Path.GetFullPath(BaseDir))) {

                    // and eventually delete it
                    DelEmptyDirectory(SourcePath);
                }
            }
        }
        
        /// <summary>
        /// Perform the standard simple backup history using a log file
        /// </summary>
        private void SimpleBackupHistory(string SourceFileName, string BackupFile, string BackupFolder) {

            // read log file
            var logName = SourceFileName + ".log";  // not "_log" anymore, because then log files and
                                                    // regular backup files are harder to distinguish between
            var BackupLogFile = Path.Combine(BackupFolder, logName);
            var LogFile = new string[] { };
            if (File.Exists(BackupLogFile))
                LogFile = File.ReadAllLines(BackupLogFile);

            // record the newest backup at the top of the file
            var newLog = new StreamWriter(BackupLogFile, false);
            newLog.WriteLine(BackupFile);

            // now go through the set of older backups and remove any that
            // take us over our limit
            for (uint i = 0, backupCount = 1; i < LogFile.Length; ++i, ++backupCount) {
                var oldBackup = LogFile[i];
                if (backupCount >= Properties.Settings.Default.BackupCount) {
                    // this backup is one more than we need, so get rid of it
                    if (File.Exists(oldBackup))
                        File.Delete(oldBackup);
                } else {
                    // backup is valid, so keep and record it
                    newLog.WriteLine(oldBackup);
                }
            }

            newLog.Close();
            newLog.Dispose();
            newLog = null;
        }

        /// <summary>
        /// Compare two files relating to their creation date/time
        /// </summary>
        public static int CompareFilesByCreationDate(string f1, string f2) {
            if (File.Exists(f1) && File.Exists(f2)) {
                return DateTime.Compare((new FileInfo(f1)).CreationTime, (new FileInfo(f2)).CreationTime);
            } else return 0; // Or maybe quit with a message box and throw an exception
        }

        /// <summary>
        /// The struct <c>DirTypes</c> und the array <c>DirNames</c> belong together and are used in the
        /// following function <c>AdvancedBackupHistory</c> to describe the different subdirectories
        /// </summary>
        public struct DirTypes {
            public const int Std = 0;
            public const int Today = 1;
            public const int Yesterday = 2;
            public const int OneWeek = 3;
            public const int FourWeeks = 4;
            public const int TwelveMonths = 5;
            public const int TenYears = 6;
        };

        public readonly static string[] DirNames = {
                                            ".",                // Std
                                            "1_Today",          // Today
                                            "2_Yesterday",      // Yesterday
                                            "3_One_Week",       // OneWeek
                                            "4_Four_Weeks",     // FourWeeks
                                            "5_Twelve_Months",  // TwelveMonths
                                            "6_Ten_Years",      // TenYears
                                            ""};                // only to avoid unnecessary special cases

        /// <summary>
        /// Perform an advanced backup history (without log file) using an additional set of subdirectories
        /// containing prior versions of the backup file
        /// </summary>
        private void AdvancedBackupHistory(string SourceFileName, string BackupFile, string BackupFolder) {
            DateTime bound;

            // for each subdirectory delete extra files or move them to the next
            // corresponding subfolder
            for (int dir = DirTypes.Std; dir <= DirTypes.TenYears; dir++) {
                string sourceDir = Path.Combine(BackupFolder, DirNames[dir]);
                string destDir = Path.Combine(BackupFolder, DirNames[dir + 1]);

                if (Directory.Exists(sourceDir)) {
                    // get only files with a matching name
                    string[] files = Directory.GetFiles(sourceDir, SourceFileName + "_*");

                    if (files.Length > 0) {

                        // 1st round: (re)move files not matching the date range
                        if (dir >= DirTypes.Today) {

                            // set the corresponding date bound
                            bound = DateTime.Today;
                            switch (dir) {
                                case DirTypes.Today:
                                    break;
                                case DirTypes.Yesterday:
                                    bound = bound.AddDays(-1);
                                    break;
                                case DirTypes.OneWeek:
                                    bound = bound.AddDays(-7);
                                    break;
                                case DirTypes.FourWeeks:
                                    bound = bound.AddDays(-28);
                                    break;
                                case DirTypes.TwelveMonths:
                                    bound = new DateTime(bound.Year, bound.Month, 1);
                                    break;
                                case DirTypes.TenYears:
                                    bound = new DateTime(bound.Year - 10, 1, 1);
                                    break;
                            }

                            // Check each file if it is older than the corresponding bound
                            foreach (string file in files) {
                                if (File.Exists(file) &&
                                    DateTime.Compare((new FileInfo(file)).CreationTime, bound) < 0) {
                                    if (dir == DirTypes.TenYears) { // delete the files older than ten years
                                        File.Delete(file);
                                    } else { // move the other file to the next directory
                                        Directory.CreateDirectory(destDir);

                                        // File.Move(file, Path.Combine(destDir, Path.GetFileName(file)));
                                        MoveBackupFile(Path.GetFileName(file), BackupFolder,
                                            DirNames[dir], DirNames[dir + 1]);
                                    }
                                }
                            }
                        }
                    }
                }

                if (Directory.Exists(sourceDir)) {
                    // 2nd round: (re)move files if there are too many

                    // get only files with a matching name
                    string[] files = Directory.GetFiles(sourceDir, SourceFileName + "_*");

                    if (files.Length > 0) {
                        if (dir == DirTypes.Std && Properties.Settings.Default.AlwaysFillSubDirs) {
                            // Copy all files into the "1_Today" directory
                            Directory.CreateDirectory(destDir);
                            foreach (string file in files) {
                                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                                if (File.Exists(file) && !File.Exists(destFile)){
                                    File.Copy(file, destFile);
                                    // Also set the file dates exactly like the original
                                    File.SetCreationTime(destFile, File.GetCreationTime(file));
                                    File.SetLastAccessTime(destFile, File.GetLastAccessTime(file));
                                    File.SetLastWriteTime(destFile, File.GetLastWriteTime(file));
                                }
                            }
                        } else {
                            // sort them by date/time (ascending, i.e. from the oldest to the newest)
                            Array.Sort(files, CompareFilesByCreationDate);

                            // For ".", "1_Today" and "2_Yesterday" the maximum number of files is
                            // limited by the preferences (HistoQty)
                            if (dir <= DirTypes.Yesterday &&
                                files.Length > Properties.Settings.Default.BackupCount) {

                                // Delete/Move from the beginning:
                                //   ".": Delete the oldest files
                                //   "1_Today" and "2_Yesterday":
                                //        Keep the old files and delete/move the youngest
                                if (dir != DirTypes.Std) Array.Reverse(files);

                                // Delete/Move as many files such that the maximum number of files (HistoQty) remains
                                for (int i = 0; i < files.Length - Properties.Settings.Default.BackupCount; i++) {
                                    if (File.Exists(files[i])) {

                                        // Check the files in the "Today" directory that should be removed, whether they
                                        // are old enough to belong into the "Yesterday" directory. If so, move them.
                                        // Otherwise, delete them.
                                        // The files in the standard and "Yesterday" directory can be moved in all cases.
                                        if ((dir != DirTypes.Today) ||
                                            (DateTime.Compare((new FileInfo(files[i])).CreationTime, DateTime.Today) < 0)) {
                                            Directory.CreateDirectory(destDir);

                                            // File.Move(files[i],
                                            //    Path.Combine(destDir, Path.GetFileName(files[i])));
                                            MoveBackupFile(Path.GetFileName(files[i]), BackupFolder,
                                                DirNames[dir], DirNames[dir + 1]);
                                        } else {
                                            // if the file is too young, delete it
                                            File.Delete(files[i]);
                                        }
                                    }
                                }
                            } else if (dir >= DirTypes.OneWeek) {
                                // For the remaining directories we use a heuristic method, e.g.:
                                // "3_One_Week": At most one file per day
                                // "4_Four_Weeks": At most one file per 7 days
                                // "5_Twelve_Month": At most one file per month
                                // "6_Ten_Years": At most one file per year

                                bool first = true;
                                bound = DateTime.Today; // this is just for compile reasons...
                                foreach (string file in files) {
                                    if (File.Exists(file)) {
                                        if (!first) {
                                            if (DateTime.Compare((new FileInfo(file)).CreationTime, bound) < 0) {
                                                File.Delete(file);
                                                continue;
                                            }
                                        } else {
                                            first = false;
                                        }
                                        bound = (new FileInfo(file)).CreationTime;
                                        switch (dir) {
                                            case DirTypes.OneWeek:
                                                bound = new DateTime(bound.Year, bound.Month, bound.Day).AddDays(1);
                                                break;
                                            case DirTypes.FourWeeks:
                                                bound = new DateTime(bound.Year, bound.Month, bound.Day).AddDays(7);
                                                break;
                                            case DirTypes.TwelveMonths:
                                                bound = new DateTime(bound.Year, bound.Month, 1).AddMonths(1);
                                                break;
                                            case DirTypes.TenYears:
                                                bound = new DateTime(bound.Year + 1, 1, 1);
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // If the directory is empty, delete it (eventually)
                    if (Properties.Settings.Default.AutoDelSubDirs) {
                        DelEmptyDirectory(sourceDir);
                    }
                }
            }
        }

        /// <summary>
        /// Do the actual database backup to the configured directories.
        /// </summary>
        private BackupError BackupDB()
        {
            if (!m_host.Database.IsOpen)
                return BackupError.DATABASE_CLOSED;

            // make sure we have some folders to actually backup to
            if (Properties.Settings.Default.BackupFolders == null ||
                Properties.Settings.Default.BackupFolders.Count == 0)
                return BackupError.NO_BACKUP_FOLDERS;

            string SourceFile = "";
            string SourceFileName = "";
            string BackupFile = "";
            string BackupFolder = "";

            // get ahold of the password database
            if (m_host.Database.IOConnectionInfo.IsLocalFile())
            {
                // handle local files
                SourceFile = m_host.Database.IOConnectionInfo.Path;
                FileInfo f = new FileInfo(m_host.Database.IOConnectionInfo.Path);
                SourceFileName = f.Name;
                f = null;
            }
            else
            {
                // handle remote files
                SourceFileName = "";
                if (m_host.MainWindow.Text.IndexOf("-") >= 0)
                {
                    SourceFileName = m_host.MainWindow.Text.Substring(0, m_host.MainWindow.Text.IndexOf("-"));
                    SourceFileName = SourceFileName.Trim();
                    if (SourceFileName.EndsWith("*"))
                        SourceFileName = SourceFileName.Substring(0, SourceFileName.Length - 1);

                }

                SourceFileName = _RemoveSpecialChars(SourceFileName);
                SourceFile = Path.GetTempFileName();

                WebClient wc = new WebClient();

                wc.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);

                if ((m_host.Database.IOConnectionInfo.UserName.Length > 0) || (m_host.Database.IOConnectionInfo.Password.Length > 0))
                    wc.Credentials = new NetworkCredential(m_host.Database.IOConnectionInfo.UserName, m_host.Database.IOConnectionInfo.Password);

                wc.DownloadFile(m_host.Database.IOConnectionInfo.Path, SourceFile);
                wc.Dispose();
                wc = null;
            }

            bool backupPerformed = false;
            foreach (var folder in Properties.Settings.Default.BackupFolders)
            {
                if (!Directory.Exists(folder))
                    continue;
                backupPerformed = true;

                // Flat backups (all database backups in one directory) or use a separate directory for
                // each database
                if (Properties.Settings.Default.DBSepUseOnlyOneDir) {
                    BackupFolder = folder;
                } else {
                    BackupFolder = Path.Combine(folder, SourceFileName);
                    Directory.CreateDirectory(BackupFolder);

                    // If there exist files in the only-one-directory structure (maybe this option was changed
                    // to "separate directories" after the first backups were already performed), try to
                    // move them into the correct subdirectory

                    // search for files with a matching name (also in the corresponding subdirectories
                    // if using the advanced backup history)

                    for (int subdir = DirTypes.Std; subdir <=
                        (Properties.Settings.Default.SimpleBackupHistory ? DirTypes.Std : DirTypes.TenYears);
                        subdir++) {
                        string sourceDir = Path.Combine(folder, DirNames[subdir]);
                        if (Directory.Exists(sourceDir)) {
                            string destDir = Path.Combine(BackupFolder, DirNames[subdir]);
                            Directory.CreateDirectory(destDir);
                            
                            string[] files = Directory.GetFiles(sourceDir, SourceFileName + "*");

                            // and try to move them into the correct subdirectory
                            if (files.Length > 0) {
                                foreach (string file in files) {
                                    if (File.Exists(file)) {
                                        // File.Move(file, Path.Combine(destDir, Path.GetFileName(file)));
                                        MoveBackupFile(Path.GetFileName(file), folder,
                                            DirNames[subdir], Path.Combine(SourceFileName, DirNames[subdir]));
                                    }
                                }
                            }

                            // If the directory is empty, delete it (eventually)
                            if (Properties.Settings.Default.AutoDelSubDirs) {
                                DelEmptyDirectory(sourceDir);
                            }
                        }
                    }
                }

                // For backward compatibility rename a potential old log file (with "_") into a new one (with ".")
                RenameLogFile(SourceFileName, BackupFolder);

                // create backup file
                var backupName = SourceFileName + "_" +
                    DateTime.Now.ToString(Properties.Settings.Default.DateFormat) + ".kdbx";

                BackupFile = Path.Combine(BackupFolder, backupName);

                bool backupExists = File.Exists(BackupFile);
                if (backupExists && !Properties.Settings.Default.OverwriteBackup)
                    continue;

                File.Copy(SourceFile, BackupFile, true);

                // if the backup existed, we just overwrote it.  There's no
                // reason to do a purge or update the log.
                if (backupExists)
                    continue;

                if (Properties.Settings.Default.SimpleBackupHistory) {
                    // Simple backup history (flat with log file)
                    SimpleBackupHistory(SourceFileName, BackupFile, BackupFolder);
                } else {
                    // Advanced backup history (with subdirectories, without log file)
                    AdvancedBackupHistory(SourceFileName, BackupFile, BackupFolder);
                }
            }

            // delete temp remote file
            if (!m_host.Database.IOConnectionInfo.IsLocalFile())
            {
                File.Delete(SourceFile);
            }

            BackupError ret = BackupError.OK;
            if (!backupPerformed)
                ret = BackupError.NO_VALID_FOLDERS;

            return ret;
        }

        /// <summary>
        /// Function to call when an automated backup is triggered.
        /// </summary>
        /// This uses the balloon tip method to let the user know if anything
        /// erroroneuous happened during an automated backup operation.
        private void AutoBackup()
        {
            switch (BackupDB())
            {
                case BackupError.DATABASE_CLOSED:
                    m_host.MainWindow.Invoke(new MethodInvoker(delegate()
                    {
                        m_host.MainWindow.MainNotifyIcon.ShowBalloonTip(5000, "Database Backup",
                            "Automatic backup faild. No database was open for backup.",
                            ToolTipIcon.Error);
                    }));
                    break;
                case BackupError.NO_BACKUP_FOLDERS:
                    m_host.MainWindow.Invoke(new MethodInvoker(delegate()
                    {
                        m_host.MainWindow.MainNotifyIcon.ShowBalloonTip(5000, "Database Backup",
                            "Automatic backup failed. No backup folders are configured.",
                            ToolTipIcon.Error);
                    }));
                    break;
                case BackupError.NO_VALID_FOLDERS:
                    m_host.MainWindow.Invoke(new MethodInvoker(delegate()
                    {
                        m_host.MainWindow.MainNotifyIcon.ShowBalloonTip(5000, "Database Backup",
                            "Automatic backup failed. None of the configured backup folders are available.",
                            ToolTipIcon.Error);
                    }));
                    break;
            }
        }

        /// <summary>
        /// Function to call when a user triggers a backup operation.
        /// </summary>
        /// This function actually examines the results of a backup operation
        /// and reports the results to the user so they know when something has
        /// happened and is completed.
        private void UserBackup()
        {
            switch (BackupDB())
            {
                case BackupError.DATABASE_CLOSED:
                    MessageBox.Show("A database must be open for a backup to be performed.",
                        "Database Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case BackupError.NO_BACKUP_FOLDERS:
                    MessageBox.Show("You have not configured any backup directories yet.",
                        "Database Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case BackupError.NO_VALID_FOLDERS:
                    MessageBox.Show("Backup could not be done with currently configured directories.",
                        "Database Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case BackupError.OK:
                    MessageBox.Show("Backup completed successfully.",
                        "Database Backup", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        /// <summary>
        /// Removes any special characters that may appear in the filename so
        /// that we have a clear name when we try and write the backup to the
        /// filesystem.
        /// </summary>
        /// <param name="input">String to be cleaned up.</param>
        /// <returns>The cleaned string.</returns>
        private string _RemoveSpecialChars(string input)
        {
            return Regex.Replace(input, @"[^0-9a-zA-Z\._]", string.Empty);
        }

        /// <summary>
        /// Handler for when the backup now menu item is selected.
        /// </summary>
        /// <param name="sender">Information about the sender of the event.</param>
        /// <param name="e">Event information.</param>
        private void OnMenuBackupNow(object sender, EventArgs e)
        {
            UserBackup();
        }

        /// <summary>
        /// Handler for when the option for toggling automatic backup is clicked.
        /// All this does is toggle the state of the auto backup property.
        /// </summary>
        /// <param name="sender">Information about the sender of the event.</param>
        /// <param name="e">Event information.</param>
        private void OnMenuAutomaticBackup(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoBackup = !Properties.Settings.Default.AutoBackup;
            Properties.Settings.Default.Save();
            ((ToolStripMenuItem)sender).Checked = Properties.Settings.Default.AutoBackup;
        }

        /// <summary>
        /// Handler for when the configuration menu option is selected.  This
        /// pops up the configuration dialog.
        /// </summary>
        /// <param name="sender">Information about the sender of the event.</param>
        /// <param name="e">Event information.</param>
        private void OnMenuConfig(object sender, EventArgs e)
        {
            ConfigDialog frm = new ConfigDialog();
            frm.ShowDialog();
            frm.Dispose();
            frm = null;
        }

        /// <summary>
        /// Handler for when the database file is opened by KeePass.  We use
        /// this event to enable the 'Backup Now' option from the tool menu.
        /// </summary>
        /// <param name="sender">Information about the sender.</param>
        /// <param name="e">Event information.</param>
        private void OnFileOpened(object sender, FileOpenedEventArgs e)
        {
            m_backupNowToolItem.Enabled = true;
        }

        /// <summary>
        /// Handler for when a database file is created by KeePass.  For some
        /// reason, the open event doesn't fire when a new database is created,
        /// so we must enable menu options after a file is created.
        /// </summary>
        /// <param name="sender">Information about the sender.</param>
        /// <param name="e">Event information.</param>
        private void OnFileCreated(object sender, FileCreatedEventArgs e)
        {
            m_backupNowToolItem.Enabled = true;
        }

        /// <summary>
        /// Handler for when a file is being saved by KeePass.  We take this
        /// opportunity to see if the file is actually modified.  We use this
        /// to control later backup events.
        /// </summary>
        /// <param name="sender">Information about the sender.</param>
        /// <param name="e">Event information.</param>
        private void OnFileSaving(object sender, FileSavingEventArgs e)
        {
            m_databaseModified = e.Database.Modified;
        }

        /// <summary>
        /// Determines if an automatic backup should be triggered.  This uses a
        /// combination of the autobackup setting along with the database
        /// modified flag to determine what should happen.
        /// </summary>
        /// <param name="extraConditions">Additional condition (and) required to enable backup. Default is true.</param>
        /// <returns>True if an automatic backup should happen.</returns>
        private bool ShouldAutoBackup(bool extraConditions)
        {
            return (!Properties.Settings.Default.AutoBackupModifiedOnly || m_databaseModified) &&
                Properties.Settings.Default.AutoBackup
                && extraConditions;
        }

        /// <summary>
        /// Handler for when the database file is saved from the KeePass GUI.
        /// If we are setup to do automatic backup, we go ahead and do the
        /// backup.  If we do the backup, we also clear the database modified
        /// flag since we now have the latest database in the backup directory.
        /// </summary>
        /// <param name="sender">Information about the sender of the event.</param>
        /// <param name="e">Event information.</param>
        private void OnFileSaved(object sender, FileSavedEventArgs e)
        {
            if (ShouldAutoBackup(Properties.Settings.Default.BackupOnFileSaved))
            {
                AutoBackup();
                m_databaseModified = false;
            }
        }

        /// <summary>
        /// Handler for when the database file is being closed by KeePass.  If
        /// we are setup for automatic backup on this event, we go ahead and do
        /// so.  We also disable any menu actions that require a database to be
        /// open and clear the modified flag.
        /// </summary>
        /// <param name="sender">Information about the sending object.</param>
        /// <param name="e">Event information.</param>
        private void OnFileClosing(object sender, FileClosingEventArgs e)
        {
            if (ShouldAutoBackup(Properties.Settings.Default.BackupOnFileClosed))
                AutoBackup();

            m_databaseModified = false;
            m_backupNowToolItem.Enabled = false;
        }
    }
}
