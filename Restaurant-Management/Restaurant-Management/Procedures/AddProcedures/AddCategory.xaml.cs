using Restaurant_Management.Database;
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

namespace Restaurant_Management.Procedures.AddProcedures
{
    /// <summary>
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    
   
    public partial class AddCategory : Window
    {
        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();
        private CategoriesWindow cw;
        private AdminCategoriesWindow acw;
        public AddCategory(CategoriesWindow cw)
        {
            InitializeComponent();
            this.cw = cw;
            this.acw = null;
        }
        public AddCategory(AdminCategoriesWindow acw)
        {
            InitializeComponent();
            this.cw = null;
            this.acw = acw;
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
            if (cw == null)
            {
                acw.loadDataGrid();
                acw.idTextBox.Clear();
                acw.Show();
            }
            else
            {
                cw.loadDataGrid();
                cw.idTextBox.Clear();
                cw.Show();
            }
            this.Close();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {

            var categoryName = categoryNameTextBox.Text;
            var details = detailsTextBox.Text;
            if (categoryName == "" || details == "")
            {
                MessageBox.Show("Please fill all the gaps!");
                return;
            }

            var newCategory = new Category
            {
                CategoryName = categoryName,
                Details = details
            };

            try
            {
                rmDataContext.Categories.InsertOnSubmit(newCategory);
                rmDataContext.SubmitChanges();
                MessageBox.Show("Category Added!");
                CloseWindowButton_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't add category!");
                return;
            }

        }
    }
}
