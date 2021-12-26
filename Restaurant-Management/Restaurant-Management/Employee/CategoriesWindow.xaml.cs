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
    /// Interaction logic for CategoriesWindow.xaml
    /// </summary>
    public partial class CategoriesWindow : Window
    {
        //database variables

        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();


        private int loggedUserID;

        //page constructor
        public CategoriesWindow(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
            clearTextBoxes();
            loadDataGrid();
        }
        public int getUID()
        {
            return this.loggedUserID;
        }
        //clear boxes
        void clearTextBoxes()
        {
            idTextBox.Clear();
        }
        //data grid loader
        public void loadDataGrid()
        {
            var categories = (from c in rmDataContext.Categories
                              select new { ID = c.CategoryID, Category = c.CategoryName, Details = c.Details }).ToList();
            dataTableChosed.ItemsSource = categories;
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
            var ac = new AddCategory(this);
            ac.Show();
            this.Hide();
        }

        //edit category procedure
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            ////function variables 
            var id = idTextBox.Text;

            if (id == "" )    //check if boxes are empty
            {
                MessageBox.Show("Please give an id first!");
                return;
            }

            var ec = new EditCategory(Convert.ToInt32(id), this);
            ec.Show();
            this.Hide();
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
