using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using TrafficPoliceDesktopApp.ViewModel;
using WCFDBService;

namespace TrafficPoliceDesktopApp.View
{
    /// <summary>
    /// Interaction logic for SearchDriverOwnerWindow.xaml
    /// </summary>
    public partial class SearchDriverOwnerWindow : MetroWindow
    {


        public SearchDriverOwnerWindow(DriverOwner drOwner, User usr)
        {
            InitializeComponent();
            this.DataContext = new SearchDriverOwnerWindowViewModel(drOwner, usr);
        }



    }
}
