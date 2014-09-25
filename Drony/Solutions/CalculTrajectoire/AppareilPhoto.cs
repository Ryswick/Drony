using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Xml;

namespace CalculTrajectoire
{
    /// <summary>
    /// L'appareil photo utilisé pour le vol.
    /// </summary>
    public class AppareilPhoto : IXmlSerializable
    {
        /// <summary>
        /// Le nom de l'appareil photo.
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
        /// Le poids de l'appareil photo.
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
        /// Le focal de l'appareil photo.
        /// </summary>
        public float Focal
        {
            get
            {
                return mFocal;
            }
        }


        /// <see cref="Focal"/>
        private float mFocal;

        /// <summary>
        /// Constructeur d'appareil photo.
        /// </summary>
        public AppareilPhoto()
            : base()
        {
        }

        /// <summary>
        /// Constructeur d'appareil photo.
        /// </summary>
        /// <param name="Nom">Nom de l'appareil photo</param>
        /// <param name="Poids">Poids de l'appareil photo</param>
        /// <param name="Focal">Focal de l'appareil photo</param>
        public AppareilPhoto(String Nom, float Poids, float Focal) : base()
        {
            this.mNom = Nom;
            this.mPoids = Poids;
            this.mFocal = Focal;
        }


        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement(XMLTags.PREFIX_AP, XMLTags.APPAREIL_PHOTO, XMLTags.URI_APPAREILPHOTO);
            writer.WriteAttributeString("xmlns", XMLTags.PREFIX, null, XMLTags.URI);
            writer.WriteAttributeString(XMLTags.PREFIX_XSI, XMLTags.SCHEMA_LOCATION, XMLTags.URI_XSI, XMLTags.URI + " " + XMLTags.XSD_APP);
            writer.WriteAttributeString("xmlns", XMLTags.PREFIX_AP, null, XMLTags.URI_APPAREILPHOTO);

            writer.WriteElementString(XMLTags.PREFIX_AP, XMLTags.NOM, XMLTags.URI_APPAREILPHOTO, Nom.ToString());

            writer.WriteStartElement(XMLTags.PREFIX_AP, XMLTags.POIDS, XMLTags.URI_APPAREILPHOTO);
            writer.WriteValue(Poids);
            writer.WriteEndElement();

            writer.WriteStartElement(XMLTags.PREFIX_AP, XMLTags.FOCAL, XMLTags.URI_APPAREILPHOTO);
            writer.WriteValue(Focal);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        /// <summary>
        /// Permet de lire un appareil photo dans un fichier xml.
        /// </summary>
        /// <param name="reader">Le flux xml contenant les informations.</param>
        public void ReadXml(XmlReader reader)
        {
            if (reader.Prefix.Equals(XMLTags.PREFIX))
            {
                reader.ReadStartElement(XMLTags.APPAREIL_PHOTO, XMLTags.URI);
            }
            else
            {
                reader.ReadStartElement(XMLTags.APPAREIL_PHOTO, XMLTags.URI_APPAREILPHOTO);
            }
            mNom = reader.ReadElementContentAsString(XMLTags.NOM, XMLTags.URI_APPAREILPHOTO);
            mPoids = reader.ReadElementContentAsFloat(XMLTags.POIDS, XMLTags.URI_APPAREILPHOTO);
            mFocal = reader.ReadElementContentAsFloat(XMLTags.FOCAL, XMLTags.URI_APPAREILPHOTO);
            reader.ReadEndElement();
        }

        /// <summary>
        /// Transforme l'appareil photo en une chaîne de caractères.
        /// </summary>
        /// <returns>Une chaîne de caractère définissant l'appareil photo.</returns>
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Nom : ").Append(mNom).Append("\n");
            s.Append("Poids de l'appareil photo : ").Append(Poids).Append(" kg").AppendLine();
            s.Append("Focal de l'appareil photo : ").Append(Focal).Append(" mm").AppendLine();
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
        /// Vérifie si l'objet est égal à cet appareil photo ou non.
        /// </summary>
        /// <param name="right">L'objet à comparer avec cet appareil photo.</param>
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

            return this.Equals(right as AppareilPhoto);
        }

        /// <summary>
        /// Vérifie si cet appareil photo est égal à l'autre appareil photo.
        /// </summary>
        /// <param name="other">L'appareil photo à comparer</param>
        /// <returns>true si les deux appareils photo sont égaux, false sinon</returns>
        public bool Equals(AppareilPhoto other)
        {
            return (this.mNom.Equals(other.Nom));
        }
    }
}
