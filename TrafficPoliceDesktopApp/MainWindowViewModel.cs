using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TrafficPoliceDesktopApp.ServiceReference1;
using TrafficPoliceDesktopApp.ViewModel;

namespace TrafficPoliceDesktopApp
{
     public class MainWindowViewModel : INotifyPropertyChanged
    {
         Service1Client service;

         //MainWindowViewModel Constructor
        public MainWindowViewModel(User usr)
        {
            User = usr;
            service = new Service1Client();

            LoggedUserName = String.Format("{0} {1}", User.FirstName, User.LastName);

            //Creating tabs VMs
            MyProfileViewModel = new MyProfileViewModel(User);
            NewUserViewModel = new NewUserViewModel();
            SearchUserViewViewModel = new SearchUserViewViewModel();
            SearchDriverOwnerViewViewModel = new SearchDriverOwnerViewViewModel();

        }

         //Get/Set User Property
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

        private string _loggedUserName;
        public string LoggedUserName
        {
            private get { return _loggedUserName; }
            set
            {
                _loggedUserName = value;
                RaisePropertyChangedEvent("LoggedUserName");
            }
        }

        private MyProfileViewModel _myProfileViewModel;
        public MyProfileViewModel MyProfileViewModel
        {
            private get { return _myProfileViewModel; }
            set
            {
                _myProfileViewModel = value;
                RaisePropertyChangedEvent("MyProfileViewModel");
            }
        }

        private NewUserViewModel _newUserViewModel;
        public NewUserViewModel NewUserViewModel
        {
            private get { return _newUserViewModel; }
            set
            {
                _newUserViewModel = value;
                RaisePropertyChangedEvent("NewUserViewModel");
            }
        }

        private SearchDriverOwnerViewViewModel _searchDriverOwnerViewViewModel;
        public SearchDriverOwnerViewViewModel SearchDriverOwnerViewViewModel
        {
            private get { return _searchDriverOwnerViewViewModel; }
            set
            {
                _searchDriverOwnerViewViewModel = value;
                RaisePropertyChangedEvent("SearchDriverOwnerViewViewModel");
            }
        }

        private SearchUserViewViewModel _searchUserViewViewModel;
        public SearchUserViewViewModel SearchUserViewViewModel
        {
            private get { return _searchUserViewViewModel; }
            set
            {
                _searchUserViewViewModel = value;
                RaisePropertyChangedEvent("SearchUserViewViewModel");
            }
        }





        //RaisePropertyChangedEvent implementation (.net 4.0, for 4.5 we can use CallerMemberName)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
