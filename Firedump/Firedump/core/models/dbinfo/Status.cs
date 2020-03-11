using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.core.models.dbinfo
{
    public enum Status : int
    {
        SUCCESS = 100,
        ERROR = 200,
        STOPPED = 300,
        CANCELED = 350,
        WAITING = 400,
        RUNNING = 500,
        FINISHED = 600
    }
}
