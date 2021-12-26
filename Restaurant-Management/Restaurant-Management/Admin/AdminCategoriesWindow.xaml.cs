using Restaurant_Management.Database;
using Restaurant_Management.Procedures.AddProcedures;
using Restaurant_Management.Procedures.EditProcedure;
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
        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();


        private int loggedUserID;
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
        }
        public int getUID()
        {
            return this.loggedUserID;
        }
        //load data grid
        public void loadDataGrid()
        {
            var categories = (from c in rmDataContext.Categories
                              select new { ID = c.CategoryID, Category = c.CategoryName, Details = c.Details }).ToList();
            dataTableChosed.ItemsSource = categories;
        }
        // add new category procedure
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var ac = new AddCategory(this);
            ac.Show();
            this.Hide();
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
            var id = idTextBox.Text;

            if (id == "")    //check if boxes are empty
            {
                MessageBox.Show("Please give an id first!");
                return;
            }

            var ec = new EditCategory(Convert.ToInt32(id), this);
            ec.Show();
            this.Hide();
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
