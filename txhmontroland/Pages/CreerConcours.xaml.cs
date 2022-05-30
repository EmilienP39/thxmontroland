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
    /// Logique d'interaction pour CreerConcours.xaml
    /// </summary>
    public partial class CreerConcours : Page
    {
        MainWindow main;
        public CreerConcours(MainWindow mainWindow)
        {
            InitializeComponent();
            this.main = mainWindow;
            List<CodeConcour> ccs = AdoConcours.getAllLight();
            lsb_cc.ItemsSource = ccs;
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new Accueil(main);
        }

        private void btn_creer_Click(object sender, RoutedEventArgs e)
        {
            if(tbx_code_matiere.Text != null && tbx_nom.Text !=null) //copier coller de matière c'est le code du concours
            {
                CodeConcour c = new(tbx_code_matiere.Text, tbx_nom.Text);
                if(clrPicker_concours.SelectedColor == null)
                {
                    c.couleur = "#FFFFFF";
                }
                else
                {
                    c.couleur = clrPicker_concours.SelectedColor.ToString();
                }
                AdoConcours.CreerConcours(c);
            }
        }
    }
}
