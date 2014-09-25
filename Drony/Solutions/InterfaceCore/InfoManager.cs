using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculTrajectoire;
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace InterfaceCore
{
    /// <summary>
    /// Classe permettant de gérer les informations qui transite entre l'interface graphique et l'application de calcul.
    /// </summary>
    public class InfoManager
    {
        /// <summary>
        /// Retourne la valeur de l'altitude du palier concerné.
        /// </summary>
        /// <param name="indexer">Index du palier</param>
        /// <returns>L'altitude du palier</returns>
        public Double this[int indexer]
        {
            get
            {
                if (lesPaliers != null && indexer >= 0 && indexer < lesPaliers.Count)
                {
                    return lesPaliers.ElementAt(indexer);
                }
                return -1;
            }
        }

        /// <summary>
        /// Les différentes altitudes. (1 palier => 1 altitude).
        /// </summary>
        private List<Double> lesPaliers = null;

        /// <summary>
        /// Instance D'infoManager.
        /// </summary>
        private static InfoManager instance = null;

        /// <summary>
        /// Plan de vol qui sera calculé.
        /// </summary>
        private PlanDeVol mPlan;

        /// <summary>
        /// Points que devra parcourir le drone.
        /// </summary>
        private List<Point> pointDeParcours;

        /// <summary>
        /// Point d'interêt du drone.
        /// </summary>
        public Point PointDInteret
        {
            get;
            set;
        }

     
        /// <summary>
        /// Liste des drones existants.
        /// </summary>
        public List<Drone> mesDrones;

        /// <summary>
        /// Liste de mesDrones mais qu'on peut observer.
        /// </summary>
        public ObservableCollection<Drone> mesDronesNotify;

        /// <summary>
        /// Liste des appareils photos existant.
        /// </summary>
        public List<AppareilPhoto> mesAppareils;

        /// <summary>
        /// Liste des appareils photos existants qu'on peut observer.
        /// </summary>
        public ObservableCollection<AppareilPhoto> mesAppareilsNotify;

        /// <summary>
        /// Parser de base d'informations.
        /// </summary>
        private DataExtractorFile data = new DataExtractorFile();

        /*-------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// Constructeur privé d'infoManager.
        /// </summary>
        private InfoManager()
        {
            mPlan = PlanDeVol.getInstance();
            try
            {
                mesDrones = data.lesDrones;
                mesAppareils = data.lesAppareils;
                mesDronesNotify = new ObservableCollection<Drone>(mesDrones);
                mesAppareilsNotify = new ObservableCollection<AppareilPhoto>(mesAppareils);
            }
            catch { }
            pointDeParcours = new List<Point>();
        }

        /// <summary>
        /// Getter d'instance d'InfoManager.
        /// </summary>
        /// <returns> L'instance d'InfoManager.</returns>
        public static InfoManager getInstance()
        {
            if (instance == null)
                instance = new InfoManager();

            return instance;
        }

        /// <summary>
        /// Ajoute un drone à la liste des drones déjà existants.
        /// </summary>
        /// <param name="drone">Le drone qui sera utilisé.</param>
        public void addDrone(String nom, float poids)
        {
            Drone drone = new Drone(nom, poids);
            try
            {
                data.WriteDrone(drone);
                mesDrones.Add(drone);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Ajoute un appareil photo à la liste des appareils déjà existants.
        /// </summary>
        /// <param name="appareil">Le nouvel appareil photo.</param>
        public void addAppareil(String nom, float poids, float focal)
        {
            AppareilPhoto appareil = new AppareilPhoto(nom, poids, focal);
            try
            {
                data.WriteAppareilPhoto(appareil);
                mesAppareils.Add(appareil);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Ajoute un palier.
        /// </summary>
        /// <param name="altitude">L'altitude du palier.</param>
        public void AddPalier(Double altitude)
        {
            if (lesPaliers == null)
                lesPaliers = new List<Double>();

            lesPaliers.Add(altitude);
        }

        /// <summary>
        /// Supprime un palier.
        /// </summary>
        /// <param name="palier"> L'altitude du palier.</param>
        public void delPalier(int palier)
        {
            for (int i = 0; i < lesPaliers.Count; i++)
            {
                if (lesPaliers.ElementAt(i) == palier)
                    lesPaliers.RemoveAt(i);
            }
        }

        /// <summary>
        /// Génère le fichier de paramétrage.
        /// </summary>
        /// <param name="nbBatterie">Le nombre de batteries.</param>
        /// <param name="autonomie">L'autonomie de la batterie.</param>
        /// <param name="modelDrone">Le modèle du drone.</param>
        /// <param name="modelAppareil">Le modèle de l'appareil.</param>
        public void générerFichierParametrage(int nbBatterie, int autonomie, Drone modelDrone, AppareilPhoto modelAppareil)
        {
            if (mesDrones.Contains(modelDrone) && mesAppareils.Contains(modelAppareil) && autonomie > 0 && nbBatterie > 0)
            {
                ParamFileCreator paramFile = new ParamFileCreator(modelDrone, modelAppareil, nbBatterie, autonomie);
                paramFile.EcritureFichierParam("param.xml");

                mPlan.chargerParametre("param.xml");
            }
        }

        /// <summary>
        /// Méthode qui va lancer la version simple du calcul de trajectoire.
        /// </summary>
        public Double lancerCalcultrajectoireSimple(List<Pushpin> list)
        {
            mPlan.calculSimple(Convertir(list));

            return mPlan.TypeCalcul.CalculerTrajectoires();
        }

        /// <summary>
        /// Exporte le résultat du calcul de trajectoires dans un format de fichier lisible par le drone.
        /// </summary>
        public void exporter(string folderPath, List<Pushpin> trajectoire)
        {
            pointDeParcours = Convertir(trajectoire);
            Export exp = new FichierExport(new CVSFormat());
            StringBuilder path = new StringBuilder(folderPath);
            path.Append("\\").Append("Trajectoire").Append(DateTime.Now.Day).Append("-").Append(DateTime.Now.Month).Append("-").Append(DateTime.Now.Year).Append(".wpl");
            exp.exporter(pointDeParcours, path.ToString());
        }

        /// <summary>
        /// Convertit une liste de pushpins en liste de points.
        /// </summary>
        /// <param name="traj">Trajectoire à convertir.</param>
        /// <returns>La liste de points.</returns>
        private List<Point> Convertir(List<Pushpin> traj)
        {
            SolidColorBrush green = new SolidColorBrush(Colors.Green);
            List<Point> points = new List<Point>();
            foreach (Pushpin p in traj)
            {
                if (p.Background.ToString() == green.ToString())
                    points.Add(new Point(p.Location.Longitude, p.Location.Latitude, p.Location.Altitude, 5));
                else
                    points.Add(new Point(p.Location.Longitude, p.Location.Latitude, p.Location.Altitude));
            }
            return points;
        }

        /// <summary>
        /// Convertit la liste de points en pushpins.
        /// </summary>
        /// <param name="points">La liste de points.</param>
        /// <returns>La liste de pushpins.</returns>
        private List<Pushpin> ConvertBack(List<Point> points)
        {
            List<Pushpin> list = new List<Pushpin>();

            foreach (Point p in points)
            {
                Pushpin pAdd = new Pushpin();
                pAdd.Location.Altitude = p.Z;
                pAdd.Location.Longitude = p.X;
                pAdd.Location.Latitude = p.Y;

                if (p.prisePhoto)
                    pAdd.Background = new SolidColorBrush(Colors.Green);
                else
                    pAdd.Background = new SolidColorBrush(Colors.Blue);
            }

            return list;
        }

        /// <summary>
        /// Méthode permettant de mettre à jour la liste.
        /// </summary>
        public void misaAJourListe()
        {
            data.miseAJour();
            mesDronesNotify = new ObservableCollection<Drone>(mesDrones);
            mesAppareilsNotify = new ObservableCollection<AppareilPhoto>(mesAppareils);
        }

        /// <summary>
        /// Méthode permettant de supprimer un drone.
        /// </summary>
        /// <param name="drone">Le drone à supprimer</param>
        public void supprimerDrone(Drone drone)
        {    
            data.supprimerDrone(drone.Nom);
            data.miseAJour();
        }

        /// <summary>
        /// Méthode permettant de supprimer un appareil photo.
        /// </summary>
        /// <param name="appareilPhoto">L'appareil photo à supprimer.</param>
        public void supprimerAppreil(AppareilPhoto appareilPhoto)
        {
            data.supprimerAppareil(appareilPhoto.Nom);
            data.miseAJour();
        }
    }
}
