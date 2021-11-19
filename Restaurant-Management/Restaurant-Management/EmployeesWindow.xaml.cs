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
    /// Interaction logic for EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        //database variables
        static String connectionString = "Server=.;Database=Restaurant-Management;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet DS = new DataSet();
        SqlDataAdapter DA = new SqlDataAdapter();
        int loggedUserID;

        //page constructor
        public EmployeesWindow(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
            loadDataGrid();
        }
        //load datagrid
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
        //check the format of a email
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
        private void idTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void phoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        //clear boxes
        void clearTextBoxes()
        {
            idTextBox.Clear();
            nameTextBox.Clear();
            phoneTextBox.Clear();
            emailTextBox.Clear();
        }

        //edit employees procedure
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            //function variables
            var id = idTextBox.Text;
            var name = nameTextBox.Text;
            var email = emailTextBox.Text;
            var phone = phoneTextBox.Text;

            if(id == "" || name == "" || email == "" || phone == "") // check if boxes are empty
            {
                MessageBox.Show("Please fill all the gaps!");
                return;
            }
            if(!IsValid(email))         //check mail format
            {
                MessageBox.Show("Wrong email address format! ");
                emailTextBox.Clear();
                return;
            }

            //database open
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
            clearTextBoxes();
            MessageBox.Show("Procedure executed with succes!");

        }
        //delete employee procedure
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(nameTextBox.Text != "" || emailTextBox.Text != "" || phoneTextBox.Text !="") //check if boxes are empty
            {
                MessageBox.Show("We need only id! Please leave the others empty!");
                nameTextBox.Clear();
                emailTextBox.Clear();
                phoneTextBox.Clear();
                return;
            }
            var id = idTextBox.Text;
            if(id == "")
            {
                MessageBox.Show("Please add an id!");
                return;
            }

            //open database
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"DELETE FROM Users
                                WHERE UserID=@id";
            cmd.Parameters.AddWithValue("@id", id);

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
            clearTextBoxes();
            MessageBox.Show("Procedure executed with succes!");
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
        //promote to admin procedure
        private void promoteToButton_Click(object sender, RoutedEventArgs e)
        {
            if (nameTextBox.Text != "" || emailTextBox.Text != "" || phoneTextBox.Text != "") //check if boxes are empty
            {
                MessageBox.Show("We need only id! Please leave the others empty!");
                nameTextBox.Clear();
                emailTextBox.Clear();
                phoneTextBox.Clear();
                return;
            }
            var id = idTextBox.Text;
            if (id == "")
            {
                MessageBox.Show("Please add an id!");
                return;
            }
            
            //open database
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
                MessageBox.Show("Imposibil de promovat la admin! " + error.Message);
                connection.Close();
                clearTextBoxes();
                return;
            }
            connection.Close();
            loadDataGrid();
            clearTextBoxes();
            MessageBox.Show("Procedure executed with succes!");
        }
    }
}
