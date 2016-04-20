using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TrafficPoliceDesktopApp.ServiceReference1;

namespace TrafficPoliceDesktopApp.ViewModel.SearchDriverOwnerSubviewsVMs
{
    public class SearchDriverOwnerLicenceDataSubViewViewModel : INotifyPropertyChanged
    {
        //Constructor
        public SearchDriverOwnerLicenceDataSubViewViewModel(DriverOwner drOwner)
        {
            _driverOwner = drOwner;

            _viewMessage = String.Format("Шофьорска книжка на {0} {1}", DriverOwner.FirstName, DriverOwner.LastName);



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



        //Get/Set ViewMessage
        private string _viewMessage;
        public string ViewMessage
        {
            private get { return _viewMessage; }
            set
            {
                _viewMessage = value;
                RaisePropertyChangedEvent("ViewMessage");
            }
        }




    }
}
