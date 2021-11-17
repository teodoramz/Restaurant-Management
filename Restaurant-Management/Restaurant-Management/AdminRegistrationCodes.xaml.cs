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
        public AdminRegistrationCodes(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
            loadDataGrid();
        }

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

        private void editButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {

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
        public static char cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {

                return ch;
            }

            char d = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - d) % 26) + d);


        }
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
        private void generateCodeButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            date.AddDays(7);
            String username = usernameTextBox.Text;
            String code=generateCode(username);

            connection.Open();

            //mai trebuie facuta verificarea pe codul generat
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
                MessageBox.Show("Imposibil de adaugat cod! " + error.Message );
                connection.Close();
                return;
            }
            // MECANISM TRATARE ERORI
            connection.Close();
        }
    }
}
