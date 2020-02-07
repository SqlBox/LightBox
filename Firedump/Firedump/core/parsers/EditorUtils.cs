using FastColoredTextBoxNS;
using Firedump.core.attributes;
using Firedump.core.exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Firedump.core.parsers
{
    internal class EditorUtils
    {
        const string REGEX = @"[\w\:=!<>]";

        internal static AutocompleteMenu CreateAutoCompleteMenu(FastColoredTextBox editor, ImageList imageList)
        {
            return CreateAutoCompleteMenu(editor, imageList, REGEX);
        }

        internal static AutocompleteMenu CreateAutoCompleteMenu(FastColoredTextBox editor, ImageList imageList, string searchPattern)
        {
            return new AutocompleteMenu(editor)
            {
                ImageList = imageList,
                SearchPattern = searchPattern,
                AppearInterval = 10,
                MinFragmentLength = 3,
                ForeColor = Color.Blue
            };
        }

        [ForTest("test success and most importantly test fail and check for sql string imutability")]
        internal static string FormatSql(string sql)
        {
            try
            {
                var formatter = new Formatter();
                formatter.Format(new StringBuilder(sql).ToString());
                if (formatter.Success)
                {
                    return formatter.LastResult;
                }
            }
            catch (MyFormatterException ex)
            {
                // Do nothing.
            }
            //Return the orignal sql
            return sql;
        }


        /**
         * 
         * Get the sql to be executed
         * So its either gonna be the user selected area from tab editor
         * Or if its empty(that means the user just clicked on the tab but didnt select any text)
         * its the whole tab text
         * and if the tab text is null or empty
         **/
        [ForTest]
        internal static string SelectedTextOrTabText(string selectedText, string tabText)
        {
            return !string.IsNullOrEmpty(selectedText) ? selectedText : !string.IsNullOrEmpty(tabText) ? tabText : null;
        }

    }
}
