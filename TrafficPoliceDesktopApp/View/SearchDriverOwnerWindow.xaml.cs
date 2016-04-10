﻿using System;
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
using TrafficPoliceDesktopApp.ServiceReference1;

namespace TrafficPoliceDesktopApp.View
{
    /// <summary>
    /// Interaction logic for SearchDriverOwnerWindow.xaml
    /// </summary>
    public partial class SearchDriverOwnerWindow : MetroWindow
    {
        public DriverOwner DriverOwner { get; set; }
        public Service1Client ServiceReference { get; set; }
        public SearchDriverOwnerWindow(DriverOwner drOwner, Service1Client serviceRef)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DriverOwner = drOwner;
            ServiceReference = serviceRef;

            this.Title = String.Format("{0} {1} - Справка за водач", DriverOwner.FirstName, DriverOwner.LastName);
        }




        private void MetroTabControl_TabItemClosingEvent(object sender, BaseMetroTabControl.TabItemClosingEventArgs e)
        {
            if (e.ClosingTabItem.Header.ToString().StartsWith("sizes"))
                e.Cancel = true;
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(driverOwnerPersonalDataTabItem.IsSelected)
            {

                return;
            }
            if(driverOwnerLicenceDataTabItem.IsSelected)
            {

                
                return;
            }
            if(driverOwnerPenaltiesDataTabItem.IsSelected)
            {
                if(!searchDriverOwnerPenaltyDataSubView.arePenaltiesAvailable())
                {
                    tabControl.SelectedItem = driverOwnerPersonalDataTabItem;
                }
                return;
            }

        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Initializing Personal Data tab
            searchDriverOwnerPersonalDataSubView.initDriverOwnerPersonalData(DriverOwner);

            //Initializing Licence Data tab
            searchDriverOwnerLicenceDataSubView.initDriverOwnerLicenceData(DriverOwner);

            //Initializing Penalty Data tab
            searchDriverOwnerPenaltyDataSubView.initDriverOwnerPenaltyData(this);
        }




    }
}
