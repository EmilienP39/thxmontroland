using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using txhmontroland.ADO;
using txhmontroland.Classes;
using txhmontroland.Pages;

namespace txhmontroland.Pages
{
    /// <summary>
    /// Logique d'interaction pour RecapConcours.xaml
    /// </summary>
    public partial class RecapConcours : Page
    {
        private MainWindow _mainWindow;
        CodeConcour codeConcour;
        public RecapConcours(MainWindow mainWindow, CodeConcour c)
        {
            InitializeComponent();
            codeConcour = c;
            this._mainWindow = mainWindow;
            tbx_code_matiere.Text = c.codeconcour;
            tbx_nom.Text = c.nom;
            Color color1 = (Color)ColorConverter.ConvertFromString(c.couleur);
            clrPicker_concours.SelectedColor = color1;
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = new ListeConcours(_mainWindow);
        }

        private void btn_creer_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_code_matiere.Text != null && tbx_nom.Text != null)
            {
                CodeConcour c = new(tbx_code_matiere.Text, tbx_nom.Text);
                if (clrPicker_concours.SelectedColor != null)
                {
                    c.couleur = clrPicker_concours.SelectedColor.ToString();
                }
                else
                {
                    c.couleur = "#FFFFFF";
                }
                AdoConcours.ModifierConcours(c);
            }
            else
            {
                MessageBox.Show("Veuillez remplir tout les champs texte","Question" ,MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_suppr_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez vous vraiment supprimer ce concours ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {

            }
            else
            {
                AdoConcours.SupprimerConcours(codeConcour);
                _mainWindow.Content = new ListeConcours(_mainWindow);
            }
        }
    }
}
