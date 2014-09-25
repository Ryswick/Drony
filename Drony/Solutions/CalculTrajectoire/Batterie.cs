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
    /// Le type de batterie utilisé.
    /// </summary>
    public class Batterie : IXmlSerializable
    {
        /// <summary>
        /// L'autonomie de la batterie en secondes.
        /// </summary>
        public double Autonomie
        {
            get
            {
                return mAutonomie;
            }
        }

        /// <see cref="Autonomie"/>
        private double mAutonomie;

        /// <summary>
        /// Charge courante de la batterie en %.
        /// </summary>
        public double ChargeCourante
        {
            get
            {
                return mChargeCourante;
            }
            set
            {
                mChargeCourante = value;
            }
        }

        /// <see cref="ChargeCourante"/>
        private double mChargeCourante;

        /// <summary>
        /// Permet de lire une batterie dans un fichier xml.
        /// </summary>
        /// <param name="reader">Le flux xml contenant les informations.</param>
        public void ReadXml(XmlReader reader)
        {
            reader.ReadStartElement(XMLTags.BATTERIE, XMLTags.URI);
            mAutonomie = reader.ReadElementContentAsDouble(XMLTags.AUTONOMIE, XMLTags.URI);

            mChargeCourante = 100;

            reader.ReadEndElement();
        }

        /// <summary>
        /// Transforme la batterie en une chaîne de caractères.
        /// </summary>
        /// <returns>Une chaîne de caractère définissant la batterie.</returns>
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Charge d'une des batteries : ").Append(Autonomie).AppendLine();
            return s.ToString();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void WriteXml(XmlWriter writer)
        {
        }
    }
}
