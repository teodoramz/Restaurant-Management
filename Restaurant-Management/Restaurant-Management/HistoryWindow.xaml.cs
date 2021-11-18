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
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        static String connectionString = "Server=.;Database=Restaurant-Management;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet DS = new DataSet();
        SqlDataAdapter DA = new SqlDataAdapter();
        int loggedUserID;
        int currSellID = 0;
        public HistoryWindow(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
            //            SqlCommand selectCMD = new SqlCommand(string.Format
            //             (@"with Produse as
            //(
            //	SELECT Categories.CategoryName as Categorie, 
            //           Products.ProductName  as Produs, Products.Price 
            //           FROM Products 
            //           INNER JOIN Categories 
            //           ON Products.ProductCategoryID = Categories.CategoryID
            //)
            //select * from Produse"), connection);
            clearTextBoxes();
            loadDataGrid();

        }
        public void clearTextBoxes()
        {
            idSellTextBox.Clear();
            productIDTextBox.Clear();
            quantityTextBox.Clear();
        }
        public void loadDataGrid()
        {
            SqlCommand selectCMD = new SqlCommand(string.Format
                                                        (@" WITH loggedUserSells AS
(
	                                                        SELECT Sells.IdSell as ID,
                                                                    History.IdHistory as [ID Sell], 
                                                                        Sells.IdProduct as [Product ID], 
                                                                               Sells.Quantity as Quantity
	                                                        from Sells
	                                                        inner join History
	                                                        on History.IdHistory = Sells.IdHistory
	                                                        where History.IdUser = @userID
                                                            )
                                                            select * from loggedUserSells
                                                            "), connection);
            selectCMD.Parameters.AddWithValue("@userID", this.loggedUserID);

            DA.SelectCommand = selectCMD;
            connection.Open();
            DS.Clear();
            DA.Fill(DS, "loggedUserSells");
            dataTableChosed.ItemsSource = DS.Tables["loggedUserSells"].DefaultView;
            connection.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void idSellTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void productIDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void quantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var id = idSellTextBox.Text;
            var product = productIDTextBox.Text;
            var quantity = quantityTextBox.Text;

            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE Sells
                                     SET IdProduct = @product, 
                                            Quantity = @quantity
                                       WHERE IdSell = @idSell";
            cmd.Parameters.AddWithValue("@product", product);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@idSell", id);
            // MECANISM TRATARE ERORI
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de editat vanzari! " + error.Message);
                connection.Close();
                clearTextBoxes();
                return;
            }
            connection.Close();
            loadDataGrid();
        }
        public void updateHistory2(int idSell)
        {
            int productID;
            int quantity;
            int currSellID;
            connection.Open();

            var otherCmd2 = connection.CreateCommand();
            otherCmd2.CommandType = CommandType.Text;
            otherCmd2.CommandText = "SELECT * FROM Sells WHERE IdSell = @sell";
            otherCmd2.Parameters.AddWithValue("@sell", idSell);

            using (var resultat2 = otherCmd2.ExecuteReader())
            {
                if (resultat2.Read()) // if there is a return of sql command 
                {
                    productID = resultat2.GetInt32(resultat2.GetOrdinal("IdProduct"));
                    quantity = resultat2.GetInt32(resultat2.GetOrdinal("Quantity"));
                    currSellID = resultat2.GetInt32(resultat2.GetOrdinal("IdHistory"));
                }
                else // if no return, then error
                {
                    MessageBox.Show("Error at deleting new sell! Please try again! ");
                    clearTextBoxes();
                    connection.Close();
                    return;
                }

            }


            var otherCmd = connection.CreateCommand();
            otherCmd.CommandType = CommandType.Text;
            otherCmd.CommandText = "SELECT * FROM Products WHERE ProductID = @product";
            otherCmd.Parameters.AddWithValue("@product", productID);
            decimal total = Convert.ToDecimal(quantity);

            using (var resultat = otherCmd.ExecuteReader())
            {
                if (resultat.Read()) // if there is a return of sql command 
                {
                    var price = resultat.GetDecimal(resultat.GetOrdinal("Price"));
                    total *= price;
                }
                else // if no return, then error
                {
                    MessageBox.Show("Error at deleting new sell! Please try again! ");
                    clearTextBoxes();
                    connection.Close();
                    return;
                }

            }
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE History
                                            SET TotalPrice -= @price
                                             WHERE IdHistory = @historyID";
            cmd.Parameters.AddWithValue("@price", total);
            cmd.Parameters.AddWithValue("@historyID", currSellID);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de sters vanzare! " + error.Message);
                connection.Close();
                clearTextBoxes();
                return;
            }
            connection.Close();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = idSellTextBox.Text;
            updateHistory2(Convert.ToInt32(id));
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM Sells
                                       WHERE IdSell = @idSell";
            
            cmd.Parameters.AddWithValue("@idSell", id);
            // MECANISM TRATARE ERORI
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de sters vanzari! " + error.Message);
                connection.Close();
                clearTextBoxes();
                return;
            }
            connection.Close();
            loadDataGrid();
        }


        private void productsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow pw = new ProductsWindow(loggedUserID);
            pw.Show();
            this.Hide();
        }

        private void categoriesButton_Click(object sender, RoutedEventArgs e)
        {
            CategoriesWindow cw = new CategoriesWindow(loggedUserID);
            cw.Show();
            this.Hide();
        }

        private void historyButton_Click(object sender, RoutedEventArgs e)
        {
            // clear la textboxuri / comboboxuri sau ce mai punem aici
            // in rest DO NOTHING
        }
        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }

        private void createSellButton_Click(object sender, RoutedEventArgs e)
        {

            DateTime date = DateTime.Now;
            connection.Open();

            //mai trebuie facuta verificarea pe codul generat
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO History(IdUser ,Date, TotalPrice) Values(@user, @date, 0)";
            cmd.Parameters.AddWithValue("@user", this.loggedUserID);
            cmd.Parameters.AddWithValue("@date", date);
            // MECANISM TRATARE ERORI
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de adaugat utilizator! " + error.Message);
                connection.Close();
                clearTextBoxes();
                return;
            }

            var otherCmd = connection.CreateCommand();
            otherCmd.CommandType = CommandType.Text;
            otherCmd.CommandText = "SELECT * FROM History WHERE Date = @date AND IdUser = @user";
            otherCmd.Parameters.AddWithValue("@user", this.loggedUserID);
            otherCmd.Parameters.AddWithValue("@date", date);

            using (var resultat = otherCmd.ExecuteReader())
            {
                if (resultat.Read()) // if there is a return of sql command 
                {
                    this.currSellID = resultat.GetInt32(resultat.GetOrdinal("IdHistory"));
                }
                else // if no return, then error
                {
                    MessageBox.Show("Error at creating new sell! Please try again! ");
                    clearTextBoxes();
                    connection.Close();
                    return;
                }
            }
            MessageBox.Show("Sell Procedure Created! ");
            connection.Close();
        }
        public void updateHistory(int productID, int quantity)
        {
            connection.Open();

            var otherCmd = connection.CreateCommand();
            otherCmd.CommandType = CommandType.Text;
            otherCmd.CommandText = "SELECT * FROM Products WHERE ProductID = @product";
            otherCmd.Parameters.AddWithValue("@product", productID);
            decimal total = Convert.ToDecimal(quantity);

            using (var resultat = otherCmd.ExecuteReader())
            {
                if (resultat.Read()) // if there is a return of sql command 
                {
                    var price = resultat.GetDecimal(resultat.GetOrdinal("Price"));
                    total *= price;
                }
                else // if no return, then error
                {
                    MessageBox.Show("Error at adding new sell! Please try again! ");
                    clearTextBoxes();
                    connection.Close();
                    return;
                }

            }
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE History
                                            SET TotalPrice += @price
                                             WHERE IdHistory = @historyID";
            cmd.Parameters.AddWithValue("@price", total);
            cmd.Parameters.AddWithValue("@historyID", this.currSellID);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("2 Imposibil de adaugat vanzare! " + error.Message);
                connection.Close();
                clearTextBoxes();
                return;
            }
            connection.Close();
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if(idSellTextBox.Text != "")
            {
                MessageBox.Show("Please leave ID box empty for this procedure! ");
                idSellTextBox.Clear();
                return;
            }
            if (this.currSellID != 0)
            {
                var product = productIDTextBox.Text;
                var quantity = quantityTextBox.Text;
                connection.Open();

                //mai trebuie facuta verificarea pe codul generat
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Sells(IdHistory , IdProduct, Quantity) Values(@historyID, @productID, @quantity)";
                cmd.Parameters.AddWithValue("@historyID", this.currSellID);
                cmd.Parameters.AddWithValue("@productID", product);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                // MECANISM TRATARE ERORI
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Imposibil 1 de adaugat vanzare! " + error.Message);
                    connection.Close();
                    clearTextBoxes();
                    return;
                }
                connection.Close();
                updateHistory(Convert.ToInt32(product), Convert.ToInt32(quantity));
            }
            else
            {
                MessageBox.Show("First create sell procedure! ");
                return;
            }
            loadDataGrid();
        }
       
    }
}
