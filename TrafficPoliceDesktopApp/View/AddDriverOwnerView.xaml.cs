using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    /// Interaction logic for AddDriverOwnerView.xaml
    /// </summary>
    public partial class AddDriverOwnerView : UserControl
    {
        public AddDriverOwnerView()
        {
            InitializeComponent();
        }

  

        

        public List<string> getCountryList()
        {
            List<string> LogList = new List<string>();
            try
            {
                var logFile = File.ReadAllLines(@"Resources\Textfiles\CountryListCyrillic.txt");
                LogList = new List<string>(logFile);
            }
            catch
            {
                MessageBox.Show("File does not exist!");
            }
            
             
            return LogList;

        }



        private void UserControl_Initialized(object sender, EventArgs e)
        {
            comboBoxCountries.ItemsSource = getCountryList();
            comboBoxCountries.SelectedItem = "България";
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clearForm();
        }

        private void clearForm()
        {
            txtBoxId.Text = "";
            txtBoxFirstName.Text = "";
            txtBoxSecondName.Text = "";
            txtBoxLastName.Text = "";
            comboBoxCountries.SelectedItem = "България";
            txtBoxBirthPlace.Text = "";
            txtBoxResidence.Text = "";
            upDownRemainingPts.Value = 39;
            txtBoxLicenceNum.Text = "";
            txtBoxIssuedBy.Text = "";
            datePickerIssuedDate.SelectedDate = null;
            datePickerExpiryDate.SelectedDate = null;
            clearCategories();

        }

        private void clearCategories()
        {
            datePickerA1AcquiryDate.SelectedDate = null;
            datePickerA1ExpiryDate.SelectedDate = null;
            txtBoxA1Restrictions.Text = "";

            datePickerAAcquiryDate.SelectedDate = null;
            datePickerAExpiryDate.SelectedDate = null;
            txtBoxARestrictions.Text = "";

            datePickerB1AcquiryDate.SelectedDate = null;
            datePickerB1ExpiryDate.SelectedDate = null;
            txtBoxB1Restrictions.Text = "";

            datePickerBAcquiryDate.SelectedDate = null;
            datePickerBExpiryDate.SelectedDate = null;
            txtBoxBRestrictions.Text = "";

            datePickerC1AcquiryDate.SelectedDate = null;
            datePickerC1ExpiryDate.SelectedDate = null;
            txtBoxC1Restrictions.Text = "";

            datePickerCAcquiryDate.SelectedDate = null;
            datePickerCExpiryDate.SelectedDate = null;
            txtBoxCRestrictions.Text = "";

            datePickerD1AcquiryDate.SelectedDate = null;
            datePickerD1ExpiryDate.SelectedDate = null;
            txtBoxD1Restrictions.Text = "";

            datePickerDAcquiryDate.SelectedDate = null;
            datePickerDExpiryDate.SelectedDate = null;
            txtBoxDRestrictions.Text = "";

            datePickerBEAcquiryDate.SelectedDate = null;
            datePickerBEExpiryDate.SelectedDate = null;
            txtBoxBERestrictions.Text = "";

            datePickerC1EAcquiryDate.SelectedDate = null;
            datePickerC1EExpiryDate.SelectedDate = null;
            txtBoxC1ERestrictions.Text = "";

            datePickerCEAcquiryDate.SelectedDate = null;
            datePickerCEExpiryDate.SelectedDate = null;
            txtBoxCERestrictions.Text = "";

            datePickerD1EAcquiryDate.SelectedDate = null;
            datePickerD1EExpiryDate.SelectedDate = null;
            txtBoxD1ERestrictions.Text = "";

            datePickerDEAcquiryDate.SelectedDate = null;
            datePickerDEExpiryDate.SelectedDate = null;
            txtBoxDERestrictions.Text = "";

            datePickerTtbAcquiryDate.SelectedDate = null;
            datePickerTtbExpiryDate.SelectedDate = null;
            txtBoxTtbRestrictions.Text = "";

            datePickerTtmAcquiryDate.SelectedDate = null;
            datePickerTtmExpiryDate.SelectedDate = null;
            txtBoxTtmRestrictions.Text = "";

            datePickerTktAcquiryDate.SelectedDate = null;
            datePickerTktExpiryDate.SelectedDate = null;
            txtBoxTktRestrictions.Text = "";

            datePickerMAcquiryDate.SelectedDate = null;
            datePickerMExpiryDate.SelectedDate = null;
            txtBoxMRestrictions.Text = "";
        }


    }
}
