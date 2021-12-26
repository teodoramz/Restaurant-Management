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
    /// Interaction logic for AdminRegistrationCodes.xaml
    /// </summary>
    public partial class AdminRegistrationCodes : Window
    {
        //database connection variables
        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();

        int loggedUserID;
        private static Random random = new Random();

        //page constructor
        public AdminRegistrationCodes(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
            clearTextBoxes();
            loadDataGrid();
        }
        //clear boxes
        public void clearTextBoxes()
        {
            idTextBox.Clear();
            usernameTextBox.Clear();
        }
        //load datagrid
        public void loadDataGrid()
        {
            var regist = (from r in rmDataContext.RegistrationCodes
                          select new
                          {
                              ID = r.IdRegistration,
                              Username = r.UserName,
                              Code = r.Code,
                              Expiration_Date = r.ExpirationDate,
                              CreatedBy = r.User.Username
                          }).ToList();
            dataTableChosed.ItemsSource = regist;
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
        //format only numbers
        private void idTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
     

        //caesar cipher
        public static char cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);


        }
        //random code generator
        String generateCode(String username)
        {
            String output = String.Empty;

            foreach (char ch in username)
                output += cipher(ch, 17);

            const String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            output += new String(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return output;
        }
        //delete registration code procedure
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (usernameTextBox.Text == "") //check if the boxes are empty
            {
                var id = idTextBox.Text;
                if (id == "")
                {
                    MessageBox.Show("Please add an ID");
                    return;
                }

                try
                {
                    var registCode = rmDataContext.RegistrationCodes.
                           Where(x => x.IdRegistration == Convert.ToInt32(id)).SingleOrDefault();

                    rmDataContext.RegistrationCodes.DeleteOnSubmit(registCode);
                    rmDataContext.SubmitChanges();
                    MessageBox.Show("Deleted!");
                    loadDataGrid();
                    clearTextBoxes();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Imposibil de sters codes! " + error.Message);
                    clearTextBoxes();
                    return;
                }
            }
            else
            {
                //username it's not necessary
                MessageBox.Show("We don't need username here! Leave it empty!");
                usernameTextBox.Clear();
                return;
            }
        }
        //log out
        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }
        //dashboard button
        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardWindow dw = new DashboardWindow(loggedUserID);
            dw.Show();
            this.Hide();
        }
        //generate code button
        private void generateCodeButton_Click(object sender, RoutedEventArgs e)
        {
            if (idTextBox.Text == "")       //check if boxes are empty
            {
                DateTime date = DateTime.Now;
                date.AddDays(7);
                String username = usernameTextBox.Text;
                if (username == "")
                {
                    MessageBox.Show("Please fill username box! ");
                    return;
                }

                String code = generateCode(username);//generate code using username

                try
                {
                    var newCode = new RegistrationCode
                    {
                        Code = code,
                        UserName = username,
                        ExpirationDate = date,
                        CreatedBy = this.loggedUserID,
                    };

                    rmDataContext.RegistrationCodes.InsertOnSubmit(newCode);
                    rmDataContext.SubmitChanges();
                    MessageBox.Show("Registration code generated!");
                    loadDataGrid();
                    clearTextBoxes();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Imposibil de adaugat cod! " + error.Message);
                    return;
                }

            }
            else
            {
                MessageBox.Show("We don't need ID! Please don't complete it!");
                idTextBox.Clear();
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
    }
}
