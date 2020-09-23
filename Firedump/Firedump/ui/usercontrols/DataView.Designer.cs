namespace Firedump.usercontrols
{
    partial class DataView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataView));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageResult = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPageHistory = new System.Windows.Forms.TabPage();
            this.dataGridViewHistory = new System.Windows.Forms.DataGridView();
            this.tabPagePlan = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.firedumpdbDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.firedumpdbDataSet = new Firedump.firedumpdbDataSet();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPageHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firedumpdbDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firedumpdbDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(241)))));
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(604, 452);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageResult);
            this.tabControl1.Controls.Add(this.tabPageHistory);
            this.tabControl1.Controls.Add(this.tabPagePlan);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(604, 452);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPageResult
            // 
            this.tabPageResult.BackColor = System.Drawing.Color.Transparent;
            this.tabPageResult.Controls.Add(this.dataGridView1);
            this.tabPageResult.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageResult.ImageIndex = 0;
            this.tabPageResult.Location = new System.Drawing.Point(4, 23);
            this.tabPageResult.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageResult.Name = "tabPageResult";
            this.tabPageResult.Size = new System.Drawing.Size(596, 425);
            this.tabPageResult.TabIndex = 0;
            this.tabPageResult.Text = "Results";
            this.tabPageResult.ToolTipText = "tab\'s last query data results";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(596, 425);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.VirtualMode = true;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView1_Scroll);
            // 
            // tabPageHistory
            // 
            this.tabPageHistory.Controls.Add(this.dataGridViewHistory);
            this.tabPageHistory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageHistory.ImageIndex = 2;
            this.tabPageHistory.Location = new System.Drawing.Point(4, 23);
            this.tabPageHistory.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageHistory.Name = "tabPageHistory";
            this.tabPageHistory.Size = new System.Drawing.Size(596, 425);
            this.tabPageHistory.TabIndex = 2;
            this.tabPageHistory.Text = "History";
            this.tabPageHistory.ToolTipText = "Tab\'s History";
            this.tabPageHistory.UseVisualStyleBackColor = true;
            // 
            // dataGridViewHistory
            // 
            this.dataGridViewHistory.AllowUserToAddRows = false;
            this.dataGridViewHistory.AllowUserToDeleteRows = false;
            this.dataGridViewHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewHistory.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewHistory.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewHistory.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridViewHistory.Name = "dataGridViewHistory";
            this.dataGridViewHistory.Size = new System.Drawing.Size(596, 425);
            this.dataGridViewHistory.TabIndex = 0;
            // 
            // tabPagePlan
            // 
            this.tabPagePlan.ImageIndex = 1;
            this.tabPagePlan.Location = new System.Drawing.Point(4, 23);
            this.tabPagePlan.Margin = new System.Windows.Forms.Padding(0);
            this.tabPagePlan.Name = "tabPagePlan";
            this.tabPagePlan.Size = new System.Drawing.Size(596, 425);
            this.tabPagePlan.TabIndex = 1;
            this.tabPagePlan.Text = "Plan";
            this.tabPagePlan.ToolTipText = "Query Plan";
            this.tabPagePlan.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "query-results-icon.png");
            this.imageList1.Images.SetKeyName(1, "plan-icon.png");
            this.imageList1.Images.SetKeyName(2, "history-icon.png");
            this.imageList1.Images.SetKeyName(3, "print-icon.png");
            this.imageList1.Images.SetKeyName(4, "html-icon.png");
            // 
            // firedumpdbDataSetBindingSource
            // 
            this.firedumpdbDataSetBindingSource.DataSource = this.firedumpdbDataSet;
            this.firedumpdbDataSetBindingSource.Position = 0;
            // 
            // firedumpdbDataSet
            // 
            this.firedumpdbDataSet.DataSetName = "firedumpdbDataSet";
            this.firedumpdbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DataView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "DataView";
            this.Size = new System.Drawing.Size(604, 452);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPageHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firedumpdbDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firedumpdbDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource firedumpdbDataSetBindingSource;
        private firedumpdbDataSet firedumpdbDataSet;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageResult;
        private System.Windows.Forms.TabPage tabPagePlan;
        private System.Windows.Forms.TabPage tabPageHistory;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.DataGridView dataGridViewHistory;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
