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
    public class SearchRegistrationViewViewModel : INotifyPropertyChanged
    {
        Service1Client service;

        //Constructor
        public SearchRegistrationViewViewModel()
        {
            service = new Service1Client();
        }

        //Get/Set RegNum
        private string _regNum;
        public string RegNum
        {
            private get { return _regNum; }
            set
            {
                _regNum = value;
                RaisePropertyChangedEvent("RegNum");
            }
        }

        public ICommand SearchRegCommand
        {
            get { return new DelegateCommand(searchReg); }
        }
        private void searchReg()
        {
            //UI validation
            //if (!uiDataValidation(RegNum)) return;

            //Creating empty user
            Registration reg = new Registration();

            //Starting new non-blocking-ui task
            Task.Factory.StartNew(() =>
            {
                //startLoading();

                DispatchService.Invoke(() =>
                {
                    startLoading();
                });


                //Constructing Registration from REST response
                reg = service.getRegByRegNum(RegNum);

                DispatchService.Invoke(() =>
                {
                    DispatchService.Invoke(() =>
                    {
                        stopLoading();
                    });

                    //DB response validation
                    if (!dbResponseValidation(reg)) return;

                    //DB - OK, USER - FOUND
                    SearchRegistrationWindowViewModel vm = new SearchRegistrationWindowViewModel(reg);
                    SearchRegistrationWindow searchRegWindow = new SearchRegistrationWindow();
                    searchRegWindow.DataContext = vm;
                    searchRegWindow.Show();
                });
            });
        }

        //RaisePropertyChangedEvent implementation (.net 4.0, for 4.5 we can use CallerMemberName)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void startLoading()
        {
            IsProgressRingActive = true;

        }
        private void stopLoading()
        {
            IsProgressRingActive = false;

        }

        private bool dbResponseValidation(Registration reg)
        {
            //DB-OK , USER - NOT FOUND
            if (reg != null && reg.RegNum ==null)
            {
                MessageBox.Show(String.Format("Не съществува регистрация {0}!",_regNum), "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //DB - NOT CONNECTED
            else if (reg == null)
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
    }
}
