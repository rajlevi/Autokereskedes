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
using Microsoft.Win32;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Autoker;

namespace Autokereskedes
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        cnAutoker cn;
        List<Autoker.Elado> users = new List<Autoker.Elado>();
        string jog;
        public LoginPage()
        {
            InitializeComponent();
            cn = new cnAutoker();
            users.AddRange(cn.Elados.ToList());
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string email = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            ErrorTextBlock.Visibility = Visibility.Collapsed;
            ErrorTextBlock.Text = "";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ShowError("Az email cím és jelszó megadása kötelező!");
                return;
            }

            // Felhasználó keresése email alapján
            var bejelentkezo = users.FirstOrDefault(u => u.email == email && u.jelszo == password);
            if (bejelentkezo == null)
            {
                ShowError("Hibás email cím vagy jelszó!");
                return;
            }
            if (bejelentkezo.Admin == true)
            {
                jog = "Admin";
            }
            else jog = "User";

            string username = bejelentkezo.Nev;
            // Logolás
            LogToFile($"Bejelentkezés: {email}, szerepkör: {jog}, időpont: {DateTime.Now}");

            // Jogosultságkezelés: átadható a szerepkör a főmenünek
            MessageBox.Show($"Sikeres bejelentkezés! Szerepköröd: {jog}", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new Mainmenu(username, jog));
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register());
        }

        private void guestBtn_Click(object sender, RoutedEventArgs e)
        {
            jog = "User";
            NavigationService.Navigate(new Mainmenu("Vendég", jog));
        }

        private void ShowError(string message)
        {
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Visibility.Visible;
        }

        private void LogToFile(string message)
        {
            try
            {
                System.IO.File.AppendAllText("log.txt", message + "\n");
            }
            catch { }
        }
    }
}