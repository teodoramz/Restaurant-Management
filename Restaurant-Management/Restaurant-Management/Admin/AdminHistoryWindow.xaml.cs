using Restaurant_Management.Database;
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
        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();

        int loggedUserID;

        //constructor for this page
        public AdminHistoryWindow(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
            loadDataGrid();
            loadComboBox();
        }
        private void loadComboBox()
        {
            usersComboBox.Items.Clear();
            usersComboBox.Items.Insert(0, "Users ... ");
            var categories = (from h in rmDataContext.Histories
                              join u in rmDataContext.Users
                              on h.IdUser equals u.UserID
                              select  u.Username).Distinct().ToList();

            foreach (var x in categories)
            {
                usersComboBox.Items.Add(x);
            }
            usersComboBox.SelectedIndex = 0;
        }
        // data grid loader
        public void loadDataGrid()
        {
            var history = (from h in rmDataContext.Histories
                           select new { ID = h.IdHistory, Username = h.User.Username,
                               Date = h.Date, Total_Sell = h.TotalPrice }).ToList();

            dataTableChosed.ItemsSource = history;
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
            var userID = usersComboBox.SelectedItem.ToString();
            if (userID == "Users ... ")               // check if userID is clear
            {
                MessageBox.Show("Please chose a user first! ");
                return;
            }
            var history = (from h in rmDataContext.Histories
                           where h.User.Username == userID
                           select new
                           {
                               ID = h.IdHistory,
                               Username = h.User.Username,
                               Date = h.Date,
                               Total_Sell = h.TotalPrice
                           }).ToList();

            dataTableChosed.ItemsSource = history;
        }
        //clear search
        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            usersComboBox.SelectedIndex = 0;
            loadDataGrid();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
