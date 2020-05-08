namespace Firedump.ui.forms
{
    partial class TerminalForm
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
            this.terminal1 = new Firedump.ui.usercontrols.Terminal();
            this.SuspendLayout();
            // 
            // terminal1
            // 
            this.terminal1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.terminal1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.terminal1.Location = new System.Drawing.Point(0, 0);
            this.terminal1.Margin = new System.Windows.Forms.Padding(0);
            this.terminal1.Name = "terminal1";
            this.terminal1.Size = new System.Drawing.Size(760, 292);
            this.terminal1.TabIndex = 0;
            // 
            // TerminalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 292);
            this.Controls.Add(this.terminal1);
            this.Name = "TerminalForm";
            this.Text = "TerminalForm";
            this.ResumeLayout(false);

        }

        #endregion

        private usercontrols.Terminal terminal1;
    }
}