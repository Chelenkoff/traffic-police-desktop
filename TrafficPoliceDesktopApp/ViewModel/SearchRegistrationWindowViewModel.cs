using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TrafficPoliceDesktopApp.ServiceReference1;
using WCFDBService;

namespace TrafficPoliceDesktopApp.ViewModel
{
    public class SearchRegistrationWindowViewModel : INotifyPropertyChanged
    {
        Service1Client service;
        public SearchRegistrationWindowViewModel(Registration reg)
        {
            service = new Service1Client();
            Registration = reg;

            _carTypesList = service.getAvailableCarTypes().ToList<string>();
            _title = String.Format("{0} - Справка за регистрация",_registration.RegNum);
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

        //Get/Set Title
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

        //Get/Set CarTypesList
        private List<string> _carTypesList;
        public List<string> CarTypesList
        {
            private get { return _carTypesList; }
            set
            {
                _carTypesList = value;
                RaisePropertyChangedEvent("CarTypesList");
            }
        }
    }
}
