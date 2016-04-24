using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TrafficPoliceDesktopApp.ServiceReference1;
using WCFDBService;

namespace TrafficPoliceDesktopApp.ViewModel
{
    public class AddRegistrationViewViewModel : INotifyPropertyChanged
    {
        Service1Client service;
        public AddRegistrationViewViewModel()
        {
            service = new Service1Client();
            service.getAvailableCarTypes();
            _registration = new Registration();
        }

        //RaisePropertyChangedEvent implementation (.net 4.0, for 4.5 we can use CallerMemberName)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        //Get/Set Registration
        private Registration _registration;
        public Registration Registration
        {
            private get { return _registration; }
            set
            {
                _registration = value;
                RaisePropertyChangedEvent("Registration");
            }
        }
    }
}
