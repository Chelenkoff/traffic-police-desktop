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
                MessageBox.Show("hahah");
            }
            
             
            return LogList;

        }



        private void UserControl_Initialized(object sender, EventArgs e)
        {
            comboBoxCountries.ItemsSource = getCountryList();
            comboBoxCountries.SelectedItem = "България";
        }


    }
}
