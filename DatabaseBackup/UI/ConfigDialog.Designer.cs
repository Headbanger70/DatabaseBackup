namespace DatabaseBackup.UI
{
    partial class ConfigDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigDialog));
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbFolders = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumBackup = new System.Windows.Forms.NumericUpDown();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.fbdBrowse = new System.Windows.Forms.FolderBrowserDialog();
            this.picBannerImage = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnHelpDateFormat = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDateFormat = new System.Windows.Forms.TextBox();
            this.tabContainer = new System.Windows.Forms.TabControl();
            this.tabDirectories = new System.Windows.Forms.TabPage();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radbtnDBSepSubDirs = new System.Windows.Forms.RadioButton();
            this.radbtnDBSepFlat = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radbtnBHistSimple = new System.Windows.Forms.RadioButton();
            this.radbtnBHistAdvanced = new System.Windows.Forms.RadioButton();
            this.tabTriggers = new System.Windows.Forms.TabPage();
            this.chkOverwriteBackup = new System.Windows.Forms.CheckBox();
            this.chkBackupOnModified = new System.Windows.Forms.CheckBox();
            this.chkBackupClosed = new System.Windows.Forms.CheckBox();
            this.chkBackupSaved = new System.Windows.Forms.CheckBox();
            this.chkAutoDelSubDirs = new System.Windows.Forms.CheckBox();
            this.chkAlwaysFillSubDirs = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumBackup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBannerImage)).BeginInit();
            this.tabContainer.SuspendLayout();
            this.tabDirectories.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabTriggers.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(478, 6);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(30, 32);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtDestination
            // 
            this.txtDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestination.Location = new System.Drawing.Point(91, 11);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(381, 20);
            this.txtDestination.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label1.Location = new System.Drawing.Point(6, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Backup Directories";
            // 
            // lbFolders
            // 
            this.lbFolders.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbFolders.FormattingEnabled = true;
            this.lbFolders.Location = new System.Drawing.Point(9, 67);
            this.lbFolders.Name = "lbFolders";
            this.lbFolders.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbFolders.Size = new System.Drawing.Size(499, 173);
            this.lbFolders.TabIndex = 4;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(514, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 32);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(514, 67);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(68, 32);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Number of Backups to keep";
            // 
            // txtNumBackup
            // 
            this.txtNumBackup.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNumBackup.Location = new System.Drawing.Point(429, 40);
            this.txtNumBackup.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtNumBackup.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNumBackup.Name = "txtNumBackup";
            this.txtNumBackup.Size = new System.Drawing.Size(79, 20);
            this.txtNumBackup.TabIndex = 11;
            this.txtNumBackup.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOK.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(400, 356);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(101, 32);
            this.btnOK.TabIndex = 12;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancel.Location = new System.Drawing.Point(507, 356);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(101, 32);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // picBannerImage
            // 
            this.picBannerImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.picBannerImage.Location = new System.Drawing.Point(0, 0);
            this.picBannerImage.Name = "picBannerImage";
            this.picBannerImage.Size = new System.Drawing.Size(620, 60);
            this.picBannerImage.TabIndex = 9;
            this.picBannerImage.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(6, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Destination";
            // 
            // btnHelpDateFormat
            // 
            this.btnHelpDateFormat.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnHelpDateFormat.CausesValidation = false;
            this.btnHelpDateFormat.Location = new System.Drawing.Point(514, 6);
            this.btnHelpDateFormat.Name = "btnHelpDateFormat";
            this.btnHelpDateFormat.Size = new System.Drawing.Size(68, 32);
            this.btnHelpDateFormat.TabIndex = 9;
            this.btnHelpDateFormat.Text = "Help...";
            this.btnHelpDateFormat.UseVisualStyleBackColor = true;
            this.btnHelpDateFormat.Click += new System.EventHandler(this.btnHelpDateFormat_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Date format";
            // 
            // txtDateFormat
            // 
            this.txtDateFormat.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtDateFormat.Location = new System.Drawing.Point(94, 11);
            this.txtDateFormat.Name = "txtDateFormat";
            this.txtDateFormat.Size = new System.Drawing.Size(414, 20);
            this.txtDateFormat.TabIndex = 8;
            // 
            // tabContainer
            // 
            this.tabContainer.Controls.Add(this.tabDirectories);
            this.tabContainer.Controls.Add(this.tabGeneral);
            this.tabContainer.Controls.Add(this.tabTriggers);
            this.tabContainer.Location = new System.Drawing.Point(12, 66);
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new System.Drawing.Size(596, 284);
            this.tabContainer.TabIndex = 14;
            // 
            // tabDirectories
            // 
            this.tabDirectories.Controls.Add(this.txtDestination);
            this.tabDirectories.Controls.Add(this.label3);
            this.tabDirectories.Controls.Add(this.btnBrowse);
            this.tabDirectories.Controls.Add(this.btnAdd);
            this.tabDirectories.Controls.Add(this.label1);
            this.tabDirectories.Controls.Add(this.lbFolders);
            this.tabDirectories.Controls.Add(this.btnRemove);
            this.tabDirectories.Location = new System.Drawing.Point(4, 22);
            this.tabDirectories.Name = "tabDirectories";
            this.tabDirectories.Padding = new System.Windows.Forms.Padding(3);
            this.tabDirectories.Size = new System.Drawing.Size(588, 258);
            this.tabDirectories.TabIndex = 0;
            this.tabDirectories.Text = "Directories";
            this.tabDirectories.UseVisualStyleBackColor = true;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.groupBox2);
            this.tabGeneral.Controls.Add(this.groupBox1);
            this.tabGeneral.Controls.Add(this.label4);
            this.tabGeneral.Controls.Add(this.btnHelpDateFormat);
            this.tabGeneral.Controls.Add(this.txtDateFormat);
            this.tabGeneral.Controls.Add(this.txtNumBackup);
            this.tabGeneral.Controls.Add(this.label2);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabGeneral.Size = new System.Drawing.Size(588, 258);
            this.tabGeneral.TabIndex = 1;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radbtnDBSepSubDirs);
            this.groupBox2.Controls.Add(this.radbtnDBSepFlat);
            this.groupBox2.Location = new System.Drawing.Point(9, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(499, 71);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database separation";
            // 
            // radbtnDBSepSubDirs
            // 
            this.radbtnDBSepSubDirs.AutoSize = true;
            this.radbtnDBSepSubDirs.Location = new System.Drawing.Point(7, 43);
            this.radbtnDBSepSubDirs.Name = "radbtnDBSepSubDirs";
            this.radbtnDBSepSubDirs.Size = new System.Drawing.Size(222, 17);
            this.radbtnDBSepSubDirs.TabIndex = 1;
            this.radbtnDBSepSubDirs.Text = "Use subdirectories to separate Databases";
            this.radbtnDBSepSubDirs.UseVisualStyleBackColor = true;
            // 
            // radbtnDBSepFlat
            // 
            this.radbtnDBSepFlat.AutoSize = true;
            this.radbtnDBSepFlat.Checked = true;
            this.radbtnDBSepFlat.Location = new System.Drawing.Point(6, 19);
            this.radbtnDBSepFlat.Name = "radbtnDBSepFlat";
            this.radbtnDBSepFlat.Size = new System.Drawing.Size(192, 17);
            this.radbtnDBSepFlat.TabIndex = 0;
            this.radbtnDBSepFlat.TabStop = true;
            this.radbtnDBSepFlat.Text = "Store all Databases in one directory";
            this.radbtnDBSepFlat.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkAlwaysFillSubDirs);
            this.groupBox1.Controls.Add(this.chkAutoDelSubDirs);
            this.groupBox1.Controls.Add(this.radbtnBHistSimple);
            this.groupBox1.Controls.Add(this.radbtnBHistAdvanced);
            this.groupBox1.Location = new System.Drawing.Point(9, 154);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 71);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Type of Backup History";
            // 
            // radbtnBHistSimple
            // 
            this.radbtnBHistSimple.AutoSize = true;
            this.radbtnBHistSimple.Checked = true;
            this.radbtnBHistSimple.Location = new System.Drawing.Point(6, 19);
            this.radbtnBHistSimple.Name = "radbtnBHistSimple";
            this.radbtnBHistSimple.Size = new System.Drawing.Size(134, 17);
            this.radbtnBHistSimple.TabIndex = 13;
            this.radbtnBHistSimple.TabStop = true;
            this.radbtnBHistSimple.Text = "Simple (flat with log file)";
            this.radbtnBHistSimple.UseVisualStyleBackColor = true;
            // 
            // radbtnBHistAdvanced
            // 
            this.radbtnBHistAdvanced.AutoSize = true;
            this.radbtnBHistAdvanced.Location = new System.Drawing.Point(6, 42);
            this.radbtnBHistAdvanced.Name = "radbtnBHistAdvanced";
            this.radbtnBHistAdvanced.Size = new System.Drawing.Size(220, 17);
            this.radbtnBHistAdvanced.TabIndex = 14;
            this.radbtnBHistAdvanced.Text = "Advanced (Subdirectories without log file)";
            this.radbtnBHistAdvanced.UseVisualStyleBackColor = true;
            this.radbtnBHistAdvanced.CheckedChanged += new System.EventHandler(this.radbtnBHistAdvanced_CheckedChanged);
            // 
            // tabTriggers
            // 
            this.tabTriggers.Controls.Add(this.chkOverwriteBackup);
            this.tabTriggers.Controls.Add(this.chkBackupOnModified);
            this.tabTriggers.Controls.Add(this.chkBackupClosed);
            this.tabTriggers.Controls.Add(this.chkBackupSaved);
            this.tabTriggers.Location = new System.Drawing.Point(4, 22);
            this.tabTriggers.Name = "tabTriggers";
            this.tabTriggers.Padding = new System.Windows.Forms.Padding(3);
            this.tabTriggers.Size = new System.Drawing.Size(588, 258);
            this.tabTriggers.TabIndex = 2;
            this.tabTriggers.Text = "Triggers";
            this.tabTriggers.UseVisualStyleBackColor = true;
            // 
            // chkOverwriteBackup
            // 
            this.chkOverwriteBackup.AutoSize = true;
            this.chkOverwriteBackup.Location = new System.Drawing.Point(7, 95);
            this.chkOverwriteBackup.Name = "chkOverwriteBackup";
            this.chkOverwriteBackup.Size = new System.Drawing.Size(235, 17);
            this.chkOverwriteBackup.TabIndex = 3;
            this.chkOverwriteBackup.Text = "Overwrite existing backups with same name.";
            this.chkOverwriteBackup.UseVisualStyleBackColor = true;
            // 
            // chkBackupOnModified
            // 
            this.chkBackupOnModified.AutoSize = true;
            this.chkBackupOnModified.Location = new System.Drawing.Point(6, 13);
            this.chkBackupOnModified.Name = "chkBackupOnModified";
            this.chkBackupOnModified.Size = new System.Drawing.Size(279, 17);
            this.chkBackupOnModified.TabIndex = 2;
            this.chkBackupOnModified.Text = "Only perform auto-backup when database is modified.";
            this.chkBackupOnModified.UseVisualStyleBackColor = true;
            // 
            // chkBackupClosed
            // 
            this.chkBackupClosed.AutoSize = true;
            this.chkBackupClosed.Location = new System.Drawing.Point(6, 67);
            this.chkBackupClosed.Name = "chkBackupClosed";
            this.chkBackupClosed.Size = new System.Drawing.Size(334, 17);
            this.chkBackupClosed.TabIndex = 1;
            this.chkBackupClosed.Text = "Backup when database is closed (this includes closing KeePass).";
            this.chkBackupClosed.UseVisualStyleBackColor = true;
            // 
            // chkBackupSaved
            // 
            this.chkBackupSaved.AutoSize = true;
            this.chkBackupSaved.Location = new System.Drawing.Point(6, 40);
            this.chkBackupSaved.Name = "chkBackupSaved";
            this.chkBackupSaved.Size = new System.Drawing.Size(184, 17);
            this.chkBackupSaved.TabIndex = 0;
            this.chkBackupSaved.Text = "Backup when database is saved.";
            this.chkBackupSaved.UseVisualStyleBackColor = true;
            // 
            // chkAutoDelSubDirs
            // 
            this.chkAutoDelSubDirs.AutoSize = true;
            this.chkAutoDelSubDirs.Enabled = false;
            this.chkAutoDelSubDirs.Location = new System.Drawing.Point(277, 42);
            this.chkAutoDelSubDirs.Name = "chkAutoDelSubDirs";
            this.chkAutoDelSubDirs.Size = new System.Drawing.Size(219, 17);
            this.chkAutoDelSubDirs.TabIndex = 15;
            this.chkAutoDelSubDirs.Text = "Automatically delete empty subdirectories";
            this.chkAutoDelSubDirs.UseVisualStyleBackColor = true;
            // 
            // chkAlwaysFillSubDirs
            // 
            this.chkAlwaysFillSubDirs.AutoSize = true;
            this.chkAlwaysFillSubDirs.Checked = true;
            this.chkAlwaysFillSubDirs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAlwaysFillSubDirs.Enabled = false;
            this.chkAlwaysFillSubDirs.Location = new System.Drawing.Point(277, 19);
            this.chkAlwaysFillSubDirs.Name = "chkAlwaysFillSubDirs";
            this.chkAlwaysFillSubDirs.Size = new System.Drawing.Size(173, 17);
            this.chkAlwaysFillSubDirs.TabIndex = 16;
            this.chkAlwaysFillSubDirs.Text = "Copy always into subdirectories";
            this.chkAlwaysFillSubDirs.UseVisualStyleBackColor = true;
            // 
            // ConfigDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 400);
            this.Controls.Add(this.tabContainer);
            this.Controls.Add(this.picBannerImage);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfigDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DatabaseBackup Configuration";
            this.Load += new System.EventHandler(this.frmConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtNumBackup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBannerImage)).EndInit();
            this.tabContainer.ResumeLayout(false);
            this.tabDirectories.ResumeLayout(false);
            this.tabDirectories.PerformLayout();
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabTriggers.ResumeLayout(false);
            this.tabTriggers.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbFolders;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtNumBackup;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FolderBrowserDialog fbdBrowse;
        private System.Windows.Forms.PictureBox picBannerImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDateFormat;
        private System.Windows.Forms.Button btnHelpDateFormat;
        private System.Windows.Forms.TabControl tabContainer;
        private System.Windows.Forms.TabPage tabDirectories;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabTriggers;
        private System.Windows.Forms.CheckBox chkBackupClosed;
        private System.Windows.Forms.CheckBox chkBackupSaved;
        private System.Windows.Forms.CheckBox chkBackupOnModified;
        private System.Windows.Forms.CheckBox chkOverwriteBackup;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radbtnBHistSimple;
        private System.Windows.Forms.RadioButton radbtnBHistAdvanced;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radbtnDBSepSubDirs;
        private System.Windows.Forms.RadioButton radbtnDBSepFlat;
        private System.Windows.Forms.CheckBox chkAutoDelSubDirs;
        private System.Windows.Forms.CheckBox chkAlwaysFillSubDirs;
    }
}