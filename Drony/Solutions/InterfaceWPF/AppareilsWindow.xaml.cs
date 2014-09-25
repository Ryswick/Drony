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

namespace InterfaceWPF
{
    /// <summary>
    /// Interaction logic for AppareilsWindow.xaml
    /// </summary>
    public partial class AppareilsWindow : Window
    {
        InfoManager infoManager;
        public AppareilsWindow(InfoManager infoManager)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.infoManager = infoManager;
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            float resultPoids;
            float resultFocal;
            string nom = tbNomDrone.Text;
            if (nom != "")
            {
                if (float.TryParse(tbPoids.Text, out resultPoids))
                {
                    if (float.TryParse(tbFocal.Text, out resultFocal))
                    {
                        this.Close();
                        infoManager.addAppareil(nom, resultPoids, resultFocal);
                    }
                    else
                        MessageBox.Show("La focal doit être un nombre", "Données invalides", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("Le poids doit être un nombre", "Données invalides", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Vous devez entrer le nom de l'appareil photo", "Données invalides", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
