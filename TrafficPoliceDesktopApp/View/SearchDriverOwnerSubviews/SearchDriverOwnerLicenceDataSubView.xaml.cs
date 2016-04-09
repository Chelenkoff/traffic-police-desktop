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
using TrafficPoliceDesktopApp.Utilities;

namespace TrafficPoliceDesktopApp.View.SearchDriverOwnerSubviews
{
    /// <summary>
    /// Interaction logic for SearchDriverOwnerLicenceDataSubView.xaml
    /// </summary>
    public partial class SearchDriverOwnerLicenceDataSubView : UserControl
    {
        public DriverOwner DriverOwner { get; set; }
        public SearchDriverOwnerLicenceDataSubView()
        {
            InitializeComponent();
        }



        public void initDriverOwnerLicenceData(DriverOwner drOwner)
        {

            

            DriverOwner = drOwner;

            lblViewMessage.Content = String.Format("Шофьорска книжка на {0} {1}", DriverOwner.FirstName, DriverOwner.LastName);

            txtBoxIssuedBy.Text = DriverOwner.LicenceIssuedBy;
            datePickerIssuedDate.SelectedDate = DriverOwner.LicenceIssueDate;
            datePickerExpiryDate.SelectedDate = DriverOwner.LicenceExpiryDate;
            upDownRemainingPts.Value = DriverOwner.RemainingPts;

            fulfilCategoriesData();

            
            
        }

        private void fulfilCategoriesData()
        {
            datePickerA1AcquiryDate.SelectedDate = DriverOwner.Categories.a1AcquiryDate;
            datePickerA1ExpiryDate.SelectedDate = DriverOwner.Categories.a1ExpiryDate;
            txtBoxA1Restrictions.Text = DriverOwner.Categories.a1Restrictions;

            datePickerAAcquiryDate.SelectedDate = DriverOwner.Categories.aAcquiryDate;
            datePickerAExpiryDate.SelectedDate = DriverOwner.Categories.aExpiryDate;
            txtBoxARestrictions.Text = DriverOwner.Categories.aRestrictions;

            datePickerB1AcquiryDate.SelectedDate = DriverOwner.Categories.b1AcquiryDate;
            datePickerB1ExpiryDate.SelectedDate = DriverOwner.Categories.b1ExpiryDate;
            txtBoxB1Restrictions.Text = DriverOwner.Categories.b1Restrictions;

            datePickerBAcquiryDate.SelectedDate = DriverOwner.Categories.bAcquiryDate;
            datePickerBExpiryDate.SelectedDate = DriverOwner.Categories.bExpiryDate;
            txtBoxBRestrictions.Text = DriverOwner.Categories.bRestrictions;

            datePickerC1AcquiryDate.SelectedDate = DriverOwner.Categories.c1AcquiryDate;
            datePickerC1ExpiryDate.SelectedDate = DriverOwner.Categories.c1ExpiryDate;
            txtBoxC1Restrictions.Text = DriverOwner.Categories.c1Restrictions;

            datePickerCAcquiryDate.SelectedDate = DriverOwner.Categories.cAcquiryDate;
            datePickerCExpiryDate.SelectedDate = DriverOwner.Categories.cExpiryDate;
            txtBoxCRestrictions.Text = DriverOwner.Categories.cRestrictions;

            datePickerD1AcquiryDate.SelectedDate = DriverOwner.Categories.d1AcquiryDate;
            datePickerD1ExpiryDate.SelectedDate = DriverOwner.Categories.d1ExpiryDate;
            txtBoxD1Restrictions.Text = DriverOwner.Categories.d1Restrictions;

            datePickerDAcquiryDate.SelectedDate = DriverOwner.Categories.dAcquiryDate;
            datePickerDExpiryDate.SelectedDate = DriverOwner.Categories.dExpiryDate;
            txtBoxDRestrictions.Text = DriverOwner.Categories.dRestrictions;

            datePickerBEAcquiryDate.SelectedDate = DriverOwner.Categories.beAcquiryDate;
            datePickerBEExpiryDate.SelectedDate = DriverOwner.Categories.beExpiryDate;
            txtBoxBERestrictions.Text = DriverOwner.Categories.beRestrictions;

            datePickerC1EAcquiryDate.SelectedDate = DriverOwner.Categories.c1eAcquiryDate;
            datePickerC1EExpiryDate.SelectedDate = DriverOwner.Categories.c1eExpiryDate;
            txtBoxC1ERestrictions.Text = DriverOwner.Categories.c1eRestrictions;

            datePickerCEAcquiryDate.SelectedDate = DriverOwner.Categories.ceAcquiryDate;
            datePickerCEExpiryDate.SelectedDate = DriverOwner.Categories.ceExpiryDate;
            txtBoxCERestrictions.Text = DriverOwner.Categories.ceRestrictions;

            datePickerD1EAcquiryDate.SelectedDate = DriverOwner.Categories.d1eAcquiryDate;
            datePickerD1EExpiryDate.SelectedDate = DriverOwner.Categories.d1eExpiryDate;
            txtBoxD1ERestrictions.Text = DriverOwner.Categories.d1eRestrictions;

            datePickerDEAcquiryDate.SelectedDate = DriverOwner.Categories.deAcquiryDate;
            datePickerDEExpiryDate.SelectedDate = DriverOwner.Categories.deExpiryDate;
            txtBoxDERestrictions.Text = DriverOwner.Categories.deRestrictions;

            datePickerTtbAcquiryDate.SelectedDate = DriverOwner.Categories.ttbAcquiryDate;
            datePickerTtbExpiryDate.SelectedDate = DriverOwner.Categories.ttbExpiryDate;
            txtBoxTtbRestrictions.Text = DriverOwner.Categories.ttbRestrictions;

            datePickerTtmAcquiryDate.SelectedDate = DriverOwner.Categories.ttmAcquiryDate;
            datePickerTtmExpiryDate.SelectedDate = DriverOwner.Categories.ttmExpiryDate;
            txtBoxTtmRestrictions.Text = DriverOwner.Categories.ttmRestrictions;

            datePickerTktAcquiryDate.SelectedDate = DriverOwner.Categories.tktAcquiryDate;
            datePickerTktExpiryDate.SelectedDate = DriverOwner.Categories.tktExpiryDate;
            txtBoxTktRestrictions.Text = DriverOwner.Categories.tktRestrictions;

            datePickerMAcquiryDate.SelectedDate = DriverOwner.Categories.mAcquiryDate;
            datePickerMExpiryDate.SelectedDate = DriverOwner.Categories.mExpiryDate;
            txtBoxMRestrictions.Text = DriverOwner.Categories.mRestrictions;

            //Coloring the available dates
            foreach (DatePicker tb in ControlsIterator.FindVisualChildren<DatePicker>(gridCategories))
            {
                if (tb.SelectedDate != null)
                    tb.Foreground = Brushes.Red;
            }



        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViewerDriverLicenceData.ScrollToTop();
        }
    }
}
