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
    /// Logique d'interaction pour ClasseRecap.xaml
    /// </summary>
    public partial class ClasseRecap : Page
    {
        MainWindow main;
        Classe classe1;
        List<Classe> classes = AdoClasse.getAll();
        List<Travailler> travaillers;
        List<Oeuvrer> oeuvrers;
        List<Matiere> matieres1;
        List<Enseignant> enseignants = new List<Enseignant>();

        private int compteur = 0;
        public ClasseRecap(MainWindow mainWindow,Classe classe)
        {
            InitializeComponent();

            this.main = mainWindow;
            this.classe1 = classe;
            this.travaillers = AdoTravailler.getFromClasse(classe1.id);
            this.matieres1 = AdoMatiere.getAllAvecEnseignant();
            this.enseignants = AdoEnseignant.getAllLight();
            this.oeuvrers = AdoOeuvrer.getAllFromClasse(classe1.id);

            tbx_nom.Text = classe1.nom_classe;
            tbx_nb_eleve.Text = Convert.ToString(classe1.nombre_eleve);
            tbx_nb_division.Text = Convert.ToString(classe1.division);      

            switch(classe1.type_formation)
            {
                case "LEGT2°":
                    radbut_legt2.IsChecked = true;
                    break;
                case "LEGT1°T°":
                    radbut_legt1T.IsChecked = true;
                    break;
                case "LEP":
                    radbut_LEP.IsChecked = true;
                    break;
                case "ES":
                    radbut_ES.IsChecked = true;
                    break;
            }

            foreach(Travailler t in travaillers)
            {
                WrapPanel wrap = new WrapPanel();

                ComboBox cb_matieres = new ComboBox();
                cb_matieres.Name = "cb_matieres";
                cb_matieres.SelectionChanged += new SelectionChangedEventHandler(cb_matiere_selection_changed);
                cb_matieres.Width = 194;
                cb_matieres.Height = 30;
                cb_matieres.Margin = new Thickness(10);
                cb_matieres.ItemsSource = matieres1;
                cb_matieres.SelectedItem = matieres1.First(x => x.id == t.matiere.id);

                ComboBox cb_enseignants = new ComboBox();
                cb_enseignants.Name = "cb_enseignants";
                cb_enseignants.Width = 194;
                cb_enseignants.Height = 30;
                cb_enseignants.Margin = new Thickness(10);
                Matiere ma = (Matiere)cb_matieres.SelectedItem;
                cb_enseignants.ItemsSource = ma.enseignants;
                cb_enseignants.SelectedItem = ma.enseignants.First(x => x.id == t.enseignant.id);

                TextBox tbx_VH = new TextBox();
                tbx_VH.Text = "Volume horaire";
                tbx_VH.Name = "tbx_VH";
                tbx_VH.Width = 194;
                tbx_VH.Height = 30;
                tbx_VH.Margin = new Thickness(10);
                tbx_VH.Text = Convert.ToString(t.volume_horaire);

                TextBox tbx_referentiel = new TextBox();
                tbx_referentiel.Text = "Volume Referentiel";
                tbx_referentiel.Name = "tbx_referentiel";
                tbx_referentiel.Width = 194;
                tbx_referentiel.Height = 30;
                tbx_referentiel.Margin = new Thickness(10);
                tbx_referentiel.Text = Convert.ToString(t.horaire_referentiel);

                CheckBox cbx_ispp = new CheckBox();
                cbx_ispp.Name = "cbx_ispp";
                cbx_ispp.Content = "Prof Principal ?";
                if (AdoOeuvrer.isPP(t.enseignant.id, t.Classe.id) == true)
                {
                    cbx_ispp.IsChecked = true;
                }

                CheckBox cbx_groupe = new CheckBox();
                cbx_groupe.Click += new RoutedEventHandler(cbx_groupe_click);
                cbx_groupe.Content = "Groupe ?";
                cbx_groupe.Name = "cbx_groupe" + compteur;
                compteur++;

                wrap.Children.Add(cb_matieres);
                wrap.Children.Add(cb_enseignants);
                wrap.Children.Add(tbx_VH);
                wrap.Children.Add(tbx_referentiel);
                wrap.Children.Add(cbx_ispp);
                wrap.Children.Add(cbx_groupe);

                stack_edit_classe.Children.Add(wrap);
            }
            
        }
        
        private void cb_matiere_selection_changed(object sender, RoutedEventArgs e)
        {
            foreach(WrapPanel wrapPanel in stack_edit_classe.Children)
            {
                Matiere ma = new Matiere();
                Enseignant en = new Enseignant();
                foreach(UIElement comboBox in wrapPanel.Children)
                {
                    if(comboBox.GetType() == typeof(ComboBox))
                    {
                        if(((ComboBox)comboBox).Name == "cb_matieres")
                        {
                            ma = (Matiere)((ComboBox)comboBox).SelectedItem;
                        }
                        if(((ComboBox)comboBox).Name == "cb_enseignants")
                        {
                            en = (Enseignant)((ComboBox)comboBox).SelectedItem;
                            ((ComboBox)comboBox).ItemsSource = ma.enseignants;
                            ((ComboBox)comboBox).SelectedItem = en;
                        }
                    }
                }
            }
        }

        private void cbx_groupe_click(object sender, RoutedEventArgs e)
        {
            string cbx_name = ((CheckBox)sender).Name;
            WrapPanel parent = (WrapPanel)((CheckBox)sender).Parent;
            if (((CheckBox) sender).IsChecked == true)
            {
                Enseignant enseignant = new Enseignant();
                Matiere matiere = new Matiere();
                WrapPanel wrapPanel = new WrapPanel();
                wrapPanel.Name = "Wrap_" + cbx_name;
                
                foreach (UIElement uiElement in parent.Children)
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
                cb_matieres.SelectedItem = matiere;

                ComboBox cb_enseignants = new ComboBox();
                cb_enseignants.Name = "cb_enseignants";
                cb_enseignants.Width = 194;
                cb_enseignants.Height = 30;
                cb_enseignants.Margin = new Thickness(10);
                cb_enseignants.ItemsSource = this.enseignants;
                cb_enseignants.IsReadOnly = true;
                cb_enseignants.SelectedItem = enseignant;

                ComboBox cb_classes = new ComboBox();
                cb_classes.Name = "cb_classes";
                cb_classes.Width = 194;
                cb_classes.Height = 30;
                cb_classes.Margin = new Thickness(10);
                cb_classes.ItemsSource = this.classes;
                
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
                        if (((WrapPanel) uiElement).Name == "Wrap_" + cbx_name)
                        {
                            wrap = (WrapPanel) uiElement;
                        }
                    }
                }
                stack_groupe.Children.Remove(wrap);
            }
        }

        private void btn_retour_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new ListeClasse(main);
        }

        private void btn_creer_Click(object sender, RoutedEventArgs e)
        {
            AdoTravailler.supprimerTravaillerFromClasse(classe1);
            foreach (WrapPanel wrapPanel in stack_edit_classe.Children)
            {
                Travailler t = new Travailler(classe1);
                Enseignant en = new Enseignant();
                Matiere m = new Matiere();
                foreach (UIElement uiElement in wrapPanel.Children)
                {
                    if (uiElement.GetType() == typeof(ComboBox))
                    {
                        if (((ComboBox) uiElement).Name == "cb_enseignants")
                        {
                            en = (Enseignant) ((ComboBox) uiElement).SelectedItem;
                        }
                        if (((ComboBox) uiElement).Name == "cb_matieres")
                        {
                            m = (Matiere) ((ComboBox) uiElement).SelectedItem;
                        }
                    }
                    if (uiElement.GetType() == typeof(TextBox))
                    {
                        if (((TextBox) uiElement).Name == "tbx_VH")
                        {
                            t.volume_horaire = Convert.ToInt16(((TextBox)uiElement).Text);
                        }
                        if (((TextBox) uiElement).Name == "tbx_referentiel")
                        {
                            t.horaire_referentiel = Convert.ToInt16(((TextBox)uiElement).Text);
                        }
                    }

                    if (uiElement.GetType() == typeof(CheckBox))
                    {
                        if (((CheckBox) uiElement).Name == "cbx_groupe")
                        {
                            CheckBox checkBox = (CheckBox) uiElement;
                            foreach (WrapPanel wrapPanelGroupe in stack_groupe.Children)
                            {
                                if (wrapPanel.Name == "Wrap_" + checkBox.Name)
                                {
                                    Travailler t_groupe = new Travailler();
                                    foreach (UIElement uiElementGroupe in wrapPanelGroupe.Children)
                                    {
                                        if (uiElement.GetType() == typeof(ComboBox))
                                        {
                                            if (((ComboBox) uiElement).Name == "cb_enseignants")
                                            {
                                                t_groupe.enseignant = (Enseignant) ((ComboBox) uiElement).SelectedItem;
                                            }
                                            if (((ComboBox) uiElement).Name == "cb_matieres")
                                            {
                                                t_groupe.matiere = (Matiere) ((ComboBox) uiElement).SelectedItem;
                                            }
                                            if (((ComboBox) uiElement).Name == "cb_classes")
                                            {
                                                t_groupe.Classe = (Classe) ((ComboBox) uiElement).SelectedItem;
                                            }

                                            t_groupe.horaire_referentiel = 0;
                                            t_groupe.volume_horaire = 0;
                                        }
                                        AdoTravailler.CreerTravailler(t_groupe, t_groupe.Classe.id);
                                    }
                                }
                                
                            }
                        }
                    }
                    AdoTravailler.CreerTravailler(t, t.Classe.id);
                }
            }
        }

        private void Btn_ajouter_ligne_OnClick(object sender, RoutedEventArgs e)
        {
            WrapPanel wrap = new WrapPanel();

            ComboBox cb_matieres = new ComboBox();
            cb_matieres.Name = "cb_matieres";
            cb_matieres.SelectionChanged += new SelectionChangedEventHandler(cb_matiere_selection_changed);
            cb_matieres.Width = 194;
            cb_matieres.Height = 30;
            cb_matieres.Margin = new Thickness(10);
            cb_matieres.ItemsSource = matieres1;

            ComboBox cb_enseignants = new ComboBox();
            cb_enseignants.Name = "cb_enseignants";
            cb_enseignants.Width = 194;
            cb_enseignants.Height = 30;
            cb_enseignants.Margin = new Thickness(10);
            Matiere ma = (Matiere)cb_matieres.SelectedItem;
            cb_enseignants.ItemsSource = enseignants;

            TextBox tbx_VH = new TextBox();
            tbx_VH.Text = "Volume horaire";
            tbx_VH.Name = "tbx_VH";
            tbx_VH.Width = 194;
            tbx_VH.Height = 30;
            tbx_VH.Margin = new Thickness(10);

            TextBox tbx_referentiel = new TextBox();
            tbx_referentiel.Text = "Volume Referentiel";
            tbx_referentiel.Name = "tbx_referentiel";
            tbx_referentiel.Width = 194;
            tbx_referentiel.Height = 30;
            tbx_referentiel.Margin = new Thickness(10);

            CheckBox cbx_ispp = new CheckBox();
            cbx_ispp.Name = "cbx_ispp";
            cbx_ispp.Content = "Prof Principal ?";

            CheckBox cbx_groupe = new CheckBox();
            cbx_groupe.Click += new RoutedEventHandler(cbx_groupe_click);
            cbx_groupe.Content = "Groupe ?";
            cbx_groupe.Name = "cbx_groupe" + compteur;
            compteur++;

            wrap.Children.Add(cb_matieres);
            wrap.Children.Add(cb_enseignants);
            wrap.Children.Add(tbx_VH);
            wrap.Children.Add(tbx_referentiel);
            wrap.Children.Add(cbx_ispp);
            wrap.Children.Add(cbx_groupe);
            
            stack_edit_classe.Children.Add(wrap);
        }
    }
}
