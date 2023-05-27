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

    public partial class Hrana : Window
    {
        private const string V = "UPDATE hrana SET naziv = @Naziv, gramaza = @Gramaza, Cena = @Cena";
        SqlConnection sqlCon = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=NarucivanjeHrane;Integrated Security=True");

        public static string V1 => V;

        public void ClearData()
        {
            TxtboxHranaID.Text = "";
            TxtboxNaziv.Text = "";
            TxtboxGramaza.Text = "";
            TxtboxCena.Text = "";
        }
        public void LoadGrid()
        {

            sqlCon.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from Hrana";
            cmd.Connection = sqlCon;
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable("Hrana");
            dataAdapter.Fill(dataTable);
            GridHrana.ItemsSource = dataTable.DefaultView;
        }

        public Hrana()
        {
            InitializeComponent();
            LoadGrid();
        }

        private void BtnNazad_Click(object sender, RoutedEventArgs e)
        {
            NarucivanjeHrane.MainWindow objMainWindow = new MainWindow();
            Visibility = Visibility.Hidden;
            objMainWindow.Show();
        }

        public bool isValid()
        {
            if (TxtboxHranaID.Text == string.Empty)
            {
                MessageBox.Show("ID hrane je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxNaziv.Text == string.Empty)
            {
                MessageBox.Show("Naziv je potreban", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxGramaza.Text == string.Empty)
            {
                MessageBox.Show("Gramaza je potrebna", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtboxCena.Text == string.Empty)
            {
                MessageBox.Show("Cena je potrebna", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }



            return true;
        }
        private void BtnDodajJelo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {

                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = "INSERT INTO Hrana(naziv, gramaza, cena) VALUES(@Naziv, @Gramaza, @Cena)";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Naziv", TxtboxNaziv.Text);
                    sqlCmd.Parameters.AddWithValue("@Gramaza", TxtboxGramaza.Text);
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

        private void BtnIzmeniJelo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    if (sqlCon.State == ConnectionState.Closed)
                        sqlCon.Open();
                    string query = V1;
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.CommandType = CommandType.Text;
                    sqlCmd.Parameters.AddWithValue("@Naziv", TxtboxNaziv.Text);
                    sqlCmd.Parameters.AddWithValue("@Marka", TxtboxGramaza.Text);
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

        private void BtnObrisiJelo_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                string query = "delete from Hrana where HranaID = " + TxtboxHranaID.Text + "";
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

