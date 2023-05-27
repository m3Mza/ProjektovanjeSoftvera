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

    public partial class Pice : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=NarucivanjeHrane;Integrated Security=True");


        public void ClearData()
        {
            TxtboxPiceID.Text = "";
            TxtboxNaziv.Text = "";
            TxtboxML.Text = "";
            TxtboxCena.Text = "";

        }
        public void LoadGrid()
        {
            sqlCon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Pice";
            cmd.Connection = sqlCon;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable("Pice");
            dataAdapter.Fill(dataTable);
            GridPice.ItemsSource = dataTable.DefaultView;
        }

        public Pice()
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
            if (TxtboxPiceID.Text == string.Empty)
            {
                MessageBox.Show("ID pica je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxNaziv.Text == string.Empty)
            {
                MessageBox.Show("Naziv pica je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxML.Text == string.Empty)
            {
                MessageBox.Show("Kolicina u ml je potrebna", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxCena.Text == string.Empty)
            {
                MessageBox.Show("Cena je potrebna", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
        private void BtnDodajPice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "INSERT INTO Pice(NazivPice, ML, cena) VALUES(@NazivPice, @ML, @Cena)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@NazivPice", TxtboxNaziv.Text);
                    sqlCmd.Parameters.AddWithValue("@ML", TxtboxML.Text);
                    sqlCmd.Parameters.AddWithValue("@Cena", TxtboxCena.Text);

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
        }

        private void BtnIzmeniPice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "UPDATE Pice SET  nazivpice = @NazivPice, ml = @ML, Cena = @Cena";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@NazivPice", TxtboxNaziv.Text);
                    sqlCmd.Parameters.AddWithValue("@ML", TxtboxML.Text);
                    sqlCmd.Parameters.AddWithValue("@Cena", TxtboxCena.Text);

                    int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                    if (count == 1)
                    {
                        MessageBox.Show("Podaci su uspesno promenjeni");
                        LoadGrid();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
                LoadGrid();
            }
        }

        private void BtnObrisiPice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "delete from Pice where PiceID = " + TxtboxPiceID.Text + "";
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

        private void BtnObrisiText_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }
    }
}
