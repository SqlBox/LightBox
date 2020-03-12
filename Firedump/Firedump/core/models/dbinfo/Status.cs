using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.models.dbinfo
{
    public enum Status : int
    {
        ERROR = 200,
        CANCELED = 300,
        RUNNING = 500,
        FINISHED = 600
    }
}
