using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalculTrajectoire;

namespace InterfaceCore
{
    /// <summary>
    /// Classe relative au format d'export pour le drone
    /// </summary>
    public abstract class Format
    {
        /// <summary>
        /// Méthode permettant de lancer l'export de la trajectoire
        /// </summary>
        /// <param name="trajectoire">La trajectoire à exporter</param>
        /// <param name="path">Le chemin où écrire le fichier.</param>
        public abstract void exporter(List<Point> trajectoire, String path);
    }
}
