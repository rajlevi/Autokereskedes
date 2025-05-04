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
    /// Interaction logic for KereskedesLista.xaml
    /// </summary>
    public partial class KereskedesLista : Page
    {
        cnAutoker cn;
        /*{
            new Kereskedes { KereskedesID = 1, Nev = "Karcsi Autóház", Varos = "Budapest", Utca = "Fő utca 1.", Jegyzekszam = "12345", Szerviz = "Van", Ferohely = 50 },
            new Kereskedes { KereskedesID = 2, Nev = "MaxiCar", Varos = "Debrecen", Utca = "Kossuth tér 2.", Jegyzekszam = "23456", Szerviz = "Nincs", Ferohely = 30 },
            new Kereskedes { KereskedesID = 3, Nev = "AutoPlaza", Varos = "Győr", Utca = "Petőfi S. u. 3.", Jegyzekszam = "34567", Szerviz = "Van", Ferohely = 40 },
            new Kereskedes { KereskedesID = 4, Nev = "CityAutó", Varos = "Szeged", Utca = "Dóm tér 4.", Jegyzekszam = "45678", Szerviz = "Nincs", Ferohely = 25 },
            new Kereskedes { KereskedesID = 5, Nev = "LuxCar", Varos = "Budapest", Utca = "Andrássy út 5.", Jegyzekszam = "56789", Szerviz = "Van", Ferohely = 60 }
        };*/

        public KereskedesLista()
        {
            InitializeComponent();
            cn = new cnAutoker();
            var combinedList = cn.Kereskedes.Include(k => k.Cim).Select(k => new
            {
                k.KereskedesId,
                k.Nev,
                k.Jegyzekszam,
                k.Cim.Varos,
                k.Cim.Utca,
                k.Cim.Hazszam
            }).ToList();
            ResultsDataGrid.ItemsSource = combinedList;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        }
    }

