using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculTrajectoire;

namespace InterfaceCore
{
    public class CVSFormat : Format
    {
        /// <summary>
        /// Exporte la liste de points dans un fichier lisible par le drone.
        /// </summary>
        /// <param name="trajectoire">La trajectoire du drone.</param>
        /// <param name="path">Le chemin où écrire le fichier.</param>
        public override void exporter(List<Point> trajectoire, String path)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                System.Globalization.CultureInfo info = System.Globalization.CultureInfo.InvariantCulture;
                file.WriteLine("[General]");
                file.WriteLine("FileVersion=3");
                file.WriteLine("NumberOfWaypoints=" + (trajectoire.Count));
                int i;
                for (i = 1; i <= trajectoire.Count; i++)
                {
                    file.WriteLine("[Point" + i + "]");
                    file.WriteLine("Latitude=" + trajectoire.ElementAt(i - 1).Y, info);
                    file.WriteLine("Longitude=" + trajectoire.ElementAt(i - 1).X, info);
                    file.WriteLine("Radius=20");
                    file.WriteLine("Altitude=" + trajectoire.ElementAt(i - 1).Z, info);
                    file.WriteLine("ClimbRate=50");
                    file.WriteLine("DelayTime=5");
                    file.WriteLine("WP_Event_Channel_Value=30");
                    file.WriteLine("Heading=0");
                    file.WriteLine("Speed=30");
                    if (trajectoire.ElementAt(i - 1).prisePhoto)
                        file.WriteLine("CAM-Nick=AUTO");
                    else
                        file.WriteLine("CAM-Nick=0");
                    file.WriteLine("Type=1");
                    file.WriteLine("Prefix=P");
                }

            }
        }
    }
}
