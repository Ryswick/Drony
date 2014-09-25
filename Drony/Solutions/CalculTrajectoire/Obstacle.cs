using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculTrajectoire
{
    /// <summary>
    /// Définit un domaine qui n'est pas traversable par le drone.
    /// </summary>
    public class Obstacle : Domaine
    {
        public Obstacle()
            : base(false)
        { }

        public Obstacle(int Palier, Point PointDepart)
            : base(PointDepart, false)
        { }
    }
}