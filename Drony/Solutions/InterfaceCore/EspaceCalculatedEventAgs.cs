using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceCore
{
    class EspaceCalculatedEventAgs : EventArgs
    {
        public Espace EspaceAModeliser
        {
            get;
            private set;
        }

        public Espace EspaceDeplacement
        {
            get;
            private set;
        }

        public Espace Ensemble
        {
            get;
            private set;
        }

        public EspaceCalculatedEventAgs(Espace aModeliser, Espace deplacement, Espace ensemble)
        {
            EspaceAModeliser = aModeliser;
            EspaceDeplacement = deplacement;
            Ensemble = ensemble;
        }
    }
}
