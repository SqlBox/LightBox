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
            this.tabPagePlan = new System.Windows.Forms.TabPage();
            this.tabPageHistory = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPagePrint = new System.Windows.Forms.TabPage();
            this.tabPageHtml = new System.Windows.Forms.TabPage();
            this.firedumpdbDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.firedumpdbDataSet = new Firedump.firedumpdbDataSet();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firedumpdbDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.firedumpdbDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(604, 452);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageResult);
            this.tabControl1.Controls.Add(this.tabPageHistory);
            this.tabControl1.Controls.Add(this.tabPagePlan);
            this.tabControl1.Controls.Add(this.tabPagePrint);
            this.tabControl1.Controls.Add(this.tabPageHtml);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.tabPageResult.Controls.Add(this.dataGridView1);
            this.tabPageResult.ImageIndex = 0;
            this.tabPageResult.Location = new System.Drawing.Point(4, 23);
            this.tabPageResult.Name = "tabPageResult";
            this.tabPageResult.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResult.Size = new System.Drawing.Size(596, 425);
            this.tabPageResult.TabIndex = 0;
            this.tabPageResult.Text = "Results";
            this.tabPageResult.ToolTipText = "tab\'s last query data results";
            this.tabPageResult.UseVisualStyleBackColor = true;
            // 
            // tabPagePlan
            // 
            this.tabPagePlan.ImageIndex = 1;
            this.tabPagePlan.Location = new System.Drawing.Point(4, 23);
            this.tabPagePlan.Name = "tabPagePlan";
            this.tabPagePlan.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlan.Size = new System.Drawing.Size(596, 425);
            this.tabPagePlan.TabIndex = 1;
            this.tabPagePlan.Text = "Plan";
            this.tabPagePlan.ToolTipText = "Query Plan";
            this.tabPagePlan.UseVisualStyleBackColor = true;
            // 
            // tabPageHistory
            // 
            this.tabPageHistory.ImageIndex = 2;
            this.tabPageHistory.Location = new System.Drawing.Point(4, 23);
            this.tabPageHistory.Name = "tabPageHistory";
            this.tabPageHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHistory.Size = new System.Drawing.Size(596, 425);
            this.tabPageHistory.TabIndex = 2;
            this.tabPageHistory.Text = "History";
            this.tabPageHistory.ToolTipText = "Tab\'s History";
            this.tabPageHistory.UseVisualStyleBackColor = true;
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
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(590, 419);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.VirtualMode = true;
            // 
            // tabPagePrint
            // 
            this.tabPagePrint.ImageIndex = 3;
            this.tabPagePrint.Location = new System.Drawing.Point(4, 23);
            this.tabPagePrint.Name = "tabPagePrint";
            this.tabPagePrint.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePrint.Size = new System.Drawing.Size(596, 425);
            this.tabPagePrint.TabIndex = 3;
            this.tabPagePrint.Text = "Print";
            this.tabPagePrint.ToolTipText = "print data results";
            this.tabPagePrint.UseVisualStyleBackColor = true;
            // 
            // tabPageHtml
            // 
            this.tabPageHtml.ImageIndex = 4;
            this.tabPageHtml.Location = new System.Drawing.Point(4, 23);
            this.tabPageHtml.Name = "tabPageHtml";
            this.tabPageHtml.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHtml.Size = new System.Drawing.Size(596, 425);
            this.tabPageHtml.TabIndex = 4;
            this.tabPageHtml.Text = "Html";
            this.tabPageHtml.ToolTipText = "export to html";
            this.tabPageHtml.UseVisualStyleBackColor = true;
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
            ((System.ComponentModel.ISupportInitialize)(this.firedumpdbDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.firedumpdbDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource firedumpdbDataSetBindingSource;
        private firedumpdbDataSet firedumpdbDataSet;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageResult;
        private System.Windows.Forms.TabPage tabPagePlan;
        private System.Windows.Forms.TabPage tabPageHistory;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabPage tabPagePrint;
        private System.Windows.Forms.TabPage tabPageHtml;
    }
}
