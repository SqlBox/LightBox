﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firedump.models.location
{
    interface ILocationCloud : ILocation
    {
        void setExtraCredentials();

        void doExtraStuff();
    }
}
