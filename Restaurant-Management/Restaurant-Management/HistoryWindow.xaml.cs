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
            loadDataGrid();

        }
        public void loadDataGrid()
        {
            SqlCommand selectCMD = new SqlCommand(string.Format
                                                        (@" WITH loggedUserSells AS
                                                            (
                                                              SELECT  History.IdHistory as ID, 
                                                                        Sells.IdProduct as [Product ID],  
                                                                            Sells.Quantity as Quantity
                                                                FROM Sells
                                                               INNER JOIN History
                                                               ON History.IdHistory = Sells.IdHistory
                                                               WHERE History.IdUser = @userID
                                                             )
                                                            SELECT *FROM loggedUserSells
                                                            "), connection);
            selectCMD.Parameters.AddWithValue("@userID", loggedUserID);

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

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
