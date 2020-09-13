using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firedump.ui.forms
{
    public partial class OptionsForm : Form
    {
        private DbConnection con;
        public OptionsForm(System.Data.Common.DbConnection con)
        {
            InitializeComponent();
            FormUtils.setFormIcon(this);
            this.con = con;
            comboBoxFonts.DataSource = System.Drawing.FontFamily.Families.ToList();
        }

        private void ComboBoxFonts_DrawItem(object sender, DrawItemEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var fontFamily = (FontFamily)comboBox.Items[e.Index];
            var font = new Font(fontFamily, comboBox.Font.SizeInPoints);

            e.DrawBackground();
            e.Graphics.DrawString(font.Name, font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {
            loadSqliteSettings();
        }

        private void OptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveSqliteSettings();
        }

        private void loadSqliteSettings()
        {
            checkBoxBeginTransAfterCommit.Checked   =   Properties.Settings.Default.option_sqlite_begintranscommit;
            checkBoxBeginTransAfterDbOpens.Checked = Properties.Settings.Default.option_sqlite_begintransdbopen;
            fastColoredTextBoxSqlAfterDbOpens.Text = Properties.Settings.Default.option_sqlite_sqlafteropen;
        }

        private void saveSqliteSettings()
        {
            Properties.Settings.Default.option_sqlite_begintranscommit = checkBoxBeginTransAfterCommit.Checked;
            Properties.Settings.Default.option_sqlite_begintransdbopen = checkBoxBeginTransAfterDbOpens.Checked;
            Properties.Settings.Default.option_sqlite_sqlafteropen = fastColoredTextBoxSqlAfterDbOpens.Text;

            Properties.Settings.Default.Save();
        }

        private void buttonSavePragma_Click(object sender, EventArgs e)
        {

        }

    }
}
