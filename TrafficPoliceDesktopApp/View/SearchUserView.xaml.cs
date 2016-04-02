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

namespace TrafficPoliceDesktopApp.View
{
    /// <summary>
    /// Interaction logic for SearchUserView.xaml
    /// </summary>
    public partial class SearchUserView : UserControl
    {
        public MainWindow ParentWindow { get; set; }
        public SearchUserView()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchUserWindow searchUsrWindow = new SearchUserWindow(ParentWindow.User);
            searchUsrWindow.Show();
        }


    }
}
