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
    /// Logique d'interaction pour ListeConcours.xaml
    /// </summary>
    public partial class ListeConcours : Page
    {
        MainWindow mainWindow_;
        public ListeConcours(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow_ = mainWindow;
            dg_concours.ItemsSource = AdoConcours.getAllLight();
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            var id = ((Button)sender).Tag;
            CodeConcour c = AdoConcours.getOneLight(Convert.ToInt32(id));
            mainWindow_.Content = new RecapConcours(mainWindow_, c);
        }
    }
}
