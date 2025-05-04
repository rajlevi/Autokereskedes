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
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Autoker;

namespace Autokereskedes
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    
    public partial class Register : Page
    {
        cnAutoker cn;
        /*public class User
        {
            public string Username { get; set; }
            public string JoinYear { get; set; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
        }*/

        List<Autoker.Elado> users = new List<Autoker.Elado>();
        public Register()
        {
            InitializeComponent();
            cn = new cnAutoker();
            users.AddRange(cn.Elados.ToList());
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;
            string telszam = PhoneBox.Text;
            string joinDate = (JoinDatePicker.Value).ToString();

            ErrorTextBlock.Visibility = Visibility.Collapsed;
            ErrorTextBlock.Text = "";

            // Validáció
            if (string.IsNullOrWhiteSpace(fullName))
            {
                ShowError("A teljes név megadása kötelező!");
                return;
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                ShowError("Az email cím megadása kötelező!");
                return;
            }
            if (!IsValidEmail(email))
            {
                ShowError("Az email cím formátuma érvénytelen!");
                return;
            }
            if (joinDate == null)
            {
                ShowError("A csatlakozás dátuma megadása kötelező!");
                return;
            }
            if (telszam == null)
            {
                ShowError("Telefonszám megadása kötelező!");
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                ShowError("A jelszó megadása kötelező!");
                return;
            }
            if (password.Length < 6)
            {
                ShowError("A jelszónak legalább 6 karakter hosszúnak kell lennie!");
                return;
            }
            if (password != confirmPassword)
            {
                ShowError("A két jelszó nem egyezik!");
                return;
            }

            // Jogosultságkezelés: az alap regisztrált admin, a többi felhasználó
            // Új felhasználó hozzáadása
            
            var e1 = new Elado { Nev = fullName, Telszam = telszam, Szuldatum = joinDate, email = email, jelszo = password, Admin = false, KereskedesId=0};
            cn.SaveChanges();

            // Logolás
            LogToFile($"Regisztráció: {fullName}, {email}, {joinDate}, szerepkör: user, időpont: {DateTime.Now}");

            // Sikeres regisztráció
            MessageBox.Show($"Sikeres regisztráció! Most jelentkezz be.\nSzerepköröd: user", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
            this.NavigationService.Navigate(new LoginPage());
        }

        private void ShowError(string message)
        {
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Visibility.Visible;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
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
