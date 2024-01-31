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
        public MainWindow()
        {
            feladatok = new();
            InitializeComponent();
            FajlFeltolt();
            Fel2();
            Fel3();
            Fel4();
            Fel5();
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

            string labelText =string.Format( $" Az adatfájlban {count} matematika feladat van\n1 pontot ér {egyPont} feladat\n2 pontot ér {kettoPont} feladat\n3 pontot ér {haromPont} feladat");
            lbFel3.Text = labelText;
            
        }

        void Fel4()
        {
            var valaszokMin= feladatok.Min(x => x.Valasz);
            var valaszokMax = feladatok.Max(x => x.Valasz);
            tbFel4a.Text=valaszokMin.ToString();
            tbFel4b.Text = valaszokMax.ToString();
            
        }

        void Fel5()
        {
            var temaKorok = feladatok.Select(x => x.Kategoria).Distinct().ToList();
            cbFel5.ItemsSource = temaKorok;
        }


    }
}