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
        public DashboardWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void categoriesTextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void productsTextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void registration_codesTextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void historyTextButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void employessTextButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeesWindow ew = new EmployeesWindow();
            ew.Show();
            this.Hide();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }
    }
}
