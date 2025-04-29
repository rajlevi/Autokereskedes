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

namespace Autokereskedes
{
    /// <summary>
    /// Interaction logic for Szerzodesiras.xaml
    /// </summary>
    public partial class Szerzodesiras : Page
    {
        public Szerzodesiras()
        {
            InitializeComponent();
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

            // Adatok beolvasása
            string eladoNev = EladoNevTextBox.Text.Trim();
            string eladoCim = EladoCimTextBox.Text.Trim();
            string vevoNev = VevoNevTextBox.Text.Trim();
            string vevoCim = VevoCimTextBox.Text.Trim();
            string marka = MarkaTextBox.Text.Trim();
            string tipus = TipusTextBox.Text.Trim();
            string evjarat = EvjaratTextBox.Text.Trim();
            string ar = ArTextBox.Text.Trim();
            string datum = DatumPicker.SelectedDate?.ToString("yyyy.MM.dd") ?? "";

            // Validáció
            if (string.IsNullOrWhiteSpace(eladoNev) || string.IsNullOrWhiteSpace(eladoCim) ||
                string.IsNullOrWhiteSpace(vevoNev) || string.IsNullOrWhiteSpace(vevoCim) ||
                string.IsNullOrWhiteSpace(marka) || string.IsNullOrWhiteSpace(tipus) ||
                string.IsNullOrWhiteSpace(evjarat) || string.IsNullOrWhiteSpace(ar) || string.IsNullOrWhiteSpace(datum))
            {
                ErrorTextBlock.Text = "Minden mező kitöltése kötelező!";
                ErrorTextBlock.Visibility = Visibility.Visible;
                return;
            }

            try
            {
                // PDF generálás (PdfSharp szükséges!)
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
                    gfx.DrawString($"Eladó címe: {eladoCim}", font, XBrushes.Black, 40, y); y += 30;
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
    }
}
