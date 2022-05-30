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
    /// Logique d'interaction pour CreerMatiere.xaml
    /// </summary>
    public partial class CreerMatiere : Page
    {
        MainWindow mainWindow;
        public CreerMatiere(MainWindow mainWindow_)
        {
            InitializeComponent();
            this.mainWindow = mainWindow_;
            lsbx_codeconcour.ItemsSource = AdoConcours.getAllLight();
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new Accueil(mainWindow);
        }

        private void btn_creer_Click(object sender, RoutedEventArgs e)
        {
            if(tbx_nom.Text != null && tbx_code_matiere.Text != null)
            {
                List<CodeConcour> codeConcours = new List<CodeConcour>();
                foreach (CodeConcour cc in lsbx_codeconcour.SelectedItems)
                {
                    codeConcours.Add(cc);
                }
                Matiere m = new(tbx_nom.Text,tbx_code_matiere.Text,codeConcours);
                AdoMatiere.CreerMatiere(m);
            }
        }
    }
}
