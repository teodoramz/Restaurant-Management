﻿using System;
using System.Collections.Generic;
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
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        int loggedUserID;
        public DashboardWindow(int userID)
        {
            InitializeComponent();
            this.loggedUserID = userID;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void categoriesTextButton_Click(object sender, RoutedEventArgs e)
        {
            AdminCategoriesWindow acw = new AdminCategoriesWindow();
            acw.Show();
            this.Hide();
        }

        private void productsTextButton_Click(object sender, RoutedEventArgs e)
        {
            AdminProductsWindow apw = new AdminProductsWindow();
            apw.Show();
            this.Hide();
        }

        private void registration_codesTextButton_Click(object sender, RoutedEventArgs e)
        {
            AdminRegistrationCodes arc = new AdminRegistrationCodes(loggedUserID);
            arc.Show();
            this.Hide();
        }

        private void historyTextButton_Click(object sender, RoutedEventArgs e)
        {
            AdminHistoryWindow ahw = new AdminHistoryWindow();
            ahw.Show();
            this.Hide();
        }

        private void employessTextButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeesWindow ew = new EmployeesWindow(loggedUserID);
            ew.Show();
            this.Hide();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow lw = new LoginWindow();
            lw.Show();
            this.Hide();
        }
    }
}
