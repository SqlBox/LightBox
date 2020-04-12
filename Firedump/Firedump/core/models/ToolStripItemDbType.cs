using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.models
{
    public class ToolStripItemDbType
    {
        public int db_type;
        public ToolStripItemDbType(int type) 
        {
            this.db_type = type;
        }

        public override string ToString()
        {
            switch (db_type)
            {
                case 0:
                    return "MYSQL";
                case 1:
                    return "MARIA DB";
                case 2:
                    return "ORACLE";
                case 3:
                    return "POSTGRESQL";
                case 4:
                    return "MICROSOFT SQL SERVER";
                case 5:
                    return "SQLITE";
                case 6:
                    return "IBM DB2";
                case 7:
                    return "FIREBIRD";
                case 8:
                    return "VistaDB";
            }
            return "";
        }
    }
}
