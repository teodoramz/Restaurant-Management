using Restaurant_Management.Database;
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

namespace Restaurant_Management.Procedures.EditProcedure
{
    /// <summary>
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();
        private AdminProductsWindow apw;
        private ProductsWindow pw;
        private int prodID;
        public EditProduct(int id, AdminProductsWindow apw)
        {
            InitializeComponent();
            categoryComboBox.Items.Insert(0, "Categories ... ");
            this.apw = apw;
            this.pw = null;
            this.prodID = id;
            loadComboBox();
            categoryComboBox.SelectedIndex = 0;
            loadGaps();
        }
        public EditProduct(int id, ProductsWindow pw)
        {
            InitializeComponent();
            categoryComboBox.Items.Insert(0, "Categories ... ");
            this.apw = null;
            this.pw = pw;
            this.prodID = id;
            loadComboBox();
            categoryComboBox.SelectedIndex = 0;
            loadGaps();
        }
        private void loadGaps()
        {
            var product = (from p in rmDataContext.Products where p.ProductID == prodID 
                           join c in rmDataContext.Categories
                           on p.ProductCategoryID equals c.CategoryID
                           select new { p.ProductName, c.CategoryName, p.Price, p.Ingredients}).SingleOrDefault();

            productNameTextBox.Text = product.ProductName;
            categoryComboBox.SelectedItem = product.CategoryName;
            priceTextBox.Text = product.Price.ToString();
            ingredientsTextBox.Text = product.Ingredients;
        }
        private void loadComboBox()
        {
            var categories = (from c in rmDataContext.Categories select c.CategoryName).ToList();

            foreach (var x in categories)
            {
                categoryComboBox.Items.Add(x);
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if(pw == null)
            {
                apw.loadDataGrid();
                apw.idTextBox.Clear();
                apw.Show();
            }
            else
            {
                pw.loadDataGrid();
                pw.idTextBox.Clear();
                pw.Show();
            }
            this.Close();
        }

        private void EDITButton_Click(object sender, RoutedEventArgs e)
        {

            string productName = productNameTextBox.Text;
            string price = priceTextBox.Text;
            string category = categoryComboBox.SelectedItem.ToString();
            string ingredients = ingredientsTextBox.Text;

            if (category == "Categories ... " ||
               string.IsNullOrEmpty(price) ||
               string.IsNullOrEmpty(ingredients) ||
               string.IsNullOrEmpty(productName))
            {
                MessageBox.Show("Please fill all the gaps!");
                return;
            }

            try
            {
                Product product = (from p in rmDataContext.Products where p.ProductID == this.prodID select p).SingleOrDefault();

                product.ProductName = productName;
                product.Price = Convert.ToDecimal(price);
                product.Category.CategoryName = category;
                product.Ingredients = ingredients;

                rmDataContext.SubmitChanges();
                MessageBox.Show("Edited!");
                CloseWindowButton_Click(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Can't edit product!" + ex.Message);
                return;
            }
        }

        private void priceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
