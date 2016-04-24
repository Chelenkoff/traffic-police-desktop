using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using TrafficPoliceDesktopApp.ServiceReference1;
using TrafficPoliceDesktopApp.View;
using TrafficPoliceDesktopApp.ViewModel;
using WCFDBService;

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
            AddDriverOwnerViewViewModel = new AddDriverOwnerViewViewModel();
            SearchDriverOwnerViewViewModel = new SearchDriverOwnerViewViewModel();
            AddRegistrationViewViewModel = new AddRegistrationViewViewModel();


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

        //Get/Set User CanWindowClose
        private bool? _canWindowClose;
        public bool? CanWindowClose
        {
            private get { return _canWindowClose; }
            set
            {
                _canWindowClose = value;
                RaisePropertyChangedEvent("CanWindowClose");
            }
        }



        //CloseWindowCommand 
        public ICommand CloseWindowCommand
        {
            get { return new DelegateCommand(CloseWindow); }
        }

        //Login func
        private void CloseWindow()
        {
            Login login = new Login();
            CanWindowClose = true;

            login.Show();


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

        private AddDriverOwnerViewViewModel _addDriverOwnerViewViewModel;
        public AddDriverOwnerViewViewModel AddDriverOwnerViewViewModel
        {
            private get { return _addDriverOwnerViewViewModel; }
            set
            {
                _addDriverOwnerViewViewModel = value;
                RaisePropertyChangedEvent("AddDriverOwnerViewViewModel");
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

        private AddRegistrationViewViewModel _addRegistrationViewViewModel;
        public AddRegistrationViewViewModel AddRegistrationViewViewModel
        {
            private get { return _addRegistrationViewViewModel; }
            set
            {
                _addRegistrationViewViewModel = value;
                RaisePropertyChangedEvent("AddRegistrationViewViewModel");
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
