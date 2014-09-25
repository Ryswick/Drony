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

namespace InterfaceWPF
{
    /// <summary>
    /// Interaction logic for UserControlPallier.xaml
    /// </summary>
    public partial class UserControlPallier : UserControl
    {
        public UserControlPallier()
        {
            InitializeComponent();
        }

        /// <summary>
        /// la propriété Id de notre player settings qui set le texte lorsqu'elle est modifiée
        /// </summary>
        public int Id
        {
            get
            {
                return mId;
            }
            internal set
            {
                mId = value;
                tbPallier.Text = "Pallier " + mId.ToString();
            }
        }
        private int mId;

        /// <summary>
        /// une propriété qui permet de lire ou d'écrire un nom dans la textbox de notre player settings
        /// </summary>
        public double Pallier
        {
            get
            {
                return mAltPallier;
            }
            internal set
            {
                mAltPallier = value;
                tbAltitudePallier.Text = mAltPallier.ToString();
            }
        }
        private double mAltPallier;
    }
}
