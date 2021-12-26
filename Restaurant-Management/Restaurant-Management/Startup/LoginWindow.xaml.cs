using Restaurant_Management.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Restaurant_Management
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        //database connection variables

        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();


        int loggedUserID;

        //********************************
        public LoginWindow()
        {
            InitializeComponent();
            this.loggedUserID = 0;
            clearTextBoxes();
        }
        public LoginWindow(String username)
        {
            InitializeComponent();
            this.loggedUserID = 0;
            usernameTextBox.Text = username;
            clearTextBoxes();
        }

        //clear all the text boxes presented in the login window
        public void clearTextBoxes()
        {
            usernameTextBox.Clear();
            passwordBox.Clear();
        }

        //the closing button marked by the Big Bold Uppercase X
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //login button
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            // get username and password from ui objects
            var username = usernameTextBox.Text;
            var password = passwordBox.Password;

            //checking if are empty
            if (password == "" || username == "")
            {
                // if (chef == 1)
                // {
                //     do message box custom, frumos asa cu culori faine ca la login
                // }
                MessageBox.Show("Fill all the gaps");
                clearTextBoxes();
                return;
            }

            var users = (from u in rmDataContext.Users 
                            where u.Username == username && u.Password == password 
                                select new { u.UserID, u.RoleID }).ToList();
            if(users.Count() == 1)
            {
                loggedUserID = users[0].UserID;
                int roleID = users[0].RoleID;
                if (roleID == 1)
                {
                    // admin page
                    DashboardWindow dw = new DashboardWindow(loggedUserID);
                    dw.Show();
                    this.Hide();
                }
                if (roleID == 2)
                {
                    ProductsWindow ew = new ProductsWindow(loggedUserID);
                    ew.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Login failed! Wrong username or password!");
                return;
            }
        }

        //event that assure us closing of all possible opened windows
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //register page
        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow rw = new RegisterWindow();
            rw.Show();
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
