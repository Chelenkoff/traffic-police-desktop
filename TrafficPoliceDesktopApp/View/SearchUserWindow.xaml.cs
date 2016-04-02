using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TrafficPoliceDesktopApp.ServiceReference1;

namespace TrafficPoliceDesktopApp.View
{
    /// <summary>
    /// Interaction logic for SearchUserWindow.xaml
    /// </summary>
    /// 
    public partial class SearchUserWindow : MetroWindow
    {
        public User User { get; set; }
        public SearchUserWindow(User usr)
        {
            InitializeComponent();
            User = usr;
            this.Title = String.Format("{0} {1} - Справка за служител", User.FirstName, User.LastName);
            populateFields();
        }

        void populateFields()
        {
            lblFirstName.Content = User.FirstName;
            lblSecondName.Content = User.SecondName;
            lblLastName.Content = User.LastName;
            lblId.Content = User.UserId;
            checkBoxIsTrafficPoliceman.IsChecked = User.IsTrafficPoliceman;
        }
    }
}
