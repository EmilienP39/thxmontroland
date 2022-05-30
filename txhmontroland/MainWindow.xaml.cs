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
using MahApps.Metro.Controls;

namespace txhmontroland
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static List<CodeConcour> codeConcours;
        public MainWindow()
        {
            InitializeComponent();
            List<CodeConcour> CodeConcour1 = new List<CodeConcour>();
            CodeConcour1 = AdoConcours.getAll();
            CodeConcour CCTotal = new CodeConcour();
            CCTotal.nom = "TOTAL :";
            foreach (CodeConcour cc in CodeConcour1)
            {
                //Heures par type de classe 
                CCTotal.afficherHeuresBts += cc.afficherHeuresBts;
                CCTotal.afficherHeuresLep += cc.afficherHeuresLep;
                CCTotal.afficherHeuresLegt1T += cc.afficherHeuresLegt1T;
                CCTotal.afficherHeuresLegt2ND += cc.afficherHeuresLegt2ND;
                //Heures et HSA totales
                CCTotal.afficherTotalHEnseignants += cc.afficherTotalHEnseignants;
                CCTotal.afficherHSA += cc.afficherHSA;
                //Complement et AS
                CCTotal.complementPoste += cc.complementPoste;
                CCTotal.AS += cc.AS;
                //Ponderations
                CCTotal.ajustementPonde += cc.ajustementPonde;
                CCTotal.afficherPondBtsEnseignants += cc.afficherPondBtsEnseignants;
                CCTotal.afficherPondLegtEnseignants += cc.afficherPondLegtEnseignants;
                CCTotal.afficherTotalponde += cc.afficherTotalponde;
            }
            CodeConcour1.Add(CCTotal);
            codeConcours = CodeConcour1;
            this.Content = new Accueil(this);
        }
        private void Home(object sender, RoutedEventArgs e)
        {
            this.Content = new Accueil(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new CreerConcours(this);
        }
    }
}
