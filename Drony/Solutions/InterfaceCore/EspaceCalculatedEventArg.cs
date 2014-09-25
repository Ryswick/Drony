using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceCore
{
    public class EspaceCalculatedEventArg : EventArgs
    {
        public Espace Space
        {
            get;
            private set;
        }

        public EspaceCalculatedEventArg(Espace space)
        {
            Space = space;
        }
    }
}
