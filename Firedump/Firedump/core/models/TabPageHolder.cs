using FastColoredTextBoxNS;
using Firedump.usercontrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firedump.models
{
    public sealed class TabPageHolder : TabPage
    {
        private readonly AutocompleteMenu menu;
        private readonly FastColoredTextBox fastColoredTextBox;
        private readonly DataView dataView;
        private readonly bool isFile;

        public TabPageHolder(FastColoredTextBox fastColoredTextBox, AutocompleteMenu menu, DataView dataView, bool isFile)
        {
            this.fastColoredTextBox = fastColoredTextBox;
            this.menu = menu;
            this.dataView = dataView;
            this.isFile = isFile;
        }


        public FastColoredTextBox GetFastColoredTextBox()
        {
            return fastColoredTextBox;
        }

        public AutocompleteMenu GetAutocompleteMenu()
        {
            return menu;
        }

        public DataView GetDataView()
        {
            return this.dataView;
        }

    }
}
