using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InterfaceCore;
using System.Diagnostics;

namespace InterfaceWPF
{
    /// <summary>
    /// Interaction logic for Popup.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        InfoManager infoManager;
        public DroneWindow(InfoManager infoManager)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.infoManager = infoManager;
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            float resultPoids;
            string nom = tbNomDrone.Text;

            if (nom == "")
            {
                MessageBox.Show("Vous devez entre le nom du drône", "Données invalides", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (float.TryParse(tbPoids.Text, out resultPoids))
            {
                this.Close();
                infoManager.addDrone(nom, resultPoids);
            }
            else
                MessageBox.Show("Le poids doit être un nombre", "Données invalides", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

