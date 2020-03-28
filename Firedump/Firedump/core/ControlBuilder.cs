﻿using FastColoredTextBoxNS;
using Firedump.models;
using Firedump.usercontrols;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Firedump.core.sql;
using sqlbox.commons;
using System.Data;
using Firedump.core.models.events;
using Firedump.core.models.dbinfo;
using System;

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

        internal static usercontrols.DataView CreateDataView(Editor editor)
        {
            return new usercontrols.DataView(editor)
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

        internal static MenuItem[] TreeTableMenuItemsBuilder(TabView _this)
        {
            var items = new List<MenuItem>();
            foreach(MenuItem menuItem in MenuItemGenerator(_this))
            {
                items.Add(menuItem);
            }
            return items.ToArray();
        }

        private static IEnumerable<MenuItem> MenuItemGenerator(TabView _this)
        {
            yield return new MenuItem("Show Data", _this.TreeViewTable_MenuItem_ShowData);
            yield return new MenuItem("Send Create Statement", _this.TreeViewTable_MenuItem_ShowCreate);
            yield return new MenuItem("-");
            yield return new MenuItem("Inspect Table", _this.TreeViewTable_MenuItem_Inspect);
            yield return new MenuItem("-");
            yield return new MenuItem("Drop Table", _this.TreeViewTable_MenuItem_DropTable);
            yield return new MenuItem("Truncate Table", _this.TreeViewTable_MenuItem_TruncateTable);
            yield return new MenuItem("-");
            yield return new MenuItem("Refresh", _this.TreeViewTable_MenuItem_RefreshTable);
        }


        internal static DataTable HistoryDataTableBuilder(ExecutionQueryEvent e)
        {
            DataTable data = new DataTable();
            DataColumn c0 = new DataColumn("Status");
            DataColumn c1 = new DataColumn("Query");
            DataColumn c2 = new DataColumn("Rows affected");
            DataColumn c3 = new DataColumn("Info");
            DataColumn c4 = new DataColumn("Secs/Millis");
            DataColumn c5 = new DataColumn("Executed At");
            c0.DataType = System.Type.GetType("System.Byte[]");
            data.Columns.Add(c0);
            data.Columns.Add(c1);
            data.Columns.Add(c2);
            data.Columns.Add(c3);
            data.Columns.Add(c4);
            data.Columns.Add(c5);
            DataRow row = data.NewRow();
            if (e.Ex != null)
            {
                row["Status"] = IconHelper.status_error_arr;
            }
            else if (e.Status == Status.CANCELED)
            {
                row["Status"] = IconHelper.status_info_arr;
            }
            else
            {
                row["Status"] = IconHelper.status_ok_arr;
            }
            row["Query"] = e.query;
            row["Rows affected"] = e.recordsAffected;
            row["Info"] = e.Ex != null ? e.Ex.Message : "";
            row["Secs/Millis"] = (int)e.duration.TotalSeconds +"/" + (int) e.duration.TotalMilliseconds;
            row["Executed At"] = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            data.Rows.Add(row);
            return data;
        }

    }
}
