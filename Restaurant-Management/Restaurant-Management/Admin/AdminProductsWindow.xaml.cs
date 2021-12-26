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
    /// Interaction logic for AdminProductsWindow.xaml
    /// </summary>
    public partial class AdminProductsWindow : Window
    {
        //database variables
        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();
        int loggedUserID;

        //page constructor
        public AdminProductsWindow(int user)
        {
            InitializeComponent();
            loadDataGrid();
            this.loggedUserID = user;
        }
        //load datagrid
        public void loadDataGrid()
        {
            var products = (from p in rmDataContext.Products
                            join c in rmDataContext.Categories
                            on p.ProductCategoryID equals c.CategoryID
                            select new
                            {
                                ID = p.ProductID,
                                Product = p.ProductName,
                                Category = c.CategoryName,
                                Price = p.Price,
                                Ingredients = p.Ingredients
                            }).ToList();

            dataTableChosed.ItemsSource = products;
        }
        //log out
        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }
        // clear text boxes
        void clearTextBoxes()
        {
            idTextBox.Clear();
        }
        // add products procedures
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddProduct ap = new AddProduct(this);
            ap.Show();
            this.Hide();
        }
        //edit button procedure
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            //function variables
            var productID = idTextBox.Text;
            if (string.IsNullOrEmpty(productID))
            {
                MessageBox.Show("Please fill the id first!");
                return;
            }
            var ep = new EditProduct(Convert.ToInt32(productID), this);
            ep.Show();
            this.Hide();
        }
        //delete product procedure
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            //function variables
            int productID = Convert.ToInt32(idTextBox.Text);
            if (string.IsNullOrEmpty(idTextBox.Text))
            {
                MessageBox.Show("Please fill the id first!");
                return;
            }
            try
            {
                Product product = (from p in rmDataContext.Products where p.ProductID == productID select p).SingleOrDefault();
                rmDataContext.Products.DeleteOnSubmit(product);
                rmDataContext.SubmitChanges();
                MessageBox.Show("Deleted!");
                loadDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't delete element!");
                return;
            }
        }
        //exit button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        // close windows in cascade
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //format only numbers
        private void idTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        //dashboard button
        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardWindow dw = new DashboardWindow(loggedUserID);
            dw.Show();
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
