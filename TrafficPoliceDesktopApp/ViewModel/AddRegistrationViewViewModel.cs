using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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

            _carTypesList = service.getAvailableCarTypes().ToList<string>();
           
            initDefaultRegistration();
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

        public ICommand AddRegistrationCommand
        {
            get { return new DelegateCommand(addReg); }
        }

        private void addReg()
        {
            //if (!uiDataValidation(_driverOwner.DriverOwnerId.ToString(), _driverOwner.FirstName, _driverOwner.SecondName, _driverOwner.LastName, _driverOwner.BirthDate.ToString(), _driverOwner.BirthPlace, _driverOwner.Residence, _driverOwner.LicenceIssuedBy, _driverOwner.LicenceIssueDate.ToString(), _driverOwner.LicenceExpiryDate.ToString()))
            //    return;



            int checkResponse;
            //Starting new non-blocking-ui task
            Task.Factory.StartNew(() =>
            {
                //startLoading();

                DispatchService.Invoke(() =>
                {
                    startLoading();
                });

                //Receiving REST api response
                checkResponse = service.InsertRegistration(_registration);

                DispatchService.Invoke(() =>
                {
                    DispatchService.Invoke(() =>
                    {
                        stopLoading();
                    });

                    switch (checkResponse)
                    {
                        case 1:
                            MessageBox.Show(String.Format("Водач с ЕГН: {0} не съществува",_registration.DriverOwnerId), "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case 2:
                            MessageBox.Show(String.Format("Съществува водач с егн: {0}", _registration.DriverOwnerId), "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case 0:
                            MessageBox.Show("Регистрацията бе успешно добавена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                            //initDefaultDriverOwner();
                            break;
                        case 3:
                            MessageBox.Show(String.Format("Съществува регистрация {0}",_registration.RegNum), "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                            //initDefaultDriverOwner();
                            break;

                    }
                });
            });

        }

        public ICommand ClearRegCommand
        {
            get { return new DelegateCommand(clearReg); }
        }

        private void clearReg()
        {
            initDefaultRegistration();

        }

        private void initDefaultRegistration()
        {
            Registration = new Registration();

            Registration.RegNum = "";
            Registration.DriverOwnerId = 0;
            Registration.CarManufacturer = "";
            Registration.CarModel = "";
            Registration.CarColor = "";
            //Registration.CarType = "Лек автомобил";
            Registration.CarCubage = 0;
            Registration.CarHp = 0;
            Registration.CarVin = "";
            Registration.FirstRegDate = new DateTime();
            Registration.RecentRegDate = new DateTime();

            Registration.HasCivilInsurance = false;
            Registration.CivilInsurer = "";
            Registration.CivilInsuranceStartDate = null;
            Registration.CivilInsuranceEndDate = null;

            Registration.HasVignette = false;
            Registration.VignetteValidUntil = null;

            Registration.HasDamageInsurance = false;
            Registration.DamageInsurer = "";
            Registration.DamageInsuranceStartDate = null;
            Registration.DamageInsuranceEndDate = null;

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

        private void startLoading()
        {
            IsProgressRingActive = true;

        }
        private void stopLoading()
        {
            IsProgressRingActive = false;

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
