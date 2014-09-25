using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using CalculTrajectoire;

namespace InterfaceCore
{
    /// <summary>
    /// Parser d'ecriture et de lecture des données (appareils et drones).
    /// </summary>
    public class DataExtractorFile
    {
        /// <summary>
        /// Pointeur d'écriture de fichier.
        /// </summary>
        protected XmlWriter mWriter;

        /// <summary>
        /// Pointeur en lecture de fichier.
        /// </summary>
        protected XmlReader mReader;

        /// <summary>
        /// Fichier XML dans lequel on va écrire.
        /// </summary>
        string mXmlFile;

        /// <summary>
        /// Schéma XLM
        /// </summary>
        string mXsdFile;

        /// <summary>
        /// Liste de drones enregistrés
        /// </summary>
        public List<Drone> lesDrones=new List<Drone>();

        /// <summary>
        /// Liste d'appareils photos enregistrés
        /// </summary>
        public List<AppareilPhoto> lesAppareils = new List<AppareilPhoto>();

        /// <summary>
        /// Constructeur de l'extracteur de données.
        /// </summary>
        public DataExtractorFile()
        {
            try
            {
                readDrones();

            }
            catch (Exception)
            {
                lesDrones = new List<Drone>();

            }
            try
            {

                readAppareil();
            }
            catch (Exception)
            {

                lesAppareils = new List<AppareilPhoto>();
            }
        }

        /// <summary>
        /// Ecrit un drone afin de l'ajouter aux drones connus.
        /// </summary>
        /// <param name="drone">nouveau drone.</param>
        public void WriteDrone(Drone drone)
        {
            if (!lesDrones.Contains(drone))
            {
                DirectoryInfo dirInfo = Directory.GetParent(Directory.GetCurrentDirectory());
                StringBuilder dirData = new StringBuilder().AppendFormat("{0}\\data\\Drone\\XML\\", dirInfo.FullName);
                mXmlFile = new StringBuilder().AppendFormat("{0}{1}.xml", dirData.ToString(), drone.Nom).ToString();
                mXsdFile = new StringBuilder().AppendFormat("{0}\\data\\Drone\\XSD\\{1}", dirInfo.FullName, XMLTags.XSD_DRONE).ToString();

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                //Création du Writer
                mWriter = XmlWriter.Create(mXmlFile, settings);
                mWriter.WriteStartDocument();
                drone.WriteXml(mWriter);
                mWriter.WriteEndDocument();
                mWriter.Close();
            }
            else
                throw new ArgumentException();
        }

        /// <summary>
        /// Ecrit un appareil afin de l'ajouter aux appareils connus.
        /// </summary>
        /// <param name="appareil">Le nouvel appareil.</param>
        public void WriteAppareilPhoto(AppareilPhoto appareil)
        {
            if (!lesAppareils.Contains(appareil))
            {
                DirectoryInfo dirInfo = Directory.GetParent(Directory.GetCurrentDirectory());
                StringBuilder dirData = new StringBuilder().AppendFormat("{0}\\data\\AppareilPhoto\\XML\\", dirInfo.FullName);
                mXmlFile = new StringBuilder().AppendFormat("{0}\\{1}.xml", dirData.ToString(), appareil.Nom).ToString();
                mXsdFile = new StringBuilder().AppendFormat("{0}\\data\\AppareilPhoto\\XSD\\{1}", dirInfo.FullName, XMLTags.XSD_APP).ToString();

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                //Création du Writer
                mWriter = XmlWriter.Create(mXmlFile, settings);
                mWriter.WriteStartDocument();
                appareil.WriteXml(mWriter);
                mWriter.WriteEndDocument();
                mWriter.Close();
            }
            else
                throw new ArgumentException();
        }

        /// <summary>
        /// Lis tous les drones connus.
        /// </summary>
        public void readDrones()
        {
            DirectoryInfo dirInfo = Directory.GetParent(Directory.GetCurrentDirectory());
            StringBuilder dirData = new StringBuilder().AppendFormat("{0}\\data\\Drone\\XML\\", dirInfo.FullName);
            mXsdFile = new StringBuilder().AppendFormat("{0}\\data\\Drone\\XSD\\{1}", dirInfo.FullName, XMLTags.XSD_DRONE).ToString();
            DirectoryInfo readFile = new DirectoryInfo(dirData.ToString());
            FileInfo[] files = readFile.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                InitReader(files[i].FullName, true);
                Drone dr = new Drone();
                dr.ReadXml(mReader);
                lesDrones.Add(dr);
            }

        }

