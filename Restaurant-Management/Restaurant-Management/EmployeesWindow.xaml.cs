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
    /// Interaction logic for EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        int loggedUserID;
        static String connectionString = "Server=.;Database=Restaurant-Management;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet DS = new DataSet();
        SqlDataAdapter DA = new SqlDataAdapter();
        

        public EmployeesWindow(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
            loadDataGrid();
        }
        public void loadDataGrid()
        {
            SqlCommand selectCMD = new SqlCommand(string.Format
                                                        (@" WITH Empl AS
                                                            (
                                                              SELECT  UserID as ID
                                                               ,Username as Name
                                                               ,Phone as Phone
                                                               ,Email as Email
                                                               
                                                               FROM Users
                                                             )
                                                            SELECT *FROM Empl"), connection);
            

            DA.SelectCommand = selectCMD;
            connection.Open();
            DS.Clear();
            DA.Fill(DS, "Empl");
            dataTableChosed.ItemsSource = DS.Tables["Empl"].DefaultView;
            connection.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void idTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        void clearTextBoxes()
        {
            //curata campurile
        }

       

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var id = idTextBox.Text;
            var name = nameTextBox.Text;
            var email = emailTextBox.Text;
            var phone = phoneTextBox.Text;
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE Users 
                                SET Username=@name,
                                    Email=@email,
                                    Phone=@phone 
                                    WHERE UserID=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@email", email);

            // MECANISM TRATARE ERORI
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de editat! " + error.Message);
                connection.Close();
                clearTextBoxes();
                return;
            }
            connection.Close();
            loadDataGrid();

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = idTextBox.Text;
            var name = nameTextBox.Text;
            var email = emailTextBox.Text;
            var phone = phoneTextBox.Text;
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM Users
                                WHERE UserID=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@email", email);

            // MECANISM TRATARE ERORI
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de sters userul! " + error.Message);
                connection.Close();
                clearTextBoxes();
                return;
            }
            connection.Close();
            loadDataGrid();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }

        private void dashboardButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardWindow dw = new DashboardWindow(loggedUserID);
            dw.Show();
            this.Hide();
        }

        private void promoteToButton_Click(object sender, RoutedEventArgs e)
        {
            var id = idTextBox.Text;
           
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE Users
                                SET RoleID=1 
                                    WHERE UserID=@id"; ;
            cmd.Parameters.AddWithValue("@id", id);
           

            // MECANISM TRATARE ERORI
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de editat! " + error.Message);
                connection.Close();
                clearTextBoxes();
                return;
            }
            connection.Close();
            loadDataGrid();
        }
    }
}
