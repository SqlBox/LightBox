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
        public static Terminal MainTerminal { get; set; }
        private MainHome home;
        public int QueueLimit = 100;
        public Terminal()
        {
            InitializeComponent();        
        }

        public void SetMainHome(MainHome mh)
        {
            this.home = mh;
        }

        public void AppendText(string command)
        {
            this.Invoke((MethodInvoker)delegate {
                if(richTextBox.Lines.Length >= QueueLimit)
                {
                    List<string> lines = richTextBox.Lines.ToList();
                    lines.RemoveAt(0);
                    richTextBox.Lines = lines.ToArray();
                }
                richTextBox.AppendText(command + "\n");
                richTextBox.ScrollToCaret();
            });
        }
    }
}
