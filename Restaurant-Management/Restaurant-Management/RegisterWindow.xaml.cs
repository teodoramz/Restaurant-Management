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
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {   //database connection variables
        static String connectionString = "Server=.;Database=Restaurant-Management;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet DS = new DataSet();
        SqlDataAdapter DA = new SqlDataAdapter();
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
            pass_againTextBox.Clear();
            phoneTextBox.Clear();
            emailTextBox.Clear();
            verification_codeTextBox.Clear();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var username = usernameTextBox.Text;
            var password = passTextBox.Text;
            var password_again = pass_againTextBox.Text;
            var phone = phoneTextBox.Text;
            var email = emailTextBox.Text;
            var verification_code = verification_codeTextBox.Text;
            //checking if confirmation password is ok
            if (password_again!=password)
            {   
                MessageBox.Show("Password not matching");
                return;
            }


        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
