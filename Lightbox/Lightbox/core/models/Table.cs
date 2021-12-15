using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightbox.models
{
    public class Table
    {
        public string TableName;
        public string ColumnName;
        public string DataType;
        public string IsNullable;
        public long length;
        public string DefaultValue;
        public Table() { }

        public Table(string t, string c, string d, string n, long l) : this()
        {
            TableName = t;
            ColumnName = c;
            DataType = d;
            IsNullable = n;
            length = l;
        }
    }
}
