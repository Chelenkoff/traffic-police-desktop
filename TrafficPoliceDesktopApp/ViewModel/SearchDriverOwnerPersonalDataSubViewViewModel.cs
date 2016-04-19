using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using TrafficPoliceDesktopApp.ServiceReference1;

namespace TrafficPoliceDesktopApp.ViewModel
{
    public class SearchDriverOwnerPersonalDataSubViewViewModel : INotifyPropertyChanged
    {
        Service1Client service;

        public SearchDriverOwnerPersonalDataSubViewViewModel(DriverOwner drOwner)
        {
            _driverOwner = drOwner;
            service = new Service1Client();

            _messageTitle = String.Format("Лични данни на {0} {1}", _driverOwner.FirstName, _driverOwner.LastName);
            _countriesList = getCountryList();

            IsManChecked = (_driverOwner.Sex == Sex.Man) ? true : false;
            IsWomanChecked = (_driverOwner.Sex == Sex.Woman) ? true : false;
        }

        //RaisePropertyChangedEvent implementation (.net 4.0, for 4.5 we can use CallerMemberName)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        //Get/Set DriverOwner
        private DriverOwner _driverOwner;
        public DriverOwner DriverOwner
        {
            private get { return _driverOwner; }
            set
            {
                _driverOwner = value;
                RaisePropertyChangedEvent("DriverOwner");
            }
        }

        //Get/Set IsManChecked
        private bool _isManChecked;
        public bool IsManChecked
        {
            private get { return _isManChecked; }
            set
            {
                _isManChecked = value;
                RaisePropertyChangedEvent("IsManChecked");
            }
        }

        //Get/Set IsWomanChecked
        private bool _isWomanChecked;
        public bool IsWomanChecked
        {
            private get { return _isWomanChecked; }
            set
            {
                _isWomanChecked = value;
                RaisePropertyChangedEvent("IsWomanChecked");
            }
        }

        //Get/Set Message Title
        private string _messageTitle;
        public string MessageTitle
        {
            private get { return _messageTitle; }
            set
            {
                _messageTitle = value;
                RaisePropertyChangedEvent("MessageTitle");
            }
        }


        //Get/Set Message Title
        private List<string> _countriesList;
        public List<string> CountriesList
        {
            private get { return _countriesList; }
            set
            {
                _countriesList = value;
                RaisePropertyChangedEvent("CountriesList");
            }
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
