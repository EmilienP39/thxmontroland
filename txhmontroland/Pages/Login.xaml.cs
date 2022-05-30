using System.Windows.Controls;
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
using txhmontroland.ADO;


namespace txhmontroland.Pages;

public partial class Login : Page
{
    MainWindow main;
    public Login(MainWindow mainWindow)
    {
        this.main = mainWindow;
        InitializeComponent();
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        if (tbx_mdp.Password == "")
        {
            tbx_mdp.BorderBrush = new SolidColorBrush(Colors.Red);
            lbl_erreur_password.Content = "Veuillez rentrer un mot de passe.";
            lbl_erreur_password.Foreground = new SolidColorBrush(Colors.Red);
        }
        else
        {
            tbx_mdp.BorderBrush = new SolidColorBrush(Colors.LightGray);
            lbl_erreur_password.Content = "";
            lbl_erreur_password.Foreground = new SolidColorBrush(Colors.White);
            string password = tbx_mdp.Password;
            string utilisateur = tbx_utilisateur.Text;
            string[] logs = AdoLogin.getLogs();
            if (logs[1] == Encode(password) && utilisateur == logs[0])
            {
                main.Content = new Accueil(main);
            }
            if (logs[1] != Encode(password))
            {
                lbl_erreur_password.Content = "Mauvais mot de passe.";
                lbl_erreur_password.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                lbl_erreur_password.Content = "";
                lbl_erreur_password.Foreground = new SolidColorBrush(Colors.White);
            }

            if (utilisateur != logs[0])
            {
                lbl_erreur_user.Content = "Mauvais nom d'utilisateur.";
                lbl_erreur_user.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                lbl_erreur_user.Content = "";
                lbl_erreur_user.Foreground = new SolidColorBrush(Colors.White);
            }
        }
        if (tbx_utilisateur.Text == "")
        {
            tbx_utilisateur.BorderBrush = new SolidColorBrush(Colors.Red);
            lbl_erreur_user.Content = "Veuillez rentrer un nom d'utilisateur.";
            lbl_erreur_user.Foreground = new SolidColorBrush(Colors.Red);
        }
        else
        {
            tbx_utilisateur.BorderBrush = new SolidColorBrush(Colors.LightGray);
            lbl_erreur_user.Content = "";
            lbl_erreur_user.Foreground = new SolidColorBrush(Colors.White);
            string password = tbx_mdp.Password;
            string utilisateur = tbx_utilisateur.Text;
            string[] logs = AdoLogin.getLogs();
            if (logs[1] == Encode(password) && utilisateur == logs[0])
            {
                main.Content = new Accueil(main);
            }
            if (logs[1] != Encode(password))
            {
                lbl_erreur_password.Content = "Mauvais mot de passe.";
                lbl_erreur_password.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                lbl_erreur_password.Content = "";
                lbl_erreur_password.Foreground = new SolidColorBrush(Colors.White);
            }

            if (utilisateur != logs[0])
            {
                lbl_erreur_user.Content = "Mauvais nom d'utilisateur.";
                lbl_erreur_user.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                lbl_erreur_user.Content = "";
                lbl_erreur_user.Foreground = new SolidColorBrush(Colors.White);
            }
        }
            

    }
    public static string Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
}