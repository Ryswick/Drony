using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculTrajectoire
{
    /// <summary>
    /// Définit un sous-espace particulier d'un espace, lui-même pouvant comporter plusieurs autres sous-espaces
    /// (par exemple : un palier est un sous-espace d'un espace contenant plusieurs paliers, et il peut également
    ///  y avoir plusieurs sous-espaces dans ce palier).
    /// </summary>
    public class SousEspace : Espace
    {
        public List<Espace> ListeEspaces
        {
            get
            {
                return mListeEspaces;
            }
        }

        /// <see cref="ListeEspaces"/>
        private List<Espace> mListeEspaces;

        public SousEspace() : base()
        {
            mListeEspaces = new List<Espace>();
        }
    }
}
