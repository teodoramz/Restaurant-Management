using Restaurant_Management.Database;
using System;
using System.Collections.Generic;
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

namespace Restaurant_Management.Procedures.EditProcedure
{
    /// <summary>
    /// Interaction logic for EditEmployees.xaml
    /// </summary>
    public partial class EditEmployees : Window
    {
        RestaurantManagementDBDataContext rmDataContext = new RestaurantManagementDBDataContext();
        private EmployeesWindow ew;
        private int currID;
        public EditEmployees(EmployeesWindow ew, int id)
        {
            InitializeComponent();
            this.ew = ew;
            currID = id;
            loadTextBoxes();
        }

        private void loadTextBoxes()
        {
            var employee = (from e in rmDataContext.Users where e.UserID == currID select e).SingleOrDefault();
            nameTextBox.Text = employee.Username;
            phoneTextBox.Text = employee.Phone;
            emailTextBox.Text = employee.Email;
        }
        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            ew.Show();
            ew.loadDataGrid();
            this.Close();
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
        private void phoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9][0-9]*");
            e.Handled = regex.IsMatch(e.Text);
        }
        //clear boxes
        void clearTextBoxes()
        {
            loadTextBoxes();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void EDITButton_Click(object sender, RoutedEventArgs e)
        {

            //function variables
            var name = nameTextBox.Text;
            var email = emailTextBox.Text;
            var phone = phoneTextBox.Text;

            if (name == "" || email == "" || phone == "") // check if boxes are empty
            {
                MessageBox.Show("Please fill all the gaps!");
                return;
            }
            if (!IsValid(email))         //check mail format
            {
                MessageBox.Show("Wrong email address format! ");
                emailTextBox.Clear();
                return;
            }

            try
            {
                User user = (from u in rmDataContext.Users
                             where u.UserID == currID
                             select u).SingleOrDefault();

                user.Username = name;
                user.Email = email;
                user.Phone = phone;

                rmDataContext.SubmitChanges();
                MessageBox.Show("Edited!");
                CloseWindowButton_Click(sender, e);
            }
            catch (Exception error)
            {
                MessageBox.Show("Imposibil de editat! " + error.Message);
                clearTextBoxes();
                return;
            }
        }

    }
}
