using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace InterfaceCore
{
 
        /// <summary>
        /// Coordonnée GPS.
        /// </summary>
        public class Point : IXmlSerializable
        {
            /// <summary>
            /// longitude.
            /// </summary>
            public int posX
            {
                get;
                private set;
            }

            /// <summary>
            /// latitude.
            /// </summary>
            public int posY
            {
                get;
                private set;
            }

            /// <summary>
            /// altitude.
            /// </summary>
            public int posZ
            {
                get;
                private set;
            }

            public Point(int x, int y, int z)
            {
                posX = x;
                posY = y;
                posZ = z;
            }



            public System.Xml.Schema.XmlSchema GetSchema()
            {
                return null;
            }

            public void ReadXml(System.Xml.XmlReader reader)
            {

            }

            public void WriteXml(System.Xml.XmlWriter writer)
            {

            }
        }
   
}