using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TrafficPoliceDesktopApp.ServiceReference1;
using TrafficPoliceDesktopApp.Utilities;
using WCFDBService;

namespace TrafficPoliceDesktopApp.ViewModel
{
    public class AddDriverOwnerViewViewModel : INotifyPropertyChanged
    {
        private const string DEFAULT_NATIONALITY = "България";
        private const byte DEFAULT_PTS = 39;
        Service1Client service;
        //Constructor
        public AddDriverOwnerViewViewModel()
        {
            service = new Service1Client();
            _countriesList = getCountryList();



            initDefaultDriverOwner();

        }

        //RaisePropertyChangedEvent implementation (.net 4.0, for 4.5 we can use CallerMemberName)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        //Get/Set CountriesList
        private List<string> _countriesList;
        public List<string> CountriesList
        {
            private get { return _countriesList; }
            set
            {
                _countriesList = value;
                RaisePropertyChangedEvent("CountriesList");
            }
        }

        //Get/Set DriverOwner
        private DriverOwner _driverOwner;
        public DriverOwner DriverOwner
        {
            private get { return _driverOwner; }
            set
            {
                _driverOwner = value;
                RaisePropertyChangedEvent("DriverOwner");
            }
        }

        //Get/Set IsManChecked
        private bool _isManChecked;
        public bool IsManChecked
        {
            private get { return _isManChecked; }
            set
            {
                _isManChecked = value;
                if (value == true) { _driverOwner.Sex = SexEnum.Man; }
                RaisePropertyChangedEvent("IsManChecked");
                

            }
        }

        //Get/Set IsWomanChecked
        private bool _isWomanChecked;
        public bool IsWomanChecked
        {
            private get { return _isWomanChecked; }
            set
            {
                _isWomanChecked = value;
                if (value == true) { _driverOwner.Sex = SexEnum.Woman; }
                RaisePropertyChangedEvent("IsWomanChecked");
            }
        }


        public ICommand Clear
        {
            get { return new DelegateCommand(initDefaultDriverOwner); }
        }
        private void initDefaultDriverOwner()
        {
            DriverOwner = new DriverOwner();
            DriverOwner.Categories = new Categories();

            DriverOwner.DriverOwnerId = 0;
            DriverOwner.FirstName = "";
            DriverOwner.SecondName = "";
            DriverOwner.LastName = "";

            DriverOwner.Sex = SexEnum.Man;
            IsManChecked = true;

            DriverOwner.Nationality = DEFAULT_NATIONALITY;
            DriverOwner.BirthDate = new DateTime();
            DriverOwner.BirthPlace = "";
            DriverOwner.Residence = "";
            DriverOwner.RemainingPts = DEFAULT_PTS;
            DriverOwner.LicenceIssueDate = DateTime.Now;
            DriverOwner.LicenceExpiryDate = _driverOwner.LicenceIssueDate.AddYears(10);
            DriverOwner.LicenceIssuedBy = "";


            DriverOwner.Categories.a1AcquiryDate = null;
            DriverOwner.Categories.a1ExpiryDate = null;
            DriverOwner.Categories.a1Restrictions = "";

            DriverOwner.Categories.aAcquiryDate = null;
            DriverOwner.Categories.aExpiryDate = null;
            DriverOwner.Categories.aRestrictions = "";

            DriverOwner.Categories.b1AcquiryDate = null;
            DriverOwner.Categories.b1ExpiryDate = null;
            DriverOwner.Categories.b1Restrictions = "";

            DriverOwner.Categories.bAcquiryDate = null;
            DriverOwner.Categories.bExpiryDate = null;
            DriverOwner.Categories.bRestrictions = "";

            DriverOwner.Categories.c1AcquiryDate = null;
            DriverOwner.Categories.c1ExpiryDate = null;
            DriverOwner.Categories.c1Restrictions = "";

            DriverOwner.Categories.cAcquiryDate = null;
            DriverOwner.Categories.cExpiryDate = null;
            DriverOwner.Categories.cRestrictions = "";

            DriverOwner.Categories.d1AcquiryDate = null;
            DriverOwner.Categories.d1ExpiryDate = null;
            DriverOwner.Categories.d1Restrictions = "";

            DriverOwner.Categories.dAcquiryDate = null;
            DriverOwner.Categories.dExpiryDate = null;
            DriverOwner.Categories.dRestrictions = "";

            DriverOwner.Categories.beAcquiryDate = null;
            DriverOwner.Categories.beExpiryDate = null;
            DriverOwner.Categories.beRestrictions = "";

            DriverOwner.Categories.c1eAcquiryDate = null;
            DriverOwner.Categories.c1eExpiryDate = null;
            DriverOwner.Categories.c1eRestrictions = "";

            DriverOwner.Categories.ceAcquiryDate = null;
            DriverOwner.Categories.ceExpiryDate = null;
            DriverOwner.Categories.ceRestrictions = "";

            DriverOwner.Categories.d1eAcquiryDate = null;
            DriverOwner.Categories.d1eExpiryDate = null;
            DriverOwner.Categories.d1eRestrictions = "";

            DriverOwner.Categories.deAcquiryDate = null;
            DriverOwner.Categories.deExpiryDate = null;
            DriverOwner.Categories.deRestrictions = "";

            DriverOwner.Categories.ttbAcquiryDate = null;
            DriverOwner.Categories.ttbExpiryDate = null;
            DriverOwner.Categories.ttbRestrictions = "";

            DriverOwner.Categories.ttmAcquiryDate = null;
            DriverOwner.Categories.ttmExpiryDate = null;
            DriverOwner.Categories.ttmRestrictions = "";

            DriverOwner.Categories.tktAcquiryDate = null;
            DriverOwner.Categories.tktExpiryDate = null;
            DriverOwner.Categories.tktRestrictions = "";

            DriverOwner.Categories.mAcquiryDate = null;
            DriverOwner.Categories.mExpiryDate = null;
            DriverOwner.Categories.mRestrictions = "";
        }


