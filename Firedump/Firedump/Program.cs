using com.protectsoft.SqlStatementParser;
using Firedump.core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace Firedump
{
    static class Program
    {
        static Program()
        {
            IconHelper.Init();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new MainHome());       
        }

    }
}
