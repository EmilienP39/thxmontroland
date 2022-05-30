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
using System.Globalization;

namespace txhmontroland.Pages
{
    /// <summary>
    /// Logique d'interaction pour ListeClasse.xaml
    /// </summary>
    public partial class ListeClasse : Page
    {
        MainWindow mainWindow;
        double totalDote;
        List<double> dotations;
        public ListeClasse(MainWindow mainWindow_)
        {
            InitializeComponent();
            this.mainWindow = mainWindow_;
            List<Classe> classes = AdoClasse.getAllAvecVH();
            Classe classe = new Classe();
            classe.nom_classe = "TOTAL :";
            foreach (Classe c in classes)
            {
                classe.division += c.division;
                classe.nombre_eleve += c.nombre_eleve;
                classe.afficherTotalHeure += c.afficherTotalHeure;
            }
            classes.Add(classe);
            dg_classes.ItemsSource = classes;

            List<double> dotations = AdoTravailler.getDotationPricipale();
            this.dotations = dotations;

            tbx_dota_principale.Text = Convert.ToString(dotations[0]);
            tbx_DDFPT.Text = Convert.ToString(dotations[1]);
            tbx_DOC.Text = Convert.ToString(dotations[2]);

            CodeConcour total = MainWindow.codeConcours.Find(x => x.nom == "TOTAL :");
            double totalDote = Convert.ToDouble(total.totalVH) + total.ajustementPonde + dotations[1] + dotations[2] + total.complementPoste + total.AS;
            double reste = dotations[0] - totalDote;
            tbx_total.Text = Convert.ToString(Math.Round(totalDote, 2));
            tbx_reste.Text = Convert.ToString(Math.Round(reste,2));
            this.totalDote = totalDote;
            if(totalDote != dotations[0])
            {
                tbx_total.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                tbx_total.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;
            mainWindow.Content = new ClasseRecap(mainWindow, AdoClasse.getOne(Convert.ToInt32(id)));
        }

        private void tbx_dota_principale_LostFocus(object sender, RoutedEventArgs e)
        {
            AdoTravailler.modifierDotationPrincipale(Convert.ToDouble(tbx_dota_principale.Text, new NumberFormatInfo() { NumberDecimalSeparator = "," }));
            if (totalDote != dotations[0])
            {
                tbx_total.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                tbx_total.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void tbx_DDFPT_LostFocus(object sender, RoutedEventArgs e)
        {
            AdoTravailler.modifierDDFPT(Convert.ToDouble(tbx_DDFPT.Text, new NumberFormatInfo() { NumberDecimalSeparator = "," }));
            if (totalDote != dotations[0])
            {
                tbx_total.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                tbx_total.BorderBrush = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void tbx_DOC_LostFocus(object sender, RoutedEventArgs e)
        {
            double DOC = Convert.ToDouble(tbx_DOC.Text);
            AdoTravailler.modifierDOC(DOC);
            if (totalDote != dotations[0])
            {
                tbx_total.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else
            {
                tbx_total.BorderBrush = new SolidColorBrush(Colors.LightGray);
            } 
        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new ListeClasse(mainWindow);
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new Accueil(mainWindow);
        }
    }
}
