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
        private MainHome home;
        public Terminal()
        {
            InitializeComponent();
            Task.Delay(500).ContinueWith(_ =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    textBox1.Focus();
                });
            });
        }

        public void SetMainHome(MainHome mh)
        {
            this.home = mh;
        }


        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                richTextBox.AppendText(textBox1.Text + "\n");
                richTextBox.ScrollToCaret();
                textBox1.Text = "";
                e.SuppressKeyPress = true;
            }
        }
    }
}
