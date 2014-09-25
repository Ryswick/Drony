using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CalculTrajectoire;

namespace InterfaceCore
{
    public class FichierExport : Export
    {
        /// <summary>
        /// Constructeur de FichierExport
        /// </summary>
        /// <param name="format">Le format d'export</param>
        public FichierExport(Format format)
            : base(format)
        {
        }

        /// <summary>
        /// Méthode permettant de lancer l'export de la trajectoire
        /// </summary>
        /// <param name="trajectoire">La trajectoire à exporter</param>
        /// <param name="path">Le chemin où écrire le fichier.</param>
        public override void exporter(List<Point> trajectoire, String path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            
            mFormat.exporter(trajectoire, path);
        }
    }
}
