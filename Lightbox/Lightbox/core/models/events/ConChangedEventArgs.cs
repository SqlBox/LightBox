﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightbox.models.events
{
    public class ConChangedEventArgs : EventArgs
    {
        public DbConnection con;
        public ConChangedEventArgs(DbConnection con)
        {
            this.con = con;
        }
    }

}
