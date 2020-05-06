using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firedump.core.models;

namespace Firedump.ui.usercontrols
{
    public partial class Terminal : UserControl
    {
        private ToolStripTextBox textBoxCommand;
        private MainHome home;
        public Terminal()
        {
            InitializeComponent();
            textBoxCommand = new FullWidthToolStripTextBox();
            textBoxCommand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            textBoxCommand.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBoxCommand.Font = new System.Drawing.Font("Courier New", 8.25F);
            textBoxCommand.ForeColor = System.Drawing.SystemColors.HighlightText;
            textBoxCommand.Name = "toolStripTextBox1";
            textBoxCommand.KeyDown += textBox1_KeyDown;
            toolStrip1.Items.Add(textBoxCommand);
        }

        public void SetMainHome(MainHome mh)
        {
            this.home = mh;
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(textBoxCommand.Text.Trim().ToLower() == "clear" || textBoxCommand.Text.Trim().ToLower() == "cls")
                {
                    richTextBox.Text = "";
                    textBoxCommand.Text = "";
                }
                richTextBox.AppendText(textBoxCommand.Text + "\n");
                richTextBox.ScrollToCaret();
                textBoxCommand.Text = "";
                e.SuppressKeyPress = true;
            }
        }
    }
}
