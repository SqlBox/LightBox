﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.sql
{
    public class Utils
    {
        public static bool IsShowDataTypeOfCommand(string sql)
        {
            if (sql == null)
            {
                return false;
            }
            string q = sql.Trim().ToLower();
            return q.Contains("select ") || q.Contains("show ") || q.Contains("describe ") || q.Contains("explain ");
        }
    }
}
