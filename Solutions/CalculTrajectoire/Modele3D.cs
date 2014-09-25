using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculTrajectoire
{
    /// <summary>
    /// Définit un domaine représenté à partir d'un modèle 3D.
    /// </summary>
    public class Modele3D : Domaine
    {
        public Modele3D()
            : base(true)
        { }

        public Modele3D(int Palier, Point PointDepart)
            : base(PointDepart, true)
        { }
    }
}