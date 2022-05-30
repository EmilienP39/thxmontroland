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
    /// Logique d'interaction pour TotalMatiere.xaml
    /// </summary>
    public partial class TotalMatiere : Page
    {
        public TotalMatiere(MainWindow mainWindow)
        {
            InitializeComponent();

            List<Classe> classes = AdoClasse.getMatiereClasses();

            List<Classe> classeLegt = classes.FindAll(x => x.type_formation == "LEGT");
            List<Classe> classeBts = classes.FindAll(x => x.type_formation == "ES");
            List<Classe> classeLep = classes.FindAll(x => x.type_formation == "LEP");

            List<Matiere> matieresLegt = new List<Matiere>();
            List<Matiere> matieresBts = new List<Matiere>();
            List<Matiere> matieresLep = new List<Matiere>();

            foreach(Classe classe in classeLegt)
            {
                matieresLegt = classe.matieres;
                foreach (Matiere matiere in matieresLegt)
                {
                    matiere.afficherTotalHeure += matiere.afficherTotalHeure;
                }
            }
            foreach(Classe classe1 in classeBts)
            {
                matieresBts = classe1.matieres;
                foreach(Matiere matiere in matieresBts)
                {
                    matiere.afficherTotalHeure += matiere.afficherTotalHeure;
                }
            }
            foreach(Classe classe2 in classeLep)
            {
                matieresLep = classe2.matieres;
                foreach (Matiere matiere in matieresLep)
                {
                    matiere.afficherTotalHeure += matiere.afficherTotalHeure;
                }
            }

            dg_classes_legt.ItemsSource = matieresLegt;
            dg_classes_lep.ItemsSource = matieresLep;
            dg_classes.ItemsSource = matieresBts;
        }
    }
}
