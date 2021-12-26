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

namespace Restaurant_Management.Procedures.EditProcedure
{
    /// <summary>
    /// Interaction logic for EditCategory.xaml
    /// </summary>
    public partial class EditCategory : Window
    {
        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();
        private CategoriesWindow cw;
        private AdminCategoriesWindow acw;
        private int id;

        public EditCategory(int id, CategoriesWindow cw)
        {
            InitializeComponent();
            this.cw = cw;
            this.acw = null;
            this.id = id;
            loadTextBoxes();
        }
        public EditCategory(int id, AdminCategoriesWindow acw)
        {
            InitializeComponent();
            this.cw = null;
            this.acw = acw;
            this.id = id;
            loadTextBoxes();
        }
        void loadTextBoxes()
        {
            var category = (from c in rmDataContext.Categories where c.CategoryID == id select c).ToList();
            categoryNameTextBox.Text = category[0].CategoryName;
            detailsTextBox.Text = category[0].Details;
        }

        private void EDITButton_Click(object sender, RoutedEventArgs e)
        {
            int id = this.id;
            string categoryName = categoryNameTextBox.Text;
            string details = detailsTextBox.Text;

            if(categoryName == "" || details == "")
            {
                MessageBox.Show("Please fill alll the gaps first!");
                return;
            }

            try
            {
                var categories = (from c in rmDataContext.Categories where c.CategoryID == id select c);

                foreach(Category c in categories)
                {
                    c.CategoryName = categoryName;
                    c.Details = details;
                }

                rmDataContext.SubmitChanges();

                MessageBox.Show("Edited with succes!");
                CloseWindowButton_Click(sender, e);


            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to edit category!" + ex.Message);
                return;
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
            if (cw == null)
            {
                acw.Show();
                acw.idTextBox.Clear();
                acw.loadDataGrid();
            }
            else
            {
                cw.Show();
                cw.idTextBox.Clear();
                cw.loadDataGrid();
            }
            this.Close();
        }

     
    }
}
