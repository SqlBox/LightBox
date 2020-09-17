namespace Firedump.Forms.mysql
{
    partial class NewSqlServer
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbHost = new System.Windows.Forms.TextBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.bTestConnection = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.buttonChooseDb = new System.Windows.Forms.Button();
            this.buttonShowPass = new System.Windows.Forms.Button();
            this.labelDbType = new System.Windows.Forms.Label();
            this.comboBoxDbTypes = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hostname:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(48, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password:";
            // 
            // tbHost
            // 
            this.tbHost.Location = new System.Drawing.Point(113, 45);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(191, 21);
            this.tbHost.TabIndex = 2;
            // 
            // tbUsername
            // 
            this.tbUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.tbUsername.Location = new System.Drawing.Point(113, 71);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(276, 20);
            this.tbUsername.TabIndex = 4;
            // 
            // tbPassword
            // 
            this.tbPassword.Font = new System.Drawing.Font("News706 BT", 8.25F, System.Drawing.FontStyle.Bold);
            this.tbPassword.Location = new System.Drawing.Point(113, 98);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(244, 21);
            this.tbPassword.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(310, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Port:";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(342, 45);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(47, 21);
            this.tbPort.TabIndex = 3;
            // 
            // bTestConnection
            // 
            this.bTestConnection.Image = global::Firedump.Properties.Resources.test_run;
            this.bTestConnection.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bTestConnection.Location = new System.Drawing.Point(18, 203);
            this.bTestConnection.Name = "bTestConnection";
            this.bTestConnection.Size = new System.Drawing.Size(125, 23);
            this.bTestConnection.TabIndex = 7;
            this.bTestConnection.Text = "Test Connection";
            this.bTestConnection.UseVisualStyleBackColor = true;
            this.bTestConnection.Click += new System.EventHandler(this.bTestConnection_Click);
            // 
            // bCancel
            // 
            this.bCancel.Image = global::Firedump.Properties.Resources.status_error;
            this.bCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bCancel.Location = new System.Drawing.Point(284, 232);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(104, 24);
            this.bCancel.TabIndex = 9;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(113, 19);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(191, 21);
            this.tbName.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(66, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Name:";
            // 
            // bSave
            // 
            this.bSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.bSave.Image = global::Firedump.Properties.Resources.save_icon;
            this.bSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bSave.Location = new System.Drawing.Point(18, 232);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(125, 24);
            this.bSave.TabIndex = 8;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // tbDatabase
            // 
            this.tbDatabase.Location = new System.Drawing.Point(113, 125);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(276, 21);
            this.tbDatabase.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(61, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Shema:";
            // 
            // textBoxPath
            // 
            this.textBoxPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPath.Location = new System.Drawing.Point(113, 152);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(276, 21);
            this.textBoxPath.TabIndex = 17;
            this.textBoxPath.Click += new System.EventHandler(this.textBoxPath_Click);
            // 
            // groupBoxDetails
            // 
            this.groupBoxDetails.Controls.Add(this.buttonChooseDb);
            this.groupBoxDetails.Controls.Add(this.buttonShowPass);
            this.groupBoxDetails.Controls.Add(this.tbName);
            this.groupBoxDetails.Controls.Add(this.textBoxPath);
            this.groupBoxDetails.Controls.Add(this.label1);
            this.groupBoxDetails.Controls.Add(this.label2);
            this.groupBoxDetails.Controls.Add(this.label6);
            this.groupBoxDetails.Controls.Add(this.label3);
            this.groupBoxDetails.Controls.Add(this.tbDatabase);
            this.groupBoxDetails.Controls.Add(this.tbHost);
            this.groupBoxDetails.Controls.Add(this.bSave);
            this.groupBoxDetails.Controls.Add(this.tbUsername);
            this.groupBoxDetails.Controls.Add(this.label5);
            this.groupBoxDetails.Controls.Add(this.tbPassword);
            this.groupBoxDetails.Controls.Add(this.label4);
            this.groupBoxDetails.Controls.Add(this.bCancel);
            this.groupBoxDetails.Controls.Add(this.tbPort);
            this.groupBoxDetails.Controls.Add(this.bTestConnection);
            this.groupBoxDetails.Location = new System.Drawing.Point(12, 46);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(414, 275);
            this.groupBoxDetails.TabIndex = 18;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "Connection details";
            // 
            // buttonChooseDb
            // 
            this.buttonChooseDb.Image = global::Firedump.Properties.Resources.database_icon;
            this.buttonChooseDb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonChooseDb.Location = new System.Drawing.Point(18, 152);
            this.buttonChooseDb.Name = "buttonChooseDb";
            this.buttonChooseDb.Size = new System.Drawing.Size(86, 21);
            this.buttonChooseDb.TabIndex = 19;
            this.buttonChooseDb.Text = "choose db:";
            this.buttonChooseDb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonChooseDb.UseVisualStyleBackColor = true;
            this.buttonChooseDb.Click += new System.EventHandler(this.textBoxPath_Click);
            // 
            // buttonShowPass
            // 
            this.buttonShowPass.Image = global::Firedump.Properties.Resources.show_pass;
            this.buttonShowPass.Location = new System.Drawing.Point(363, 98);
            this.buttonShowPass.Name = "buttonShowPass";
            this.buttonShowPass.Size = new System.Drawing.Size(25, 23);
            this.buttonShowPass.TabIndex = 18;
            this.buttonShowPass.UseVisualStyleBackColor = true;
            // 
            // labelDbType
            // 
            this.labelDbType.AutoSize = true;
            this.labelDbType.Location = new System.Drawing.Point(65, 22);
            this.labelDbType.Name = "labelDbType";
            this.labelDbType.Size = new System.Drawing.Size(51, 13);
            this.labelDbType.TabIndex = 19;
            this.labelDbType.Text = "DB Type:";
            // 
            // comboBoxDbTypes
            // 
            this.comboBoxDbTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDbTypes.FormattingEnabled = true;
            this.comboBoxDbTypes.Location = new System.Drawing.Point(125, 19);
            this.comboBoxDbTypes.Name = "comboBoxDbTypes";
            this.comboBoxDbTypes.Size = new System.Drawing.Size(190, 21);
            this.comboBoxDbTypes.TabIndex = 20;
            this.comboBoxDbTypes.SelectedIndexChanged += new System.EventHandler(this.comboBoxDbTypes_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialogPath";
            this.openFileDialog1.ReadOnlyChecked = true;
            // 
            // NewSqlServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(442, 345);
            this.Controls.Add(this.comboBoxDbTypes);
            this.Controls.Add(this.labelDbType);
            this.Controls.Add(this.groupBoxDetails);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewSqlServer";
            this.Text = "Manage SQL Server";
            this.groupBoxDetails.ResumeLayout(false);
            this.groupBoxDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Button bTestConnection;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.TextBox tbDatabase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.GroupBox groupBoxDetails;
        private System.Windows.Forms.Label labelDbType;
        private System.Windows.Forms.ComboBox comboBoxDbTypes;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonShowPass;
        private System.Windows.Forms.Button buttonChooseDb;
    }
}