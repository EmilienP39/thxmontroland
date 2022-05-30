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
    /// Logique d'interaction pour RecapMatiere.xaml
    /// </summary>
    public partial class RecapMatiere : Page
    {
        Matiere matiere;
        MainWindow mainWindow;
        List<CodeConcour> cc;
        List<CodeConcour> ccm;
        public RecapMatiere(MainWindow main, Matiere m)
        {
            InitializeComponent();
            this.mainWindow = main;
            tbx_nom.Text = m.nom_matiere;
            tbx_code.Text = m.sigle_matiere;

            btn_creer.Visibility = Visibility.Collapsed;
            btn_annuler_add_concour.Visibility = Visibility.Collapsed; 

            List<CodeConcour> codeConcours = AdoConcours.getAllLight();
            List<CodeConcour> codeConcoursMatiere = AdoConcours.GetOneFromMatiere(m);
            cc = codeConcours;
            ccm = codeConcoursMatiere;

            if (m.codeConcours == null)
            {
                m.codeConcours = codeConcoursMatiere;
            }
            else
            {
                m.codeConcours.Clear();
                m.codeConcours = codeConcoursMatiere;
            }

            lsbx_codeconcour.ItemsSource = codeConcoursMatiere;
            foreach (CodeConcour codeConcour1 in m.codeConcours)
            {
                if (codeConcoursMatiere.Find(x => x.id == codeConcour1.id) != null)
                {
                    lsbx_codeconcour.SelectedItems.Add(codeConcoursMatiere.Find(x => x.id == codeConcour1.id));
                }
            }
            matiere = m;
        }

        private void btn_suppr_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez vous vraiment supprimer cette matière ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                
            }
            else
            {
                AdoMatiere.SupprimerMatiere(matiere);
                mainWindow.Content = new ListeMatiere(mainWindow);
            }
        }

        private void btn_creer_Click(object sender, RoutedEventArgs e)
        {
            List<CodeConcour> ccE = new List<CodeConcour>();
            matiere.codeConcours.Clear();
            foreach (CodeConcour cc in lsbx_codeconcour.SelectedItems)
            {
                ccE.Add(cc);
            }
            Matiere m = new(matiere.id, tbx_nom.Text, tbx_code.Text, ccE);
            AdoMatiere.ModifierMatiere(m);
            mainWindow.Content = new ListeMatiere(mainWindow);
        } 
        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new ListeMatiere(mainWindow);
        }

        private void lsbx_codeconcour_GotFocus(object sender, RoutedEventArgs e)
        {
            btn_creer.Visibility = Visibility.Visible;
        }

        private void tbx_nom_GotFocus(object sender, RoutedEventArgs e)
        {
            btn_creer.Visibility = Visibility.Visible;
        }

        private void tbx_code_GotFocus(object sender, RoutedEventArgs e)
        {
            btn_creer.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lsbx_codeconcour.ItemsSource = cc;
            foreach (CodeConcour codeConcour1 in matiere.codeConcours)
            {
                if (cc.Find(x => x.id == codeConcour1.id) != null)
                {
                    lsbx_codeconcour.SelectedItems.Add(cc.Find(x => x.id == codeConcour1.id));
                }
            }
            btn_add_cc.Visibility = Visibility.Collapsed;
            btn_annuler_add_concour.Visibility = Visibility.Visible;
        }

        private void btn_annuler_add_concour_Click(object sender, RoutedEventArgs e)
        {
            lsbx_codeconcour.ItemsSource = ccm;
            foreach (CodeConcour codeConcour1 in matiere.codeConcours)
            {
                if (ccm.Find(x => x.id == codeConcour1.id) != null)
                {
                    lsbx_codeconcour.SelectedItems.Add(ccm.Find(x => x.id == codeConcour1.id));
                }
            }
            btn_add_cc.Visibility = Visibility.Visible;
            btn_annuler_add_concour.Visibility = Visibility.Collapsed;
        }
    }
}
