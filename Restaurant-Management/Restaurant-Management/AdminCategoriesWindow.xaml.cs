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
    /// Interaction logic for AdminCategoriesWindow.xaml
    /// </summary>
    public partial class AdminCategoriesWindow : Window
    {
        //database variables
        static String connectionString = "Server=.;Database=Restaurant-Management;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet DS = new DataSet();
        SqlDataAdapter DA = new SqlDataAdapter();

        int loggedUserID;
        //constructor for this window
        public AdminCategoriesWindow(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
            clearTextBoxes();
            loadDataGrid();
        }
        //clearTextBoxes
        void clearTextBoxes()
        {
            idTextBox.Clear();
            categoryNameTextBox.Clear();
            detailsTextBox.Clear();
        }
        //load data grid
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
        // add new category procedure
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (idTextBox.Text == "")           //we don't need id
            {
                var categoryName = categoryNameTextBox.Text;
                var details = detailsTextBox.Text;
                if(categoryName == "" || details =="")     //check if the boxes are empty
                {
                    MessageBox.Show("Please fill all the gaps! ");
                    return;
                }

                //open db connection 
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
                MessageBox.Show("You can't add categories with your id!\n Please leave it empty ! \n(it will be autocompleted by the server)  ");
                idTextBox.Clear();
                return;
            }
        }
        //log out
        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }
        //dashboard 
        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardWindow dw = new DashboardWindow(loggedUserID);
            dw.Show();
            this.Hide();
        }
        //close button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        // closing in cascade
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //allowing only numbers
        private void idTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        //edit procedures
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            //variables
            var id = idTextBox.Text;  
            var categoryName = categoryNameTextBox.Text;
            var details = detailsTextBox.Text;
            if (categoryName == "" || details == "" || id == "") //check if the boxes are empty
            {
                MessageBox.Show("Please fill all the gaps! ");
                return;
            }

            //open db
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
                connection.Close();
                clearTextBoxes();
                return;
            }
            connection.Close();
            loadDataGrid();
            clearTextBoxes();
            MessageBox.Show("Procedure executed with succes!");
        }
    }
}
