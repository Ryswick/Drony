using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculTrajectoire
{
    /// <summary>
    /// Classe utilisée pour effectuer la vérification de la possibiité de vol du drone pour un ensemble de points donnés.
    /// </summary>
    class CalculSimple : ICalculTrajectoire
    {
        /// <summary>
        /// La trajectoire que peut parcourir le drone
        /// </summary>
        public Trajectoire Trajectoire
        {
            get
            {
                return mTrajectoires;
            }
        }

        /// <see cref="Trajectoires"/>
        private Trajectoire mTrajectoires;

        /// <summary>
        /// La liste des points que l'on veut faire parcourir aux drones
        /// </summary>
        public List<Point> PointParcours
        {
            get
            {
                return mPointParcours;
            }
        }

        /// <see cref="mPointParcours"/>
        private List<Point> mPointParcours;

        /// <summary>
        /// Constructeur de CalculSimple
        /// </summary>
        public CalculSimple(List<Point> PointsParcours)
        {
            mTrajectoires = new Trajectoire();
            mPointParcours = PointsParcours;
        }


        /// <summary>
        /// Méthode lançant le calcul de vérification
        /// </summary>
        /// <returns>La consommation des batteries</returns>
        public double CalculerTrajectoires()
        {
            int index = 1;

            //Consommation en % de la batterie utilisée
            double consommationBatterie = 0.0f;

            //Consommation en % pour le trajet d'un point vers un autre
            double consommationTrajet = 0;

            //Consommation en % pour le temps de stabilisation sur un point
            double consommationStabilisation = 0;

            //Batterie utilisée pour le vol
            Batterie b = PlanDeVol.getInstance().Drone.Batteries[0];

            Trajectoire.Points.Add(mPointParcours[0]);

            while (index < mPointParcours.Count())
            {
                consommationTrajet = CalculerConsommationTrajet(Trajectoire.Points[Trajectoire.Points.Count() - 1], mPointParcours[index], b);

                //Si le point doit être pris en photo, on calcule également la consommation pour le temps de stabilisation
                if (mPointParcours[index].prisePhoto)
                {
                    consommationStabilisation = mPointParcours[index].TempsStabilisation / b.Autonomie * 100;
                }

                //Si la consommation courante est insuffisante (à la marge près) pour pouvoir parcourir le trajet
                //du point actuel au point suivant, on retourne la valeur d'erreur 101.
                if (b.ChargeCourante - consommationStabilisation - consommationTrajet <= 0.05)
                {
                    consommationBatterie = 101;
                    return consommationBatterie;
                }

                Trajectoire.Points.Add(mPointParcours[index++]);

                b.ChargeCourante = b.ChargeCourante - consommationTrajet - consommationStabilisation;

                consommationBatterie += consommationTrajet + consommationStabilisation;
                consommationTrajet = 0;
                consommationStabilisation = 0;
            }
            return consommationBatterie;
        }

        /// <summary>
        /// Calcule la consommation nécessaire au parcours du drone d'un point vers un autre.
        /// </summary>
        /// <param name="point1">Le point où se trouve actuellement le drone.</param>
        /// <param name="point2">Le point où veut se diriger le drone.</param>
        /// <param name="Batterie">La batterie utilisée.</param>
        /// <returns>La consommation nécessaire au trajet.</returns>
        public double CalculerConsommationTrajet(Point point1, Point point2, Batterie batterie)
        {
            double distance = 0;

            double consommationTrajet = 0;

            //Séquence de calcul pour calculer la distance avec les points GPS reçus
            double e = (3.1415926538 * point1.X / 180);
            double f = (3.1415926538 * point1.Y / 180);
            double g = (3.1415926538 * point2.X / 180);
            double h = (3.1415926538 * point2.Y / 180);
            double i = (Math.Cos(e) * Math.Cos(g) * Math.Cos(f) * Math.Cos(h) + Math.Cos(e) * Math.Sin(f) * Math.Cos(g) * Math.Sin(h) + Math.Sin(e) * Math.Sin(g));
            double j = (Math.Acos(i));
            distance = (6371 * j);

            //Calcul de la consommation en fonction de la distance et de l'autonomie de la batterie
            consommationTrajet = (((distance / 30)*3600)/batterie.Autonomie)*100;

            return consommationTrajet;
        }
    }
}
