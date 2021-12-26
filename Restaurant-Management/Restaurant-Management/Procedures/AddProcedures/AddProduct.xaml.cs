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

namespace Restaurant_Management.Procedures.AddProcedures
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();
        private AdminProductsWindow apw;
        private ProductsWindow pw;
        public AddProduct(AdminProductsWindow apw)
        {
            InitializeComponent();
            categoriesComboBox.Items.Insert(0, "Categories ... ");
            this.apw = apw;
            this.pw = null;
            loadComboBox();
            categoriesComboBox.SelectedIndex = 0;
        }
        public AddProduct(ProductsWindow pw)
        {
            InitializeComponent();
            categoriesComboBox.Items.Insert(0, "Categories ... ");
            this.apw = null;
            this.pw = pw;
            loadComboBox();
            categoriesComboBox.SelectedIndex = 0;
            
        }
        private void loadComboBox()
        {
            var categories = (from c in rmDataContext.Categories select c.CategoryName).ToList();

            foreach(var x in categories)
            {
                categoriesComboBox.Items.Add(x);
            }
        }
        private void clearGaps()
        {
            productNameTextBox.Clear();
            categoriesComboBox.SelectedIndex = 0;
            priceTextBox.Clear();
            ingredientsTextBox.Clear();
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
            if (pw == null)
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

        private void priceTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string productName = productNameTextBox.Text;
            string price = priceTextBox.Text;
            string category = categoriesComboBox.SelectedItem.ToString();
            string ingredients = ingredientsTextBox.Text;

            if (category == "Categories ... " ||
               string.IsNullOrEmpty(price) ||
               string.IsNullOrEmpty(ingredients) ||
               string.IsNullOrEmpty(productName) )
            {
                MessageBox.Show("Please fill all the gaps!");
                clearGaps();
                return;
            }
            try
            {
                var newProduct = new Product
                {
                    ProductName = productName,
                    ProductCategoryID = (from c in rmDataContext.Categories where c.CategoryName == category select c.CategoryID).SingleOrDefault(),
                    Price = Convert.ToDecimal(price),
                    Ingredients = ingredients
                };

                rmDataContext.Products.InsertOnSubmit(newProduct);
                rmDataContext.SubmitChanges();
                MessageBox.Show("Product Added!");
                CloseWindowButton_Click(sender, e);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Can't add product!" + ex.Message);
                clearGaps();
                return;
            }
        }
    }
}
