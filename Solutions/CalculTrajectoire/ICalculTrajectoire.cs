using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculTrajectoire
{
    // Interface défnissant l'ensemble des méthodes possibles de calcul de trajectoires.
    public interface ICalculTrajectoire
    {
        /// <summary>
        /// Méthode lançant le calcul des différentes trajectoires
        /// </summary>
        /// <returns>La consommation des batteries</returns>
        double CalculerTrajectoires();
    }
}
