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

namespace txhmontroland.Pages
{
    /// <summary>
    /// Logique d'interaction pour Accueil.xaml
    /// </summary>
    public partial class Accueil : Page
    {
        MainWindow mainWindow_;
        public Accueil(MainWindow main)
        {
            this.mainWindow_ = main;
            InitializeComponent();
        }

        private void btn_add_enseignant_Click(object sender, RoutedEventArgs e)
        {
            mainWindow_.Content = new CreerProf(mainWindow_);
        }

        private void btn_trm_Click(object sender, RoutedEventArgs e)
        {
            mainWindow_.Content = new TRM(mainWindow_);
        }

        private void btn_add_classe_Click(object sender, RoutedEventArgs e)
        {
            mainWindow_.Content = new CreerClasse(mainWindow_);
        }

        private void btn_liste_enseignant_Click(object sender, RoutedEventArgs e)
        {
            mainWindow_.Content = new ListeEnseignant(mainWindow_);
        }

        private void btn_add_matiere_Click(object sender, RoutedEventArgs e)
        {
           mainWindow_.Content = new CreerMatiere(mainWindow_);
        }

        private void btn_liste_classe_Click(object sender, RoutedEventArgs e)
        {
            mainWindow_.Content = new ListeClasse(mainWindow_);
        }

        private void btn_matiere_liste_Click(object sender, RoutedEventArgs e)
        {
            mainWindow_.Content = new ListeMatiere(mainWindow_);
        }

        private void btn_concours_liste_Click(object sender, RoutedEventArgs e)
        {
            mainWindow_.Content = new ListeConcours(mainWindow_);
        }

        private void btn_add_concours_Click(object sender, RoutedEventArgs e)
        {
            mainWindow_.Content = new CreerConcours(mainWindow_);
        }

        private void btn_easteregg_Click(object sender, RoutedEventArgs e)
        {
            mainWindow_.Content = new rick(mainWindow_);
        }
    }
}
