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
        //database variables
        static String connectionString = "Server=.;Database=Restaurant-Management;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet DS = new DataSet();
        SqlDataAdapter DA = new SqlDataAdapter();
        int loggedUserID;

        //page constructor
        public AdminProductsWindow(int user)
        {
            InitializeComponent();
            loadDataGrid();
            this.loggedUserID = user;
        }
        //load datagrid
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

            DA.SelectCommand = selectCMD;
            connection.Open();
            DS.Clear();
            DA.Fill(DS, "CTEProducts");
            dataTableChosed.ItemsSource = DS.Tables["CTEProducts"].DefaultView;
            connection.Close();
        }
        //log out
        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }
        // clear text boxes
        void clearTextBoxes()
        {
            idTextBox.Clear();
            productNameTextBox.Clear();
            categoryIDTextBox.Clear();
            priceTextBox.Clear();
            ingredientsTextBox.Clear();
        }
        // add products procedures
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (idTextBox.Text == "")   //check if id is empty
            {
                var productname = productNameTextBox.Text;
                var catid = categoryIDTextBox.Text;
                var price = priceTextBox.Text;
                var ingredients = ingredientsTextBox.Text;
                if (productname == "" || catid == "" || price == "" || ingredients == "") // check if boxes are empty
                {
                    MessageBox.Show("Please fill all the gaps! ");
                    return;
                }
                //open database
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
                clearTextBoxes();
                MessageBox.Show("Procedure executed with succes!");
            }
            else
            {
                //leave id empty for this procedure
                MessageBox.Show("You can't add products with your id!\n Please leave it empty ! \n(it will be autocompleted by the server)  ");
                idTextBox.Clear();
                return;
            }
        }
        //edit button procedure
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            // function variables
            var productID = idTextBox.Text;
            var productname = productNameTextBox.Text;
            var catid = categoryIDTextBox.Text;
            var price = priceTextBox.Text;
            var ingredients = ingredientsTextBox.Text;

            //open database
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
            //close database
            connection.Close();
            loadDataGrid();
            clearTextBoxes();
            MessageBox.Show("Procedure executed with succes!");
        }
        //delete product procedure
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            //variables
            var productID = idTextBox.Text;

            //open database
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
            //close database
            connection.Close();
            loadDataGrid();
            clearTextBoxes();
            MessageBox.Show("Procedure executed with succes!");
        }
        //exit button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        // close windows in cascade
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //format only numbers
        private void idTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        //dashboard button
        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardWindow dw = new DashboardWindow(loggedUserID);
            dw.Show();
            this.Hide();
        }
        private void priceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void categoryIDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
