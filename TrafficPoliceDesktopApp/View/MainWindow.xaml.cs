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
        
        public Service1Client Service { get; set; }
        public Login LoginWindow { get; set; }

        public User User { get; set; }

        public MainWindow(User usr)
        {
            Service = new Service1Client();
            InitializeComponent();
            
            User = usr;

            //Passing User to default tab item view
            //viewMyProfile.User = User;

            lblLoggedUserName.Content = String.Format("{0} {1}", User.FirstName, User.LastName);   
        }
        
        







        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            logout();
        }

        private void logout()
        {
            
            
            LoginWindow.Show();
            this.Hide();
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
                viewMyProfile.ParentWindow = this;
            }
            if(newUserTabItem.IsSelected == true)
            {
                viewNewUser.ParentWindow = this;
            }
            if(searchUserTabItem.IsSelected == true)
            {
                viewSearchUser.ParentWindow = this;
            }
            

        }

        public void updateLoggedUsername()
        {
            lblLoggedUserName.Content = String.Format("{0} {1}", User.FirstName, User.LastName);
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Service.Close();
            //Environment.Exit(0);
        }











    }
}
