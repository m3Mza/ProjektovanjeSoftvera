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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using NarucivanjeHrane;

namespace NarucivanjeHrane
{
    /// <summary>
    /// Interaction logic for Radnja.xaml
    /// </summary>
    public partial class Radnja : Window
    {

        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=NarucivanjeHrane;Integrated Security=True");

        public void ClearData()
        {
            TxtboxPib.Text = "";
            TxtboxNaziv.Text = "";
            TxtboxAdresa.Text = "";
            TxtboxBrtel.Text = "";
            TxtboxZiroracun.Text = "";
            TxtboxPib.Text = "";

        }

        public void LoadGrid()
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Radnja";
            cmd.Connection = sqlCon;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable("Radnja");
            dataAdapter.Fill(dataTable);
            GridRadnja.ItemsSource = dataTable.DefaultView;
        }

        public Radnja()
        {
            InitializeComponent();
            LoadGrid();
        }

        private void BtnNazadRadnja_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }

        public bool isValid()
        {
            if (TxtboxNaziv.Text == string.Empty)
            {
                MessageBox.Show("Naziv je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxAdresa.Text == string.Empty)
            {
                MessageBox.Show("Adresa je potrebna", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxBrtel.Text == string.Empty)
            {
                MessageBox.Show("Br.Tel je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxZiroracun.Text == string.Empty)
            {
                MessageBox.Show("Žiro Racun je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxPib.Text == string.Empty)
            {
                MessageBox.Show("PIB je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void BtnDodajRadnja_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (isValid())
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "INSERT INTO Radnja(naziv, adresa, brtel, ziroracun) VALUES(@Naziv, @Adresa, @BrTel, @ZiroRacun)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Naziv", TxtboxNaziv.Text);
                    sqlCmd.Parameters.AddWithValue("@Adresa", TxtboxAdresa.Text);
                    sqlCmd.Parameters.AddWithValue("@BrTel", TxtboxBrtel.Text);
                    sqlCmd.Parameters.AddWithValue("@ZiroRacun", TxtboxZiroracun.Text);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    MessageBox.Show("Podaci su uspesno uneti.", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                LoadGrid();
            }
            ClearData();
        }

        private void BtnIzmeniRadnja_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (isValid())
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "UPDATE Radnja SET naziv = @Naziv, adresa = @Adresa, BrTel = @BrTel, ziroracun = @ZiroRacun WHERE PIB = " + TxtboxPib.Text + "";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Naziv", TxtboxNaziv.Text);
                    sqlCmd.Parameters.AddWithValue("@Adresa", TxtboxAdresa.Text);
                    sqlCmd.Parameters.AddWithValue("@BrTel", TxtboxBrtel.Text);
                    sqlCmd.Parameters.AddWithValue("@ZiroRacun", TxtboxZiroracun.Text);
                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                LoadGrid();
            }
            ClearData();
        }

        private void BtnObrisiRadnja_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "DELETE Radnja WHERE PIB = " + TxtboxPib.Text + "";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                MessageBox.Show("Podaci su uspesno izbrisani.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                LoadGrid();
            }
            ClearData();
        }
        private void BtnObrisiPodatke_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }
    }
}

