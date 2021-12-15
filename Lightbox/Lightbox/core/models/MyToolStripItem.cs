using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightbox.core.models
{
    public class MyToolStripItem
    {
        public int Limit;
        public MyToolStripItem(int limit) { Limit = limit; }
        public override string ToString()
        {
            return this.Limit != 0 ? "Limit " + this.Limit : "Limit Disabled";
        }
    }
}
