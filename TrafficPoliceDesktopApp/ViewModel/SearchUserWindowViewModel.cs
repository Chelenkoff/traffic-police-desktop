using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TrafficPoliceDesktopApp.ServiceReference1;

namespace TrafficPoliceDesktopApp.ViewModel
{
    public class SearchUserWindowViewModel : INotifyPropertyChanged
    {
        Service1Client service;

        public SearchUserWindowViewModel(User usr)
        {
            _user = usr;

            _title = String.Format("{0} {1} - Справка за служител", User.FirstName, User.LastName);

            service = new Service1Client();

        }

        //RaisePropertyChangedEvent implementation (.net 4.0, for 4.5 we can use CallerMemberName)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        //Get/Set User
        private User _user;
        public User User
        {
            private get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChangedEvent("User");
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
    }
}
