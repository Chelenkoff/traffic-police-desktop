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
using TrafficPoliceDesktopApp.ServiceReference1;

namespace TrafficPoliceDesktopApp.View.SearchDriverOwnerSubviews
{
    /// <summary>
    /// Interaction logic for PenaltyDetailsWindow.xaml
    /// </summary>
    public partial class PenaltyDetailsWindow : MetroWindow
    {
        public Penalty Penalty { get; set; }
        public DriverOwner DriverOwner { get; set; }
        public Service1Client ServiceReference { get; set; }
        public PenaltyDetailsWindow(Penalty pen, DriverOwner drOwner, Service1Client servRef)
        {
            InitializeComponent();

            Penalty = pen;
            DriverOwner = drOwner;
            ServiceReference = servRef;

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Title = String.Format("Нарушение № {0} - {1} {2}",Penalty.PenaltyId, DriverOwner.FirstName, DriverOwner.LastName);

            initPenaltyDetails();

        }

        private void initPenaltyDetails()
        {
            lblViewMessage.Content = String.Format("Нарушение на {0} {1}", DriverOwner.FirstName, DriverOwner.LastName);

            txtBoxPenaltyId.Text = Penalty.PenaltyId.ToString();

            datePickeIssuedDate.SelectedDate = Penalty.IssuedDateTime;
            datePickerHappenedDate.SelectedDate = Penalty.HappenedDateTime;

            txtBoxIssuerId.Text = Penalty.IssuerId.ToString();
            txtBoxLocation.Text = Penalty.Location;
            txtBoxDescription.Text = Penalty.Description;
            txtBoxDisagreement.Text = Penalty.Disagreement;
        }
    }
}
