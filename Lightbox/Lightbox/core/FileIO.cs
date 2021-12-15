using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightbox.core
{
    internal class FileIO
    {

        internal static void Save(FastColoredTextBox tb,string fileName)
        {
            try
            {
                tb.SaveToFile(fileName, System.Text.Encoding.UTF8);
            }
            catch (IOException ex) { }
        }

        internal static FileInfo FileInfo(string fileName) 
        {
            try
            {
                return new FileInfo(fileName);
            }
            catch (Exception ex) { }
            return null;
        }

        internal static string ReadAllText(string fileName)
        {
            try
            {
                return File.ReadAllText(fileName);
            }
            catch (Exception ex) { }
            return null;
        }
    }
}
