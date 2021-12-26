using Restaurant_Management.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        //database connection variables
        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();
        public RegisterWindow()
        {
            InitializeComponent();
            clearTextBoxes();
        }
        //clear all text boxes
        public void clearTextBoxes()
        {
            usernameTextBox.Clear();
            passTextBox.Clear();
            confPassTextBox.Clear();
            phoneTextBox.Clear();
            emailTextBox.Clear();
            verification_codeTextBox.Clear();
        }
        // check if there is a verification code  created in database
        bool check_code(String code, String username)
        {
            var verCode = code;
            var userName = username;
            bool state = false;

            var codes = (from c in rmDataContext.RegistrationCodes where c.UserName == userName && c.Code == code select c);

            if(codes.Count() == 1)
            {
                state = true;
            }

            return state;
        }

        //check email format
        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        //register procedure
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            //variables
            var username = usernameTextBox.Text;
            var password = passTextBox.Text;
            var confPass = confPassTextBox.Text;
            var phone = phoneTextBox.Text;
            var email = emailTextBox.Text;
            var verification_code = verification_codeTextBox.Text;

            //checking if confirmation password is ok
            if (confPass != password)
            {
                MessageBox.Show("Password not matching");
                passTextBox.Clear();
                confPassTextBox.Clear();
                return;
            }
            //check lenght of phone number
            if (phone.Length != 10)
            {
                MessageBox.Show("Wrong phone number format!");
                return;
            }
            //check email validity
            if (!IsValid(email))
            {
                MessageBox.Show("Wrong email format!");
                return;
            }

            //check if boxes are empty
            if (username == "" || password == "" || phone == "" || email == "") // mail nu ar fi obligatoriu
            {
                MessageBox.Show("Fill all the gaps");
                return;
            }

            if (check_code(verification_code, username)) //if verification code is ok
            {
                var newUser = new User
                {
                    Username = username,
                    Password = password,
                    Phone = phone,
                    Email = email,
                    RoleID = 2
                };
                try
                {
                    rmDataContext.Users.InsertOnSubmit(newUser);
                    rmDataContext.SubmitChanges();
                    LoginWindow lw = new LoginWindow(username);
                    lw.Show();
                    this.Hide();
                    MessageBox.Show("Registred succesfully! ");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Can't register!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Cod de inregistrare invalid pentru username-ul " + username + "!");
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
        //exit
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //phone format condition
        private void phoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        //closing in cascade
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
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
