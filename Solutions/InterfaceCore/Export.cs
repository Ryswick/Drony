using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculTrajectoire;

namespace InterfaceCore
{
    public abstract class Export
    {
        /// <summary>
        /// Format d'export.
        /// </summary>
        protected Format mFormat;

        /// <summary>
        /// Constructeur complet d'Export
        /// </summary>
        /// <param name="format">Le format d'export</param>
        public Export(Format format)
        {
            mFormat = format;
        }

        /// <summary>
        /// Méthode d'exportation des données.
        /// </summary>
        /// <param name="points">La trajectoire à exporter.</param>
        public abstract void exporter(List<Point> trajectoire, String path);
    }
}
