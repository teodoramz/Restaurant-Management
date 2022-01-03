using Restaurant_Management.Database;
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
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        //database variables

        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();

        int loggedUserID;
        int currSellID = 0;

        //page constructor
        public HistoryWindow(int userID)
        {
            InitializeComponent();
            productsComboBox.Items.Insert(0, "Products...");
            this.loggedUserID = userID;
            
            loadDataGrid();
            loadComboBox();
            clearTextBoxes();

        }
        //clear boxes
        public void clearTextBoxes()
        {
            idSellTextBox.Clear();
            productsComboBox.SelectedIndex = 0;
            quantityTextBox.Clear();
        }

        private void loadComboBox()
        {
            var products = (from p in rmDataContext.Products select p.ProductName).ToList();
            foreach(var x in products)
            {
                productsComboBox.Items.Add(x);
            }
        }
        //load datagrid
        public void loadDataGrid()
        {
            var sells = (from s in rmDataContext.Sells
                        join h in rmDataContext.Histories
                            on s.IdHistory equals h.IdHistory
                        join p in rmDataContext.Products
                        on s.IdProduct equals p.ProductID
                        where h.IdUser == loggedUserID
                        orderby h.Date descending
                         select new { ID = s.IdSell, Date = h.Date,
                                    Product = p.ProductName, Quantity = s.Quantity }).ToList();

            dataTableChosed.ItemsSource = sells;
        }
        //close button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //close windows in cascade
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //only numbers format
        private void idSellTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        //only numbers format
        private void productIDTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        //only numbers format
        private void quantityTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        //edit sells procedure
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            ////functions variables
            var id = idSellTextBox.Text;
            var product = productsComboBox.SelectedItem.ToString();
            var quantity = quantityTextBox.Text;
            if (id == "" || product == "" || quantity == "")     //check if boxes are empty
            {
                MessageBox.Show("Please fill all the gaps!");
                return;
            }

            try
            {
                Sell sell = (from s in rmDataContext.Sells 
                             where s.IdSell == currSellID select s).SingleOrDefault();

                //sell.IdProduct = (from s in rmDataContext.Products
                //                  where s.ProductName == product select s.ProductID).SingleOrDefault();

                //sell.IdProduct = rmDataContext.Products.Where(x => x.ProductName == product).SingleOrDefault().ProductID;
                sell.Product.ProductName = product;
                sell.Quantity = Convert.ToInt32(quantity);

                rmDataContext.SubmitChanges();
                MessageBox.Show("Edited!");
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de editat vanzari! " + error.Message);
                clearTextBoxes();
                return;
            }

            loadDataGrid();
            clearTextBoxes();
        }

        //if a sell is deleted, do : 
        public void updateHistory2(int idSell)
        {
            ////get data using idSell

            var sell = (from s in rmDataContext.Sells
                        where s.IdSell == idSell
                        select new { s.IdProduct, s.Quantity, s.IdHistory }).SingleOrDefault();

            int productID = sell.IdProduct;
            int quantity = sell.Quantity;
            int currSellID = sell.IdHistory;

            try
            {
                var history = (from h in rmDataContext.Histories
                               where h.IdHistory == currSellID
                               select h).SingleOrDefault();

                history.TotalPrice -= Convert.ToDecimal(quantity) * (from p in rmDataContext.Products
                                                                     where p.ProductID == productID
                                                                     select p.Price).SingleOrDefault();

                rmDataContext.SubmitChanges();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de adaugat vanzare(update 2 funct)! " + error.Message);
                clearTextBoxes();
                return;
            }
        }
        //delete procedure
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (productsComboBox.SelectedItem != "Products..." || quantityTextBox.Text != "") //check if boxes are empty
            {
                MessageBox.Show("We only need ID! Please leave the other boxes empty!");
                productsComboBox.SelectedIndex = 0;
                quantityTextBox.Clear();
                return;
            }
            var id = idSellTextBox.Text;
            if (id == "")
            {
                MessageBox.Show("Please add an id! ");
                return;
            }
            updateHistory2(Convert.ToInt32(id));

            try
            {
                Sell sell = (from p in rmDataContext.Sells where p.IdSell == Convert.ToInt32(id) select p).SingleOrDefault();
                rmDataContext.Sells.DeleteOnSubmit(sell);
                rmDataContext.SubmitChanges();
                MessageBox.Show("Deleted!");
                loadDataGrid();
                clearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't delete element!" + ex.Message);
                return;
            }
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
            CategoriesWindow cw = new CategoriesWindow(loggedUserID);
            cw.Show();
            this.Hide();
        }
        //history button
        private void historyButton_Click(object sender, RoutedEventArgs e)
        {
            clearTextBoxes();
        }
        //log out
        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }
        //create sell id procedure
        private void createSellButton_Click(object sender, RoutedEventArgs e)
        {

            DateTime date = DateTime.Now;

            var newHistory = new History
            {
                IdUser = loggedUserID,
                Date = date,
                TotalPrice = 0,
            };

            try
            {
                rmDataContext.Histories.InsertOnSubmit(newHistory);
                rmDataContext.SubmitChanges();
                MessageBox.Show("Created!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error making the new sell procedure!" + ex.Message);
                return;
            }

            var history = (from h in rmDataContext.Histories 
                           where h.Date == date && h.IdUser == loggedUserID 
                           select h.IdHistory).SingleOrDefault();

            this.currSellID = history;

            clearTextBoxes();
        }
        //update history by adding sell
        public void updateHistory(int productID, int quantity)
        {
            try
            {
                var history = (from h in rmDataContext.Histories
                               where h.IdHistory == currSellID
                               select h).SingleOrDefault();

                history.TotalPrice += Convert.ToDecimal(quantity) * (from p in rmDataContext.Products
                                                                     where p.ProductID == productID
                                                                     select p.Price).SingleOrDefault();

                rmDataContext.SubmitChanges();
            }
            catch (Exception error)
            {
                MessageBox.Show("2 Imposibil de adaugat vanzare! " + error.Message);
                clearTextBoxes();
                return;
            }

        }
        //add sell
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (idSellTextBox.Text != "")   //check if boxes are empty
            {
                MessageBox.Show("Please leave ID box empty for this procedure! ");
                idSellTextBox.Clear();
                return;
            }
            if (this.currSellID != 0)
            {
                var product = productsComboBox.SelectedItem.ToString();
                var quantity = quantityTextBox.Text;
                if (product == "" || quantity == "")
                {
                    MessageBox.Show("Please fill product ID and quantity!");
                    return;
                }
                if (Convert.ToInt32(quantity) < 1)
                {
                    MessageBox.Show("Quantity must be greater than 0!");
                    quantityTextBox.Clear();
                    return;
                }

                try
                {
                    var newSell = new Sell
                    {
                        IdHistory = currSellID,
                        IdProduct = (from p in rmDataContext.Products
                                     where p.ProductName == product
                                     select p.ProductID).SingleOrDefault(),
                        Quantity = Convert.ToInt32(quantity),
                    };

                    rmDataContext.Sells.InsertOnSubmit(newSell);
                    rmDataContext.SubmitChanges();
                    MessageBox.Show("Added!");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Eroare la adaugare!" + ex.Message);
                    return;
                }

                int id = (from p in rmDataContext.Products
                          where p.ProductName == product
                          select p.ProductID).SingleOrDefault();

                updateHistory(id, Convert.ToInt32(quantity));
                loadDataGrid();
                clearTextBoxes();

            }
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
