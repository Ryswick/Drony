using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculTrajectoire
{
    /// <summary>
    /// Une trajectoire du plan de vol
    /// </summary>
    public class Trajectoire
    {
        /// <summary>
        /// La liste des points de la trajectoire.
        /// </summary>
        public List<Point> Points
        {
            get
            {
                return mPoints;
            }
        }

        /// <see cref="Points"/>
        private List<Point> mPoints;

        public Trajectoire()
        {
            mPoints = new List<Point>();
        }
    }
}
