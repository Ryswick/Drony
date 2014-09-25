using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculTrajectoire
{
    public class XMLTags
    {
        /// <summary>
        ///URI de parametrage.xsd
        /// </summary>
        public const string URI = @"namespace";

        /// <summary>
        ///URI de drone.xsd
        /// </summary>
        public const string URI_DRONE = @"Drone";

        /// <summary>
        ///URI de appareilPhoto.xsd
        /// </summary>
        public const string URI_APPAREILPHOTO = @"AppareilPhoto";

        /// <summary>
        /// Préfixe pour un xml de paramétrage
        /// </summary>
        public const string PREFIX = "pa";

        /// <summary>
        /// Préfixe xsi
        /// </summary>
        public const string PREFIX_XSI = "xsi";

        /// <summary>
        /// Préfixe pour un xml d'appareil photo
        /// </summary>
        public const string PREFIX_AP = "ap";

        /// <summary>
        /// Préfixe pour un xml de drone
        /// </summary>
        public const string PREFIX_DR = "dr";

        /// <summary>
        /// URI XSI
        /// </summary>
        public const string URI_XSI = @"http://www.w3.org/2001/XMLSchema-instance";

        /// <summary>
        /// Balise pour la location du schéma
        /// </summary>
        public const string SCHEMA_LOCATION = "schemaLocation";

        /// <summary>
        /// Le schéma du fichier XML et le chemin pour le paramétrage
        /// </summary>
        public const string XSD_FILE = "../XSD/parametrage.xsd";

        /// <summary>
        /// Le schéma du fichier XML et le chemin pour le drone
        /// </summary>
        public const string XSD_DRONE = "../XSD/drone.xsd";

        /// <summary>
        /// Le schéma du fichier XML et le chemin pour l'appareil photo
        /// </summary>
        public const string XSD_APP = "../XSD/appareilPhoto.xsd";

        /// <summary>
        /// Le schéma du fichier XML pour le paramétrage
        /// </summary>
        public const string XSD_FILE1 = "parametrage.xsd";

        /// <summary>
        /// Le schéma du fichier XML pour le drone
        /// </summary>
        public const string XSD_DRONE1 = "drone.xsd";

        /// <summary>
        /// Le schéma du fichier XML pour l'appareil photo
        /// </summary>
        public const string XSD_APP1 = "appareilPhoto.xsd";

        /// <summary>
        /// Nom du paramètre "paramétrage".
        /// </summary>
        public const string PARAMETRAGE = "parametrage";

        /// <summary>
        /// Nom du paramètre "batterie".
        /// </summary>
        public const string BATTERIE = "batterie";

        /// <summary>
        /// Nom du paramètre "drone".
        /// </summary>
        public const string DRONE = "drone";

        /// <summary>
        /// Nom du paramètre "poids".
        /// </summary>
        public const string POIDS = "poids";

        /// <summary>
        /// Nom du paramètre "focal".
        /// </summary>
        public const string FOCAL = "focal";

        /// <summary>
        /// Nom du paramètre "nom".
        /// </summary>
        public const string NOM = "nom";
        /// <summary>
        /// Nom du paramètre "autonomie".
        /// </summary>
        public const string AUTONOMIE = "autonomie";

        /// <summary>
        /// Nom du paramètre "appareilPhoto".
        /// </summary>
        public const string APPAREIL_PHOTO = "appareilPhoto";
    }
}
