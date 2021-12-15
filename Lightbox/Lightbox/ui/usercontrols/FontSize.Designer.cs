namespace Lightbox.ui.usercontrols
{
    partial class FontSize
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
            this.numericFontSize = new System.Windows.Forms.NumericUpDown();
            this.labelFontSize = new System.Windows.Forms.Label();
            this.labelFont = new System.Windows.Forms.Label();
            this.comboBoxFonts = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // numericFontSize
            // 
            this.numericFontSize.Location = new System.Drawing.Point(69, 42);
            this.numericFontSize.Maximum = new decimal(new int[] {
            72,
            0,
            0,
            0});
            this.numericFontSize.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numericFontSize.Name = "numericFontSize";
            this.numericFontSize.Size = new System.Drawing.Size(315, 21);
            this.numericFontSize.TabIndex = 32;
            this.numericFontSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // labelFontSize
            // 
            this.labelFontSize.AutoSize = true;
            this.labelFontSize.Location = new System.Drawing.Point(2, 44);
            this.labelFontSize.Name = "labelFontSize";
            this.labelFontSize.Size = new System.Drawing.Size(55, 13);
            this.labelFontSize.TabIndex = 31;
            this.labelFontSize.Text = "Font Size:";
            // 
            // labelFont
            // 
            this.labelFont.AutoSize = true;
            this.labelFont.Location = new System.Drawing.Point(2, 6);
            this.labelFont.Name = "labelFont";
            this.labelFont.Size = new System.Drawing.Size(33, 13);
            this.labelFont.TabIndex = 30;
            this.labelFont.Text = "Font:";
            // 
            // comboBoxFonts
            // 
            this.comboBoxFonts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxFonts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFonts.FormattingEnabled = true;
            this.comboBoxFonts.Location = new System.Drawing.Point(69, 3);
            this.comboBoxFonts.Name = "comboBoxFonts";
            this.comboBoxFonts.Size = new System.Drawing.Size(315, 22);
            this.comboBoxFonts.TabIndex = 29;
            this.comboBoxFonts.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBoxFonts_DrawItem);
            // 
            // FontSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(241)))));
            this.Controls.Add(this.numericFontSize);
            this.Controls.Add(this.labelFontSize);
            this.Controls.Add(this.labelFont);
            this.Controls.Add(this.comboBoxFonts);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "FontSize";
            this.Size = new System.Drawing.Size(387, 65);
            ((System.ComponentModel.ISupportInitialize)(this.numericFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericFontSize;
        private System.Windows.Forms.Label labelFontSize;
        private System.Windows.Forms.Label labelFont;
        private System.Windows.Forms.ComboBox comboBoxFonts;
    }
}
