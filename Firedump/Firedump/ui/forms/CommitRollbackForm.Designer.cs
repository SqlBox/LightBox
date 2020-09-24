namespace Firedump.ui.forms
{
    partial class CommitRollbackForm
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
            this.buttonCommit = new System.Windows.Forms.Button();
            this.buttonRollback = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCommit
            // 
            this.buttonCommit.Location = new System.Drawing.Point(15, 64);
            this.buttonCommit.Name = "buttonCommit";
            this.buttonCommit.Size = new System.Drawing.Size(108, 23);
            this.buttonCommit.TabIndex = 0;
            this.buttonCommit.Text = "Commit and save";
            this.buttonCommit.UseVisualStyleBackColor = true;
            this.buttonCommit.Click += new System.EventHandler(this.buttonCommit_Click);
            // 
            // buttonRollback
            // 
            this.buttonRollback.Location = new System.Drawing.Point(289, 64);
            this.buttonRollback.Name = "buttonRollback";
            this.buttonRollback.Size = new System.Drawing.Size(108, 23);
            this.buttonRollback.TabIndex = 1;
            this.buttonRollback.Text = "Rollback and save";
            this.buttonRollback.UseVisualStyleBackColor = true;
            this.buttonRollback.Click += new System.EventHandler(this.buttonRollback_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "*If any transaction is active a commit or rollback is required in order to save p" +
    "ragma";
            // 
            // CommitRollbackForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 98);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRollback);
            this.Controls.Add(this.buttonCommit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "CommitRollbackForm";
            this.Text = "Warning";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCommit;
        private System.Windows.Forms.Button buttonRollback;
        private System.Windows.Forms.Label label1;
    }
}