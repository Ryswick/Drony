using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculTrajectoire
{
    /// <summary>
    /// Le plan de vol
    /// </summary>
    public class PlanDeVol
    {
        /// <summary>
        /// Instance du singleton PlanDeVol.
        /// </summary>
        private static PlanDeVol Instance;

        /// <summary>
        /// Le drone qui suivra le plan de vol
        /// </summary>
        public Drone Drone
        {
            get
            {
                return mDrone;
            }
        }

        /// <see cref="Drone"/>
        private Drone mDrone;

        /// <summary>
        /// Le type de calcul de trajectoires
        /// </summary>
        public ICalculTrajectoire TypeCalcul
        {
            get
            {
                return mTypeCalcul;
            }
        }

        /// <see cref="TypeCalcul"/>
        private ICalculTrajectoire mTypeCalcul;

        /// <summary>
        /// Constructeur privé du Singleton PlanDeVol.
        /// </summary>
        private PlanDeVol()
        {
        }

        /// <summary>
        /// Permet d'obtenir une instance du Singleton PlanDeVol
        /// </summary>
        /// <returns></returns>
        public static PlanDeVol getInstance()
        {
            if (Instance == null)
            {
                Instance = new PlanDeVol();
            }
            return Instance;
        }

        /// <summary>
        /// Permet de charger les différents paramètres de chacun des composants du drône (Drône, Appareil Photo, Batteries).
        /// </summary>
        /// <param name="XmlFile">Le flux xml contenant les informations.</param>
        public void chargerParametre(String XmlFile)
        {
            ParametreParser parser = new ParametreParser(XmlFile);
            mDrone = new Drone();
            parser.InitReader();
            parser.mReader.ReadStartElement(XMLTags.PARAMETRAGE, XMLTags.URI);
            Drone.ReadXml(parser.mReader);
            Drone.AppareilPhoto.ReadXml(parser.mReader);
            while (parser.mReader.LocalName != XMLTags.PARAMETRAGE)
            {
                Batterie bat = new Batterie();
                bat.ReadXml(parser.mReader);
                Drone.Batteries.Add(bat);
            }
            parser.mReader.ReadEndElement();
            parser.mReader.Close();
        }

        /// <summary>
        /// Méthode permettant d'instancier le type de calcul comme un calcul simple
        /// </summary>
        /// <param name="pointsParcours">Les points qui servent de trajectoire au drone</param>
        public void calculSimple(List<Point> PointsParcours)
        {
            mTypeCalcul = new CalculSimple(PointsParcours);
        }
    }
}
