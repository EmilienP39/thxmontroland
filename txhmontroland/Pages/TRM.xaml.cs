
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Windows.Controls.Primitives;
using System.Collections;


namespace txhmontroland.Pages
{
    /// <summary>
    /// Logique d'interaction pour TRM.xaml
    /// </summary>
    public partial class TRM : Page
    {
        private MainWindow _mainWindow;
        public List<CodeConcour> codeConcours;
        private List<Enseignant> enseignants1 = new List<Enseignant>();
        public TRM(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            InitializeComponent();

            List<Enseignant> enseignants = new List<Enseignant>();
            enseignants = AdoEnseignant.getAllLight();

            this.codeConcours = MainWindow.codeConcours;
            this.enseignants1 = enseignants;

            dg_enseignants.Items.Clear();
            dg_enseignants.ItemsSource = MainWindow.codeConcours;
        }

        private void btn_view_Click(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;
            List<Travailler> t = AdoTravailler.getOneFromEnseignant(Convert.ToInt32(id));
            _mainWindow.Content = new ProfInfo(_mainWindow, t);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Document document = new Document(iTextSharp.text.PageSize.A3.Rotate(), 10, 10, 42, 35);

            PdfPTable table = new PdfPTable(dg_enseignants.Columns.Count());

            PdfWriter writer = PdfWriter.GetInstance(document, new System.IO.FileStream("TRM.pdf", System.IO.FileMode.Create));
            document.Open();
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            for (int j = 0; j < dg_enseignants.Columns.Count; j++)
            {
                table.AddCell(new Phrase(dg_enseignants.Columns[j].Header.ToString()));
            }
            table.HeaderRows = 1;
            if (dg_enseignants != null)
            {
                foreach (var item in codeConcours)
                {
                    DataGridRow row = dg_enseignants.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (row != null)
                    {
                        DataGridCellsPresenter presenter = FindVisualChild<DataGridCellsPresenter>(row);
                        for (int i = 0; i < dg_enseignants.Columns.Count; ++i)
                        {
                            DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(i);
                            TextBlock txt = cell.Content as TextBlock;
                            if (txt == null)
                            {
                                TextBlock txt2 = new TextBlock();
                                txt = txt2;
                                txt.Text = "voir";
                            }
                            table.AddCell(new Phrase(txt.Text));
                        }
                    }
                }
                document.Add(table);

                document.Close();
                MessageBox.Show("PDF généré");
            }
        }

        private void dg_enseignants_LostFocus(object sender, RoutedEventArgs e)
        {
            CodeConcour codeConcour = (CodeConcour)dg_enseignants.SelectedItem;
            Enseignant en = enseignants1.Find(x => x.id == codeConcour.afficherEnseignantId);
            en.ponderationLegt = codeConcour.complementPoste; //je met complément de poste dans legt psq flemme de changer la base et de créer complément de poste, ponderation legt sert à r 
            en.ponderationBts = codeConcour.AS; //pareil podé BTS -> AS
            AdoEnseignant.modifierComplementEnseignant(en); //ça modifie complement ET AS
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

        public TRM() { }
    }
}
