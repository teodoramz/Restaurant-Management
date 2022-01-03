using Microsoft.Win32;
using Restaurant_Management.Database;
using Restaurant_Management.Procedures.EditProcedure;
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
using System.Xml;

namespace Restaurant_Management
{
    /// <summary>
    /// Interaction logic for EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        //database variables
        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();

        int loggedUserID;

        //page constructor
        public EmployeesWindow(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
            loadDataGrid();
        }
        private void loadComboBox()
        {
            usersComboBox.Items.Clear();
            usersComboBox.Items.Insert(0, "Users ... ");
            var categories = (from c in rmDataContext.Users
                              where c.RoleID == 2
                              select c.Username).ToList();

            foreach (var x in categories)
            {
                usersComboBox.Items.Add(x);
            }
            usersComboBox.SelectedIndex = 0;
        }
        //load datagrid
        public void loadDataGrid()
        {
            var users = (from u in rmDataContext.Users
                         join r in rmDataContext.Roles
                         on u.RoleID equals r.RoleID
                         select new
                         {
                             ID = u.UserID,
                             Name = u.Username,
                             Phone = u.Phone,
                             Email = u.Email,
                             Role = r.RoleName
                         }).ToList();

            dataTableChosed.ItemsSource = users;
            loadComboBox();

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

        //clear boxes
        void clearTextBoxes()
        {
            idTextBox.Clear();
        }

        //edit employees procedure
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            //function variables
            var id = idTextBox.Text;

            if (id == "") // check if boxes are empty
            {
                MessageBox.Show("Please fill all the gaps!");
                return;
            }

            EditEmployees ee = new EditEmployees(this, Convert.ToInt32(id));
            ee.Show();
            this.Hide();
        }
        //delete employee procedure
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var id = idTextBox.Text;
            if (id == "")
            {
                MessageBox.Show("Please add an id!");
                return;
            }

            // MECANISM TRATARE ERORI
            try
            {
                User user = rmDataContext.Users.Where(x => x.UserID == Convert.ToInt32(id)).FirstOrDefault();
                RegistrationCode reg = rmDataContext.RegistrationCodes.Where(x => x.CreatedBy == user.UserID).FirstOrDefault();
                List<History> history = rmDataContext.Histories.Where(x => x.IdUser == user.UserID).ToList();
                if (history.Count() != 0)
                {
                    foreach (var x in history)
                    {
                        var helper = x;
                        List<Sell> sell = rmDataContext.Sells.Where(y => y.IdHistory == x.IdHistory).ToList();
                        if(sell.Count()!=0)
                        {
                            foreach (var z in sell)
                            {
                                rmDataContext.Sells.DeleteOnSubmit(z);
                            }
                        }
                        rmDataContext.Histories.DeleteOnSubmit(helper);
                    }
                }
                if (reg != null)
                {
                    rmDataContext.RegistrationCodes.DeleteOnSubmit(reg);
                }

                rmDataContext.Users.DeleteOnSubmit(user);
                rmDataContext.SubmitChanges();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de sters userul! " + error.Message);
                clearTextBoxes();
                return;
            }
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
            string username = usersComboBox.SelectedItem.ToString();
            if (username == "Users ... ")
            {
                MessageBox.Show("Chose an employee first!");
                return;
            }

            try
            {
                int id = rmDataContext.Users.
                    Where(x => x.Username == username).SingleOrDefault().UserID;

                User user = rmDataContext.Users.Where(x => x.UserID == id).SingleOrDefault();

                user.RoleID = 1;

                rmDataContext.SubmitChanges();
                MessageBox.Show("Promoted!");
                loadDataGrid();
                clearTextBoxes();
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de promovat la admin! " + error.Message);
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

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            string filename = "";
            OpenFileDialog sf = new OpenFileDialog();
            sf.Filter = "XML(*.XML)|*.xml";
            if (sf.ShowDialog() == true)
            {
                filename = sf.FileName;
            }
            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Adding canceled!");
                return;
            }
            List<int> uid = new List<int>();
            List<string> usernames = new List<string>();
            List<string> phones = new List<string>();
            List<string> emails = new List<string>();
            List<string> roles = new List<string>();
            List<string> passwords = new List<string>();

            int nrUsers = 0;
            using (var xmlReader = new XmlTextReader(filename))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        switch (xmlReader.Name)
                        {
                            case "User":
                                nrUsers++;
                                break;
                            case "Username":
                                    usernames.Add(xmlReader.ReadElementContentAsString());
                                    break;
                            case "Password":
                                passwords.Add(xmlReader.ReadElementContentAsString());
                                break;
                            case "Email":
                                emails.Add(xmlReader.ReadElementContentAsString());
                                break;
                            case "Phone":
                                phones.Add(xmlReader.ReadElementContentAsString());
                                break;
                            case "Role":
                                roles.Add(xmlReader.ReadElementContentAsString());
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            if(nrUsers!=0)
            {
                List<User> listUsers = new List<User>();
                for(int i = 0; i<nrUsers; i++)
                {
                    var user = new User
                    {
                        Username = usernames[i],
                        Password = passwords[i],
                        Email = emails[i],
                        Phone = phones[i],
                        RoleID = (from r in rmDataContext.Roles 
                                  where r.RoleName == roles[i] select r.RoleID).SingleOrDefault(),
                    };

                    var usr = (from u in rmDataContext.Users where u.Username == user.Username select u).ToList();
                    if(usr.Count()!=0)
                    {
                        MessageBox.Show($"Can't register a user with this credidentials! Prbl: Username:{user.Username}");
                        return;
                    }
                }
                foreach (var x in listUsers)
                {
                    rmDataContext.Users.InsertOnSubmit(x);
                }
                rmDataContext.SubmitChanges();
                loadDataGrid();
            }
        }
    }
}
