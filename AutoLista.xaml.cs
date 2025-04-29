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

namespace Autokereskedes
{
    /// <summary>
    /// Interaction logic for AutoLista.xaml
    /// </summary>
    public partial class AutoLista : Page
    {
        public class Auto
        {
            public int AutoID { get; set; }
            public string Marka { get; set; }
            public int Evjarat { get; set; }
            public string Kivitel { get; set; }
            public string Szin { get; set; }
            public string Garancia { get; set; }
            public string Uzemenyag { get; set; }

            public override string ToString()
            {
                return $"{Marka} - {Kivitel} - {Evjarat} - {Szin} - {Garancia} - {Uzemenyag}";
            }
        }

        private List<Auto> autoLista = new List<Auto>
        {
            new Auto { AutoID = 1, Marka = "Toyota", Evjarat = 2018, Kivitel = "Sedan", Szin = "Fehér", Garancia = "2 év", Uzemenyag = "Benzin" },
            new Auto { AutoID = 2, Marka = "BMW", Evjarat = 2020, Kivitel = "Kombi", Szin = "Fekete", Garancia = "3 év", Uzemenyag = "Dízel" },
            new Auto { AutoID = 3, Marka = "Audi", Evjarat = 2019, Kivitel = "SUV", Szin = "Kék", Garancia = "1 év", Uzemenyag = "Benzin" },
            new Auto { AutoID = 4, Marka = "Suzuki", Evjarat = 2017, Kivitel = "Hatchback", Szin = "Piros", Garancia = "Nincs", Uzemenyag = "Benzin" }
        };
        public AutoLista()
        {
            InitializeComponent();
            // IDE KELL AZ ADATBÁZISBÓL BETÖLTENI AZ AUTÓKAT, ha majd csatlakoztatod az adatbázist:
            // Példa Entity Framework esetén:
            // using (var context = new SajatDbContext())
            // {
            //     autoLista = context.Autos.ToList();
            // }
            ResultsDataGrid.ItemsSource = autoLista;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Az autók listájának inicializálása

        }

        private void keresesBtn_Click(object sender, RoutedEventArgs e)
        {
            string marka = MarkaTextBox.Text.ToLower();
            string evjarat = EvjaratTextBox.Text.ToLower();
            string uzemanyag = UzemanyagTextBox.Text.ToLower();
            string szin = SzinTextBox.Text.ToLower();
            string kivitel = KivitelTextBox.Text.ToLower();

            var talalatok = autoLista.Where(a =>
                (string.IsNullOrWhiteSpace(marka) || a.Marka.ToLower().Contains(marka)) &&
                (string.IsNullOrWhiteSpace(evjarat) || a.Evjarat.ToString().Contains(evjarat)) &&
                (string.IsNullOrWhiteSpace(uzemanyag) || a.Uzemenyag.ToLower().Contains(uzemanyag)) &&
                (string.IsNullOrWhiteSpace(szin) || a.Szin.ToLower().Contains(szin)) &&
                (string.IsNullOrWhiteSpace(kivitel) || a.Kivitel.ToLower().Contains(kivitel))
            ).ToList();

            if (talalatok.Count == 0)
            {
                MessageBox.Show("Nem található adat a megadott mezők alapján.", "Nincs találat", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ResultsDataGrid.ItemsSource = talalatok;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Kereses_TextChanged(object sender, TextChangedEventArgs e)
        {
            string marka = MarkaTextBox.Text.ToLower();
            string evjarat = EvjaratTextBox.Text.ToLower();
            string uzemanyag = UzemanyagTextBox.Text.ToLower();
            string szin = SzinTextBox.Text.ToLower();
            string kivitel = KivitelTextBox.Text.ToLower();

            var talalatok = autoLista.Where(a =>
                (string.IsNullOrWhiteSpace(marka) || a.Marka.ToLower().Contains(marka)) &&
                (string.IsNullOrWhiteSpace(evjarat) || a.Evjarat.ToString().Contains(evjarat)) &&
                (string.IsNullOrWhiteSpace(uzemanyag) || a.Uzemenyag.ToLower().Contains(uzemanyag)) &&
                (string.IsNullOrWhiteSpace(szin) || a.Szin.ToLower().Contains(szin)) &&
                (string.IsNullOrWhiteSpace(kivitel) || a.Kivitel.ToLower().Contains(kivitel))
            ).ToList();

            ResultsDataGrid.ItemsSource = talalatok;
        }
    }
}



