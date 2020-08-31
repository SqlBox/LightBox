namespace Firedump.usercontrols
{
    public partial class TableView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TableView));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabObjectInfo = new System.Windows.Forms.TabPage();
            this.tabSnippet = new System.Windows.Forms.TabPage();
            this.tabSession = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "table-icon.png");
            this.imageList1.Images.SetKeyName(1, "fieldicon.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(298, 497);
            this.panel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabObjectInfo);
            this.tabControl1.Controls.Add(this.tabSnippet);
            this.tabControl1.Controls.Add(this.tabSession);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(298, 497);
            this.tabControl1.TabIndex = 0;
            // 
            // tabObjectInfo
            // 
            this.tabObjectInfo.Location = new System.Drawing.Point(4, 22);
            this.tabObjectInfo.Name = "tabObjectInfo";
            this.tabObjectInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabObjectInfo.Size = new System.Drawing.Size(290, 471);
            this.tabObjectInfo.TabIndex = 0;
            this.tabObjectInfo.Text = "Object Info";
            this.tabObjectInfo.UseVisualStyleBackColor = true;
            // 
            // tabSnippet
            // 
            this.tabSnippet.Location = new System.Drawing.Point(4, 22);
            this.tabSnippet.Name = "tabSnippet";
            this.tabSnippet.Padding = new System.Windows.Forms.Padding(3);
            this.tabSnippet.Size = new System.Drawing.Size(290, 471);
            this.tabSnippet.TabIndex = 1;
            this.tabSnippet.Text = "Snippets";
            this.tabSnippet.UseVisualStyleBackColor = true;
            // 
            // tabSession
            // 
            this.tabSession.Location = new System.Drawing.Point(4, 22);
            this.tabSession.Name = "tabSession";
            this.tabSession.Size = new System.Drawing.Size(290, 471);
            this.tabSession.TabIndex = 2;
            this.tabSession.Text = "Session";
            this.tabSession.UseVisualStyleBackColor = true;
            // 
            // TableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(241)))));
            this.Controls.Add(this.panel1);
            this.Name = "TableView";
            this.Size = new System.Drawing.Size(298, 497);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabObjectInfo;
        private System.Windows.Forms.TabPage tabSnippet;
        private System.Windows.Forms.TabPage tabSession;
    }
}
