using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lightbox.core.models.dbinfo
{
    public enum Status : int
    {
        ERROR = 200,
        CANCELED = 300,
        ABORTED = 301,
        RUNNING = 500,
        FINISHED = 600,
        HIDDEN = -100
    }
}