        public List<string> getCountryList()
        {
            List<string> LogList = new List<string>();
            try
            {
                var logFile = File.ReadAllLines(@"Resources\Textfiles\CountryListCyrillic.txt");
                LogList = new List<string>(logFile);
            }
            catch
            {
                MessageBox.Show("File does not exist!");
            }


            return LogList;

        }

        public ICommand AddDriverOwnerCommand
        {
            get { return new DelegateCommand(addDriverOwner); }
        }

        private void addDriverOwner()
        {
            if (!uiDataValidation(_driverOwner.DriverOwnerId.ToString(), _driverOwner.FirstName, _driverOwner.SecondName, _driverOwner.LastName, _driverOwner.BirthDate.ToString(), _driverOwner.BirthPlace, _driverOwner.Residence, _driverOwner.LicenceIssuedBy, _driverOwner.LicenceIssueDate.ToString(), _driverOwner.LicenceExpiryDate.ToString()))
                return;



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
                checkResponse = service.RegisterDriverOwner(_driverOwner);

                DispatchService.Invoke(() =>
                {
                    DispatchService.Invoke(() =>
                    {
                        stopLoading();
                    });

                    switch (checkResponse)
                    {
                        case 1:
                            MessageBox.Show("Възникна проблем при свързването с базата данни ", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case 2:
                            MessageBox.Show(String.Format("Съществува водач с егн: {0}", _driverOwner.DriverOwnerId), "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        case 0:
                            MessageBox.Show("Водачът бе успешно добавен", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                            initDefaultDriverOwner();
                            break;

                    }
                });
            });

        }

        private bool uiDataValidation(string id, string firstName, string secondName, string lastName, string birthDate, string birthPlace,
                               string residence, string issuedBy, string issuedDate, string expiryDate)
        {
            string idValidation = InputValidator.validateId(id);
            if (idValidation != null)
            {
                MessageBox.Show(idValidation, "Грешка в ЕГН", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string firstNameValidation = InputValidator.validateName(firstName);
            if (firstNameValidation != null)
            {
                MessageBox.Show(firstNameValidation, "Грешка в името", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string secondNameValidation = InputValidator.validateName(secondName);
            if (secondNameValidation != null)
            {
                MessageBox.Show(secondNameValidation, "Грешка в презимето", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string lastNameValidation = InputValidator.validateName(lastName);
            if (lastNameValidation != null)
            {

                MessageBox.Show(lastNameValidation, "Грешка във фамилията", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string birthDateValidation = InputValidator.validateDate(birthDate);
            if (birthDateValidation != null)
            {
                MessageBox.Show(birthDateValidation, "Грешка в датата на раждане", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string birthPlaceValidation = InputValidator.validateBirthPlace(birthPlace);
            if (birthPlaceValidation != null)
            {

                 MessageBox.Show(birthPlaceValidation, "Грешка в месторождението", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string residenceValidation = InputValidator.validateResidence(residence);
            if (residenceValidation != null)
            {
                MessageBox.Show(residenceValidation, "Грешка в постоянен адрес", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string issuerValidation = InputValidator.validateIssuer(issuedBy);
            if (issuerValidation != null)
            {
                MessageBox.Show(issuerValidation, "Грешка в постоянен адрес", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string issuedDateValidation = InputValidator.validateIssuedDate(issuedDate);
            if (issuedDateValidation != null)
            {
                MessageBox.Show(issuedDateValidation, "Грешка в датата на издаване на документа", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void startLoading()
        {
            IsProgressRingActive = true;

        }
        private void stopLoading()
        {
            IsProgressRingActive = false;

        }



    }
}
