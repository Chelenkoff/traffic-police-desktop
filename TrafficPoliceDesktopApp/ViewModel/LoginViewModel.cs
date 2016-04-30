using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using TrafficPoliceDesktopApp.ServiceReference1;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using System.Windows;
using WCFDBService;

namespace TrafficPoliceDesktopApp
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        Service1Client service;
        

        //LoginViewModel Constructor
        public LoginViewModel()
        {
            service = new Service1Client();
            stopLoading();
        }


        //Password property
        private string _pass;
        public string Password
        {
            private get { return _pass; }
            set
             {
                 _pass = value;
                 RaisePropertyChangedEvent("Password");
             }
        }





        //UserId property
        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                RaisePropertyChangedEvent("UserId");
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



        //Login command
        public ICommand LoginCommand
        {
            get { return new DelegateCommand(Login); }
        }

        //Login func
        private void Login()
        {

             //UI validation of ID and PASS
             if (!uiPassAndIdValidation(Password, UserId)) return;

            //Creating empty user
            User user = new User();

            //Starting new non-blocking-ui task

            Task.Factory.StartNew(() =>
            {
                //startLoading();

                DispatchService.Invoke(() =>
                {
                    startLoading();
                });



                //Constructing USER from DB
            user = service.GetUserByIdAndPass(UserId, Password);

            DispatchService.Invoke(() =>
            {
                    stopLoading();

                if (!dbResponseValidation(user)) return;

                //Creating MainWindowViewModel and attaching it to MainWindow
                MainWindowViewModel mainWindowVM = new MainWindowViewModel(user);
                MainWindow mainWindow = new MainWindow();
                mainWindow.DataContext = mainWindowVM;

                mainWindow.Show();
                CanWindowClose = true;//Closing login window
                
            });
           });


            
        }

        private bool uiPassAndIdValidation(string pass, string id)
        {
            //Null or whitespaces
            if (String.IsNullOrWhiteSpace(pass) || String.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Не са въведени необходимите данни!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            //Digit check
            else if (!id.All(char.IsDigit))
            {
               MessageBox.Show("Некоректен формат на 'Служебен номер'! \nМоже да съдържа само цифри!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            else return true;
        }

        private bool dbResponseValidation(User usr)
        {
            //DB-OK , USER - NOT FOUND
            if (usr != null && usr.UserId == 0)
            {
                MessageBox.Show("Не съществува потребител с тези данни!", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //DB - NOT CONNECTED
            else if (usr == null)
            {
                MessageBox.Show("Проблем с връзката с базата данни.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }
            else return true;

        }



        //IsProgressRingActive property
        private bool _isProgressRingActive;
        public bool IsProgressRingActive
        {
            get { return _isProgressRingActive; }
            set
            {
                _isProgressRingActive = value;
                RaisePropertyChangedEvent("IsProgressRingActive");
            }
        }

        //IsTxtBoxEGNActive property
        private bool _isTxtBoxEGNEnabled;
        public bool IsTxtBoxEGNEnabled
        {
            get { return _isTxtBoxEGNEnabled; }
            set
            {
                _isTxtBoxEGNEnabled = value;
                RaisePropertyChangedEvent("IsTxtBoxEGNEnabled");
            }
        }

        //IsPswdBoxPassEnabled property
        private bool _isPswdBoxPassEnabled;
        public bool IsPswdBoxPassEnabled
        {
            get { return _isPswdBoxPassEnabled; }
            set
            {
                _isPswdBoxPassEnabled = value;
                RaisePropertyChangedEvent("IsPswdBoxPassEnabled");
            }
        }

        //IsBtnLoginEnabled property
        private bool _isBtnLoginEnabled;
        public bool IsBtnLoginEnabled
        {
            get { return _isBtnLoginEnabled; }
            set
            {
                _isBtnLoginEnabled = value;
                RaisePropertyChangedEvent("IsBtnLoginEnabled");
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






        private void startLoading()
        {
            IsProgressRingActive = true;
            IsTxtBoxEGNEnabled = false;
            IsPswdBoxPassEnabled = false;
            IsBtnLoginEnabled = false;
        }
        private void stopLoading()
        {
            IsProgressRingActive = false;
            IsTxtBoxEGNEnabled = true;
            IsPswdBoxPassEnabled = true;
            IsBtnLoginEnabled = true;
        }

    }
}
