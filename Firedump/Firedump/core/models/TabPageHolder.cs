using FastColoredTextBoxNS;
using Firedump.core.parsers;
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

        public TabPageHolder(FastColoredTextBox fastColoredTextBox, AutocompleteMenu menu)
        {
            this.fastColoredTextBox = fastColoredTextBox;
            this.menu = menu;
        }
        

        public FastColoredTextBox GetFastColoredTextBox()
        {
            return fastColoredTextBox;
        }

        public AutocompleteMenu GetAutocompleteMenu()
        {
            return menu;
        }

    }
}
