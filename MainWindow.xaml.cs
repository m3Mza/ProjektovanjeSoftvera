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

namespace NarucivanjeHrane
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnRadnja_Click(object sender, RoutedEventArgs e)
        {
            Radnja objRadnja = new Radnja();
            Visibility = Visibility.Hidden;
            objRadnja.Show();
        }

        private void BtnHrana_Click(object sender, RoutedEventArgs e)
        {
            Hrana objHrana = new Hrana();
            Visibility = Visibility.Hidden;
            objHrana.Show();
        }

        private void BtnRadnik_Click(object sender, RoutedEventArgs e)
        {
            Radnik objRadnik = new Radnik();
            Visibility = Visibility.Hidden;
            objRadnik.Show();
        }

        private void BtnPice_Click(object sender, RoutedEventArgs e)
        {
            Pice objPice = new Pice();
            Visibility = Visibility.Hidden;
            objPice.Show();
        }

        private void BtnProdaja_Click(object sender, RoutedEventArgs e)
        {
            Prodaja objProdaja = new Prodaja();
            Visibility = Visibility.Hidden;
            objProdaja.Show();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
