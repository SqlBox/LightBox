using FastColoredTextBoxNS;
using Firedump.models;
using Firedump.usercontrols;
using System.Collections.Generic;
using System.Windows.Forms;
using Firedump.core.parsers;
using System.Drawing;
using Firedump.core.sql;

namespace Firedump.core
{
    public sealed class ControlBuilder
    {

        internal static FastColoredTextBox CreateFastColoredTextBox(Control Control)
        {
            return new FastColoredTextBox()
            {
                BackBrush = null,
                CharHeight = 14,
                CharWidth = 8,
                Cursor = Cursors.IBeam,
                Dock = DockStyle.Fill,
                IsReplaceMode = false,
                Name = "fastColoredTextBox" + (Control.Controls.Count + 1),
                Location = new System.Drawing.Point(30, 30),
                Text = " ",
                Zoom = 100,
                AutoScrollMinSize = new System.Drawing.Size(179, 14),
                TabIndex = Control.Controls.Count,
                DisabledColor = System.Drawing.Color.FromArgb(100, 180, 180, 180),
                SelectionColor = System.Drawing.Color.FromArgb(60, 0, 0, 255),
                Size = new System.Drawing.Size(726, 120),
                Language = Language.SQL,
                AutoIndent = true,
            };
        }

        internal static Panel CreatePanel(Control c)
        {
            return new Panel()
            {
                Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(3, 3),
                TabIndex = c.Controls.Count,
                Name = "panel" + (c.Controls.Count + 1)
            };
        }

        internal static AutocompleteMenu CreateAutoCompleteMenu(FastColoredTextBox editor, ImageList imageList, List<AutocompleteItem> menuItems)
        {
            return CreateAutoCompleteMenu(editor, imageList, menuItems,StringUtils.REGEX);
        }

        internal static AutocompleteMenu CreateAutoCompleteMenu(FastColoredTextBox editor, ImageList imageList,List<AutocompleteItem> menuItems, string searchPattern)
        {
            var menu = new AutocompleteMenu(editor)
            {
                ImageList = imageList,
                SearchPattern = searchPattern,
                AppearInterval = 10,
                MinFragmentLength = 3,
                ForeColor = Color.Blue,
            };
            menu.Items.SetAutocompleteItems(menuItems);
            menu.Items.MaximumSize = new System.Drawing.Size(300, 400);
            menu.AutoSize = true;
            menu.Items.AutoSize = true;
            menu.Items.Width = 300;
            return menu;
        }

        internal static DataView CreateDataView(QueryExecutor qe)
        {
            return new DataView(qe)
            {
                Dock = DockStyle.Fill,
                Location = new System.Drawing.Point(0, 0),
                Name = "dataView1",
                Size = new System.Drawing.Size(726, 120),
                TabIndex = 0
            };
        }

        internal static SplitContainer CreateSplitContainer()
        {
            return new SplitContainer()
            {
                Dock = DockStyle.Fill,
                SplitterDistance = 200,
                Orientation = Orientation.Horizontal
            };
        }

    }
}
