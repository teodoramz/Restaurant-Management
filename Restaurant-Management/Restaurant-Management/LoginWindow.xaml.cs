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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        //database connection variables
        static String connectionString = "Server=.;Database=Restaurant-Management;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet DS = new DataSet();
        SqlDataAdapter DA = new SqlDataAdapter();

        //********************************
        public LoginWindow()
        {
            InitializeComponent();
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

            //open connection with database
            connection.Open();

            //create sql command
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from Users where Username = @user and Password = @pass";
            cmd.Parameters.AddWithValue("@user", username);
            cmd.Parameters.AddWithValue("@pass", password);

            //exec sql command
            using (var resultat = cmd.ExecuteReader())
            {
                if (resultat.Read()) // if there is a return of sql command : login
                {
                    var role = resultat.GetInt32(resultat.GetOrdinal("RoleID"));
                    if (role == 1)
                    {
                       // admin page
                    }
                    if (role == 2)
                    {
                        ProductsWindow ew = new ProductsWindow();
                        ew.Show();
                        this.Hide();
                    }
                }
                else // if no return, then error
                {
                    MessageBox.Show("Invalid credidentials! ");
                    clearTextBoxes();
                    return;
                }
            }

            //close connection with database
            connection.Close();
        }

        //event that assure us closing of all possible opened windows
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow rw = new RegisterWindow();
            rw.Show();
            this.Hide();
        }
    }
}
