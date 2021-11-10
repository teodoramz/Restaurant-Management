using System;
using System.Collections.Generic;
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
    /// Interaction logic for EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        public EmployeesWindow()
        {
            InitializeComponent();
        }

         private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void idTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void employersButton_Click(object sender, RoutedEventArgs e)
        {
            // clear la textboxuri / comboboxuri sau ce mai punem aici
            // in rest DO NOTHING
        }

        private void productsButton_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow pw = new ProductsWindow();
            pw.Show();
            this.Hide();
        }

        private void categoriesButton_Click(object sender, RoutedEventArgs e)
        {
            CategoriesWindow cw = new CategoriesWindow();
            cw.Show();
            this.Hide();
        }

        private void historyButton_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow hw = new HistoryWindow();
            hw.Show();
            this.Hide();
        }
        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }
    }
}
