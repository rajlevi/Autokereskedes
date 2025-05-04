using System;
using System.IO;
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
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using Autoker;

namespace Autokereskedes
{
    /// <summary>
    /// Interaction logic for Szerzodesiras.xaml
    /// </summary>
    public partial class Szerzodesiras : Page
    {

        private void ArTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _); // Csak számokat engedélyez
        }
        public Szerzodesiras()
        {
            InitializeComponent();
            BetoltEladok();
            BetoltAutok();
        }

        private void BetoltEladok()
        {
            using (var context = new cnAutoker())
            {
                var eladok = context.Elados.ToList();
                if (eladok.Count == 0)
                {
                    MessageBox.Show("Nincsenek eladók az adatbázisban!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                EladokListBox.ItemsSource = eladok;
            }
        }

        private void BetoltAutok()
        {
            using (var context = new cnAutoker())
            {
                var autok = context.Autos.ToList();
                if (autok.Count == 0)
                {
                    MessageBox.Show("Nincsenek autók az adatbázisban!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                AutokListBox.ItemsSource = autok;
            }
        }

        private void EladokListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EladokListBox.SelectedItem is Elado kivalasztottElado)
            {
                // Az eladó adatait közvetlenül a kiválasztott objektumból használhatod
                MessageBox.Show($"Kiválasztott eladó: {kivalasztottElado.Nev}, Telefonszám: {kivalasztottElado.Telszam}");
            }
        }

        private void AutokListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AutokListBox.SelectedItem is Auto kivalasztottAuto)
            {
                // Az autó adatait közvetlenül a kiválasztott objektumból használhatod
                MessageBox.Show($"Kiválasztott autó: {kivalasztottAuto.Marka}, Típus: {kivalasztottAuto.Kivitel}, Évjárat: {kivalasztottAuto.Evjarat}");
            }
        }
        public class CustomFontResolver : IFontResolver
        {

            public string DefaultFontName => "TimesNewRoman";

            public byte[] GetFont(string faceName)
            {
                // Betűtípus betöltése a fájlrendszerből
                if (faceName == "TimesNewRoman")
                {
                    return File.ReadAllBytes(@"C:\Windows\Fonts\times.ttf"); // betűtípus fájl elérési útja
                }
                throw new ArgumentException($"A betűtípus nem található: {faceName}");
            }

            public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
            {
                // Betűtípusok hozzárendelése
                if (familyName.Equals("TimesNewRoman", StringComparison.OrdinalIgnoreCase))
                {
                    return new FontResolverInfo("TimesNewRoman");
                }
                throw new ArgumentException($"A betűtípus nem támogatott: {familyName}");
            }
        }

        private void doneBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("A dokumentum mentésre került!", "Save completed!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void pdfBtn_Click(object sender, RoutedEventArgs e)
        {

            PdfSharp.Fonts.GlobalFontSettings.FontResolver = new CustomFontResolver();
            ErrorTextBlock.Visibility = Visibility.Collapsed;
            ErrorTextBlock.Text = "";

            if (EladokListBox.SelectedItem is Elado kivalasztottElado && AutokListBox.SelectedItem is Auto kivalasztottAuto)
            {
                string eladoNev = kivalasztottElado.Nev;
                string eladoTel = kivalasztottElado.Telszam; // Telefonszámot használjuk címként
                string marka = kivalasztottAuto.Marka;
                string tipus = kivalasztottAuto.Kivitel;
                string evjarat = kivalasztottAuto.Evjarat;

                string ar = ArTextBox.Text.Trim();

                string vevoNev = VevoNevTextBox.Text.Trim();
                string vevoCim = VevoCimTextBox.Text.Trim();
                string datum = DatumPicker.SelectedDate?.ToString("yyyy.MM.dd") ?? "";

                // Validáció
                if (string.IsNullOrWhiteSpace(vevoNev) || string.IsNullOrWhiteSpace(vevoCim) || string.IsNullOrWhiteSpace(datum))
                {
                    ErrorTextBlock.Text = "Minden mező kitöltése kötelező!";
                    ErrorTextBlock.Visibility = Visibility.Visible;
                    return;
                }

                try
                {
                    // PDF generálás
                    var dlg = new Microsoft.Win32.SaveFileDialog
                    {
                        FileName = "szerzodes.pdf",
                        Filter = "PDF dokumentum (*.pdf)|*.pdf"
                    };
                    if (dlg.ShowDialog() == true)
                    {
                        var doc = new PdfDocument();
                        doc.Info.Title = "Autó adásvételi szerződés";
                        var page = doc.AddPage();
                        var gfx = XGraphics.FromPdfPage(page);
                        var font = new XFont("TimesNewRoman", 12);
                        double y = 40;
                        gfx.DrawString("Autó adásvételi szerződés", new XFont("TimesNewRoman", 16), XBrushes.Black, 40, y);
                        y += 40;
                        gfx.DrawString($"Eladó neve: {eladoNev}", font, XBrushes.Black, 40, y); y += 20;
                        gfx.DrawString($"Eladó telefonszáma: {eladoTel}", font, XBrushes.Black, 40, y); y += 30;
                        gfx.DrawString($"Vevő neve: {vevoNev}", font, XBrushes.Black, 40, y); y += 20;
                        gfx.DrawString($"Vevő címe: {vevoCim}", font, XBrushes.Black, 40, y); y += 30;
                        gfx.DrawString($"Autó márkája: {marka}", font, XBrushes.Black, 40, y); y += 20;
                        gfx.DrawString($"Autó típusa: {tipus}", font, XBrushes.Black, 40, y); y += 20;
                        gfx.DrawString($"Évjárat: {evjarat}", font, XBrushes.Black, 40, y); y += 20;
                        gfx.DrawString($"Vételár: {ar} Ft", font, XBrushes.Black, 40, y); y += 30;
                        gfx.DrawString($"Dátum: {datum}", font, XBrushes.Black, 40, y); y += 40;
                        gfx.DrawString("Eladó aláírása: ______________________", font, XBrushes.Black, 40, y); y += 30;
                        gfx.DrawString("Vevő aláírása: ______________________", font, XBrushes.Black, 40, y);
                        doc.Save(dlg.FileName);
                        MessageBox.Show("A szerződés PDF-ben elmentve!", "Siker", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba a PDF mentésekor: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Kérlek, válassz ki egy eladót és egy autót!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
