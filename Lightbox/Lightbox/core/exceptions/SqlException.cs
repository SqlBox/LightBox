using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightbox.core.exceptions
{
    public class SqlException : AppException
    {
        public SqlException(string msg) : base(msg) { }
    }
}
