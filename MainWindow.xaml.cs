using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        cnAutoker cn;
        public MainWindow()
        {
            InitializeComponent();
            cn = new cnAutoker();
            DBInic();
            // MainFrame.Navigate(new LoginPage());
        }
        private void DBInic()
        {
            KezdőAdatok();
            cn.Database.EnsureCreated();
            if (cn.Kereskedes == null) return;
            if (!cn.Kereskedes.Any())
            {
                KezdőAdatok();
            }
            AdatKiír();
        }
        private void KezdőAdatok()
        {
            var c1 = new Cim { Varos = "Kecskemét", Utca = "Izsáki út", Hazszam = "10" };
            var k1 = new Autoker.Kereskedes { Nev = "JoKocsi", Jegyzekszam = "123-124-123" };
            c1.Kereskedes=k1;
            var e1 = new Elado { Nev = "Kis Pista", Telszam = "+36303527532", Szuldatum = "1989.10.05", email="kispista@gmail.com", jelszo = "kispista" };
            e1.Kereskedes1=k1;
            var a1 = new Auto { Marka = "BMW", Kivitel = "coupe", Evjarat = "2006", Uzemanyag = "Benzin", Szin = "Szürke" };
            a1.Kereskedes1 = k1;
            cn.Cims.Add(c1);
            cn.Kereskedes.Add(k1);
            cn.Autok.Add(a1);
            cn.Eladok.Add(e1);
            cn.SaveChanges();
        }
        private void AdatKiír()
        {
            var s = "";
            foreach (var p in
                cn.Kereskedes.Include(pe => pe.Cim).Include(pe => pe.Autos).Include(pe => pe.Elados).ToList())
            {
                s += p.Nev;
            }
            MessageBox.Show(s);
        }
    }
}