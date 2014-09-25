using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculTrajectoire
{
    /// <summary>
    /// Un domaine est un espace définit par une liste de point.
    /// </summary>
    public class Domaine : Espace
    {
        /// <summary>
        /// Le point de départ de l'espace.
        /// </summary>
        public Point PointDepart
        {
            get
            {
                return mPointDepart;
            }
        }

        /// <see cref="PoitnDepart"/>
        private Point mPointDepart;

        /// <summary>
        /// La liste des points de l'espace.
        /// </summary>
        public List<Point> PointEspace
        {
            get
            {
                return mPointEspace;
            }
        }

        /// <see cref="PointEspace"/>
        private List<Point> mPointEspace;

        /// <summary>
        /// Indique si le domaine est traversable ou non
        /// </summary>
        public bool Traversable
        {
            get
            {
                return mTraversable;
            }
        }

        /// <see cref="mTraversable"/>
        private bool mTraversable;

        /// <summary>
        /// Constructeur d'un domaine
        /// </summary>
        public Domaine(bool Traversable)
            : base()
        {
            mTraversable = Traversable;
        }

        /// <summary>
        /// Constructeur d'un domaine
        /// </summary>
        public Domaine(Point PointDepart, bool Traversable)
            : base()
        {
            mPointDepart = PointDepart;
            mPointEspace = new List<Point>();
            mTraversable = Traversable;
        }
    }
}
