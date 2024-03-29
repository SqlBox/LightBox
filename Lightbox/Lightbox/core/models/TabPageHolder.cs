﻿using FastColoredTextBoxNS;
using Lightbox.usercontrols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lightbox.models
{
    public sealed class TabPageHolder : TabPage
    {
        private readonly AutocompleteMenu menu;
        private readonly FastColoredTextBox fastColoredTextBox;
        private readonly DataView dataView;
        public readonly bool IsFile;

        public TabPageHolder(FastColoredTextBox fastColoredTextBox, AutocompleteMenu menu, DataView dataView, bool isFile)
        {
            this.fastColoredTextBox = fastColoredTextBox;
            this.menu = menu;
            this.dataView = dataView;
            this.IsFile = isFile;
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
