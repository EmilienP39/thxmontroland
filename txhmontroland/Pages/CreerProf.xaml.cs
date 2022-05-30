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
    /// Logique d'interaction pour CreerProf.xaml
    /// </summary>
    public partial class CreerProf : Page
    {
        MainWindow MainWindow;
        public CreerProf(MainWindow mainWindow_)
        {
            InitializeComponent();
            this.MainWindow = mainWindow_;
            lsbx_codeconcour.ItemsSource = AdoConcours.getAllLight();
        }

        private void btn_creer_Click(object sender, RoutedEventArgs e)
        {
            if (tbx_nom.Text == "" || tbx_nom.Text == " ")
            {
                tbx_nom.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (tbx_prenom.Text == "" || tbx_prenom.Text == " ")
            {
                tbx_prenom.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (tbx_ors.Text == "" || tbx_ors.Text == " " || tbx_ors.Text != null)
            {
                tbx_prenom.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (btn_contract_ma.IsChecked == false && btn_contract_certif.IsChecked == false && btn_contract_PLP.IsChecked == false && btn_contract_aggreg.IsChecked == false)
            {
                btn_contract_aggreg.Background = new SolidColorBrush(Colors.Red);
                btn_contract_certif.Background = new SolidColorBrush(Colors.Red);
                btn_contract_PLP.Background = new SolidColorBrush(Colors.Red);
                btn_contract_ma.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                List<CodeConcour> ccE = new List<CodeConcour>();
                foreach (CodeConcour cc in lsbx_codeconcour.SelectedItems)
                {
                    ccE.Add(cc);
                }
                string contract = "";
                if(btn_contract_ma.IsChecked == true)
                {
                    contract = "MA";
                }
                else if(btn_contract_PLP.IsChecked == true)
                {
                    contract = "PLP";
                }
                else if(btn_contract_aggreg.IsChecked == true)
                {
                    contract = "Agrégé";
                }
                else if (btn_contract_certif.IsChecked == true)
                {
                    contract = "Certifié";
                }
                Enseignant en = new Enseignant(contract,tbx_nom.Text,tbx_prenom.Text,Convert.ToInt16(tbx_ors.Text),ccE);
                AdoEnseignant.creerEnseignant(en);

                tbx_nom.BorderBrush = new SolidColorBrush(Colors.LightGray); tbx_nom.Text = string.Empty;
                tbx_prenom.BorderBrush = new SolidColorBrush(Colors.LightGray); tbx_prenom.Text = string.Empty;
                tbx_ors.BorderBrush = new SolidColorBrush(Colors.LightGray); tbx_ors.Text = string.Empty;
                btn_contract_aggreg.Background = new SolidColorBrush(Colors.White);
                btn_contract_certif.Background = new SolidColorBrush(Colors.White);
                btn_contract_PLP.Background = new SolidColorBrush(Colors.White);
                btn_contract_ma.Background = new SolidColorBrush(Colors.White);
            }
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Content = new Accueil(MainWindow);
        }
    }
}
