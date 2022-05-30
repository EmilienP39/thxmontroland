 using System;
using System.IO;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Controls.Primitives;
using System.Collections;
 using Paragraph = iTextSharp.text.Paragraph;

 namespace txhmontroland.Pages
{
    /// <summary>
    /// Logique d'interaction pour ProfInfo.xaml
    /// </summary>
    public partial class ProfInfo : Page
    {
        MainWindow main;
        Enseignant enseignant = new Enseignant();
        List<Travailler> travaillers = new List<Travailler>();
        private List<Oeuvrer> _oeuvrers = new List<Oeuvrer>();
        public ProfInfo(MainWindow mainWindow_, List<Travailler> t )
        {
            InitializeComponent();
            this.travaillers = t;
            main = mainWindow_; 
            double heuresTotales = 0;
            double HSATotales= 0;
            double heures_mini= 0;
            foreach(Travailler travailler in t)
            {
                enseignant = travailler.enseignant;
                _oeuvrers = AdoOeuvrer.getAllForEnseignant(travailler.enseignant);
                
                foreach(Oeuvrer oeuvrer in _oeuvrers)
                {
                    if (oeuvrer.is_pp == true)
                    {
                        lbl_pp.Content = oeuvrer.classe.nom_classe;
                    }
                }

                string nomJ = travailler.enseignant.nom;
                string prenomJ = travailler.enseignant.prénom;
                lbl_initial.Content = nomJ +"\n"+prenomJ;
                heuresTotales += travailler.volume_horaire;
                heures_mini = travailler.enseignant.heures_mini;
                if (heuresTotales > travailler.enseignant.heures_mini)
                {
                    HSATotales = heuresTotales - travailler.enseignant.heures_mini;
                }
                else
                {
                    HSATotales = 0;
                }
            }
            lbl_heures_totales.Content = heuresTotales;
            lbl_HSA_totales.Content = HSATotales;
            lbl_heure_ors.Content = heures_mini;
            dg_joueurs.ItemsSource = t;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new TRM(main);
        }

        protected void GeneratePDF(object sender, System.EventArgs e)
        {
            Document document = new Document(iTextSharp.text.PageSize.A4, 47, 47, 47, 47);
            PdfPTable table = new PdfPTable(dg_joueurs.Columns.Count());

            PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream(enseignant.nom +"_" + enseignant.prénom+".pdf", System.IO.FileMode.Create));
            document.Open();
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            for (int j = 0; j < dg_joueurs.Columns.Count; j++)
            {
                Phrase nom = new Phrase(dg_joueurs.Columns[j].Header.ToString());
                PdfPCell cellColor = new PdfPCell(nom);
                cellColor.BackgroundColor = BaseColor.YELLOW;
                table.AddCell(cellColor);
            }
            table.HeaderRows = 1;
            if (dg_joueurs != null)
            {
                foreach (var item in travaillers)
                {
                    DataGridRow row = dg_joueurs.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (row != null)
                    {
                        DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                        for (int i = 0; i < dg_joueurs.Columns.Count; ++i)
                        {
                            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                            TextBlock txt = cell.Content as TextBlock;
                            if (txt != null)
                            {
                                table.AddCell(new Phrase(txt.Text));
                            }
                        }
                    }
                }
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

                Font fontNom = new Font(bf, 20, Font.NORMAL);
                Font fontRemarque = new Font(bf, 14, Font.BOLD, BaseColor.RED);

                Paragraph nom = new Paragraph(new Chunk(enseignant.nom + " " + enseignant.prénom  + "\n ",fontNom));
                document.Add(nom);
                document.Add(table);
                foreach(Oeuvrer oeuvrer in _oeuvrers)
                {
                    if (oeuvrer.is_pp == true)
                    {
                        document.Add(new iTextSharp.text.Paragraph("\nProf Principal :" + lbl_pp.Content));
                    }
                }
                document.Add(new iTextSharp.text.Paragraph("\n"));
                document.Add(new iTextSharp.text.Paragraph("Heures totales : " + lbl_heures_totales.Content + "\nHSA totales : " + lbl_HSA_totales.Content + "\nHeures ORS : " + lbl_heure_ors.Content));
                
                Paragraph remarque = new Paragraph(new Chunk("Cette répartition n'est pas définitive. Des ajustements peuvent encore être réalisés jusqu'à la rentrée scolaire. Ce document n'est donc ni définitif, ni contractuel.\nOlivier RIANT",fontRemarque));
                document.Add(remarque);
                document.Close();
                if (MessageBox.Show("PDF généré, voulez vous l'ouvrir ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    string pathLogiciel = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                    string pathPdf = pathLogiciel + @"\" + enseignant.nom +"_" + enseignant.prénom+".pdf";
                    pathPdf = pathPdf.Replace(@"\\\\", @"\\");
                    var p = new System.Diagnostics.Process();
                    p.StartInfo = new System.Diagnostics.ProcessStartInfo(pathLogiciel + @"\" + enseignant.nom +"_" + enseignant.prénom+".pdf")
                    { 
                        UseShellExecute = true 
                    };
                    p.Start();
                }
            }
        }

        private void GenerateDF(object sender, RoutedEventArgs e)
        {
            GeneratePDF(sender, e);
        }
        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
