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
        static String connectionString = "Server=.;Database=Restaurant-Management;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet DS = new DataSet();
        SqlDataAdapter DA = new SqlDataAdapter();

        int loggedUserID;

        public AdminHistoryWindow(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
        }
        public void loadComboBox()
        {
            List<string> str = new List<string>();
            try
            {
                SqlCommand selectCMD = new SqlCommand("SELECT Username  FROM Users WHERE RoleID = 2", connection);
                connection.Open();

                SqlDataReader reader = selectCMD.ExecuteReader();
               
                int i = 0;
                while (reader.Read())
                {
                    str.Add(reader.GetValue(i).ToString());
                    i++;
                }
                reader.Close();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
                for(int j = 0; j< str.Count(); j++)
                {
                    Console.WriteLine(str[j]);
                }
            }
        }
        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardWindow dw = new DashboardWindow(loggedUserID);
            dw.Show();
            this.Hide();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void idUserTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void totalPriceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][.0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            loadComboBox();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
