namespace Firedump.ui.forms
{
    partial class OptionsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeneric = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxSqlEditor = new System.Windows.Forms.GroupBox();
            this.groupBoxFont = new System.Windows.Forms.GroupBox();
            this.groupBoxTabHistory = new System.Windows.Forms.GroupBox();
            this.groupBoxAppHistory = new System.Windows.Forms.GroupBox();
            this.tabPageMySqlMaria = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxConnection = new System.Windows.Forms.GroupBox();
            this.tabPageSqlite = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fastColoredTextBoxSqlAfterDbOpens = new FastColoredTextBoxNS.FastColoredTextBox();
            this.labelSqlAfterDbOpens = new System.Windows.Forms.Label();
            this.checkBoxBeginTransAfterDbOpens = new System.Windows.Forms.CheckBox();
            this.checkBoxBeginTransAfterCommit = new System.Windows.Forms.CheckBox();
            this.groupBoxPragmaEditor = new System.Windows.Forms.GroupBox();
            this.comboBoxLockMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.labelJournalMode = new System.Windows.Forms.Label();
            this.checkBoxIgnoreCheckConstraints = new System.Windows.Forms.CheckBox();
            this.labelIgnoreCheckConst = new System.Windows.Forms.Label();
            this.checkBoxForeignKeys = new System.Windows.Forms.CheckBox();
            this.labelForeignKeys = new System.Windows.Forms.Label();
            this.checkBoxDeferForeignKeys = new System.Windows.Forms.CheckBox();
            this.labelDefForKeys = new System.Windows.Forms.Label();
            this.checkBoxCheckFullSync = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxCellSizeCheck = new System.Windows.Forms.CheckBox();
            this.labelCellSizeCheck = new System.Windows.Forms.Label();
            this.checkBoxAutoIndex = new System.Windows.Forms.CheckBox();
            this.labelAutoIndex = new System.Windows.Forms.Label();
            this.comboBoxAutoVacuum = new System.Windows.Forms.ComboBox();
            this.labelAutoVacuum = new System.Windows.Forms.Label();
            this.tabPageOracle = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabPagePostgreSql = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tabPageDB2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tabPageMSSQL = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.labelMaxPageCount = new System.Windows.Forms.Label();
            this.comboBoxMaxPageCount = new System.Windows.Forms.NumericUpDown();
            this.labelPageSize = new System.Windows.Forms.Label();
            this.comboboxPageSize = new System.Windows.Forms.NumericUpDown();
            this.labelRecursiveTriggers = new System.Windows.Forms.Label();
            this.checkBoxRecursiveTriggers = new System.Windows.Forms.CheckBox();
            this.labelSecureDelete = new System.Windows.Forms.Label();
            this.checkBoxSecureDelete = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxSynchronous = new System.Windows.Forms.ComboBox();
            this.labelUserVersion = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.buttonSavePragma = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPageGeneric.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabPageMySqlMaria.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tabPageSqlite.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxSqlAfterDbOpens)).BeginInit();
            this.groupBoxPragmaEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPageOracle.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.tabPagePostgreSql.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.tabPageDB2.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.tabPageMSSQL.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxMaxPageCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboboxPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGeneric);
            this.tabControl1.Controls.Add(this.tabPageMySqlMaria);
            this.tabControl1.Controls.Add(this.tabPageSqlite);
            this.tabControl1.Controls.Add(this.tabPageOracle);
            this.tabControl1.Controls.Add(this.tabPagePostgreSql);
            this.tabControl1.Controls.Add(this.tabPageDB2);
            this.tabControl1.Controls.Add(this.tabPageMSSQL);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(722, 575);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPageGeneric
            // 
            this.tabPageGeneric.Controls.Add(this.flowLayoutPanel1);
            this.tabPageGeneric.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneric.Name = "tabPageGeneric";
            this.tabPageGeneric.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneric.Size = new System.Drawing.Size(714, 549);
            this.tabPageGeneric.TabIndex = 0;
            this.tabPageGeneric.Text = "Generic Options";
            this.tabPageGeneric.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.groupBoxSqlEditor);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxFont);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxTabHistory);
            this.flowLayoutPanel1.Controls.Add(this.groupBoxAppHistory);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(708, 543);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // groupBoxSqlEditor
            // 
            this.groupBoxSqlEditor.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSqlEditor.Name = "groupBoxSqlEditor";
            this.groupBoxSqlEditor.Size = new System.Drawing.Size(669, 203);
            this.groupBoxSqlEditor.TabIndex = 0;
            this.groupBoxSqlEditor.TabStop = false;
            this.groupBoxSqlEditor.Text = "Sql Editor";
            // 
            // groupBoxFont
            // 
            this.groupBoxFont.Location = new System.Drawing.Point(3, 212);
            this.groupBoxFont.Name = "groupBoxFont";
            this.groupBoxFont.Size = new System.Drawing.Size(669, 123);
            this.groupBoxFont.TabIndex = 1;
            this.groupBoxFont.TabStop = false;
            this.groupBoxFont.Text = "Font";
            // 
            // groupBoxTabHistory
            // 
            this.groupBoxTabHistory.Location = new System.Drawing.Point(3, 341);
            this.groupBoxTabHistory.Name = "groupBoxTabHistory";
            this.groupBoxTabHistory.Size = new System.Drawing.Size(669, 108);
            this.groupBoxTabHistory.TabIndex = 2;
            this.groupBoxTabHistory.TabStop = false;
            this.groupBoxTabHistory.Text = "User/Tab History";
            // 
            // groupBoxAppHistory
            // 
            this.groupBoxAppHistory.Location = new System.Drawing.Point(3, 455);
            this.groupBoxAppHistory.Name = "groupBoxAppHistory";
            this.groupBoxAppHistory.Size = new System.Drawing.Size(669, 108);
            this.groupBoxAppHistory.TabIndex = 3;
            this.groupBoxAppHistory.TabStop = false;
            this.groupBoxAppHistory.Text = "App History";
            // 
            // tabPageMySqlMaria
            // 
            this.tabPageMySqlMaria.Controls.Add(this.flowLayoutPanel2);
            this.tabPageMySqlMaria.Location = new System.Drawing.Point(4, 22);
            this.tabPageMySqlMaria.Name = "tabPageMySqlMaria";
            this.tabPageMySqlMaria.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMySqlMaria.Size = new System.Drawing.Size(714, 549);
            this.tabPageMySqlMaria.TabIndex = 1;
            this.tabPageMySqlMaria.Text = "MySql/MariaDb";
            this.tabPageMySqlMaria.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoScroll = true;
            this.flowLayoutPanel2.Controls.Add(this.groupBoxConnection);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(708, 543);
            this.flowLayoutPanel2.TabIndex = 1;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // groupBoxConnection
            // 
            this.groupBoxConnection.Location = new System.Drawing.Point(3, 3);
            this.groupBoxConnection.Name = "groupBoxConnection";
            this.groupBoxConnection.Size = new System.Drawing.Size(699, 203);
            this.groupBoxConnection.TabIndex = 0;
            this.groupBoxConnection.TabStop = false;
            this.groupBoxConnection.Text = "MySql Connection";
            // 
            // tabPageSqlite
            // 
            this.tabPageSqlite.Controls.Add(this.flowLayoutPanel3);
            this.tabPageSqlite.Location = new System.Drawing.Point(4, 22);
            this.tabPageSqlite.Name = "tabPageSqlite";
            this.tabPageSqlite.Size = new System.Drawing.Size(714, 549);
            this.tabPageSqlite.TabIndex = 2;
            this.tabPageSqlite.Text = "Sqlite";
            this.tabPageSqlite.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoScroll = true;
            this.flowLayoutPanel3.Controls.Add(this.groupBox2);
            this.flowLayoutPanel3.Controls.Add(this.groupBoxPragmaEditor);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(714, 549);
            this.flowLayoutPanel3.TabIndex = 1;
            this.flowLayoutPanel3.WrapContents = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fastColoredTextBoxSqlAfterDbOpens);
            this.groupBox2.Controls.Add(this.labelSqlAfterDbOpens);
            this.groupBox2.Controls.Add(this.checkBoxBeginTransAfterDbOpens);
            this.groupBox2.Controls.Add(this.checkBoxBeginTransAfterCommit);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(702, 239);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SQL";
            // 
            // fastColoredTextBoxSqlAfterDbOpens
            // 
            this.fastColoredTextBoxSqlAfterDbOpens.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fastColoredTextBoxSqlAfterDbOpens.AutoIndentCharsPatterns = "";
            this.fastColoredTextBoxSqlAfterDbOpens.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.fastColoredTextBoxSqlAfterDbOpens.BackBrush = null;
            this.fastColoredTextBoxSqlAfterDbOpens.CharHeight = 14;
            this.fastColoredTextBoxSqlAfterDbOpens.CharWidth = 8;
            this.fastColoredTextBoxSqlAfterDbOpens.CommentPrefix = "--";
            this.fastColoredTextBoxSqlAfterDbOpens.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBoxSqlAfterDbOpens.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBoxSqlAfterDbOpens.IsReplaceMode = false;
            this.fastColoredTextBoxSqlAfterDbOpens.Language = FastColoredTextBoxNS.Language.SQL;
            this.fastColoredTextBoxSqlAfterDbOpens.LeftBracket = '(';
            this.fastColoredTextBoxSqlAfterDbOpens.Location = new System.Drawing.Point(17, 88);
            this.fastColoredTextBoxSqlAfterDbOpens.Name = "fastColoredTextBoxSqlAfterDbOpens";
            this.fastColoredTextBoxSqlAfterDbOpens.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBoxSqlAfterDbOpens.RightBracket = ')';
            this.fastColoredTextBoxSqlAfterDbOpens.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBoxSqlAfterDbOpens.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBoxSqlAfterDbOpens.ServiceColors")));
            this.fastColoredTextBoxSqlAfterDbOpens.Size = new System.Drawing.Size(679, 134);
            this.fastColoredTextBoxSqlAfterDbOpens.TabIndex = 3;
            this.fastColoredTextBoxSqlAfterDbOpens.Zoom = 100;
            // 
            // labelSqlAfterDbOpens
            // 
            this.labelSqlAfterDbOpens.AutoSize = true;
            this.labelSqlAfterDbOpens.Location = new System.Drawing.Point(14, 72);
            this.labelSqlAfterDbOpens.Name = "labelSqlAfterDbOpens";
            this.labelSqlAfterDbOpens.Size = new System.Drawing.Size(201, 13);
            this.labelSqlAfterDbOpens.TabIndex = 2;
            this.labelSqlAfterDbOpens.Text = "* Sql to execute after opening database";
            // 
            // checkBoxBeginTransAfterDbOpens
            // 
            this.checkBoxBeginTransAfterDbOpens.AutoSize = true;
            this.checkBoxBeginTransAfterDbOpens.Checked = true;
            this.checkBoxBeginTransAfterDbOpens.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBeginTransAfterDbOpens.Location = new System.Drawing.Point(17, 43);
            this.checkBoxBeginTransAfterDbOpens.Name = "checkBoxBeginTransAfterDbOpens";
            this.checkBoxBeginTransAfterDbOpens.Size = new System.Drawing.Size(225, 17);
            this.checkBoxBeginTransAfterDbOpens.TabIndex = 1;
            this.checkBoxBeginTransAfterDbOpens.Text = "Begin transaction after opening database";
            this.checkBoxBeginTransAfterDbOpens.UseVisualStyleBackColor = true;
            // 
            // checkBoxBeginTransAfterCommit
            // 
            this.checkBoxBeginTransAfterCommit.AutoSize = true;
            this.checkBoxBeginTransAfterCommit.Checked = true;
            this.checkBoxBeginTransAfterCommit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBeginTransAfterCommit.Location = new System.Drawing.Point(17, 20);
            this.checkBoxBeginTransAfterCommit.Name = "checkBoxBeginTransAfterCommit";
            this.checkBoxBeginTransAfterCommit.Size = new System.Drawing.Size(195, 17);
            this.checkBoxBeginTransAfterCommit.TabIndex = 0;
            this.checkBoxBeginTransAfterCommit.Text = "Begin new transaction after commit";
            this.checkBoxBeginTransAfterCommit.UseVisualStyleBackColor = true;
            // 
            // groupBoxPragmaEditor
            // 
            this.groupBoxPragmaEditor.Controls.Add(this.buttonSavePragma);
            this.groupBoxPragmaEditor.Controls.Add(this.numericUpDown2);
            this.groupBoxPragmaEditor.Controls.Add(this.labelUserVersion);
            this.groupBoxPragmaEditor.Controls.Add(this.comboBoxSynchronous);
            this.groupBoxPragmaEditor.Controls.Add(this.label4);
            this.groupBoxPragmaEditor.Controls.Add(this.checkBoxSecureDelete);
            this.groupBoxPragmaEditor.Controls.Add(this.labelSecureDelete);
            this.groupBoxPragmaEditor.Controls.Add(this.checkBoxRecursiveTriggers);
            this.groupBoxPragmaEditor.Controls.Add(this.labelRecursiveTriggers);
            this.groupBoxPragmaEditor.Controls.Add(this.comboboxPageSize);
            this.groupBoxPragmaEditor.Controls.Add(this.labelPageSize);
            this.groupBoxPragmaEditor.Controls.Add(this.comboBoxMaxPageCount);
            this.groupBoxPragmaEditor.Controls.Add(this.labelMaxPageCount);
            this.groupBoxPragmaEditor.Controls.Add(this.comboBoxLockMode);
            this.groupBoxPragmaEditor.Controls.Add(this.label3);
            this.groupBoxPragmaEditor.Controls.Add(this.numericUpDown1);
            this.groupBoxPragmaEditor.Controls.Add(this.label2);
            this.groupBoxPragmaEditor.Controls.Add(this.comboBox1);
            this.groupBoxPragmaEditor.Controls.Add(this.labelJournalMode);
            this.groupBoxPragmaEditor.Controls.Add(this.checkBoxIgnoreCheckConstraints);
            this.groupBoxPragmaEditor.Controls.Add(this.labelIgnoreCheckConst);
            this.groupBoxPragmaEditor.Controls.Add(this.checkBoxForeignKeys);
            this.groupBoxPragmaEditor.Controls.Add(this.labelForeignKeys);
            this.groupBoxPragmaEditor.Controls.Add(this.checkBoxDeferForeignKeys);
            this.groupBoxPragmaEditor.Controls.Add(this.labelDefForKeys);
            this.groupBoxPragmaEditor.Controls.Add(this.checkBoxCheckFullSync);
            this.groupBoxPragmaEditor.Controls.Add(this.label1);
            this.groupBoxPragmaEditor.Controls.Add(this.checkBoxCellSizeCheck);
            this.groupBoxPragmaEditor.Controls.Add(this.labelCellSizeCheck);
            this.groupBoxPragmaEditor.Controls.Add(this.checkBoxAutoIndex);
            this.groupBoxPragmaEditor.Controls.Add(this.labelAutoIndex);
            this.groupBoxPragmaEditor.Controls.Add(this.comboBoxAutoVacuum);
            this.groupBoxPragmaEditor.Controls.Add(this.labelAutoVacuum);
            this.groupBoxPragmaEditor.Location = new System.Drawing.Point(3, 248);
            this.groupBoxPragmaEditor.Name = "groupBoxPragmaEditor";
            this.groupBoxPragmaEditor.Size = new System.Drawing.Size(702, 293);
            this.groupBoxPragmaEditor.TabIndex = 1;
            this.groupBoxPragmaEditor.TabStop = false;
            this.groupBoxPragmaEditor.Text = "Pragma Editor";
            // 
            // comboBoxLockMode
            // 
            this.comboBoxLockMode.AutoCompleteCustomSource.AddRange(new string[] {
            "NORMAL",
            "EXCLUSIVE"});
            this.comboBoxLockMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLockMode.FormattingEnabled = true;
            this.comboBoxLockMode.Items.AddRange(new object[] {
            "NORMAL",
            "EXCLUSIVE"});
            this.comboBoxLockMode.Location = new System.Drawing.Point(546, 23);
            this.comboBoxLockMode.Name = "comboBoxLockMode";
            this.comboBoxLockMode.Size = new System.Drawing.Size(121, 21);
            this.comboBoxLockMode.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(393, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Locking Mode";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(158, 260);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(84, 21);
            this.numericUpDown1.TabIndex = 17;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 262);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Journal Size Limit";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "DELETE",
            "TRUNCATE",
            "PERSIST",
            "MEMORY",
            "WAL",
            "OFF"});
            this.comboBox1.Location = new System.Drawing.Point(158, 229);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 15;
            // 
            // labelJournalMode
            // 
            this.labelJournalMode.AutoSize = true;
            this.labelJournalMode.Location = new System.Drawing.Point(14, 232);
            this.labelJournalMode.Name = "labelJournalMode";
            this.labelJournalMode.Size = new System.Drawing.Size(71, 13);
            this.labelJournalMode.TabIndex = 14;
            this.labelJournalMode.Text = "Journal Mode";
            // 
            // checkBoxIgnoreCheckConstraints
            // 
            this.checkBoxIgnoreCheckConstraints.AutoSize = true;
            this.checkBoxIgnoreCheckConstraints.Location = new System.Drawing.Point(158, 199);
            this.checkBoxIgnoreCheckConstraints.Name = "checkBoxIgnoreCheckConstraints";
            this.checkBoxIgnoreCheckConstraints.Size = new System.Drawing.Size(15, 14);
            this.checkBoxIgnoreCheckConstraints.TabIndex = 13;
            this.checkBoxIgnoreCheckConstraints.UseVisualStyleBackColor = true;
            // 
            // labelIgnoreCheckConst
            // 
            this.labelIgnoreCheckConst.AutoSize = true;
            this.labelIgnoreCheckConst.Location = new System.Drawing.Point(14, 199);
            this.labelIgnoreCheckConst.Name = "labelIgnoreCheckConst";
            this.labelIgnoreCheckConst.Size = new System.Drawing.Size(129, 13);
            this.labelIgnoreCheckConst.TabIndex = 12;
            this.labelIgnoreCheckConst.Text = "Ignore Check Constraints";
            // 
            // checkBoxForeignKeys
            // 
            this.checkBoxForeignKeys.AutoSize = true;
            this.checkBoxForeignKeys.Location = new System.Drawing.Point(158, 169);
            this.checkBoxForeignKeys.Name = "checkBoxForeignKeys";
            this.checkBoxForeignKeys.Size = new System.Drawing.Size(15, 14);
            this.checkBoxForeignKeys.TabIndex = 11;
            this.checkBoxForeignKeys.UseVisualStyleBackColor = true;
            // 
            // labelForeignKeys
            // 
            this.labelForeignKeys.AutoSize = true;
            this.labelForeignKeys.Location = new System.Drawing.Point(14, 169);
            this.labelForeignKeys.Name = "labelForeignKeys";
            this.labelForeignKeys.Size = new System.Drawing.Size(69, 13);
            this.labelForeignKeys.TabIndex = 10;
            this.labelForeignKeys.Text = "Foreign Keys";
            // 
            // checkBoxDeferForeignKeys
            // 
            this.checkBoxDeferForeignKeys.AutoSize = true;
            this.checkBoxDeferForeignKeys.Location = new System.Drawing.Point(158, 140);
            this.checkBoxDeferForeignKeys.Name = "checkBoxDeferForeignKeys";
            this.checkBoxDeferForeignKeys.Size = new System.Drawing.Size(15, 14);
            this.checkBoxDeferForeignKeys.TabIndex = 9;
            this.checkBoxDeferForeignKeys.UseVisualStyleBackColor = true;
            // 
            // labelDefForKeys
            // 
            this.labelDefForKeys.AutoSize = true;
            this.labelDefForKeys.Location = new System.Drawing.Point(14, 140);
            this.labelDefForKeys.Name = "labelDefForKeys";
            this.labelDefForKeys.Size = new System.Drawing.Size(99, 13);
            this.labelDefForKeys.TabIndex = 8;
            this.labelDefForKeys.Text = "Defer Foreign Keys";
            // 
            // checkBoxCheckFullSync
            // 
            this.checkBoxCheckFullSync.AutoSize = true;
            this.checkBoxCheckFullSync.Location = new System.Drawing.Point(158, 111);
            this.checkBoxCheckFullSync.Name = "checkBoxCheckFullSync";
            this.checkBoxCheckFullSync.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCheckFullSync.TabIndex = 7;
            this.checkBoxCheckFullSync.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Checkpoint Fullsync";
            // 
            // checkBoxCellSizeCheck
            // 
            this.checkBoxCellSizeCheck.AutoSize = true;
            this.checkBoxCellSizeCheck.Location = new System.Drawing.Point(158, 84);
            this.checkBoxCellSizeCheck.Name = "checkBoxCellSizeCheck";
            this.checkBoxCellSizeCheck.Size = new System.Drawing.Size(15, 14);
            this.checkBoxCellSizeCheck.TabIndex = 5;
            this.checkBoxCellSizeCheck.UseVisualStyleBackColor = true;
            // 
            // labelCellSizeCheck
            // 
            this.labelCellSizeCheck.AutoSize = true;
            this.labelCellSizeCheck.Location = new System.Drawing.Point(14, 84);
            this.labelCellSizeCheck.Name = "labelCellSizeCheck";
            this.labelCellSizeCheck.Size = new System.Drawing.Size(78, 13);
            this.labelCellSizeCheck.TabIndex = 4;
            this.labelCellSizeCheck.Text = "Cell Size Check";
            // 
            // checkBoxAutoIndex
            // 
            this.checkBoxAutoIndex.AutoSize = true;
            this.checkBoxAutoIndex.Location = new System.Drawing.Point(158, 57);
            this.checkBoxAutoIndex.Name = "checkBoxAutoIndex";
            this.checkBoxAutoIndex.Size = new System.Drawing.Size(15, 14);
            this.checkBoxAutoIndex.TabIndex = 3;
            this.checkBoxAutoIndex.UseVisualStyleBackColor = true;
            // 
            // labelAutoIndex
            // 
            this.labelAutoIndex.AutoSize = true;
            this.labelAutoIndex.Location = new System.Drawing.Point(14, 57);
            this.labelAutoIndex.Name = "labelAutoIndex";
            this.labelAutoIndex.Size = new System.Drawing.Size(86, 13);
            this.labelAutoIndex.TabIndex = 2;
            this.labelAutoIndex.Text = "Automatic Index";
            // 
            // comboBoxAutoVacuum
            // 
            this.comboBoxAutoVacuum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAutoVacuum.FormattingEnabled = true;
            this.comboBoxAutoVacuum.Items.AddRange(new object[] {
            "NONE",
            "FULL",
            "INCREMENTAL"});
            this.comboBoxAutoVacuum.Location = new System.Drawing.Point(158, 23);
            this.comboBoxAutoVacuum.Name = "comboBoxAutoVacuum";
            this.comboBoxAutoVacuum.Size = new System.Drawing.Size(121, 21);
            this.comboBoxAutoVacuum.TabIndex = 1;
            // 
            // labelAutoVacuum
            // 
            this.labelAutoVacuum.AutoSize = true;
            this.labelAutoVacuum.Location = new System.Drawing.Point(14, 26);
            this.labelAutoVacuum.Name = "labelAutoVacuum";
            this.labelAutoVacuum.Size = new System.Drawing.Size(70, 13);
            this.labelAutoVacuum.TabIndex = 0;
            this.labelAutoVacuum.Text = "Auto Vacuum";
            // 
            // tabPageOracle
            // 
            this.tabPageOracle.Controls.Add(this.flowLayoutPanel4);
            this.tabPageOracle.Location = new System.Drawing.Point(4, 22);
            this.tabPageOracle.Name = "tabPageOracle";
            this.tabPageOracle.Size = new System.Drawing.Size(714, 549);
            this.tabPageOracle.TabIndex = 3;
            this.tabPageOracle.Text = "Oracle";
            this.tabPageOracle.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.AutoScroll = true;
            this.flowLayoutPanel4.Controls.Add(this.groupBox3);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(714, 549);
            this.flowLayoutPanel4.TabIndex = 1;
            this.flowLayoutPanel4.WrapContents = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(702, 203);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sql Editor";
            // 
            // tabPagePostgreSql
            // 
            this.tabPagePostgreSql.Controls.Add(this.flowLayoutPanel5);
            this.tabPagePostgreSql.Location = new System.Drawing.Point(4, 22);
            this.tabPagePostgreSql.Name = "tabPagePostgreSql";
            this.tabPagePostgreSql.Size = new System.Drawing.Size(714, 549);
            this.tabPagePostgreSql.TabIndex = 4;
            this.tabPagePostgreSql.Text = "PostgreSql";
            this.tabPagePostgreSql.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.AutoScroll = true;
            this.flowLayoutPanel5.Controls.Add(this.groupBox4);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(714, 549);
            this.flowLayoutPanel5.TabIndex = 1;
            this.flowLayoutPanel5.WrapContents = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(702, 203);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sql Editor";
            // 
            // tabPageDB2
            // 
            this.tabPageDB2.Controls.Add(this.flowLayoutPanel6);
            this.tabPageDB2.Location = new System.Drawing.Point(4, 22);
            this.tabPageDB2.Name = "tabPageDB2";
            this.tabPageDB2.Size = new System.Drawing.Size(714, 549);
            this.tabPageDB2.TabIndex = 5;
            this.tabPageDB2.Text = "DB2";
            this.tabPageDB2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.AutoScroll = true;
            this.flowLayoutPanel6.Controls.Add(this.groupBox5);
            this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel6.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(714, 549);
            this.flowLayoutPanel6.TabIndex = 1;
            this.flowLayoutPanel6.WrapContents = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(702, 203);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Sql Editor";
            // 
            // tabPageMSSQL
            // 
            this.tabPageMSSQL.Controls.Add(this.flowLayoutPanel7);
            this.tabPageMSSQL.Location = new System.Drawing.Point(4, 22);
            this.tabPageMSSQL.Name = "tabPageMSSQL";
            this.tabPageMSSQL.Size = new System.Drawing.Size(714, 549);
            this.tabPageMSSQL.TabIndex = 6;
            this.tabPageMSSQL.Text = "MSSQL";
            this.tabPageMSSQL.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.AutoScroll = true;
            this.flowLayoutPanel7.Controls.Add(this.groupBox6);
            this.flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel7.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(714, 549);
            this.flowLayoutPanel7.TabIndex = 1;
            this.flowLayoutPanel7.WrapContents = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(702, 203);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Sql Editor";
            // 
            // labelMaxPageCount
            // 
            this.labelMaxPageCount.AutoSize = true;
            this.labelMaxPageCount.Location = new System.Drawing.Point(393, 58);
            this.labelMaxPageCount.Name = "labelMaxPageCount";
            this.labelMaxPageCount.Size = new System.Drawing.Size(86, 13);
            this.labelMaxPageCount.TabIndex = 20;
            this.labelMaxPageCount.Text = "Max Page Count";
            // 
            // comboBoxMaxPageCount
            // 
            this.comboBoxMaxPageCount.Location = new System.Drawing.Point(546, 56);
            this.comboBoxMaxPageCount.Maximum = new decimal(new int[] {
            -1530494977,
            232830,
            0,
            0});
            this.comboBoxMaxPageCount.Name = "comboBoxMaxPageCount";
            this.comboBoxMaxPageCount.Size = new System.Drawing.Size(84, 21);
            this.comboBoxMaxPageCount.TabIndex = 21;
            // 
            // labelPageSize
            // 
            this.labelPageSize.AutoSize = true;
            this.labelPageSize.Location = new System.Drawing.Point(393, 85);
            this.labelPageSize.Name = "labelPageSize";
            this.labelPageSize.Size = new System.Drawing.Size(53, 13);
            this.labelPageSize.TabIndex = 22;
            this.labelPageSize.Text = "Page Size";
            // 
            // comboboxPageSize
            // 
            this.comboboxPageSize.Location = new System.Drawing.Point(546, 82);
            this.comboboxPageSize.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.comboboxPageSize.Minimum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.comboboxPageSize.Name = "comboboxPageSize";
            this.comboboxPageSize.Size = new System.Drawing.Size(84, 21);
            this.comboboxPageSize.TabIndex = 23;
            this.comboboxPageSize.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            // 
            // labelRecursiveTriggers
            // 
            this.labelRecursiveTriggers.AutoSize = true;
            this.labelRecursiveTriggers.Location = new System.Drawing.Point(393, 112);
            this.labelRecursiveTriggers.Name = "labelRecursiveTriggers";
            this.labelRecursiveTriggers.Size = new System.Drawing.Size(102, 13);
            this.labelRecursiveTriggers.TabIndex = 24;
            this.labelRecursiveTriggers.Text = "Recursive Trigggers";
            // 
            // checkBoxRecursiveTriggers
            // 
            this.checkBoxRecursiveTriggers.AutoSize = true;
            this.checkBoxRecursiveTriggers.Location = new System.Drawing.Point(546, 111);
            this.checkBoxRecursiveTriggers.Name = "checkBoxRecursiveTriggers";
            this.checkBoxRecursiveTriggers.Size = new System.Drawing.Size(15, 14);
            this.checkBoxRecursiveTriggers.TabIndex = 25;
            this.checkBoxRecursiveTriggers.UseVisualStyleBackColor = true;
            // 
            // labelSecureDelete
            // 
            this.labelSecureDelete.AutoSize = true;
            this.labelSecureDelete.Location = new System.Drawing.Point(393, 141);
            this.labelSecureDelete.Name = "labelSecureDelete";
            this.labelSecureDelete.Size = new System.Drawing.Size(74, 13);
            this.labelSecureDelete.TabIndex = 26;
            this.labelSecureDelete.Text = "Secure Delete";
            // 
            // checkBoxSecureDelete
            // 
            this.checkBoxSecureDelete.AutoSize = true;
            this.checkBoxSecureDelete.Location = new System.Drawing.Point(546, 140);
            this.checkBoxSecureDelete.Name = "checkBoxSecureDelete";
            this.checkBoxSecureDelete.Size = new System.Drawing.Size(15, 14);
            this.checkBoxSecureDelete.TabIndex = 27;
            this.checkBoxSecureDelete.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(393, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Synchronous";
            // 
            // comboBoxSynchronous
            // 
            this.comboBoxSynchronous.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSynchronous.FormattingEnabled = true;
            this.comboBoxSynchronous.Items.AddRange(new object[] {
            "OFF",
            "NORMAL",
            "FULL"});
            this.comboBoxSynchronous.Location = new System.Drawing.Point(546, 166);
            this.comboBoxSynchronous.Name = "comboBoxSynchronous";
            this.comboBoxSynchronous.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSynchronous.TabIndex = 29;
            // 
            // labelUserVersion
            // 
            this.labelUserVersion.AutoSize = true;
            this.labelUserVersion.Location = new System.Drawing.Point(396, 199);
            this.labelUserVersion.Name = "labelUserVersion";
            this.labelUserVersion.Size = new System.Drawing.Size(67, 13);
            this.labelUserVersion.TabIndex = 30;
            this.labelUserVersion.Text = "User Version";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(546, 197);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(121, 21);
            this.numericUpDown2.TabIndex = 31;
            // 
            // buttonSavePragma
            // 
            this.buttonSavePragma.Location = new System.Drawing.Point(546, 260);
            this.buttonSavePragma.Name = "buttonSavePragma";
            this.buttonSavePragma.Size = new System.Drawing.Size(121, 23);
            this.buttonSavePragma.TabIndex = 32;
            this.buttonSavePragma.Text = "Save Pragma Values";
            this.buttonSavePragma.UseVisualStyleBackColor = true;
            this.buttonSavePragma.Click += new System.EventHandler(this.buttonSavePragma_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(722, 575);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "OptionsForm";
            this.Text = "OptionsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageGeneric.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tabPageMySqlMaria.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tabPageSqlite.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBoxSqlAfterDbOpens)).EndInit();
            this.groupBoxPragmaEditor.ResumeLayout(false);
            this.groupBoxPragmaEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPageOracle.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.tabPagePostgreSql.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.tabPageDB2.ResumeLayout(false);
            this.flowLayoutPanel6.ResumeLayout(false);
            this.tabPageMSSQL.ResumeLayout(false);
            this.flowLayoutPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxMaxPageCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboboxPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGeneric;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TabPage tabPageMySqlMaria;
        private System.Windows.Forms.TabPage tabPageSqlite;
        private System.Windows.Forms.TabPage tabPageOracle;
        private System.Windows.Forms.TabPage tabPagePostgreSql;
        private System.Windows.Forms.TabPage tabPageDB2;
        private System.Windows.Forms.TabPage tabPageMSSQL;
        private System.Windows.Forms.GroupBox groupBoxSqlEditor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBoxConnection;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBoxFont;
        private System.Windows.Forms.GroupBox groupBoxTabHistory;
        private System.Windows.Forms.GroupBox groupBoxAppHistory;
        private System.Windows.Forms.GroupBox groupBoxPragmaEditor;
        private System.Windows.Forms.CheckBox checkBoxBeginTransAfterCommit;
        private System.Windows.Forms.CheckBox checkBoxBeginTransAfterDbOpens;
        private System.Windows.Forms.Label labelSqlAfterDbOpens;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBoxSqlAfterDbOpens;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.ComboBox comboBoxAutoVacuum;
        private System.Windows.Forms.Label labelAutoVacuum;
        private System.Windows.Forms.CheckBox checkBoxAutoIndex;
        private System.Windows.Forms.Label labelAutoIndex;
        private System.Windows.Forms.CheckBox checkBoxCellSizeCheck;
        private System.Windows.Forms.Label labelCellSizeCheck;
        private System.Windows.Forms.CheckBox checkBoxCheckFullSync;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxDeferForeignKeys;
        private System.Windows.Forms.Label labelDefForKeys;
        private System.Windows.Forms.CheckBox checkBoxForeignKeys;
        private System.Windows.Forms.Label labelForeignKeys;
        private System.Windows.Forms.CheckBox checkBoxIgnoreCheckConstraints;
        private System.Windows.Forms.Label labelIgnoreCheckConst;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label labelJournalMode;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxLockMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown comboBoxMaxPageCount;
        private System.Windows.Forms.Label labelMaxPageCount;
        private System.Windows.Forms.NumericUpDown comboboxPageSize;
        private System.Windows.Forms.Label labelPageSize;
        private System.Windows.Forms.CheckBox checkBoxRecursiveTriggers;
        private System.Windows.Forms.Label labelRecursiveTriggers;
        private System.Windows.Forms.CheckBox checkBoxSecureDelete;
        private System.Windows.Forms.Label labelSecureDelete;
        private System.Windows.Forms.ComboBox comboBoxSynchronous;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label labelUserVersion;
        private System.Windows.Forms.Button buttonSavePragma;
    }
}