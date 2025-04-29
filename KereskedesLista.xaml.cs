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
    /// Interaction logic for KereskedesLista.xaml
    /// </summary>
    public partial class KereskedesLista : Page
    {
        private List<Kereskedes> kereskedesek = new List<Kereskedes>
        {
            new Kereskedes { KereskedesID = 1, Nev = "Karcsi Autóház", Varos = "Budapest", Utca = "Fő utca 1.", Jegyzekszam = "12345", Szerviz = "Van", Ferohely = 50 },
            new Kereskedes { KereskedesID = 2, Nev = "MaxiCar", Varos = "Debrecen", Utca = "Kossuth tér 2.", Jegyzekszam = "23456", Szerviz = "Nincs", Ferohely = 30 },
            new Kereskedes { KereskedesID = 3, Nev = "AutoPlaza", Varos = "Győr", Utca = "Petőfi S. u. 3.", Jegyzekszam = "34567", Szerviz = "Van", Ferohely = 40 },
            new Kereskedes { KereskedesID = 4, Nev = "CityAutó", Varos = "Szeged", Utca = "Dóm tér 4.", Jegyzekszam = "45678", Szerviz = "Nincs", Ferohely = 25 },
            new Kereskedes { KereskedesID = 5, Nev = "LuxCar", Varos = "Budapest", Utca = "Andrássy út 5.", Jegyzekszam = "56789", Szerviz = "Van", Ferohely = 60 }
        };

        public KereskedesLista()
        {
            InitializeComponent();
            // IDE KELL AZ ADATBÁZISBÓL BETÖLTENI A KERESKEDÉSEKET, ha majd csatlakoztatod az adatbázist:
            // Példa Entity Framework esetén:
            // using (var context = new SajatDbContext())
            // {
            //     kereskedesek = context.Kereskedesek.ToList();
            // }
            ResultsDataGrid.ItemsSource = kereskedesek;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }



        private void Kereses_TextChanged(object sender, TextChangedEventArgs e)
        {
            string nev = NevTextBox.Text.ToLower();
            string varos = VarosTextBox.Text.ToLower();
            string utca = UtcaTextBox.Text.ToLower();
            string jegyzekszam = JegyzekszamTextBox.Text.ToLower();
            string szerviz = SzervizTextBox.Text.ToLower();
            string ferohely = FerohelyTextBox.Text.ToLower();

            var talalatok = kereskedesek.Where(k =>
                (string.IsNullOrWhiteSpace(nev) || k.Nev.ToLower().Contains(nev)) &&
                (string.IsNullOrWhiteSpace(varos) || k.Varos.ToLower().Contains(varos)) &&
                (string.IsNullOrWhiteSpace(utca) || k.Utca.ToLower().Contains(utca)) &&
                (string.IsNullOrWhiteSpace(jegyzekszam) || k.Jegyzekszam.ToLower().Contains(jegyzekszam)) &&
                (string.IsNullOrWhiteSpace(szerviz) || k.Szerviz.ToLower().Contains(szerviz)) &&
                (string.IsNullOrWhiteSpace(ferohely) || k.Ferohely.ToString().Contains(ferohely))
            ).ToList();

            ResultsDataGrid.ItemsSource = talalatok;
        }
    }

    public class Kereskedes
    {
        public int KereskedesID { get; set; }
        public string Nev { get; set; }
        public string Varos { get; set; }
        public string Utca { get; set; }
        public string Jegyzekszam { get; set; }
        public string Szerviz { get; set; }
        public int Ferohely { get; set; }
    }
}
