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
using MahApps.Metro;
using txhmontroland.Classes;
using txhmontroland.ADO;
using txhmontroland.Pages;

namespace txhmontroland.Pages
{
    /// <summary>
    /// Logique d'interaction pour ProfRecap.xaml
    /// </summary>
    public partial class ProfRecap : Page
    {
        MainWindow mainWindow;
        Enseignant enseignant = new Enseignant();
        public ProfRecap(MainWindow mainWindow_,Enseignant e)
        {
            InitializeComponent();

            btn_creer.Visibility = Visibility.Collapsed;

            List<CodeConcour> codeConcours = AdoConcours.getAllLight();
            List<CodeConcour> codeConcoursEnseignant = new List<CodeConcour>();

            codeConcoursEnseignant.Clear();
            codeConcoursEnseignant = AdoConcours.GetFromEnseignant(e);

            tbx_prenom.Text = e.prénom;
            tbx_nom.Text = e.nom;
            tbx_ors.Text = e.afficherORS;

            switch (e.contract)
            {
                case "Certifié":
                    btn_contract_certif.IsChecked = true;
                    break;

                case "Agrégé":
                    btn_contract_aggreg.IsChecked = true;
                    break;

                case "MA":
                    btn_contract_ma.IsChecked = true;
                    break;

                case "PLP":
                    btn_contract_PLP.IsChecked = true;
                    break;
            }

            enseignant = e;
            if(e.CodesConcours == null)
            {
                e.CodesConcours = codeConcoursEnseignant;
            }
            else
            {
                e.CodesConcours.Clear();
                e.CodesConcours = codeConcoursEnseignant;
            }
                      
            mainWindow = mainWindow_;

            tbx_nom.Text = e.nom;
            tbx_prenom.Text = e.prénom;

            lsbx_codeconcour.ItemsSource = codeConcours;
            foreach(CodeConcour codeConcour1 in e.CodesConcours)
            {
                if(codeConcours.Find(x => x.id == codeConcour1.id) != null)
                {
                    lsbx_codeconcour.SelectedItems.Add(codeConcours.Find(x => x.id == codeConcour1.id));
                }
            }
        }
        private void btn_creer_Click(object sender, RoutedEventArgs e) //C'est du copier coller en vrai c'est bouton modifier mais flemme
        {
            List<CodeConcour> ccE = new List<CodeConcour>();
            enseignant.CodesConcours.Clear();
            foreach(CodeConcour cc in lsbx_codeconcour.SelectedItems)
            {
                ccE.Add(cc);
            }
            string contract = "";
            if (btn_contract_ma.IsChecked == true)
            {
                contract = "MA";
            }
            else if (btn_contract_PLP.IsChecked == true)
            {
                contract = "PLP";
            }
            else if (btn_contract_aggreg.IsChecked == true)
            {
                contract = "Aggregé";
            }
            else if (btn_contract_certif.IsChecked == true)
            {
                contract = "Certifié";
            }
            Enseignant en = new Enseignant(enseignant.id,contract, tbx_nom.Text, tbx_prenom.Text, Convert.ToInt16(tbx_ors.Text),ccE);
            AdoEnseignant.modifierEnseignant(en);
            mainWindow.Content = new ListeEnseignant(mainWindow);
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new ListeEnseignant(mainWindow);
        }

        private void btn_suppr_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Voulez vous vraiment supprimer cet enseignant ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {

            }
            else
            {
                AdoEnseignant.supprimerEnseignant(enseignant);
                mainWindow.Content = new ListeEnseignant(mainWindow);
            }          
        }

        //Faire apparaitre le bouton sauvegarder quand un élément est modifié
        private void tbx_nom_GotFocus(object sender, RoutedEventArgs e)
        {
            afficherBtnCreer();
        }

        private void tbx_prenom_GotFocus(object sender, RoutedEventArgs e)
        {
            afficherBtnCreer();
        }

        private void lsbx_codeconcour_GotFocus(object sender, RoutedEventArgs e)
        {
            afficherBtnCreer();
        }

        private void btn_contract_certif_GotFocus(object sender, RoutedEventArgs e)
        {
            afficherBtnCreer();
        }

        private void btn_contract_aggreg_GotFocus(object sender, RoutedEventArgs e)
        {
            afficherBtnCreer();
        }

        private void btn_contract_PLP_GotFocus(object sender, RoutedEventArgs e)
        {
            afficherBtnCreer();
        }

        private void btn_contract_ma_GotFocus(object sender, RoutedEventArgs e)
        {
            afficherBtnCreer();
        }

        private void tbx_ors_GotFocus(object sender, RoutedEventArgs e)
        {
            afficherBtnCreer();
        }

        private void afficherBtnCreer()
        {
            btn_creer.Visibility = Visibility.Visible;
        }
    }
}
