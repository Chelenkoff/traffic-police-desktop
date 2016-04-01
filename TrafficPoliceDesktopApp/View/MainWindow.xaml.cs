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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using TrafficPoliceDesktopApp.Utilities;
using TrafficPoliceDesktopApp.ServiceReference1;
using TrafficPoliceDesktopApp.View;

namespace TrafficPoliceDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        

        public User User { get; set; }

        public MainWindow(User usr)
        {
            InitializeComponent();
            
            User = usr;

            //Passing User to default tab item view
            //viewMyProfile.User = User;

            lblLoggedUserName.Content = String.Format("{0} {1}", User.FirstName, User.LastName);   
        }
        
        







        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            logout();
        }

        private void logout()
        {
            Login loginForm = new Login();
            loginForm.Show();
            this.Close();
        }

        private void MetroTabControl_TabItemClosingEvent(object sender, BaseMetroTabControl.TabItemClosingEventArgs e)
        {
            if (e.ClosingTabItem.Header.ToString().StartsWith("sizes"))
                e.Cancel = true;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myProfileTabItem.IsSelected == true)
            {
                viewMyProfile.User = User;
                viewMyProfile.ParentWindow = this;
                
            }

        }

        public void updateLoggedUsername()
        {
            lblLoggedUserName.Content = String.Format("{0} {1}", User.FirstName, User.LastName);
        }







    }
}
