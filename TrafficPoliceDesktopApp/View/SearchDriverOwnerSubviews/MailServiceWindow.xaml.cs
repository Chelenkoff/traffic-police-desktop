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
using TrafficPoliceDesktopApp.ViewModel.SearchDriverOwnerSubviewsVMs;
using WCFDBService;

namespace TrafficPoliceDesktopApp.View.SearchDriverOwnerSubviews
{
    /// <summary>
    /// Interaction logic for MailServiceWindow.xaml
    /// </summary>
    public partial class MailServiceWindow : MetroWindow
    {
        public MailServiceWindow(DriverOwner drOwner, User usr)
        {
            InitializeComponent();
            this.DataContext = new MailServiceWindowViewModel(drOwner,usr);
        }
    }
}
