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
        static String connectionString = "Server=.;Database=Restaurant-Management;Trusted_Connection=true";
        SqlConnection connection = new SqlConnection(connectionString);
        DataSet DS = new DataSet();
        SqlDataAdapter DA = new SqlDataAdapter();
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
            SqlCommand selectCMD = new SqlCommand(string.Format
                                                        (@" WITH loggedAdminCodes AS
                                                            (
                                                              SELECT  RegistrationCodes.IdRegistration as ID, 
                                                                        RegistrationCodes.UserName as Username,
                                                                            RegistrationCodes.Code as Code,
                                                                                RegistrationCodes.ExpirationDate as [Expiration Date]
                                                                FROM RegistrationCodes
                                                               INNER JOIN Users
                                                               ON Users.UserID = RegistrationCodes.CreatedBy
                                                               WHERE Users.UserID = @userID
                                                             )
                                                            SELECT *FROM loggedAdminCodes
                                                            "), connection);
            selectCMD.Parameters.AddWithValue("@userID", loggedUserID);

            DA.SelectCommand = selectCMD;
            connection.Open();
            DS.Clear();
            DA.Fill(DS, "loggedAdminCodes");
            dataTableChosed.ItemsSource = DS.Tables["loggedAdminCodes"].DefaultView;
            connection.Close();
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
        //edit registration codes procedure
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            //variables
            var id = idTextBox.Text;  
            var username = usernameTextBox.Text;
            if(id == "" || username == "" )     //check if boxes are empty
            {
                MessageBox.Show("Please fill all the gaps! ");
                return;
            }
            DateTime date = DateTime.Now;
            date.AddDays(7);
            String code = generateCode(username);       //generate code using username

            //open database
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"UPDATE RegistrationCodes
                                     SET UserName = @user, 
                                            ExpirationDate = @date
                                       WHERE IdRegistration = @idCodes";
            cmd.Parameters.AddWithValue("@user", username);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@idCodes", id);
            // MECANISM TRATARE ERORI
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de editat codurile de inregistrare! " + error.Message);
                connection.Close();
                clearTextBoxes();
                return;
            }
            connection.Close();
            loadDataGrid();
            //close database
            clearTextBoxes();
            MessageBox.Show("Procedure executed with succes!");
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
                //open database 
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"DELETE FROM RegistrationCodes
                                       WHERE IdRegistration = @idCodes";

                cmd.Parameters.AddWithValue("@idCodes", id);
                // MECANISM TRATARE ERORI
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Imposibil de sters codes! " + error.Message);
                    connection.Close();
                    clearTextBoxes();
                    return;
                }
                connection.Close();
                loadDataGrid();
                clearTextBoxes();
                MessageBox.Show("Procedure executed with succes!");
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
                if(username == "")
                {
                    MessageBox.Show("Please fill username box! ");
                    return;
                }

                String code = generateCode(username);//generate code using username

                //open database
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO RegistrationCodes(Code, [ExpirationDate], [UserName], [CreatedBy]) Values(@code, @expDate, @user, @userID)";
                cmd.Parameters.AddWithValue("@user", username);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.AddWithValue("@expDate", date);
                cmd.Parameters.AddWithValue("@userID", this.loggedUserID);
                // MECANISM TRATARE ERORI
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Imposibil de adaugat cod! " + error.Message);
                    connection.Close();
                    return;
                }
                // MECANISM TRATARE ERORI
                connection.Close();
                loadDataGrid();
                clearTextBoxes();
                MessageBox.Show("Procedure executed with succes!");
            }
            else
            {
                MessageBox.Show("We don't need ID! Please don't complete it!");
                idTextBox.Clear();
                return;
            }
        }
    }
}
