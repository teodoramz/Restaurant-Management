using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        //variables
        int loggedUserID;

        //page constructor
        public DashboardWindow(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
        }
        //close button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //categories button
        private void categoriesTextButton_Click(object sender, RoutedEventArgs e)
        {
            AdminCategoriesWindow acw = new AdminCategoriesWindow(loggedUserID);
            acw.Show();
            this.Hide();
        }
        //products button
        private void productsTextButton_Click(object sender, RoutedEventArgs e)
        {
            AdminProductsWindow apw = new AdminProductsWindow(loggedUserID);
            apw.Show();
            this.Hide();
        }
        //registration codes button
        private void registration_codesTextButton_Click(object sender, RoutedEventArgs e)
        {
            AdminRegistrationCodes arc = new AdminRegistrationCodes(loggedUserID);
            arc.Show();
            this.Hide();
        }
        //history button
        private void historyTextButton_Click(object sender, RoutedEventArgs e)
        {
            AdminHistoryWindow ahw = new AdminHistoryWindow(loggedUserID);
            ahw.Show();
            this.Hide();
        }
        //employess button
        private void employessTextButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeesWindow ew = new EmployeesWindow(loggedUserID);
            ew.Show();
            this.Hide();
        }
        //close windows in cascade
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
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
