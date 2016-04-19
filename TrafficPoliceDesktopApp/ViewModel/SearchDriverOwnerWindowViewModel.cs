using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TrafficPoliceDesktopApp.ServiceReference1;

namespace TrafficPoliceDesktopApp.ViewModel
{
    public class SearchDriverOwnerWindowViewModel : INotifyPropertyChanged
    {
        Service1Client service;

        //Default constructor
        public SearchDriverOwnerWindowViewModel(DriverOwner drOwner)
        {
            _driverOwner = drOwner;
            service = new Service1Client();

            _title = String.Format("{0} {1} - Справка за водач", DriverOwner.FirstName, DriverOwner.LastName);

            //Declaring VM's of tabs
            _searchDriverOwnerPersonalDataSubViewViewModel = new SearchDriverOwnerPersonalDataSubViewViewModel(_driverOwner);
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



        //Get/Set SearchDriverOwnerPersonalDataSubViewViewModel
        private SearchDriverOwnerPersonalDataSubViewViewModel _searchDriverOwnerPersonalDataSubViewViewModel;
        public SearchDriverOwnerPersonalDataSubViewViewModel SearchDriverOwnerPersonalDataSubViewViewModel
        {
            private get { return _searchDriverOwnerPersonalDataSubViewViewModel; }
            set
            {
                _searchDriverOwnerPersonalDataSubViewViewModel = value;
                RaisePropertyChangedEvent("SearchDriverOwnerPersonalDataSubViewViewModel");
            }
        }

        //Get/Set window title
        private string _title;
        public string Title
        {
            private get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChangedEvent("Title");
            }
        }
    }
}
