using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Restaurant_Management
{
    /// <summary>
    /// Interaction logic for AdminProductsWindow.xaml
    /// </summary>
    public partial class AdminProductsWindow : Window
    {
        int loggedUserID;
        static String connectionString = "Server=.;Database=Restaurant-Management;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet DS = new DataSet();
        SqlDataAdapter DA = new SqlDataAdapter();
        

        public AdminProductsWindow()
        {
            InitializeComponent();
            loadDataGrid();
        }
        public void loadDataGrid()
        {
            SqlCommand selectCMD = new SqlCommand(string.Format
                                                        (@" WITH CTEProducts AS
                                                            (
                                                              SELECT ProductID as ID
                                                              ,ProductCategoryID as ProductCategoryID
                                                              ,ProductName as Name
                                                              ,Price as Price
                                                              ,Ingredients as Ingredients
                                                               FROM Products
                                                             )
                                                            SELECT *FROM CTEProducts
                                                            "), connection);
            // selectCMD.Parameters.AddWithValue("@userID", lo);

            DA.SelectCommand = selectCMD;
            connection.Open();
            DS.Clear();
            DA.Fill(DS, "CTEProducts");
            dataTableChosed.ItemsSource = DS.Tables["CTEProducts"].DefaultView;
            connection.Close();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }
        void clearTextBoxes()
        {
            idTextBox.Clear();
            productNameTextBox.Clear();
            categoryIDTextBox.Clear();
            priceTextBox.Clear();
            ingredientsTextBox.Clear();
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var productname = productNameTextBox.Text;
            var catid = categoryIDTextBox.Text;
            var price = priceTextBox.Text;
            var ingredients = ingredientsTextBox.Text;
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO Products(ProductCategoryID,ProductName,Price, Ingredients) Values(@catid,@productname,@price,@ingredients)";

            cmd.Parameters.AddWithValue("@productname", productname);
            cmd.Parameters.AddWithValue("@catid", catid);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@ingredients", ingredients);
            // MECANISM TRATARE ERORI
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de adaugat produsul! " + error.Message);
                connection.Close();
                clearTextBoxes();
                return;
            }
            connection.Close();
            loadDataGrid();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var productID = idTextBox.Text;
            var productname = productNameTextBox.Text;
            var catid = categoryIDTextBox.Text;
            var price = priceTextBox.Text;
            var ingredients = ingredientsTextBox.Text;
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE Products 
                                SET ProductCategoryID=@catid,
                                    ProductName=@productname,
                                    Price=@price, 
                                    Ingredients=@ingredients 
                                    WHERE ProductID=@productID";
            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@productname", productname);
            cmd.Parameters.AddWithValue("@catid", catid);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@ingredients", ingredients);
            // MECANISM TRATARE ERORI
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de editat produsul! " + error.Message);
                connection.Close();
                clearTextBoxes();
                return;
            }
            connection.Close();
            loadDataGrid();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var productID = idTextBox.Text;

            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM Products 
                                WHERE ProductID=@productID";
            cmd.Parameters.AddWithValue("@productID", productID);

            // MECANISM TRATARE ERORI
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de editat produsul! " + error.Message);
                connection.Close();
                clearTextBoxes();
                return;
            }
            connection.Close();
            loadDataGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void idTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardWindow dw = new DashboardWindow(loggedUserID);
            dw.Show();
            this.Hide();
        }

    }
}
