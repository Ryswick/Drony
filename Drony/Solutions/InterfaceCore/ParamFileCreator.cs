using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CalculTrajectoire;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace InterfaceCore 
{
    /// <summary>
    /// Classe permettant de générer le document de paramétrage.
    /// </summary>
    public class ParamFileCreator
    {
        /// <summary>
        /// Pointeur d'écriture de fichier.
        /// </summary>
        protected XmlWriter mWriter;
        
        /// <summary>
        /// Fichier XML dans lequel on va écrire.
        /// </summary>
        string mXmlFile;

        /// <summary>
        /// Le schéma XLM.
        /// </summary>
        string mXsdFile;

        /// <summary>
        /// Drone utilisé pour le calcul de trajectoire.
        /// </summary>
        public Drone drone
        {
            get;
            private set;
        }

        /// <summary>
        /// Appareil photo utilisé pour le calcul de la trajectoire.
        /// </summary>
        public AppareilPhoto appareil
        {
            get;
            private set;
        }

        /// <summary>
        /// Nombre de batteries utilisé pour le calcul de trajectoire.
        /// </summary>
        public int nbBatterie
        {
            get;
            private set;
        }

        /// <summary>
        /// Autonomie des batteries.
        /// </summary>
        public int autonomieBatterie
        {
            get;
            private set;
        }

        /// <summary>
        /// Constructeur du générateur de fichier de paramétrage.
        /// </summary>
        /// <param name="drone">Le libellé du modèle de drone.</param>
        /// <param name="appareil">Le libellé du modèle de l'appareil photo.</param>
        /// <param name="nbBat">Le nombre de batteries.</param>
        /// <param name="autonomieBat">L'autonomie des batteries.</param>
        public ParamFileCreator(Drone drone, AppareilPhoto appareil, int nbBat, int autonomieBat)
        {
            this.drone = drone;
            this.appareil = appareil;
            nbBatterie = nbBat;
            autonomieBatterie = autonomieBat;
        }

        /// <summary>
        /// Méthode qui va générer le fichier de paramétrage.
        /// </summary>
        /// <param name="nomFichier"></param>
        public void EcritureFichierParam(string nomFichier)
        {
            DirectoryInfo dirInfo = Directory.GetParent(Directory.GetCurrentDirectory());
            StringBuilder dirData = new StringBuilder().AppendFormat("{0}\\data\\Parametrage\\XML\\", dirInfo.FullName);
            mXmlFile = new StringBuilder().AppendFormat("{0}{1}", dirData.ToString(), nomFichier).ToString();
            mXsdFile = new StringBuilder().AppendFormat("{0}{1}", dirData.ToString(), XMLTags.XSD_FILE).ToString();

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            //Création du writer
            mWriter = XmlWriter.Create(mXmlFile, settings);

            mWriter.WriteStartElement(XMLTags.PREFIX, XMLTags.PARAMETRAGE, XMLTags.URI);

            //Ajoute un attribut "schemaLocation" avec sa valeur
            mWriter.WriteAttributeString(XMLTags.PREFIX_XSI, XMLTags.SCHEMA_LOCATION, XMLTags.URI_XSI, XMLTags.URI + " " + XMLTags.XSD_FILE);
            mWriter.WriteAttributeString("xmlns", XMLTags.PREFIX_DR, null, XMLTags.URI_DRONE);
            mWriter.WriteAttributeString("xmlns", XMLTags.PREFIX_AP, null, XMLTags.URI_APPAREILPHOTO);


            drone.WriteXml(mWriter);
            appareil.WriteXml(mWriter);

            for (int i = 0; i < nbBatterie; i++)
            {
                mWriter.WriteStartElement(XMLTags.PREFIX, XMLTags.BATTERIE, XMLTags.URI);

                mWriter.WriteStartElement(XMLTags.PREFIX, XMLTags.AUTONOMIE, XMLTags.URI);
                mWriter.WriteValue(autonomieBatterie);
                mWriter.WriteEndElement();

                mWriter.WriteEndElement();
            }

            mWriter.WriteEndElement();
            mWriter.Close();
        }

    }
}