        /// <summary>
        /// Lis tous les appareils connus.
        /// </summary>
        public void readAppareil()
        {
            DirectoryInfo dirInfo = Directory.GetParent(Directory.GetCurrentDirectory());
            StringBuilder dirData = new StringBuilder().AppendFormat("{0}\\data\\AppareilPhoto\\XML\\", dirInfo.FullName);
            mXsdFile = new StringBuilder().AppendFormat("{0}\\data\\AppareilPhoto\\XSD\\{1}", dirInfo.FullName, XMLTags.XSD_APP).ToString();
            DirectoryInfo readFile = new DirectoryInfo(dirData.ToString());
            FileInfo[] files = readFile.GetFiles();
            

            for (int i = 0; i < files.Length; i++)
            {
                InitReader(files[i].FullName, false);
                AppareilPhoto aP = new AppareilPhoto();
                aP.ReadXml(mReader);
                lesAppareils.Add(aP);
            }
        }
        /// <summary>
        /// Initialise le Reader.
        /// </summary>
        protected void InitReader(string path, bool t)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;

            //schema validation
            settings.ValidationType = ValidationType.Schema;
            if (t)
            {
                settings.Schemas.Add(XMLTags.URI_DRONE, mXsdFile);
            }
            else
            {
                settings.Schemas.Add(XMLTags.URI_APPAREILPHOTO, mXsdFile);
            }
            settings.ValidationEventHandler += ValidationHandler;

            mReader = XmlReader.Create(path, settings);
        }

        /// <summary>
        /// Gestionnaire de validation du schéma
        /// </summary>
        /// <param name="sender">L'objet qui envoie l'évènement</param>
        /// <param name="e">Les arguments de l'évènement</param>
        public void ValidationHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
        {
            if (e.Severity == System.Xml.Schema.XmlSeverityType.Warning)
            {
                Console.Write("WARNING: ");
                Console.WriteLine(e.Message);
            }
            else if (e.Severity == System.Xml.Schema.XmlSeverityType.Error)
            {
                Console.Write("ERROR: ");
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Permet de supprimer un drone du répertoire contenant les XML relatifs aux drones.
        /// </summary>
        /// <param name="nomDrone">Le nom du drone.</param>
        public void supprimerDrone(String nomDrone)
        {
            DirectoryInfo dirInfo = Directory.GetParent(Directory.GetCurrentDirectory());
            StringBuilder dirData = new StringBuilder().AppendFormat("{0}\\data\\Drone\\XML\\", dirInfo.FullName);
            mXmlFile = new StringBuilder().AppendFormat("{0}\\{1}.xml", dirData.ToString(), nomDrone).ToString();

            File.Delete(mXmlFile.ToString());

        }

        /// <summary>
        /// Permet de supprimer un appareil photo du répertoire contenant les XML relatifs aux appareils photos.
        /// </summary>
        /// <param name="nomDrone">Le nom du drone.</param>
        public void supprimerAppareil(String nomAppreil)
        {
            DirectoryInfo dirInfo = Directory.GetParent(Directory.GetCurrentDirectory());
            StringBuilder dirData = new StringBuilder().AppendFormat("{0}\\data\\AppareilPhoto\\XML\\", dirInfo.FullName);
            mXmlFile = new StringBuilder().AppendFormat("{0}\\{1}.xml", dirData.ToString(), nomAppreil).ToString();

            File.Delete(mXmlFile.ToString());
        }

        /// <summary>
        /// Méthode de mise à jour des listes par rapport aux dossiers contenant les différents fichiers XML.
        /// </summary>
        public void miseAJour()
        {
            lesAppareils.Clear();
            lesDrones.Clear();

            readDrones();
            readAppareil();

            InfoManager.getInstance().mesDrones = lesDrones;
            InfoManager.getInstance().mesAppareils = lesAppareils;
        }
    }
}
