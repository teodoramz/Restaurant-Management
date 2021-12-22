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
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        //database variables
        static String connectionString = "Server=.;Database=Restaurant-Management;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet DS = new DataSet();
        SqlDataAdapter DA = new SqlDataAdapter();
        int loggedUserID;

        //page constructor
        public ProductsWindow(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
            loadDataGrid();
        }

        //load datagrid
        public void loadDataGrid()
        {
            SqlCommand selectCMD = new SqlCommand(string.Format
                                                        (@" WITH CTEProducts AS
                                                            (
                                                              SELECT ProductID as ID
                                                              ,ProductCategoryID as [Category ID]
                                                              ,ProductName as [Product name]
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
        //close button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //close windows in cascade
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //only numbers format
        private void idTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void categoryTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void priceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        //clear text boxes
        void clearTextBoxes()
        {
            idTextBox.Clear();
            productNameTextBox.Clear();
            categoryIDTextBox.Clear();
            priceTextBox.Clear();
            ingredientsTextBox.Clear();
        }
        //add product procedure
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (idTextBox.Text == "")
            {
                //funtion variables
                var productname = productNameTextBox.Text;
                var catid = categoryIDTextBox.Text;
                var price = priceTextBox.Text;
                var ingredients = ingredientsTextBox.Text;
                if (productname == "" || catid == "" ||
                           price == "" || ingredients == "")
                {
                    MessageBox.Show("Please fill all the gaps!");
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
                MessageBox.Show("We don't need id here! Please leave that box empty!");
                idTextBox.Clear();
                return;
            }
        }
        //edit products procedure
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            //function variables
            var productID = idTextBox.Text;
            var productname = productNameTextBox.Text;
            var catid = categoryIDTextBox.Text;
            var price = priceTextBox.Text;
            var ingredients = ingredientsTextBox.Text;
            //check if boxes are empty
            if (productID == "" || productname == "" || catid == "" ||
                          price == "" || ingredients == "")
            {
                MessageBox.Show("Please fill all the gaps!");
                return;
            }

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
            connection.Close();
            loadDataGrid();
            clearTextBoxes();
            MessageBox.Show("Procedure executed with succes!");
        }
        //delete procedure
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (productNameTextBox.Text != "" || categoryIDTextBox.Text != "" || priceTextBox.Text != "" || ingredientsTextBox.Text != "")
            {
                MessageBox.Show("We only need Id!");
                productNameTextBox.Clear();
                categoryIDTextBox.Clear();
                priceTextBox.Clear();
                ingredientsTextBox.Clear();
                return;
            }
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
            connection.Close();
            loadDataGrid();
            clearTextBoxes();
            MessageBox.Show("Procedure executed with succes!");
        }

        //products button
        private void productsButton_Click(object sender, RoutedEventArgs e)
        {
            clearTextBoxes();
        }
        //categories button
        private void categoriesButton_Click(object sender, RoutedEventArgs e)
        {
            CategoriesWindow cw = new CategoriesWindow(loggedUserID);
            cw.Show();
            this.Hide();
        }
        //history button
        private void historyButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow hw = new HistoryWindow(loggedUserID);
            hw.Show();
            this.Hide();
        }
        //log out
        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }
    }
}
