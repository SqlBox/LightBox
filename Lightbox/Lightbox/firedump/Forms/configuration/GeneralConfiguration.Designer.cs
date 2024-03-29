﻿namespace Firedump.Forms.configuration
{
    partial class GeneralConfiguration
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
            this.gbFolders = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bLogFolder = new System.Windows.Forms.Button();
            this.tbLogFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bTempFolder = new System.Windows.Forms.Button();
            this.tbTempFolder = new System.Windows.Forms.TextBox();
            this.gbDumpOptions = new System.Windows.Forms.GroupBox();
            this.cbProcsFuncs = new System.Windows.Forms.CheckBox();
            this.bMoreSQLOptions = new System.Windows.Forms.Button();
            this.cbSingleFile = new System.Windows.Forms.CheckBox();
            this.cbTriggers = new System.Windows.Forms.CheckBox();
            this.cbEvents = new System.Windows.Forms.CheckBox();
            this.cbDumpData = new System.Windows.Forms.CheckBox();
            this.cbCreateSchema = new System.Windows.Forms.CheckBox();
            this.bCancel = new System.Windows.Forms.Button();
            this.bReset = new System.Windows.Forms.Button();
            this.gbCompressionSettings = new System.Windows.Forms.GroupBox();
            this.cbUseMultithreading = new System.Windows.Forms.CheckBox();
            this.cmbFileFormat = new System.Windows.Forms.ComboBox();
            this.lbFileFormat = new System.Windows.Forms.Label();
            this.cmbCompressionLevel = new System.Windows.Forms.ComboBox();
            this.lbCompressionLevel = new System.Windows.Forms.Label();
            this.cbEnableComp = new System.Windows.Forms.CheckBox();
            this.gbEncryption = new System.Windows.Forms.GroupBox();
            this.lbPassHelp = new System.Windows.Forms.Label();
            this.cbEncryptHeader = new System.Windows.Forms.CheckBox();
            this.tbConfirmPass = new System.Windows.Forms.TextBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.lbConfirmPass = new System.Windows.Forms.Label();
            this.lbPass = new System.Windows.Forms.Label();
            this.cbEnablePasswordEncryption = new System.Windows.Forms.CheckBox();
            this.bSave = new System.Windows.Forms.Button();
            this.bBinlogConfig = new System.Windows.Forms.Button();
            this.gbFolders.SuspendLayout();
            this.gbDumpOptions.SuspendLayout();
            this.gbCompressionSettings.SuspendLayout();
            this.gbEncryption.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbFolders
            // 
            this.gbFolders.Controls.Add(this.label2);
            this.gbFolders.Controls.Add(this.bLogFolder);
            this.gbFolders.Controls.Add(this.tbLogFolder);
            this.gbFolders.Controls.Add(this.label1);
            this.gbFolders.Controls.Add(this.bTempFolder);
            this.gbFolders.Controls.Add(this.tbTempFolder);
            this.gbFolders.Location = new System.Drawing.Point(36, 37);
            this.gbFolders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbFolders.Name = "gbFolders";
            this.gbFolders.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbFolders.Size = new System.Drawing.Size(765, 255);
            this.gbFolders.TabIndex = 0;
            this.gbFolders.TabStop = false;
            this.gbFolders.Text = "Folder locations";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 169);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Log folder:";
            // 
            // bLogFolder
            // 
            this.bLogFolder.Location = new System.Drawing.Point(681, 158);
            this.bLogFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bLogFolder.Name = "bLogFolder";
            this.bLogFolder.Size = new System.Drawing.Size(60, 35);
            this.bLogFolder.TabIndex = 4;
            this.bLogFolder.Text = "...";
            this.bLogFolder.UseVisualStyleBackColor = true;
            this.bLogFolder.Click += new System.EventHandler(this.bLogFolder_Click);
            // 
            // tbLogFolder
            // 
            this.tbLogFolder.Location = new System.Drawing.Point(200, 162);
            this.tbLogFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbLogFolder.Name = "tbLogFolder";
            this.tbLogFolder.ReadOnly = true;
            this.tbLogFolder.Size = new System.Drawing.Size(470, 26);
            this.tbLogFolder.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Temporary folder:";
            // 
            // bTempFolder
            // 
            this.bTempFolder.Location = new System.Drawing.Point(681, 72);
            this.bTempFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bTempFolder.Name = "bTempFolder";
            this.bTempFolder.Size = new System.Drawing.Size(60, 35);
            this.bTempFolder.TabIndex = 1;
            this.bTempFolder.Text = "...";
            this.bTempFolder.UseVisualStyleBackColor = true;
            this.bTempFolder.Click += new System.EventHandler(this.bTempFolder_Click);
            // 
            // tbTempFolder
            // 
            this.tbTempFolder.Location = new System.Drawing.Point(200, 75);
            this.tbTempFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbTempFolder.Name = "tbTempFolder";
            this.tbTempFolder.ReadOnly = true;
            this.tbTempFolder.Size = new System.Drawing.Size(470, 26);
            this.tbTempFolder.TabIndex = 0;
            // 
            // gbDumpOptions
            // 
            this.gbDumpOptions.Controls.Add(this.cbProcsFuncs);
            this.gbDumpOptions.Controls.Add(this.bMoreSQLOptions);
            this.gbDumpOptions.Controls.Add(this.cbSingleFile);
            this.gbDumpOptions.Controls.Add(this.cbTriggers);
            this.gbDumpOptions.Controls.Add(this.cbEvents);
            this.gbDumpOptions.Controls.Add(this.cbDumpData);
            this.gbDumpOptions.Controls.Add(this.cbCreateSchema);
            this.gbDumpOptions.Location = new System.Drawing.Point(36, 369);
            this.gbDumpOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbDumpOptions.Name = "gbDumpOptions";
            this.gbDumpOptions.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbDumpOptions.Size = new System.Drawing.Size(765, 402);
            this.gbDumpOptions.TabIndex = 1;
            this.gbDumpOptions.TabStop = false;
            this.gbDumpOptions.Text = "SQL Dump options";
            // 
            // cbProcsFuncs
            // 
            this.cbProcsFuncs.AutoSize = true;
            this.cbProcsFuncs.Location = new System.Drawing.Point(513, 297);
            this.cbProcsFuncs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbProcsFuncs.Name = "cbProcsFuncs";
            this.cbProcsFuncs.Size = new System.Drawing.Size(237, 24);
            this.cbProcsFuncs.TabIndex = 6;
            this.cbProcsFuncs.Text = "Dump Procedures/Functions";
            this.cbProcsFuncs.UseVisualStyleBackColor = true;
            // 
            // bMoreSQLOptions
            // 
            this.bMoreSQLOptions.Location = new System.Drawing.Point(340, 152);
            this.bMoreSQLOptions.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bMoreSQLOptions.Name = "bMoreSQLOptions";
            this.bMoreSQLOptions.Size = new System.Drawing.Size(326, 35);
            this.bMoreSQLOptions.TabIndex = 5;
            this.bMoreSQLOptions.Text = "More SQL Options ...";
            this.bMoreSQLOptions.UseVisualStyleBackColor = true;
            this.bMoreSQLOptions.Click += new System.EventHandler(this.bMoreSQLOptions_Click);
            // 
            // cbSingleFile
            // 
            this.cbSingleFile.AutoSize = true;
            this.cbSingleFile.Location = new System.Drawing.Point(280, 351);
            this.cbSingleFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbSingleFile.Name = "cbSingleFile";
            this.cbSingleFile.Size = new System.Drawing.Size(139, 24);
            this.cbSingleFile.TabIndex = 4;
            this.cbSingleFile.Text = "Single SQL file";
            this.cbSingleFile.UseVisualStyleBackColor = true;
            // 
            // cbTriggers
            // 
            this.cbTriggers.AutoSize = true;
            this.cbTriggers.Location = new System.Drawing.Point(280, 297);
            this.cbTriggers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbTriggers.Name = "cbTriggers";
            this.cbTriggers.Size = new System.Drawing.Size(139, 24);
            this.cbTriggers.TabIndex = 3;
            this.cbTriggers.Text = "Dump Triggers";
            this.cbTriggers.UseVisualStyleBackColor = true;
            // 
            // cbEvents
            // 
            this.cbEvents.AutoSize = true;
            this.cbEvents.Location = new System.Drawing.Point(42, 297);
            this.cbEvents.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbEvents.Name = "cbEvents";
            this.cbEvents.Size = new System.Drawing.Size(131, 24);
            this.cbEvents.TabIndex = 2;
            this.cbEvents.Text = "Dump Events";
            this.cbEvents.UseVisualStyleBackColor = true;
            // 
            // cbDumpData
            // 
            this.cbDumpData.AutoSize = true;
            this.cbDumpData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.cbDumpData.Location = new System.Drawing.Point(42, 152);
            this.cbDumpData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDumpData.Name = "cbDumpData";
            this.cbDumpData.Size = new System.Drawing.Size(130, 24);
            this.cbDumpData.TabIndex = 1;
            this.cbDumpData.Text = "Dump Data";
            this.cbDumpData.UseVisualStyleBackColor = true;
            // 
            // cbCreateSchema
            // 
            this.cbCreateSchema.AutoSize = true;
            this.cbCreateSchema.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.cbCreateSchema.Location = new System.Drawing.Point(42, 82);
            this.cbCreateSchema.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbCreateSchema.Name = "cbCreateSchema";
            this.cbCreateSchema.Size = new System.Drawing.Size(230, 24);
            this.cbCreateSchema.TabIndex = 0;
            this.cbCreateSchema.Text = "Include Create Schema";
            this.cbCreateSchema.UseVisualStyleBackColor = true;
            // 
            // bCancel
            // 
            this.bCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.bCancel.Location = new System.Drawing.Point(1515, 808);
            this.bCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(142, 57);
            this.bCancel.TabIndex = 3;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bReset
            // 
            this.bReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.bReset.Location = new System.Drawing.Point(1122, 808);
            this.bReset.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(276, 57);
            this.bReset.TabIndex = 4;
            this.bReset.Text = "Reset to defaults";
            this.bReset.UseVisualStyleBackColor = true;
            this.bReset.Click += new System.EventHandler(this.bReset_Click);
            // 
            // gbCompressionSettings
            // 
            this.gbCompressionSettings.Controls.Add(this.cbUseMultithreading);
            this.gbCompressionSettings.Controls.Add(this.cmbFileFormat);
            this.gbCompressionSettings.Controls.Add(this.lbFileFormat);
            this.gbCompressionSettings.Controls.Add(this.cmbCompressionLevel);
            this.gbCompressionSettings.Controls.Add(this.lbCompressionLevel);
            this.gbCompressionSettings.Controls.Add(this.cbEnableComp);
            this.gbCompressionSettings.Location = new System.Drawing.Point(876, 37);
            this.gbCompressionSettings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbCompressionSettings.Name = "gbCompressionSettings";
            this.gbCompressionSettings.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbCompressionSettings.Size = new System.Drawing.Size(782, 255);
            this.gbCompressionSettings.TabIndex = 5;
            this.gbCompressionSettings.TabStop = false;
            this.gbCompressionSettings.Text = "Compression Settings";
            // 
            // cbUseMultithreading
            // 
            this.cbUseMultithreading.AutoSize = true;
            this.cbUseMultithreading.Location = new System.Drawing.Point(24, 171);
            this.cbUseMultithreading.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbUseMultithreading.Name = "cbUseMultithreading";
            this.cbUseMultithreading.Size = new System.Drawing.Size(168, 24);
            this.cbUseMultithreading.TabIndex = 5;
            this.cbUseMultithreading.Text = "Use Multithreading";
            this.cbUseMultithreading.UseVisualStyleBackColor = true;
            // 
            // cmbFileFormat
            // 
            this.cmbFileFormat.FormattingEnabled = true;
            this.cmbFileFormat.Location = new System.Drawing.Point(120, 102);
            this.cmbFileFormat.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbFileFormat.Name = "cmbFileFormat";
            this.cmbFileFormat.Size = new System.Drawing.Size(206, 28);
            this.cmbFileFormat.TabIndex = 4;
            // 
            // lbFileFormat
            // 
            this.lbFileFormat.AutoSize = true;
            this.lbFileFormat.Location = new System.Drawing.Point(24, 106);
            this.lbFileFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFileFormat.Name = "lbFileFormat";
            this.lbFileFormat.Size = new System.Drawing.Size(88, 20);
            this.lbFileFormat.TabIndex = 3;
            this.lbFileFormat.Text = "File format:";
            // 
            // cmbCompressionLevel
            // 
            this.cmbCompressionLevel.FormattingEnabled = true;
            this.cmbCompressionLevel.Location = new System.Drawing.Point(531, 102);
            this.cmbCompressionLevel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbCompressionLevel.Name = "cmbCompressionLevel";
            this.cmbCompressionLevel.Size = new System.Drawing.Size(202, 28);
            this.cmbCompressionLevel.TabIndex = 2;
            // 
            // lbCompressionLevel
            // 
            this.lbCompressionLevel.AutoSize = true;
            this.lbCompressionLevel.Location = new System.Drawing.Point(374, 106);
            this.lbCompressionLevel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCompressionLevel.Name = "lbCompressionLevel";
            this.lbCompressionLevel.Size = new System.Drawing.Size(147, 20);
            this.lbCompressionLevel.TabIndex = 1;
            this.lbCompressionLevel.Text = "Compression Level:";
            // 
            // cbEnableComp
            // 
            this.cbEnableComp.AutoSize = true;
            this.cbEnableComp.Location = new System.Drawing.Point(24, 43);
            this.cbEnableComp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbEnableComp.Name = "cbEnableComp";
            this.cbEnableComp.Size = new System.Drawing.Size(182, 24);
            this.cbEnableComp.TabIndex = 0;
            this.cbEnableComp.Text = "Enable Compression";
            this.cbEnableComp.UseVisualStyleBackColor = true;
            this.cbEnableComp.CheckedChanged += new System.EventHandler(this.cbEnableComp_CheckedChanged);
            // 
            // gbEncryption
            // 
            this.gbEncryption.Controls.Add(this.lbPassHelp);
            this.gbEncryption.Controls.Add(this.cbEncryptHeader);
            this.gbEncryption.Controls.Add(this.tbConfirmPass);
            this.gbEncryption.Controls.Add(this.tbPass);
            this.gbEncryption.Controls.Add(this.lbConfirmPass);
            this.gbEncryption.Controls.Add(this.lbPass);
            this.gbEncryption.Controls.Add(this.cbEnablePasswordEncryption);
            this.gbEncryption.Location = new System.Drawing.Point(876, 369);
            this.gbEncryption.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEncryption.Name = "gbEncryption";
            this.gbEncryption.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gbEncryption.Size = new System.Drawing.Size(782, 246);
            this.gbEncryption.TabIndex = 6;
            this.gbEncryption.TabStop = false;
            this.gbEncryption.Text = "Encryption Settings";
            // 
            // lbPassHelp
            // 
            this.lbPassHelp.AutoSize = true;
            this.lbPassHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lbPassHelp.ForeColor = System.Drawing.Color.Red;
            this.lbPassHelp.Location = new System.Drawing.Point(242, 208);
            this.lbPassHelp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPassHelp.Name = "lbPassHelp";
            this.lbPassHelp.Size = new System.Drawing.Size(216, 20);
            this.lbPassHelp.TabIndex = 6;
            this.lbPassHelp.Text = "Passwords do not match";
            this.lbPassHelp.Visible = false;
            // 
            // cbEncryptHeader
            // 
            this.cbEncryptHeader.AutoSize = true;
            this.cbEncryptHeader.Location = new System.Drawing.Point(52, 65);
            this.cbEncryptHeader.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbEncryptHeader.Name = "cbEncryptHeader";
            this.cbEncryptHeader.Size = new System.Drawing.Size(143, 24);
            this.cbEncryptHeader.TabIndex = 5;
            this.cbEncryptHeader.Text = "Encrypt header";
            this.cbEncryptHeader.UseVisualStyleBackColor = true;
            // 
            // tbConfirmPass
            // 
            this.tbConfirmPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.tbConfirmPass.Location = new System.Drawing.Point(246, 157);
            this.tbConfirmPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbConfirmPass.Name = "tbConfirmPass";
            this.tbConfirmPass.PasswordChar = '*';
            this.tbConfirmPass.Size = new System.Drawing.Size(256, 26);
            this.tbConfirmPass.TabIndex = 4;
            this.tbConfirmPass.Leave += new System.EventHandler(this.tbConfirmPass_Leave);
            // 
            // tbPass
            // 
            this.tbPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.tbPass.Location = new System.Drawing.Point(246, 100);
            this.tbPass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPass.Name = "tbPass";
            this.tbPass.PasswordChar = '*';
            this.tbPass.Size = new System.Drawing.Size(256, 26);
            this.tbPass.TabIndex = 3;
            // 
            // lbConfirmPass
            // 
            this.lbConfirmPass.AutoSize = true;
            this.lbConfirmPass.Location = new System.Drawing.Point(99, 162);
            this.lbConfirmPass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbConfirmPass.Name = "lbConfirmPass";
            this.lbConfirmPass.Size = new System.Drawing.Size(140, 20);
            this.lbConfirmPass.TabIndex = 2;
            this.lbConfirmPass.Text = "Confirm password:";
            // 
            // lbPass
            // 
            this.lbPass.AutoSize = true;
            this.lbPass.Location = new System.Drawing.Point(154, 105);
            this.lbPass.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPass.Name = "lbPass";
            this.lbPass.Size = new System.Drawing.Size(82, 20);
            this.lbPass.TabIndex = 1;
            this.lbPass.Text = "Password:";
            // 
            // cbEnablePasswordEncryption
            // 
            this.cbEnablePasswordEncryption.AutoSize = true;
            this.cbEnablePasswordEncryption.Location = new System.Drawing.Point(24, 29);
            this.cbEnablePasswordEncryption.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbEnablePasswordEncryption.Name = "cbEnablePasswordEncryption";
            this.cbEnablePasswordEncryption.Size = new System.Drawing.Size(210, 24);
            this.cbEnablePasswordEncryption.TabIndex = 0;
            this.cbEnablePasswordEncryption.Text = "Enable zip file encryption";
            this.cbEnablePasswordEncryption.UseVisualStyleBackColor = true;
            this.cbEnablePasswordEncryption.CheckedChanged += new System.EventHandler(this.cbEnablePasswordEncryption_CheckedChanged);
            // 
            // bSave
            // 
            this.bSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.bSave.Image = global::Lightbox.Properties.Resources.save_image1;
            this.bSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSave.Location = new System.Drawing.Point(78, 808);
            this.bSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(258, 57);
            this.bSave.TabIndex = 2;
            this.bSave.Text = "Save Options";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bBinlogConfig
            // 
            this.bBinlogConfig.Location = new System.Drawing.Point(876, 677);
            this.bBinlogConfig.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bBinlogConfig.Name = "bBinlogConfig";
            this.bBinlogConfig.Size = new System.Drawing.Size(326, 35);
            this.bBinlogConfig.TabIndex = 7;
            this.bBinlogConfig.Text = "Binary log dump configuration ...";
            this.bBinlogConfig.UseVisualStyleBackColor = true;
            this.bBinlogConfig.Click += new System.EventHandler(this.bBinlogConfig_Click);
            // 
            // GeneralConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1716, 883);
            this.Controls.Add(this.bBinlogConfig);
            this.Controls.Add(this.gbEncryption);
            this.Controls.Add(this.gbCompressionSettings);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.gbDumpOptions);
            this.Controls.Add(this.gbFolders);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GeneralConfiguration";
            this.Text = "GeneralConfiguration";
            this.Load += new System.EventHandler(this.GeneralConfiguration_Load);
            this.gbFolders.ResumeLayout(false);
            this.gbFolders.PerformLayout();
            this.gbDumpOptions.ResumeLayout(false);
            this.gbDumpOptions.PerformLayout();
            this.gbCompressionSettings.ResumeLayout(false);
            this.gbCompressionSettings.PerformLayout();
            this.gbEncryption.ResumeLayout(false);
            this.gbEncryption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbFolders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bLogFolder;
        private System.Windows.Forms.TextBox tbLogFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bTempFolder;
        private System.Windows.Forms.TextBox tbTempFolder;
        private System.Windows.Forms.GroupBox gbDumpOptions;
        private System.Windows.Forms.CheckBox cbDumpData;
        private System.Windows.Forms.CheckBox cbCreateSchema;
        private System.Windows.Forms.CheckBox cbSingleFile;
        private System.Windows.Forms.CheckBox cbTriggers;
        private System.Windows.Forms.CheckBox cbEvents;
        private System.Windows.Forms.Button bMoreSQLOptions;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bReset;
        private System.Windows.Forms.GroupBox gbCompressionSettings;
        private System.Windows.Forms.CheckBox cbEnableComp;
        private System.Windows.Forms.ComboBox cmbFileFormat;
        private System.Windows.Forms.Label lbFileFormat;
        private System.Windows.Forms.ComboBox cmbCompressionLevel;
        private System.Windows.Forms.Label lbCompressionLevel;
        private System.Windows.Forms.CheckBox cbUseMultithreading;
        private System.Windows.Forms.GroupBox gbEncryption;
        private System.Windows.Forms.CheckBox cbEnablePasswordEncryption;
        private System.Windows.Forms.CheckBox cbEncryptHeader;
        private System.Windows.Forms.TextBox tbConfirmPass;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.Label lbConfirmPass;
        private System.Windows.Forms.Label lbPass;
        private System.Windows.Forms.Label lbPassHelp;
        private System.Windows.Forms.CheckBox cbProcsFuncs;
        private System.Windows.Forms.Button bBinlogConfig;
    }
}