using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace CalculTrajectoire
{
    public class ParametreParser
    {
        /// <summary>
        /// Le Reader du fichier
        /// </summary>
        public XmlReader mReader;

        /// <summary>
        /// Le fichier XML à parser
        /// </summary>
        string mXmlFile = String.Empty;

        /// <summary>
        /// Le schéma XSD attaché au schéma XML
        /// </summary>
        string mXsdFile = String.Empty;

        /// <summary>
        /// Constructeur de ParametreParser
        /// </summary>
        public ParametreParser(string xmlFile)
        {
            DirectoryInfo dirInfo = Directory.GetParent((Directory.GetCurrentDirectory()));
            string dirXMLData = dirInfo.FullName + "../data/Parametrage/XML/";
            string dirXSDData = dirInfo.FullName + "../data/Parametrage/XSD/";
            mXmlFile = dirXMLData + xmlFile;
            mXsdFile = dirXSDData + XMLTags.XSD_FILE1;

            /*pour vérifier que le fichier est schema valide
            InitReader();
            while (mReader.Read());*/
        }

        /// <summary>
        /// Initialise le reader
        /// </summary>
        public void InitReader()
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;

            //Validation du schéma
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(XMLTags.URI, mXsdFile);
            settings.ValidationEventHandler += ValidationHandler;

            mReader = XmlReader.Create(mXmlFile, settings);
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
    }
}
