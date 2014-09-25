using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculTrajectoire
{
    /// <summary>
    /// Un point dans l'espace.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// La position X du point
        /// </summary>
        public double X
        {
            get
            {
                return mX;
            }
            set
            {
                mX = value;
            }
        }

        /// <see cref="X"/>
        private double mX;

        /// <summary>
        /// La position Y du point
        /// </summary>
        public double Y
        {
            get
            {
                return mY;
            }
            set
            {
                mY = value;
            }
        }

        /// <see cref="Y"/>
        private double mY;

        /// <summary>
        /// La position Z du point
        /// </summary>
        public double Z
        {
            get
            {
                return mZ;
            }
            set
            {
                mZ = value;
            }
        }

        /// <see cref="Z"/>
        private double mZ;

        /// <summary>
        /// Définit si on prend ou non une photo sur ce point
        /// </summary>
        public bool prisePhoto
        {
            get
            {
                return mPrisePhoto;
            }
            set
            {
                mPrisePhoto = value;
            }
        }

        /// <see cref="prisePhoto"/>
        private bool mPrisePhoto;

        /// <summary>
        /// Le temps de stabilisation sur le point
        /// </summary>
        public int TempsStabilisation
        {
            get
            {
                return mTempsStabilisation;
            }
            set
            {
                mTempsStabilisation = value;
            }
        }

        /// <see cref="TempsStabilisation"/>
        private int mTempsStabilisation;


        

        /// <summary>
        /// Constructeur du point.
        /// </summary>
        /// <param name="X">Position X du point.</param>
        /// <param name="Y">Position Y du point.</param>
        /// <param name="Z">Position Z du point.</param>
        public Point(double X, double Y, double Z)
        {
            mX = X;

            mY = Y;

            mZ = Z;

            mPrisePhoto = false;
        }

        /// <summary>
        /// Constructeur du point.
        /// </summary>
        /// <param name="X">Position X du point.</param>
        /// <param name="Y">Position Y du point.</param>
        /// <param name="Z">Position Z du point.</param>
        /// <param name="isTaken">Le temps de stabilisation du drone sur ce point.</param>
        public Point(double X, double Y, double Z, int tempsStabilisation)
        {
            mX = X;

            mY = Y;

            mZ = Z;

            mPrisePhoto = true;

            mTempsStabilisation = tempsStabilisation;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Vérifie si l'objet est égal à ce point ou non.
        /// </summary>
        /// <param name="right">L'objet à comparer avec ce point.</param>
        /// <returns>true si les deux objets sont égaux, false sinon</returns>
        public override bool Equals(object right)
        {
            if (object.ReferenceEquals(right, null))
            {
                return false;
            }

            if (object.ReferenceEquals(this, right))
            {
                return true;
            }

            if (this.GetType() != right.GetType())
            {
                return false;
            }

            return this.Equals(right as Point);
        }

        /// <summary>
        /// Vérifie si ce point est égal à l'autre point.
        /// </summary>
        /// <param name="other">Le point à comparer</param>
        /// <returns>true si les deux points sont égaux, false sinon</returns>
        public bool Equals(Point other)
        {
            return (this.mX.Equals(other.mX) && this.mY.Equals(other.mY) && this.mZ.Equals(other.mZ));
        }
    }
}
