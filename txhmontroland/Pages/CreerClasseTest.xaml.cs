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
    /// Logique d'interaction pour CreerClasseTest.xaml
    /// </summary>
    public partial class CreerClasse : Page
    {
        private MainWindow _mainWindow;
        List<Matiere> matieres1 = AdoMatiere.getAllAvecEnseignant();
        List<Classe> classes = AdoClasse.getAll();
        List<Enseignant> enseignants = AdoEnseignant.getAllLight();
        int compteur = 1;
        public CreerClasse(MainWindow main_)
        {
            InitializeComponent();
            this._mainWindow = main_;
            
            stack_creer_classe.Children.Clear();
            WrapPanel wrap = new WrapPanel();

            ComboBox cb_matieres = new ComboBox();
            cb_matieres.Name = "cb_matieres";
            cb_matieres.SelectionChanged += new SelectionChangedEventHandler(cb_matiere_selection_changed);
            cb_matieres.Width = 194;
            cb_matieres.Height = 30;
            cb_matieres.Margin = new Thickness(10);
            cb_matieres.FontSize = 15;
            cb_matieres.ItemsSource = matieres1;

            ComboBox cb_enseignants = new ComboBox();
            cb_enseignants.Name = "cb_enseignants";
            cb_enseignants.Width = 194;
            cb_enseignants.Height = 30;
            cb_enseignants.FontSize = 15;
            cb_enseignants.Margin = new Thickness(10);

            TextBox tbx_VH = new TextBox();
            tbx_VH.Text = "Volume horaire";
            tbx_VH.Width = 194;
            tbx_VH.Height = 30;
            tbx_VH.FontSize = 15;
            tbx_VH.Margin = new Thickness(10);

            TextBox tbx_referentiel = new TextBox();
            tbx_referentiel.Text = "Volume Referentiel";
            tbx_referentiel.Width = 194;
            tbx_referentiel.Height = 30;
            tbx_referentiel.FontSize = 15;
            tbx_referentiel.Margin = new Thickness(10);

            CheckBox cbx_ispp = new CheckBox();
            cbx_ispp.Name = "cbx_ispp";
            cbx_ispp.FontSize = 15;
            cbx_ispp.Content = "Prof Principal ?";

            CheckBox cbx_groupe = new CheckBox();
            cbx_groupe.Click += new RoutedEventHandler(cbx_groupe_click);
            cbx_groupe.Content = "Groupe ?";
            cbx_groupe.FontSize = 15;
            cbx_groupe.Name = "cbx_groupe1";

            wrap.Children.Add(cb_matieres);
            wrap.Children.Add(cb_enseignants);
            wrap.Children.Add(tbx_VH);
            wrap.Children.Add(tbx_referentiel);
            wrap.Children.Add(cbx_ispp);
            wrap.Children.Add(cbx_groupe);

            stack_creer_classe.Children.Add(wrap);
        }
        private void cb_matiere_selection_changed(object sender, RoutedEventArgs e)
        {
            foreach (WrapPanel wrapPanel in stack_creer_classe.Children)
            {
                Matiere ma = new Matiere();
                foreach (UIElement comboBox in wrapPanel.Children)
                {
                    if (comboBox.GetType() == typeof(ComboBox))
                    {
                        if (((ComboBox)comboBox).Name == "cb_matieres")
                        {
                            ma = (Matiere)((ComboBox)comboBox).SelectedItem;
                        }
                        if (((ComboBox)comboBox).Name == "cb_enseignants")
                        {
                            ((ComboBox)comboBox).ItemsSource = ma.enseignants;
                        }
                    }
                }
            }
        }
        private void cbx_groupe_click(object sender, RoutedEventArgs e)
        {
            string cbx_name = ((CheckBox)sender).Name;
            WrapPanel parent = (WrapPanel)((CheckBox)sender).Parent;
            if (((CheckBox)sender).IsChecked == true)
            {
                Enseignant enseignant = new Enseignant();
                Matiere matiere = new Matiere();
                WrapPanel wrapPanel1 = (WrapPanel)((CheckBox)sender).Parent;
                wrapPanel1.Name = "Wrap_" + cbx_name;

                foreach (UIElement uiElement in (parent.Children))
                {
                    if (uiElement.GetType() == typeof(ComboBox))
                    {
                        if (((ComboBox)uiElement).Name == "cb_enseignants")
                        {
                            enseignant = (Enseignant)((ComboBox)uiElement).SelectedItem;
                        }
                        if (((ComboBox)uiElement).Name == "cb_matieres")
                        {
                            matiere = (Matiere)((ComboBox)uiElement).SelectedItem;
                        }
                    }
                }
                ComboBox cb_matieres = new ComboBox();
                cb_matieres.Name = "cb_matieres";
                cb_matieres.Width = 194;
                cb_matieres.Height = 30;
                cb_matieres.Margin = new Thickness(10);
                cb_matieres.ItemsSource = this.matieres1;
                cb_matieres.IsReadOnly = true;
                cb_matieres.FontSize = 15;
                cb_matieres.SelectedItem = matiere;

                ComboBox cb_enseignants = new ComboBox();
                cb_enseignants.Name = "cb_enseignants";
                cb_enseignants.Width = 194;
                cb_enseignants.Height = 30;
                cb_enseignants.Margin = new Thickness(10);
                cb_enseignants.ItemsSource = this.enseignants;
                cb_enseignants.IsReadOnly = true;
                cb_enseignants.FontSize = 15;
                cb_enseignants.SelectedItem = this.enseignants.First(x => x.id == enseignant.id);

                ComboBox cb_classes = new ComboBox();
                cb_classes.Name = "cb_classes";
                cb_classes.Width = 194;
                cb_classes.Height = 30;
                cb_classes.Margin = new Thickness(10);
                cb_classes.FontSize = 15;
                cb_classes.ItemsSource = this.classes;

                WrapPanel wrapPanel = new WrapPanel();
                stack_groupe.Children.Add(wrapPanel);

                wrapPanel.Children.Add(cb_classes);
                wrapPanel.Children.Add(cb_enseignants);
                wrapPanel.Children.Add(cb_matieres);
            }
            else
            {
                WrapPanel wrap = new WrapPanel();
                foreach (UIElement uiElement in stack_groupe.Children)
                {
                    if (uiElement.GetType() == typeof(WrapPanel))
                    {
                        if (((WrapPanel)uiElement).Name == "Wrap_" + cbx_name)
                        {
                            wrap = (WrapPanel)uiElement;
                        }
                    }
                }
                stack_groupe.Children.Remove(wrap);
            }
        }
        private void btn_ajouter_ligne_Click(object sender, RoutedEventArgs e)
        {
            WrapPanel wrap = new WrapPanel();

            ComboBox cb_matieres = new ComboBox();
            cb_matieres.Name = "cb_matieres";
            cb_matieres.SelectionChanged += new SelectionChangedEventHandler(cb_matiere_selection_changed);
            cb_matieres.Width = 194;
            cb_matieres.Height = 30;
            cb_matieres.Margin = new Thickness(10);
            cb_matieres.FontSize = 15;
            cb_matieres.ItemsSource = matieres1;

            ComboBox cb_enseignants = new ComboBox();
            cb_enseignants.Name = "cb_enseignants";
            cb_enseignants.Width = 194;
            cb_enseignants.Height = 30;
            cb_enseignants.FontSize = 15;
            cb_enseignants.Margin = new Thickness(10);

            TextBox tbx_VH = new TextBox();
            tbx_VH.Text = "Volume horaire";
            tbx_VH.Name = "tbx_VH";
            tbx_VH.Width = 194;
            tbx_VH.Height = 30;
            tbx_VH.FontSize = 15;
            tbx_VH.Margin = new Thickness(10);

            TextBox tbx_referentiel = new TextBox();
            tbx_referentiel.Text = "Volume Referentiel";
            tbx_referentiel.Name = "tbx_referentiel";
            tbx_referentiel.Width = 194;
            tbx_referentiel.Height = 30;
            tbx_referentiel.FontSize = 15;
            tbx_referentiel.Margin = new Thickness(10);

            CheckBox cbx_ispp = new CheckBox();
            cbx_ispp.Name = "cbx_ispp";
            cbx_ispp.FontSize = 15;
            cbx_ispp.Content = "Prof Principal ?";

            CheckBox cbx_groupe = new CheckBox();
            cbx_groupe.Click += new RoutedEventHandler(cbx_groupe_click);
            cbx_groupe.Content = "Groupe ?";
            cbx_groupe.FontSize = 15;
            cbx_groupe.Name = "cbx_groupe" + compteur;
            compteur++;

            wrap.Children.Add(cb_matieres);
            wrap.Children.Add(cb_enseignants);
            wrap.Children.Add(tbx_VH);
            wrap.Children.Add(tbx_referentiel);
            wrap.Children.Add(cbx_ispp);
            wrap.Children.Add(cbx_groupe);

            stack_creer_classe.Children.Add(wrap);
        }
        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.Content = new Accueil(_mainWindow);
        }

        private void btn_creer_Click(object sender, RoutedEventArgs e)
        {
            tbx_nom.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbx_nb_eleve.BorderBrush = new SolidColorBrush(Colors.LightGray);
            tbx_nb_division.BorderBrush = new SolidColorBrush(Colors.LightGray);
            radbut_ES.Background = new SolidColorBrush(Colors.White);
            radbut_legt1T.Background = new SolidColorBrush(Colors.White);
            radbut_legt2.Background = new SolidColorBrush(Colors.White);
            radbut_LEP.Background = new SolidColorBrush(Colors.White);

            if (tbx_nom.Text == "" || tbx_nom.Text == " ")
            {
                tbx_nom.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (tbx_nb_eleve.Text == "" || tbx_nb_eleve.Text == " ")
            {
                tbx_nb_eleve.BorderBrush = new SolidColorBrush(Colors.Red);
            }
            else if (radbut_ES.IsChecked == false && radbut_legt1T.IsChecked == false && radbut_legt2.IsChecked == false && radbut_LEP.IsChecked == false)
            {
                grid_type_classe.Background = new SolidColorBrush(Colors.Red);
                radbut_ES.Background = new SolidColorBrush(Colors.Red);
                radbut_legt1T.Background = new SolidColorBrush(Colors.Red);
                radbut_legt2.Background = new SolidColorBrush(Colors.Red);
                radbut_LEP.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                Classe c = new Classe(Convert.ToInt32(tbx_nb_eleve.Text), tbx_nom.Text, Convert.ToDouble(tbx_nb_division.Text));
                if (radbut_legt2.IsChecked == true)
                {
                    c.valeur_niveau = 1;
                    c.type_formation = "LEGT2°";
                }
                else if (radbut_legt1T.IsChecked == true)
                {
                    c.valeur_niveau = 2;
                    c.type_formation = "LEGT1°T°";
                }
                else if (radbut_ES.IsChecked == true)
                {
                    c.valeur_niveau = 3;
                    c.type_formation = "ES";
                }
                else if (radbut_LEP.IsChecked == true)
                {
                    c.valeur_niveau = 0;
                    c.type_formation = "LEP";
                }
                int idClasse = AdoClasse.creerClasse(c);
                foreach (WrapPanel wrapPanel in stack_creer_classe.Children)
                {
                    Travailler t = new Travailler();

                    t.Classe = c;
                    foreach (UIElement comboBox in wrapPanel.Children)
                    {
                        if(comboBox.GetType() == typeof(ComboBox))
                        {
                            if (((ComboBox)comboBox).Name == "cb_enseignants")
                            {
                                t.enseignant = (Enseignant)((ComboBox)comboBox).SelectedItem;
                                foreach (UIElement checkbox in wrapPanel.Children)
                                {
                                    if (checkbox.GetType() == typeof(CheckBox))
                                    {
                                        if (((CheckBox)checkbox).IsChecked == true && ((CheckBox)checkbox).Name == "cbx_ispp")
                                        {
                                            Oeuvrer o = new Oeuvrer();
                                            o.classe = c;
                                            o.enseignant = (Enseignant)((ComboBox)comboBox).SelectedItem;
                                            o.is_pp = true;
                                            if (o.is_pp != null && o.enseignant != null && o.classe != null)
                                            {
                                                AdoOeuvrer.creerOeuvrer(o, idClasse);
                                            }
                                        }
                                    }
                                }
                            }
                            if (((ComboBox)comboBox).Name == "cb_matieres")
                            {
                                t.matiere = (Matiere)((ComboBox)comboBox).SelectedItem;
                            }
                        }
                    }
                    foreach (UIElement textBox in wrapPanel.Children)
                    {
                        if(textBox.GetType() == typeof(TextBox))
                        {
                            if (((TextBox)textBox).Name == "tbx_VH")
                            {
                                t.volume_horaire = Convert.ToDouble(((TextBox)textBox).Text);
                            }
                            if (((TextBox)textBox).Name == "tbx_referentiel")
                            {
                                t.horaire_referentiel = Convert.ToDouble(((TextBox)textBox).Text);
                            }
                        }
                    }
                    AdoTravailler.CreerTravailler(t,idClasse); 
                }
                stack_creer_classe.Children.Clear();
            }
        }

        private void btn_reset_Click(object sender, RoutedEventArgs e)
        {
            stack_creer_classe.Children.Clear();
            stack_groupe.Children.Clear();
        }
    }
}