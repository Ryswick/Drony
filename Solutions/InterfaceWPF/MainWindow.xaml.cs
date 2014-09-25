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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;
using System.Diagnostics;
using System.IO;
using InterfaceCore;
using Microsoft.Win32;
using System.Windows.Controls.Primitives;

namespace InterfaceWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //La liste de pushpins
        List<Pushpin> pushPinList = new List<Pushpin>();

        //La liste des points de départ (ici on ne peut mettre qu'un seul point de départ)
        List<Pushpin> pushPinDépart = new List<Pushpin>();

        /* Si vous comptez implémenter le calcul avancé, ces lignes pourraient vous être utiles.
         * List<UserControlPallier> listPalliers = new List<UserControlPallier>();*/

        MapLayer mapLayer = new MapLayer();

        //Pour tracer des lignes entres les différents pushpins dans l'ordre.
        MapPolygon newPolygon = new MapPolygon();

        // Instance de infoManager
        InfoManager infoManager = InfoManager.getInstance();

        //Booléen : True si on Drag&Drop, false sinon.
        bool isDragging;

        //Le pushpin utilisé pendant le Drag&Drop.
        Pushpin selectedPushpin;

        //L'altitude choisie par l'utilisateur.
        double altitude;

        //L'altitude du pushpin que l'on Drag&Drop.
        double altitudeSelectedPushpin;

        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;

            cbModeleDrone.ItemsSource = infoManager.mesDronesNotify;
            cbAppareilPhoto.ItemsSource = infoManager.mesAppareilsNotify;

            maMap.Children.Add(mapLayer);
            setPolygon();

            maMap.MouseMove += new MouseEventHandler(maMap_MouseMove);
            maMap.MouseLeftButtonUp += new MouseButtonEventHandler(maMap_MouseLeftButtonUp);
        }

        /// <summary>
        /// Permet d'ajouter un pushpin sur la carte à l'endroit de la souris lors d'un double click.
        /// </summary>
        private void maMap_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Pushpin pin = new Pushpin();
            Point mousePosition = e.GetPosition(this);
            //Converti la position de la souris sur la map en Location avec Lat, Long, Altitude.
            Location location = maMap.ViewportPointToLocation(mousePosition);

            //Permet d'enlever le comportement par défaut du double clic qui est le zoom.
            e.Handled = true;
            pin.Location = location;
            pin.Location.Altitude = altitude;

            if (cbDépart.IsChecked.Value)
            {
                if (pushPinDépart.Count == 0)
                {
                    pin.Background = new SolidColorBrush(Colors.Firebrick);
                    pushPinDépart.Add(pin);
                    newPolygon.Locations.Add(location);
                    mapLayer.Children.Add(pin);
                }
            }
            else if (cbTrajectoire.IsChecked.Value)
            {
                pin.Background = new SolidColorBrush(Colors.Blue);
                if (pushPinDépart.Count == 0)
                    MessageBox.Show("Vous devez avoir un point de départ.", "Présence du point de départ", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                else
                {
                    addPin(pin, location);
                }
            }
            else
            {
                pin.Background = new SolidColorBrush(Colors.Green);
                if (pushPinDépart.Count == 0)
                    MessageBox.Show("Vous devez avoir un point de départ.", "Présence du point de départ", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                else
                {
                    addPin(pin, location);
                }
            }
            pin.MouseRightButtonDown += new MouseButtonEventHandler(pin_MouseRightButtonDown);
            pin.MouseEnter += new MouseEventHandler(pin_MouseEnter);
            pin.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(pin_MouseLeftButtonDown);
        }

        /// <summary>
        /// Méthode permettant d'ajouter un pushpin sur la carte dans la liste de trajectoires et dans le tracé polygone.
        /// </summary>
        /// <param name="pin">Pushpin à ajouter</param>
        /// <param name="location">Location du pushpin</param>
        void addPin(Pushpin pin, Location location)
        {
            pushPinList.Add(pin);
            newPolygon.Locations.Add(location);
            mapLayer.Children.Add(pin);
        }

        /// <summary>
        ///  Méthode permettant de gérer le comportement lorsque l'on bouge la souris et que l'on Drag&Drop un pushpin.
        /// </summary>
        void maMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isDragging && selectedPushpin != null)
            {
                e.Handled = true;
                // Met le pushpin à l'endroit où se situe la souris.
                Point mousePosition = e.GetPosition(maMap);
                //Converti la position de la souris sur la map en Location avec Lat, Long, Altitude.
                Location location = maMap.ViewportPointToLocation(mousePosition);

                selectedPushpin.Location = location;
                selectedPushpin.Location.Altitude = altitudeSelectedPushpin;
            }
        }

        /// <summary>
        /// Méthode permettant d'affecter le pushpin selectionner et d'activer le mode Drag&Drop.(booléen à true)
        /// </summary>
        void pin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            selectedPushpin = sender as Pushpin;
            altitudeSelectedPushpin = selectedPushpin.Location.Altitude;
            if (selectedPushpin != null)
            {
                e.Handled = true;
                this.isDragging = true;
            }
        }

        void maMap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.isDragging)
                this.isDragging = false;
            updateLists();
        }

        /// <summary>
        /// Permet de mettre à jour les listes de pushpins (point de départ et les autres points)
        /// </summary>
        public void updateLists()
        {
            if (pushPinList.Contains(selectedPushpin))
            {
                newPolygon.Locations.Clear();
                int index = pushPinList.IndexOf(selectedPushpin);
                pushPinList[index] = selectedPushpin;
                newPolygon.Locations.Add(pushPinDépart.ElementAt(0).Location);
                foreach (Pushpin p in pushPinList)
                {
                    newPolygon.Locations.Add(p.Location);
                }
            }
            else if (pushPinDépart.Contains(selectedPushpin))
            {
                newPolygon.Locations.Clear();
                int index = pushPinDépart.IndexOf(selectedPushpin);

                pushPinDépart[index] = selectedPushpin;
                newPolygon.Locations.Add(pushPinDépart.ElementAt(0).Location);
                foreach (Pushpin p in pushPinList)
                {
                    newPolygon.Locations.Add(p.Location);
                }
            }
        }

        /// <summary>
        /// Définit le style du tracé entre les différents points.
        /// </summary>
        private void setPolygon()
        {
            newPolygon.Locations = new LocationCollection();
            newPolygon.Fill = new SolidColorBrush(Colors.Transparent);
            newPolygon.Stroke = new SolidColorBrush(Colors.GreenYellow);
            newPolygon.StrokeThickness = 3;
            newPolygon.Opacity = 0.9;
            maMap.Focus();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pin_MouseEnter(object sender, MouseEventArgs e)
        {
            Pushpin pin = sender as Pushpin;
            StringBuilder str = new StringBuilder();
            str.Append("Lat : ").Append(pin.Location.Latitude).AppendLine().
                Append("Long : ").Append(pin.Location.Longitude).AppendLine().
                Append("Alt : ").Append(pin.Location.Altitude).AppendLine();
            if (pin != null)
                ToolTipService.SetToolTip(pin, str.ToString());
        }




        /// <summary>
        /// Permet de supprimer un pushpin et la location qui lui est associée pour tracer le polygone.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pin_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pushpin p = sender as Pushpin;

            MessageBoxResult result = MessageBox.Show("Voulez vous vraiment supprimer ?", "Confirmation de suppression", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (pushPinDépart.Contains(p))
                    pushPinDépart.Clear();
                else
                {
                    pushPinList.Remove(p);
                }
                //On désabonne tous les évènements du pushpin que l'on supprime.
                p.MouseRightButtonDown -= pin_MouseRightButtonDown;
                p.MouseEnter -= pin_MouseEnter;
                p.PreviewMouseLeftButtonDown -= pin_MouseLeftButtonDown;

                newPolygon.Locations.Remove(p.Location);
                mapLayer.Children.Remove(p);
            }

        }


        /// <summary>
        /// Méthode permettant de créer la requête pour géocaliser l'adresse donnée. Puis de centrer la carte sur ce lieu.
        /// </summary>
        /// <param name="adresse">L'adresse à géolocaliser.</param>
        private void makeGeocodeRequest(string adresse)
        {
            try
            {
                // Mettre une clé d'authentification Bing Map avant de faire la requête.
                // Pour créer une clé veuillez vous rendre sur ce site : https://www.bingmapsportal.com/
                // Remplacer la clé existante par la vôtre.
                // Set a valid Bing Map Key before making a geocode Request.
                string key = "Ai5VMSWbT1jGloD6rJoiMgm4TD1OP9zciEjl5Y7cJk_NzXEbB1v1oIjx65GJrPRj";

                GeocodeService.GeocodeRequest geocodeRequest = new GeocodeService.GeocodeRequest();

                // Fixer l'authentification en utilisant une clé Bing Maps valide.
                // Set the credentials using a valid Bing Maps Key
                geocodeRequest.Credentials = new Credentials();
                geocodeRequest.Credentials.ApplicationId = key;

                // Fixer l'adresse dans la requête.
                // Set the full address query
                geocodeRequest.Query = adresse;

                // Définir les options pour qu'elles retournent seulement des résultats précis.
                // Set the options to only return high confidence results (leaving High is adviced).
                GeocodeService.ConfidenceFilter[] filters = new GeocodeService.ConfidenceFilter[1];
                filters[0] = new GeocodeService.ConfidenceFilter();
                filters[0].MinimumConfidence = GeocodeService.Confidence.Medium;

                GeocodeService.GeocodeOptions geocodeOptions = new GeocodeService.GeocodeOptions();
                geocodeOptions.Filters = filters;

                geocodeRequest.Options = geocodeOptions;

                // Exécuter la requête.
                // Make the geocode request
                GeocodeService.GeocodeServiceClient geocodeService =
                new GeocodeService.GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
                GeocodeService.GeocodeResponse geocodeResponse = geocodeService.Geocode(geocodeRequest);

                // Centrer la position de la carte sur la valeur retourné par la requête de géolocalisation.
                // Center the position of the map on that location returned by the geocode request.
                Location mapCenterAdresse = new Location(geocodeResponse.Results.ElementAt(0).Locations.ElementAt(0).Latitude, geocodeResponse.Results.ElementAt(0).Locations.ElementAt(0).Longitude);
                maMap.Center = mapCenterAdresse;
                maMap.ZoomLevel = 18;
            }
            catch (Exception)
            {
                MessageBox.Show("Veuillez saisir une adresse existante (Rue et ville au minimum).", "Error Adresse", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void btnCharger_Click(object sender, RoutedEventArgs e)
        {
            makeGeocodeRequest(tbAdresse.Text);
        }

        private void tbAdresse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && tbAdresse.Text.Trim() != "")
            {
                makeGeocodeRequest(tbAdresse.Text);
            }
        }

        /// <summary>
        /// Méthode permettant de vider la carte, les listes et le tracé, ainsi que tous les pushpins.
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            mapLayer.Children.Clear();
            pushPinDépart.Clear();
            pushPinList.Clear();
            newPolygon.Locations.Clear();
        }



        /// <summary>
        /// Méthode reliant les pushpins avec des lignes, créant ainsi la trajectoire.
        /// </summary>
        private void btnCreerTrajectoire_Click(object sender, RoutedEventArgs e)
        {
            if (newPolygon.Locations.Count >= 2)
            {
                if (mapLayer.Children.Contains(newPolygon))
                    mapLayer.Children.Remove(newPolygon);
                else
                    mapLayer.Children.Add(newPolygon);
            }
        }

        /// <summary>
        /// Créé la liste de trajectoires à exporter dans le format lisible par le drône.
        /// </summary>
        /// <returns>La trajectoire</returns>
        private List<Pushpin> creationListExport()
        {
            List<Pushpin> listExport = new List<Pushpin>(pushPinList);
            if (pushPinList.Count == 0 || pushPinDépart.Count == 0)
            {
                MessageBox.Show("Veuillez poser au moins 2 points : Un point de départ et un point de trajectoire.", "Création impossible", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                return null;
            }
            else
            {
                listExport.Insert(0, pushPinDépart.ElementAt(0));
                listExport.Add(pushPinDépart.ElementAt(0));
                return listExport;
            }
        }

        /// <summary>
        /// Méthode permettant de vérifier si la trajectoire est possible ou non.
        /// </summary>
        private void btnVerificationTrajectoire_Click(object sender, RoutedEventArgs e)
        {
            List<Pushpin> listExport = creationListExport();
            Double consommation = 0;
            StringBuilder str = new StringBuilder("La trajectoire est possible ! Vous consommerez : ");
            int autonomieBatterie;

            if (cbModeleDrone.SelectedIndex == -1 || cbAppareilPhoto.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez sélectionner un modèle de drone et un appareil photo ! ", "Erreur sélection", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(tbAutoBatterie.Text, out autonomieBatterie) || autonomieBatterie < 0)
            {
                MessageBox.Show("Veuillez saisir une autonomie de batterie (> 0) !", "Autonomie batterie obligatoire", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (listExport != null)
            {
                infoManager.générerFichierParametrage(1, autonomieBatterie, ((CalculTrajectoire.Drone)(cbModeleDrone.SelectedItem)), ((CalculTrajectoire.AppareilPhoto)(cbAppareilPhoto.SelectedItem)));
                consommation = infoManager.lancerCalcultrajectoireSimple(listExport);
                if (consommation > 100)
                    MessageBox.Show("La trajectoire est impossible à réaliser avec ce matériel, veuillez modifier vos points ou utiliser un matériel différent.", "Trajectoire Impossible", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    str.Append(Math.Round(consommation, 2)).Append("% de la batterie.\n")
                        .Append("ATTENTION : Ce calcul reste approximatif et ne tient pas compte des facteurs environnementaux.");
                    MessageBox.Show(str.ToString(), "Trajectoire possible", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }


        /// <summary>
        /// Permet d'exporter la trajectoire en format wpl.
        /// </summary>
        private void btnExporterTrajectoire_Click(object sender, RoutedEventArgs e)
        {
            List<Pushpin> listExport = creationListExport();
            if (verificationDossierSortie())
                infoManager.exporter(tbPath.Text, listExport);
        }


        /// <summary>
        /// Méthode permettant de vérifier si l'utilisateur a sélectionné un dossier de sortie.
        /// </summary>
        /// <returns>True si le dossier de sortie à été sélectionné, false sinon.</returns>
        private bool verificationDossierSortie()
        {
            if (tbPath.Text == "")
            {
                MessageBox.Show("Veuillez saisir un dossier de sortie.", "Dossier de sortie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            else
                return true;
        }

        private void sliderAltitude_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.altitude = ((double)(Math.Round(sliderAltitude.Value, 2)));
        }

        /// <summary>
        /// Méthode ouvrant la fêntre pour sélectionner le dossier de sortie.
        /// </summary>
        private void btnParcourir_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.Desktop;

            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                tbPath.Text = dialog.SelectedPath;
            }
        }

        private void ajouterDrone_Click(object sender, RoutedEventArgs e)
        {
            DroneWindow window = new DroneWindow(infoManager);
            window.ShowDialog();
            infoManager.misaAJourListe();
            cbModeleDrone.ItemsSource = infoManager.mesDronesNotify;
        }


        private void ajouterAppareil_Click(object sender, RoutedEventArgs e)
        {
            AppareilsWindow window = new AppareilsWindow(infoManager);
            window.ShowDialog();
            infoManager.misaAJourListe();
            cbAppareilPhoto.ItemsSource = infoManager.mesAppareilsNotify;
        }

        private void supprimerDrone_Click(object sender, RoutedEventArgs e)
        {
            infoManager.supprimerDrone((CalculTrajectoire.Drone)cbModeleDrone.SelectedItem);
            infoManager.misaAJourListe();
            cbModeleDrone.ItemsSource = infoManager.mesDronesNotify;
        }

        private void supprimerAppareil_Click(object sender, RoutedEventArgs e)
        {
            infoManager.supprimerAppreil((CalculTrajectoire.AppareilPhoto)cbAppareilPhoto.SelectedItem);
            infoManager.misaAJourListe();
            cbAppareilPhoto.ItemsSource = infoManager.mesAppareilsNotify;
        }

  
    }

    /*
     * Si vous comptez implémenter le calcul avancé, ces lignes pourraient vous être utiles.
     * private void btnAjouterPalliers_Click(object sender, RoutedEventArgs e)
     {
         //on crée un nouveau player settings
         UserControlPallier pallier = new UserControlPallier();
         //on incrémente l'identifiant pour lui donner la bonne valeur
         pallier.Id = listPalliers.Count + 1;
         pallier.tbAltitudePallier.Name = pallier.tbAltitudePallier.Name + pallier.Id;
         SettingPanel.Children.Add(pallier);
         listPalliers.Add(pallier);
     }*/
}
