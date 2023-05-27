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
    /// Interaction logic for Radnik.xaml
    /// </summary>
    public partial class Radnik : Window
    {

        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=NarucivanjeHrane;Integrated Security=True");


        public void ClearData()
        {
            TxtboxRadnikID.Text = "";
            TxtboxIme.Text = "";
            TxtboxPrezime.Text = "";
            TxtboxBrTel.Text = "";
            TxtboxAdresa.Text = "";
            TxtboxPib.Text = "";

        }
        public void LoadGrid()
        {

            sqlCon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Radnik";
            cmd.Connection = sqlCon;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable("Zaposleni");
            dataAdapter.Fill(dataTable);
            GridRadnik.ItemsSource = dataTable.DefaultView;

        }

        public Radnik()
        {
            InitializeComponent();
            LoadGrid();
        }


        private void BtnNazadR_Click(object sender, RoutedEventArgs e)
        {
            MainWindow objMainWindow = new MainWindow();
            Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }

        public bool isValid()
        {
            if (TxtboxRadnikID.Text == string.Empty)
            {
                MessageBox.Show("ID radnika je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxIme.Text == string.Empty)
            {
                MessageBox.Show("Ime je potrebno", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxPrezime.Text == string.Empty)
            {
                MessageBox.Show("Prezime je potrebno", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxBrTel.Text == string.Empty)
            {
                MessageBox.Show("Broj telefona je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxAdresa.Text == string.Empty)
            {
                MessageBox.Show("Adresa je potrebna", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxPib.Text == string.Empty)
            {
                MessageBox.Show("PIB je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }


        private void BtnDodajR_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (isValid())
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "INSERT INTO radnik(ime, prezime, brTel, adresa, pib) VALUES(@Ime, @Prezime, @BrTel, @Adresa, @PIB)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Ime", TxtboxIme.Text);
                    sqlCmd.Parameters.AddWithValue("@Prezime", TxtboxPrezime.Text);
                    sqlCmd.Parameters.AddWithValue("@BrTel", TxtboxBrTel.Text);
                    sqlCmd.Parameters.AddWithValue("@Adresa", TxtboxAdresa.Text);
                    sqlCmd.Parameters.AddWithValue("@PIB", TxtboxPib.Text);
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

        private void BtnIzmeniR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "UPDATE radnik SET ime = @Ime, prezime = @Prezime, BrTel = @BrTel, adresa = @Adresa, pib = @PIB WHERE RadnikID = " + TxtboxRadnikID.Text + "";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Ime", TxtboxIme.Text);
                    sqlCmd.Parameters.AddWithValue("@Prezime", TxtboxPrezime.Text);
                    sqlCmd.Parameters.AddWithValue("@BrTel", TxtboxBrTel.Text);
                    sqlCmd.Parameters.AddWithValue("@Adresa", TxtboxAdresa.Text);
                    sqlCmd.Parameters.AddWithValue("@PIB", TxtboxPib.Text);
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

        private void BtnObrisiR_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "DELETE radnik WHERE RadnikID = " + TxtboxRadnikID.Text + "";
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

        private void BtnObrisiTextR_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }
    }
}
