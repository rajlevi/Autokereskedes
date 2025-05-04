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
    /// Interaction logic for Mainmenu.xaml
    /// </summary>
    public partial class Mainmenu : Page
    {
        cnAutoker cn;
        private string role;
        private string username;
        public Mainmenu(string username,string role)
        {
            InitializeComponent();
            cn = new cnAutoker();
            this.username = username;
            this.role = role;
            if (username != "Vendég")
            {
                UserInfoTextBlock.Text = $"Bejelentkezve: {username} ({role})";
                szerzodesBtn.IsEnabled = true;
            }
            else
            {
                UserInfoTextBlock.Text = "Nincs bejelentkezett felhasználó!";
                szerzodesBtn.IsEnabled = false;
            }
        }

        private void autokBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AutoLista());
        }

        private void kereskedesekBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KereskedesLista());
        }

        private void szerzodesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (username != "Vendég" && role == "Admin")
            {
                NavigationService.Navigate(new Szerzodesiras());
            }
            else
            {
                MessageBox.Show("Nincs jogosultságod!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
