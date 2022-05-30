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
using txhmontroland.Classes;
using txhmontroland.ADO;
using txhmontroland.Pages;

namespace txhmontroland.Pages
{
    /// <summary>
    /// Logique d'interaction pour ListeEnseignant.xaml
    /// </summary>
    public partial class ListeEnseignant : Page
    {
        MainWindow mainWindow; 
        public ListeEnseignant(MainWindow _mainWindow)
        {
            mainWindow = _mainWindow;
            InitializeComponent();
            List<Enseignant> enseignants = AdoEnseignant.getAllLight();
            dg_joueurs.Items.Clear(); //dg_joueurs = tableau enseignant c'est un copié collé flemme de changer
            dg_joueurs.ItemsSource = enseignants;
        }
        private void voir_enseignant(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;
            Enseignant en = AdoEnseignant.getOneLight(Convert.ToInt32(id));
            mainWindow.Content = new ProfRecap(mainWindow,en);
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new Accueil(mainWindow);
        }
    }
}
