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
    /// Logique d'interaction pour ListeMatiere.xaml
    /// </summary>
    public partial class ListeMatiere : Page
    {
        MainWindow mainWindow;
        public ListeMatiere(MainWindow main_)
        {
            InitializeComponent();
            this.mainWindow = main_;
            dg_matiere.ItemsSource = AdoMatiere.getAll();
        }
        private void click_voir_matiere(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;
            Matiere m = AdoMatiere.getOne(Convert.ToInt32(id));
            mainWindow.Content = new RecapMatiere(mainWindow,m);
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new Accueil(mainWindow);
        }
    }
}
