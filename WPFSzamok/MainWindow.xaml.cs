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
using System.IO;

namespace WPFSzamok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<KvizFeladat> feladatok;
        private KvizFeladat selectedFeladat;
        private int pontszam;

        public MainWindow()
        {
            feladatok = new();
            InitializeComponent();
            btEll.IsEnabled = false;
            FajlFeltolt();
            Fel2();
            Fel3();
            Fel4();
            Fel5();
            Fel7();
        }

        void FajlFeltolt()
        {
            using (StreamReader sr = new StreamReader("felszam.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string sor1 = sr.ReadLine();
                    string sor2 = sr.ReadLine();

                    if (sor2 != null)
                    {
                        KvizFeladat feladat = new(sor1, sor2);
                        feladatok.Add(feladat);
                    }
                }
            }
        }

        void Fel2()
        {
            //feladatok száma
            tbFel2.Text = feladatok.Count.ToString();
        }

        void Fel3()
        {
            var matek = feladatok.Where(x => x.Kategoria == "matematika").ToList();
            string count = matek.Count.ToString();
            string egyPont = matek.Where(x => x.Pont == 1).Count().ToString();
            string kettoPont = matek.Where(x => x.Pont == 2).Count().ToString();
            string haromPont = matek.Where(x => x.Pont == 3).Count().ToString();

            string labelText = string.Format($" Az adatfájlban {count} matematika feladat van\n1 pontot ér {egyPont} feladat\n2 pontot ér {kettoPont} feladat\n3 pontot ér {haromPont} feladat");
            lbFel3.Text = labelText;
        }

        void Fel4()
        {
            var valaszokMin = feladatok.Min(x => x.Valasz);
            var valaszokMax = feladatok.Max(x => x.Valasz);
            tbFel4a.Text = valaszokMin.ToString();
            tbFel4b.Text = valaszokMax.ToString();
        }

        void Fel5()
        {
            var temaKorok = feladatok.Select(x => x.Kategoria).Distinct().ToList();
            cbFel5.ItemsSource = temaKorok;
        }

        /// <summary>
        /// Véletlenszerűen kiválaszt egy kérdést a kiválasztott témakör alapján.
        /// </summary>
        void Fel6()
        {
            string selectedItem = cbFel5.SelectedItem.ToString();

            int random = new Random().Next(0, feladatok.Where(x => x.Kategoria == selectedItem).Count());

            string kerdesFromSelectedItem = feladatok.Where(x => x.Kategoria == selectedItem).Select(x => x.Kerdes).ToList()[random];

            tbKerdes.Content = kerdesFromSelectedItem;

            selectedFeladat = feladatok.Where(x => x.Kerdes == kerdesFromSelectedItem).ToList()[0];
        }

        private void cbFel5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFel5.SelectedItem != null)
            {
                Fel6();
                btEll.IsEnabled = true;
            }
            else
            {
                btEll.IsEnabled = false;
            }
        }

        private void btEll_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(tbValasz.Text, out int valasz) && valasz == selectedFeladat.Valasz)
            {
                MessageBox.Show($"Pontszám: {pontszam += selectedFeladat.Pont}");
            }
            else
            {
                MessageBox.Show($"Pontszám: {pontszam}\nHelyes válasz: {selectedFeladat.Valasz}");
            }

            Fel6();
        }

        void Fel7()
        {
            var random = new Random();
            var kivKerdes = new HashSet<KvizFeladat>();
            int osszPont = 0;

            while (kivKerdes.Count < 10)
            {
                var kerdes = feladatok[random.Next(feladatok.Count)];
                if (kivKerdes.Add(kerdes))
                {
                    osszPont += kerdes.Pont;
                }
            }

            using (var sw = new StreamWriter("tesztfel.txt"))
            {
                foreach (var kerdes in kivKerdes)
                {
                    sw.WriteLine($"{kerdes.Pont} {kerdes.Valasz} {kerdes.Kerdes}");
                }
                sw.WriteLine($"A feladatsorra osszesen {osszPont} pont adhato.");
            }
        }
    }
}