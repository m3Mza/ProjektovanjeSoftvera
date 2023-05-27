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
    /// Interaction logic for Prodaja.xaml
    /// </summary>
    public partial class Prodaja : Window
    {

        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=NarucivanjeHrane;Integrated Security=True");

        public void ClearData()
        {
            TxtboxProdajaID.Text = "";
            TxtboxHranaID.Text = "";
            TxtboxHranaID.Text = "";
            TxtboxPiceID.Text = "";
        }

        public void LoadGrid()
        {

            sqlCon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Prodaja";
            cmd.Connection = sqlCon;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable("Prodaja");
            dataAdapter.Fill(dataTable);
            GridProdaja.ItemsSource = dataTable.DefaultView;

        }

        public Prodaja()
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
            if (TxtboxProdajaID.Text == string.Empty)
            {
                MessageBox.Show("ID prodaje je potrebna", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (DatePicker.Text == string.Empty)
            {
                MessageBox.Show("Datum je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxHranaID.Text == string.Empty)
            {
                MessageBox.Show("ID Hrane je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxPiceID.Text == string.Empty)
            {
                MessageBox.Show("ID Pica je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }


            return true;
        }

        private void BtnDodajP_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (isValid())
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "INSERT INTO Prodaja(Datum, HranaID, PiceID) VALUES(@Datum, @HranaID, @PiceID)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Datum", DatePicker.SelectedDate);
                    sqlCmd.Parameters.AddWithValue("@HranaID", TxtboxHranaID.Text);
                    sqlCmd.Parameters.AddWithValue("@PiceID", TxtboxPiceID.Text);
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

        private void BtnIzmeniP_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                if (isValid())
                {

                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "UPDATE Prodaja SET  Datum = @Datum, hranaID = @HranaID, piceID= @PiceID, WHERE ProdajaID = " + TxtboxProdajaID.Text + "";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Datum", DatePicker.SelectedDate);
                    sqlCmd.Parameters.AddWithValue("@HranaID", TxtboxHranaID.Text);
                    sqlCmd.Parameters.AddWithValue("@PiceID", TxtboxPiceID.Text);
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

        private void BtnObrisiP_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "delete from Prodaja where ProdajaID = " + TxtboxProdajaID.Text + "";
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

        private void BtnObrisiTextP_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }
    }
}
