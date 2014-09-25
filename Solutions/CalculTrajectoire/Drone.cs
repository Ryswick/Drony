using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace CalculTrajectoire
{
    /// <summary>
    /// Le drone utilisé pour le vol.
    /// </summary>
    public class Drone : IXmlSerializable
    {
        /// <summary>
        /// Le nom du drone.
        /// </summary>
        public String Nom
        {
            get
            {
                return mNom;
            }
        }

        /// <see cref="Nom"/>
        private String mNom;

        /// <summary>
        /// Le poids du drone.
        /// </summary>
        public float Poids
        {
            get
            {
                return mPoids;
            }
        }

        /// <see cref="Poids"/>
        private float mPoids;


        /// <summary>
        /// La batterie utilisée par le drone.
        /// </summary>
        public List<Batterie> Batteries
        {
            get
            {
                return mBatteries;
            }
        }

        /// <see cref="Batteries"/>
        private List<Batterie> mBatteries;

        /// <summary>
        /// L'appareil photo utilisé par le drone.
        /// </summary>
        public AppareilPhoto AppareilPhoto
        {
            get
            {
                return mAppareilPhoto;
            }
        }

        /// <see cref="AppareilPhoto"/>
        private AppareilPhoto mAppareilPhoto;


        /// <summary>
        /// Constructeur de Drone.
        /// </summary>
        public Drone() : base()
        {
            mBatteries = new List<Batterie>();
            mAppareilPhoto = new AppareilPhoto();
        }

        /// <summary>
        /// Constructeur de Drone.
        /// </summary>
        /// <param name="Nom">Le nom du drone</param>
        /// <param name="Poids">Le poids du drone</param>
        public Drone(String Nom, float Poids) : base()
        {
            mBatteries = new List<Batterie>();
            mAppareilPhoto = new AppareilPhoto();
            this.mNom = Nom;
            this.mPoids = Poids;
        }                                                                     

        /// <summary>
        /// Permet d'écrire les informations relatives à un drone dans un fichier xml.
        /// </summary>
        /// <param name="writer">Le flux xml où l'on va écrire les informations.</param>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(XMLTags.PREFIX_DR, XMLTags.DRONE, XMLTags.URI_DRONE);
            writer.WriteAttributeString("xmlns", XMLTags.PREFIX, null, XMLTags.URI);
            writer.WriteAttributeString(XMLTags.PREFIX_XSI, XMLTags.SCHEMA_LOCATION, XMLTags.URI_XSI, XMLTags.URI + " " + XMLTags.XSD_DRONE);
            writer.WriteAttributeString("xmlns", XMLTags.PREFIX_DR, null, XMLTags.URI_DRONE);
            
            writer.WriteElementString(XMLTags.PREFIX_DR, XMLTags.NOM, XMLTags.URI_DRONE, Nom.ToString());

            writer.WriteStartElement(XMLTags.PREFIX_DR, XMLTags.POIDS, XMLTags.URI_DRONE);
            writer.WriteValue(Poids);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        /// <summary>
        /// Permet de lire les informations relatives à un drone dans un fichier xml.
        /// </summary>
        /// <param name="reader">Le flux xml contenant les informations.</param>
        public void ReadXml(XmlReader reader)
        {
            if (reader.Prefix.Equals(XMLTags.PREFIX))
            {
                reader.ReadStartElement(XMLTags.DRONE, XMLTags.URI);
            }
            else
            {
                reader.ReadStartElement(XMLTags.DRONE, XMLTags.URI_DRONE);
            }
            mNom = reader.ReadElementContentAsString(XMLTags.NOM, XMLTags.URI_DRONE);
            mPoids = reader.ReadElementContentAsFloat(XMLTags.POIDS, XMLTags.URI_DRONE);
            reader.ReadEndElement();
        }

        /// <summary>
        /// Transforme les informations du drone en une chaîne de caractères.
        /// </summary>
        /// <returns>Un string décrivant le drone.</returns>
        public override String ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Nom : ").Append(mNom).Append("\n");
            s.Append("Poids du drône : ").Append(Poids).Append(" kg").AppendLine();
            return s.ToString();
        }
        

        public XmlSchema GetSchema()
        {
            return null;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Vérifie si l'objet est égal à ce drone ou non.
        /// </summary>
        /// <param name="right">L'objet à comparer avec ce drone.</param>
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

            return this.Equals(right as Drone);
        }

        /// <summary>
        /// Vérifie si ce drone est égal à l'autre drone.
        /// </summary>
        /// <param name="other">Le drone à comparer</param>
        /// <returns>true si les deux drones sont égaux, false sinon</returns>
        public bool Equals(Drone other)
        {
            return (this.mNom.Equals(other.Nom));
        }
    }
}