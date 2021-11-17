﻿using System;
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
            confPassTextBox.Clear();
            phoneTextBox.Clear();
            emailTextBox.Clear();
            verification_codeTextBox.Clear();
        }

        bool check_code(String code, String username)
        {
            var verCode = code;
            var userName = username;
            bool state = false;
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from RegistrationCodes where UserName = @user and Code = @code";
            cmd.Parameters.AddWithValue("@code", verCode);
            cmd.Parameters.AddWithValue("@user", userName);

            //exec sql command
            using (var resultat = cmd.ExecuteReader())
            {
                if (resultat.Read()) // if there is a return of sql command : login
                {
                    state = true;
                }
            }
  
            connection.Close();

            return state;
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
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
                return;
            }
            if(phone.Length != 10)
            {
                MessageBox.Show("Wrong phone number format!");
                return;
            }
            if (username == "" || password =="" || phone =="" || email == "") // mail nu ar fi obligatoriu
            {
                MessageBox.Show("Fill all the gaps");
                clearTextBoxes();
                return;
            }

            if (check_code(verification_code, username))
            {
                connection.Open();

                //mai trebuie facuta verificarea pe codul generat
                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into Users(Username, Password, Phone, Email, RoleID) Values(@user, @pass, @phone, @email, 2)";
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@pass", password);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phone", phone);
                // MECANISM TRATARE ERORI
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Imposibil de adaugat utilizator! " + error.Message);
                    connection.Close();
                    clearTextBoxes();
                    return;
                }
                // MECANISM TRATARE ERORI
                connection.Close();
                LoginWindow lw = new LoginWindow();
                lw.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Cod de inregistrare invalid pentru username-ul " + username + "!");
                return;
            }

        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void phoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
