using System;
using System.Collections.Generic;
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
using TrafficPoliceDesktopApp.ServiceReference1;

namespace TrafficPoliceDesktopApp.View.SearchDriverOwnerSubviews
{
    /// <summary>
    /// Interaction logic for SearchDriverOwnerDataSubView.xaml
    /// </summary>
    public partial class SearchDriverOwnerPersonalDataSubView : UserControl
    {
        public DriverOwner DriverOwner { get; set; }
        public SearchDriverOwnerPersonalDataSubView()
        {
            InitializeComponent();
            comboBoxCountries.ItemsSource = getCountryList();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            fulfillDriverOwnerPersonalData();
            lblViewMessage.Content = String.Format("Лични данни на {0} {1}", DriverOwner.FirstName, DriverOwner.LastName);
        }


        private void fulfillDriverOwnerPersonalData()
        {
            txtBoxId.Text = DriverOwner.DriverOwnerId.ToString();
            txtBoxFirstName.Text = DriverOwner.FirstName;
            txtBoxSecondName.Text = DriverOwner.SecondName;
            txtBoxLastName.Text = DriverOwner.LastName;
            comboBoxCountries.SelectedItem = DriverOwner.Nationality;

            radioBtnMan.IsChecked = (DriverOwner.Sex == Sex.Man) ? true : false;
            radioBtnWoman.IsChecked = (DriverOwner.Sex == Sex.Woman) ? true : false;

            datePickerBirthDate.SelectedDate = DriverOwner.BirthDate;
            txtBoxBirthPlace.Text = DriverOwner.BirthPlace;
            txtBoxResidence.Text = DriverOwner.Residence;
            
            
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
    }
}
