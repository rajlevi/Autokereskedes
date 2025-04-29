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

namespace Autokereskedes
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        // Ideiglenes: felhasználók listája (a regisztrációból át kell majd adni)
        public static List<Register.User> Felhasznalok = new List<Register.User>();

        public LoginPage()
        {
            InitializeComponent();
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
            var user = Felhasznalok.FirstOrDefault(u => u.PhoneNumber == email && u.Password == password);
            if (user == null)
            {
                ShowError("Hibás email cím vagy jelszó!");
                return;
            }

            // Logolás
            LogToFile($"Bejelentkezés: {email}, szerepkör: {user.Role}, időpont: {DateTime.Now}");

            // Jogosultságkezelés: átadható a szerepkör a főmenünek
            MessageBox.Show($"Sikeres bejelentkezés! Szerepköröd: {user.Role}", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new Mainmenu(user));
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Register());
        }

        private void guestBtn_Click(object sender, RoutedEventArgs e)
        {
            var guest = new Register.User
            {
                Username = "Vendég",
                Password = "",
                Role = "vásárló",
                JoinYear = "",
                PhoneNumber = ""
            };
            NavigationService.Navigate(new Mainmenu(guest));
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