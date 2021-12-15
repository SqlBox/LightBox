using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lightbox.ui.usercontrols
{
    public partial class FontSize : UserControl
    {
        public FontSize()
        {
            InitializeComponent();
            comboBoxFonts.DataSource = System.Drawing.FontFamily.Families.ToList();
        }


        private void comboBoxFonts_DrawItem(object sender, DrawItemEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var font = new Font((FontFamily)comboBox.Items[e.Index], comboBox.Font.SizeInPoints);
            e.DrawBackground();
            e.Graphics.DrawString(font.Name, font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
        }

        internal void setSize(int size)
        {
        }

        internal decimal getSize()
        {
            return numericFontSize.Value;
        }

        internal void setFont(FontFamily font)
        {
        }

        internal FontFamily getFont()
        {
            return comboBoxFonts.SelectedItem as FontFamily;
        }


    }
}
