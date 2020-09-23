using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.protectsoft.SqlStatementParser.formatter;
using FastColoredTextBoxNS;
using Firedump.core.sql;
using Firedump.models;
using Firedump.usercontrols;

namespace Firedump.core
{
    // A helper adapter class for editor ui and the creational utilities class
    public class EditorAdapter
    {

        // The order of declarations and 'Add's' Matters
        internal static TabPageHolder  CreateQueryTab<C>(C Control, ImageList imageList1, List<AutocompleteItem> menuItems,Editor editor, string sql,string tabname,bool isFile)
            where C : Control
        {
            var fastColoredTextBox1 = ControlBuilder.CreateFastColoredTextBox(Control);
            
            //problem with formater
            /*
            if (EditorConfig.isAutoFormatConfigOn())
            {
                sql = new Formatter().Format(sql);
            }
            */

            fastColoredTextBox1.Text = sql;
            var tabPage = new TabPageHolder(fastColoredTextBox1, ControlBuilder.CreateAutoCompleteMenu(fastColoredTextBox1, imageList1, menuItems), 
                ControlBuilder.CreateDataView(editor),isFile)
            {
                Name = "tabPageQuery" + (Control.Controls.Count + 1),
                Text = tabname == null ? ("Tab" + (Control.Controls.Count + 1)) : tabname,
                UseVisualStyleBackColor = true,
                TabIndex = Control.Controls.Count,
                Location = new System.Drawing.Point(4, 22),
            };

            var splitContainer = ControlBuilder.CreateSplitContainer();
            splitContainer.Panel1.Controls.Add(tabPage.GetFastColoredTextBox());
            splitContainer.Panel2.Controls.Add(tabPage.GetDataView());
            tabPage.Controls.Add(splitContainer);
            return tabPage;
        }

    }
}
