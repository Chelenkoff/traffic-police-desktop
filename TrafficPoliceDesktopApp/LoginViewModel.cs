﻿using System;
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


namespace TrafficPoliceDesktopApp
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        const int MAX_ID_LENGTH = 10;
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
                DispatchService.Invoke(() =>
                {
                    stopLoading();
                });

                if (!dbResponseValidation(user)) return;

                //Creating MainWindowViewModel and attaching it to MainWindow
                MainWindowViewModel mainWindowVM = new MainWindowViewModel(user);
                MainWindow mainWindow = new MainWindow();
                mainWindow.DataContext = mainWindowVM;
                mainWindow.Show();
            });
           });


            
        }

        private bool uiPassAndIdValidation(string pass, string id)
        {
            //Null or whitespaces
            if (String.IsNullOrWhiteSpace(pass) || String.IsNullOrWhiteSpace(id))
            {
                System.Windows.MessageBox.Show("Не са въведени необходимите данни!", "Внимание");
                return false;
            }
            //Digit check
            else if (!id.All(char.IsDigit))
            {
                System.Windows.MessageBox.Show("Некоректен формат на ЕГН! \nМоже да съдържа само цифри!", "Внимание");
                return false;
            }
            //Id length check
            else if (id.Length != MAX_ID_LENGTH)
            {
                System.Windows.MessageBox.Show("ЕГН трябва да съдържа точно 10 цифри!", "Внимание");
                return false;
            }
            else return true;
        }

        private bool dbResponseValidation(User usr)
        {
            //DB-OK , USER - NOT FOUND
            if (usr != null && usr.UserId == 0)
            {
                System.Windows.MessageBox.Show("Не съществува потребител с тези данни!", "Грешка");
                return false;
            }

            //DB - NOT CONNECTED
            else if (usr == null)
            {
                System.Windows.MessageBox.Show("Проблем с връзката с базата данни.", "Грешка");
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