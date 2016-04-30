using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TrafficPoliceDesktopApp.ServiceReference1;
using TrafficPoliceDesktopApp.Utilities;
using TrafficPoliceDesktopApp.View;
using WCFDBService;

namespace TrafficPoliceDesktopApp.ViewModel
{
    public class SearchUserViewViewModel : INotifyPropertyChanged
    {
        Service1Client service;

        //Default constructor
        public SearchUserViewViewModel()
        {
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

        //Get/Set UserId
        private string _userId;
        public string UserId
        {
            private get { return _userId; }
            set
            {
                _userId = value;
                RaisePropertyChangedEvent("UserId");
            }
        }

        private void startLoading()
        {
            IsProgressRingActive = true;

        }
        private void stopLoading()
        {
            IsProgressRingActive = false;

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


        //UI Data validation
        private bool uiDataValidation(string id)
        {
            string idValidation = InputValidator.validateId(id);

            if (idValidation != null)
            {
                MessageBox.Show(idValidation, "Невалиден 'Служебен номер'", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                return false;
            }

            return true;
        }

        //DB Response validation
        private bool dbResponseValidation(User usr)
        {
            //DB-OK , USER - NOT FOUND
            if (usr != null && usr.UserId == 0)
            {
               MessageBox.Show("Не съществува служител с тези данни!", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

            //DB - NOT CONNECTED
            else if (usr == null)
            {
                MessageBox.Show("Проблем с връзката с базата данни.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else return true;

        }

        public ICommand SearchUserCommand
        {
            get { return new DelegateCommand(searchUser); }
        }
        private void searchUser()
        {


            //UI validation
            if (!uiDataValidation(UserId)) return;

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
                user = service.GetReadOnlyUserById(UserId);

                DispatchService.Invoke(() =>
                {
                    DispatchService.Invoke(() =>
                    {
                        stopLoading();
                    });

                    //DB response validation
                    if (!dbResponseValidation(user)) return;

                    //DB - OK, USER - FOUND
                    SearchUserWindowViewModel searchUserWindowVM = new SearchUserWindowViewModel(user);
                    SearchUserWindow searchUsrWindow = new SearchUserWindow();
                    searchUsrWindow.DataContext = searchUserWindowVM;
                    searchUsrWindow.Show();
                });
            });
        }



    }
}
