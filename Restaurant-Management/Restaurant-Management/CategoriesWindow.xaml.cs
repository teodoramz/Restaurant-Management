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
    /// Interaction logic for CategoriesWindow.xaml
    /// </summary>
    public partial class CategoriesWindow : Window
    {
        //database variables
        static String connectionString = "Server=.;Database=Restaurant-Management;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet DS = new DataSet();
        SqlDataAdapter DA = new SqlDataAdapter();
    
        int loggedUserID;

        //page constructor
        public CategoriesWindow(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
            clearTextBoxes();
            loadDataGrid();
        }
        //clear boxes
        void clearTextBoxes()
        {
            idTextBox.Clear();
            categoryNameTextBox.Clear();
            detailsTextBox.Clear();
        }
        //data grid loader
        public void loadDataGrid()
        {
            SqlCommand selectCMD = new SqlCommand(string.Format
                                                        (@"SELECT CategoryID as ID, CategoryName as Category, Details as Details " +
                                                            "FROM Categories"), connection);
            DA.SelectCommand = selectCMD;
            connection.Open();
            DS.Clear();
            DA.Fill(DS, "Categories");
            dataTableChosed.ItemsSource = DS.Tables["Categories"].DefaultView;
            connection.Close();
        }
        //close button 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //window pages closing in cascade
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
        //add category procedure
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (idTextBox.Text == "")       //check if boxes are empty
            {
                var categoryName = categoryNameTextBox.Text;
                var details = detailsTextBox.Text;
                if(categoryName == "" || details =="")
                {
                    MessageBox.Show("Please fill all the gaps!");
                    return;
                }

                //open database
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Categories(CategoryName, Details) Values(@catName, @details)";
                cmd.Parameters.AddWithValue("@catName", categoryName);
                cmd.Parameters.AddWithValue("@details", details);
                // MECANISM TRATARE ERORI
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Imposibil de adaugat categorii! " + error.Message);
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
                //no need of id
                MessageBox.Show("We don't need id here! Please leave it empty!");
                idTextBox.Clear();
                return;
            }
        }

        //edit category procedure
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            //function variables 
            var id = idTextBox.Text;
            var categoryName = categoryNameTextBox.Text;
            var details = detailsTextBox.Text;

            if(id == ""|| categoryName =="" || details =="")    //check if boxes are empty
            {
                MessageBox.Show("Please fill all the gaps!");
                return;
            }

            //database procedure
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE Categories
                                     SET CategoryName = @catName, 
                                            Details = @details
                                       WHERE CategoryID = @catID";
            cmd.Parameters.AddWithValue("@catName", categoryName);
            cmd.Parameters.AddWithValue("@details", details);
            cmd.Parameters.AddWithValue("@catID", id);
            // MECANISM TRATARE ERORI
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de editat categorii! " + error.Message);
                //close database
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

        //products button
        private void productsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow pw = new ProductsWindow(loggedUserID);
            pw.Show();
            this.Hide();
        }
        //categories button
        private void categoriesButton_Click(object sender, RoutedEventArgs e)
        {
            clearTextBoxes();
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
