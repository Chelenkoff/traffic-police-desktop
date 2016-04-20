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

namespace TrafficPoliceDesktopApp.ViewModel
{
    public class SearchDriverOwnerViewViewModel : INotifyPropertyChanged
    {
        Service1Client service;

        //Default constructor
        public SearchDriverOwnerViewViewModel()
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
        private string _driverOwnerId;
        public string DriverOwnerId
        {
            private get { return _driverOwnerId; }
            set
            {
                _driverOwnerId = value;
                RaisePropertyChangedEvent("DriverOwnerId");
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

        private bool dbResponseValidation(DriverOwner drOwner)
        {
            //DB-OK , USER - NOT FOUND
            if (drOwner != null && drOwner.DriverOwnerId == 0)
            {
                MessageBox.Show("Не съществува водач с тези данни!", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //DB - NOT CONNECTED
            else if (drOwner == null)
            {
                MessageBox.Show("Проблем с връзката с базата данни.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else return true;

        }

        private bool uiDataValidation(string id)
        {
            //Name validation
            string idValidation = InputValidator.validateId(id);

            if (idValidation != null)
            {
                MessageBox.Show(idValidation, "Невалидно ЕГН", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return false;
            }

            return true;
        }

        public ICommand SearchDriverOwnerCommand
        {
            get { return new DelegateCommand(searchDriverOwner); }
        }
        private void searchDriverOwner()
        {
            //UI validation
            if (!uiDataValidation(DriverOwnerId)) return;

            //Creating empty user
            DriverOwner drOwner = new DriverOwner();

            //Starting new non-blocking-ui task
            Task.Factory.StartNew(() =>
            {
                //startLoading();

                DispatchService.Invoke(() =>
                {
                    startLoading();
                });


                //Constructing DrierOwner from DB
                drOwner = service.GetDriverOwnerById(DriverOwnerId);

                DispatchService.Invoke(() =>
                {
                    DispatchService.Invoke(() =>
                    {
                        stopLoading();
                    });

                    //DB response validation
                    if (!dbResponseValidation(drOwner)) return;

                    //DB - OK, USER - FOUND
                    SearchDriverOwnerWindowViewModel drOwnerWindowViewModel = new SearchDriverOwnerWindowViewModel(drOwner);
                    SearchDriverOwnerWindow searchDrOwnerWindow = new SearchDriverOwnerWindow();
                    searchDrOwnerWindow.DataContext = drOwnerWindowViewModel;
                    searchDrOwnerWindow.Show();
                });
            });
        }
    }
}
