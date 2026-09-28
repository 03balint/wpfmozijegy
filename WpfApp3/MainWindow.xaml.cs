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
using System.Xml.Linq;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Foglalas(object sender, RoutedEventArgs e)
        {
            try
            {
                int age = Convert.ToInt32(inputAge.Text);
                if (age <= 0)
                {
                    MessageBox.Show("Az életkor pozitív legyen!");
                    return;
                }
                if (!IsNameValid())
                {
                    return;
                }
                if (!IsFilmValid())
                {
                    return;
                }
                if (!IsTicketValid())
                {
                    return;
                }
                if (!IsTicketNumberValid())
                {
                    return;
                }
                if (termsCB.IsChecked == false)
                {
                    MessageBox.Show("A feltételeket fogadd el!");
                    return;
                }



            }

            catch(FormatException)
            {
                MessageBox.Show("Az életkorhoz számot adj meg!","Hibás adat!",MessageBoxButton.OK, MessageBoxImage.Warning);
                inputAge.Focus();
                return;
            }
            catch(OverflowException)
            {
                MessageBox.Show("Az életkorhoz számot adj meg!", "Hibás adat!", MessageBoxButton.OK, MessageBoxImage.Warning);
                inputAge.Focus();
                return;
            }

            string nev = inputName.Text;
            int kor = Convert.ToInt32(inputAge.Text);
            ComboBoxItem film = (ComboBoxItem)filmValaszt.SelectedItem;
            string jegytipus="";
            if (normalRB.IsChecked == true)
            {
                jegytipus = "Normál";
            }
            else if(diakRB.IsChecked == true)
            {
                jegytipus = "Diák";
            }
            else if(vipRB.IsChecked == true)
            {
                jegytipus = "VIP";
            }
            int jegyekszama = Convert.ToInt32(inputTicketNumber.Text);
            
            int fizetendo = 0;

            List<string> extrak=new List<string>();
            if (popcornCB.IsChecked == true)
            {
                extrak.Add( "Popcorn");
                fizetendo += 1200;
            }
            if (uditoCB.IsChecked == true)
            {
                extrak.Add( "Üdítő");
                fizetendo += 800;
            }
            if (szemuvegCB.IsChecked == true)
            {
                extrak.Add("3D szemüveg");
                fizetendo += 500;
            }

            string extrakstring = "";
            foreach (var i in extrak)
            {
                extrakstring += i;
                extrakstring += ", ";
            }

            if (jegytipus == "Normál")
            {
                fizetendo += jegyekszama * 2500;
            }
            else if (jegytipus == "Diák")
            {
                fizetendo += jegyekszama * 1900;
            }
            else if (jegytipus == "VIP")
            {
                fizetendo += jegyekszama * 4000;
            }





            MessageBox.Show($"Sikeres foglalás!\n\nNév: {nev}\nÉletkor: {kor}\nFilm: {film.Content}\nJegytípus: {jegytipus}\nJegyek Száma: {jegyekszama}\nExtrák: {extrakstring}\n\nFizetendő:{fizetendo}Ft", "Foglalás", MessageBoxButton.OK, MessageBoxImage.Information);



        }

        private bool IsNameValid()
        {
            string name = inputName.Text.Trim();
            if (name == "")
            {
                MessageBox.Show("A név nem lehet üres!");
                inputName.Focus();
                return false;
            }
            if (name.Length<3)
            {
                MessageBox.Show("A név legalább 3 karakter legyen!");
                inputName.Focus();
                return false;
            }
            return true;
        }

        private bool IsFilmValid()
        {
            ComboBoxItem selectedMovie = (ComboBoxItem)filmValaszt.SelectedItem;
            
            if (selectedMovie == null || selectedMovie.Content.ToString() == "" )
            {
                MessageBox.Show("Válasszon egy filmet!");
                filmValaszt.Focus();
                return false;
            }
            return true;
        }

        private bool IsTicketValid()
        {

            if(normalRB.IsChecked==false &&  diakRB.IsChecked == false && vipRB.IsChecked == false)
            {
                MessageBox.Show("Válasszon egy jegytípust!");
                normalRB.Focus();
                return false;
            }
            return true;
        }

        private bool IsTicketNumberValid()
        {
            
            int jegyekSzama= Convert.ToInt32(inputTicketNumber.Text);

            try 
            {
                if (jegyekSzama < 1 || jegyekSzama > 10)
                {
                    MessageBox.Show("1-10 darab jegyet lehet venni!");
                    return false;
                }
            }

            catch (FormatException)
            {
                MessageBox.Show("A jegyek számához számot adj meg!", "Hibás adat!", MessageBoxButton.OK, MessageBoxImage.Warning);
                inputTicketNumber.Focus();
            }
            catch (OverflowException)
            {
                MessageBox.Show("A jegyek számához számot adj meg!", "Hibás adat!", MessageBoxButton.OK, MessageBoxImage.Warning);
                inputTicketNumber.Focus();
            }

            return true;
        }
    }
}