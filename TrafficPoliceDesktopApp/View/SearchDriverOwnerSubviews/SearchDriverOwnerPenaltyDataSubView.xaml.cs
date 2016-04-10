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
using TrafficPoliceDesktopApp.ServiceReference1;

namespace TrafficPoliceDesktopApp.View.SearchDriverOwnerSubviews
{
    /// <summary>
    /// Interaction logic for SearchDriverOwnerPenaltyDataSubView.xaml
    /// </summary>
    
    public partial class SearchDriverOwnerPenaltyDataSubView : UserControl
    {
        public DriverOwner DriverOwner { get; set; }
        public SearchDriverOwnerPenaltyDataSubView()
        {
            InitializeComponent();
        }

        public void initDriverOwnerPenaltyData(DriverOwner drOwner)
        {

            DriverOwner = drOwner;

            lblViewMessage.Content = String.Format("Нарушения на {0} {1}", DriverOwner.FirstName, DriverOwner.LastName);


            //Set DataGrid items source 
            dataGridPenalties.ItemsSource = DriverOwner.Penalties;

        }

        private void dataGridPenalties_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Getting 'Penalty info' on selected row

            if((Penalty)dataGridPenalties.SelectedItem != null)
            {
                PenaltyDetailsWindow penDetailsWindow = new PenaltyDetailsWindow((Penalty)dataGridPenalties.SelectedItem,DriverOwner);
                penDetailsWindow.Show();
            }
        }








    }
}
