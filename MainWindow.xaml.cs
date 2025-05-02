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
            MainFrame.Navigate(new LoginPage());
        }
        private void DBInic()
        {
            cn.Database.EnsureCreated();
            if (cn.Kereskedes == null) return;
            if (!cn.Kereskedes.Any())
            {
                KezdőAdatok();
            }
            //AdatKiír();
        }
        private void KezdőAdatok()
        {
            var c1 = new Cim { Varos = "Kecskemét", Utca = "Izsáki út", Hazszam = "10" };
            var c2 = new Cim { Varos = "Kalocsa", Utca = "Kossuth Lajos út", Hazszam = "16" };
            var k1 = new Autoker.Kereskedes { Nev = "JoKocsi", Jegyzekszam = "123-124-123" };
            var k2 = new Autoker.Kereskedes { Nev = "Kalocsacar", Jegyzekszam = "843-128-182" };
            c1.Kereskedes=k1;
            c2.Kereskedes=k2;
            var e1 = new Elado { Nev = "Kis Pista", Telszam = "+36303527532", Szuldatum = "1989.10.05", email="kispista@gmail.com", jelszo = "kispista" };
            var e2 = new Elado { Nev = "Nagy Béla", Telszam = "+36303755312", Szuldatum = "2000.03.05", email = "nagybela@gmail.com", jelszo = "nagybela" };
            e1.Kereskedes1=k1;
            e2.Kereskedes1=k2;
            var a1 = new Auto {Marka = "BMW", Kivitel = "coupe", Evjarat = "2006", Uzemanyag = "Benzin", Szin = "Szürke" };
            var a2 = new Auto {Marka = "Audi", Kivitel = "coupe", Evjarat = "2010", Uzemanyag = "Benzin", Szin = "Fekete" };
            a1.Kereskedes1 = k1;
            a2.Kereskedes1 = k2;
            cn.Kereskedes.Add(k1);
            cn.Cims.Add(c1);
            cn.SaveChanges();
            cn.Autos.Add(a1);
            cn.Elados.Add(e1);
            cn.SaveChanges();
        }
        private void AdatKiír()
        {
            var s = "";
            foreach (var p in
                cn.Kereskedes.Include(pe => pe.Cim).Include(pe => pe.Autos).Include(pe => pe.Elados).ToList())
            {
                s += p.Nev+' '+p.Jegyzekszam+' '+p.Cim.Varos + ' ' + p.Cim.Utca + ' ' + p.Cim.Hazszam;
            }
            MessageBox.Show(s);
        }
    }
}