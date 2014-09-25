using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculTrajectoire
{
    /// <summary>
    /// Définit un domaine représenté à partir d'une carte 2D.
    /// </summary>
    public class Carte2D : Domaine
    {
        /// <summary>
        /// Le palier de l'espace.
        /// </summary>
        public int Palier
        {
            get
            {
                return mPalier;
            }
        }

        /// <see cref="Palier"/>
        private int mPalier;

        public Carte2D()
            : base(true)
        { }

        public Carte2D(int Palier, Point PointDepart)
            : base(PointDepart, true)
        {
            mPalier = Palier;
        }
    }
}
