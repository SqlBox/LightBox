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
using Firedump.core;
using FastColoredTextBoxNS;

namespace Firedump.ui.usercontrols
{
    public partial class Terminal : UserControl
    {
        public static Terminal MainTerminal { get; set; }
        private MainHome home;
        private  int QueueLimit = 100;
        FastColoredTextBox tb;
        
        public Terminal()
        {
            InitializeComponent();
            tb = ControlBuilder.CreateFastColoredTextBox();
            tb.ReadOnly = true;
            tb.WordWrap = true;
            tb.BackColor = Color.LightGray;
            this.Controls.Add(tb);
        }

        public void SetMainHome(MainHome mh)
        {
            this.home = mh;
        }

        public void AppendText(string command)
        {
            this.Invoke((MethodInvoker)delegate {
                tb.SuspendLayout();
                if (tb.Lines.Count >= QueueLimit)
                {
                    tb.Lines.RemoveAt(0);
                }
                tb.AppendText(command);
                if (!command.Trim().EndsWith(";"))
                {
                    tb.AppendText(";");
                }
                tb.AppendText("\n");
                tb.GoEnd();
                tb.ResumeLayout();
            });
        }

    }
}
