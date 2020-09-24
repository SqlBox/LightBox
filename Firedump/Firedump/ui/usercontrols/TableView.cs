using Firedump.core;
using Firedump.core.db;
using Firedump.core.sql;
using Firedump.models;
using Firedump.models.events;
using Firedump.ui.usercontrols;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firedump.usercontrols
{
    public partial class TableView : UserControlReference
    {

        public TableView() { InitializeComponent(); }

        internal void setTableInfo(string tableName, List<string> fields, List<string> tableInfos)
        {
            richTextBoxObjectInfo.Clear();
            Font f = richTextBoxObjectInfo.SelectionFont;
            richTextBoxObjectInfo.SelectionFont = new Font(richTextBoxObjectInfo.Font, FontStyle.Bold);
            richTextBoxObjectInfo.AppendText("Table:" + tableName + "\n\n");
            richTextBoxObjectInfo.SelectionFont = new Font(richTextBoxObjectInfo.Font, FontStyle.Bold);
            richTextBoxObjectInfo.AppendText("Columns:" + "\n");
            richTextBoxObjectInfo.SelectionFont = f;
            foreach (string s in fields)
            {
                richTextBoxObjectInfo.AppendText(s + "\n");
            }
            richTextBoxObjectInfo.AppendText("\n");
            richTextBoxObjectInfo.SelectionFont = new Font(richTextBoxObjectInfo.Font, FontStyle.Bold);
            richTextBoxObjectInfo.AppendText("Info:" + "\n");
            richTextBoxObjectInfo.SelectionFont = f;
            foreach (string s in tableInfos)
            {
                richTextBoxObjectInfo.AppendText(s + "\n");
            }
        }

    }
}
