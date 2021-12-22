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
    /// Interaction logic for AdminHistoryWindow.xaml
    /// </summary>
    public partial class AdminHistoryWindow : Window
    {
       //database variables
        static String connectionString = "Server=.;Database=Restaurant-Management;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet DS = new DataSet();
        SqlDataAdapter DA = new SqlDataAdapter();

        int loggedUserID;

        //constructor for this page
        public AdminHistoryWindow(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
            loadDataGrid();
        }
        // data grid loader
        public void loadDataGrid()
        {
            SqlCommand selectCMD = new SqlCommand(string.Format
                                                       (@"SELECT IdHistory as ID, IdUser as [ID User], Date as Date, TotalPrice as [Total Sell]" +
                                                           "FROM History"), connection);
            DA.SelectCommand = selectCMD;
            connection.Open();
            DS.Clear();
            DA.Fill(DS, "History");
            dataTableChosed.ItemsSource = DS.Tables["History"].DefaultView;
            connection.Close();
        }
        // dashboard button
        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardWindow dw = new DashboardWindow(loggedUserID);
            dw.Show();
            this.Hide();
        }
        // log out
        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }
        // exit button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        // close window in cascade
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        // accepting only numbers
        private void idUserTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        // search button procedure
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            var userID = idUserTextBox.Text;
            if (userID == "")               // check if userID is clear
            {
                MessageBox.Show("Please fill the ID User! ");
                return;
            }
            //sql command
            SqlCommand selectCMD = new SqlCommand(string.Format
                                                      (@"SELECT IdHistory as ID, IdUser as [ID User], Date as Date, TotalPrice as [Total Sell]" +
                                                          "FROM History WHERE IdUser = @user"), connection);
            selectCMD.Parameters.AddWithValue("@user", userID);

            DA.SelectCommand = selectCMD;
            connection.Open();
            DS.Clear();
            DA.Fill(DS, "History");
            dataTableChosed.ItemsSource = DS.Tables["History"].DefaultView;
            connection.Close();
            idUserTextBox.Clear();
        }
        //clear search
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            idUserTextBox.Clear();
            loadDataGrid();
        }
    }
}
