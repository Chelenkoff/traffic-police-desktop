using ceTe.DynamicPDF.Merger;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
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

namespace TrafficPoliceDesktopApp
{
    public class MyProfileViewModel : INotifyPropertyChanged
    {
        Service1Client service;
        //VM Constructor
        public MyProfileViewModel(User user)
        {
            service = new Service1Client();
            _user = user;
            LoggedUserGreeting = String.Format("Здравейте, {0} {1}", user.FirstName, user.LastName);
           

            disableEdit();
            
        }

        


        private string _loggedUserGreeting;
        public string LoggedUserGreeting
        {
            private get { return _loggedUserGreeting; }
            set
            {
                _loggedUserGreeting = value;
                RaisePropertyChangedEvent("LoggedUserGreeting");
            }
        }

        //Get/Set user
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

        //RaisePropertyChangedEvent implementation (.net 4.0, for 4.5 we can use CallerMemberName)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        //IsBtnEditEnabled set/get
        private bool _isBtnEditEnabled;
        public bool IsBtnEditEnabled
        {
            private get { return _isBtnEditEnabled; }
            set
            {
                _isBtnEditEnabled = value;
                RaisePropertyChangedEvent("IsBtnEditEnabled");
            }
        }

        //IsFirstNameTextboxEnabled set/get
        private bool _isFirstNameTxtBoxEnabled;
        public bool IsFirstNameTxtBoxEnabled
        {
            private get { return _isFirstNameTxtBoxEnabled; }
            set
            {
                _isFirstNameTxtBoxEnabled = value;
                RaisePropertyChangedEvent("IsFirstNameTxtBoxEnabled");
            }
        }

        //IsEditBtnEnabled set/get
        private bool _isEditBtnEnabled;
        public bool IsEditBtnEnabled
        {
            private get { return _isEditBtnEnabled; }
            set
            {
                _isEditBtnEnabled = value;
                RaisePropertyChangedEvent("IsEditBtnEnabled");
            }
        }

        //IsSecondNameTextboxEnabled set/get
        private bool _isSecondNameTxtBoxEnabled;
        public bool IsSecondNameTxtBoxEnabled
        {
            private get { return _isSecondNameTxtBoxEnabled; }
            set
            {
                _isSecondNameTxtBoxEnabled = value;
                RaisePropertyChangedEvent("IsSecondNameTxtBoxEnabled");
            }
        }

        //IsLastNameTextboxEnabled set/get
        private bool _isLastNameTxtBoxEnabled;
        public bool IsLastNameTxtBoxEnabled
        {
            private get { return _isLastNameTxtBoxEnabled; }
            set
            {
                _isLastNameTxtBoxEnabled = value;
                RaisePropertyChangedEvent("IsLastNameTxtBoxEnabled");
            }
        }


        //IsCheckBoxTrafficPolicemanEnabled set/get
        private bool _isCheckBoxTrafficPolicemanEnabled;
        public bool IsCheckBoxTrafficPolicemanEnabled
        {
            private get { return _isCheckBoxTrafficPolicemanEnabled; }
            set
            {
                _isCheckBoxTrafficPolicemanEnabled = value;
                RaisePropertyChangedEvent("IsCheckBoxTrafficPolicemanEnabled");
            }
        }

        //IsCanceEditBtnEnabled set/get
        private bool _isCanceEditBtnVisible;
        public bool IsCanceEditBtnVisible
        {
            private get { return _isCanceEditBtnVisible; }
            set
            {
                _isCanceEditBtnVisible = value;
                RaisePropertyChangedEvent("IsCanceEditBtnVisible");
            }
        }

        //IsSaveEditBtnEnabled set/get
        private bool _isSaveEditBtnVisible;
        public bool IsSaveEditBtnVisible
        {
            private get { return _isSaveEditBtnVisible; }
            set
            {
                _isSaveEditBtnVisible = value;
                RaisePropertyChangedEvent("IsSaveEditBtnVisible");
            }
        }

        //EditCommand
        public ICommand EditCommand
        {
            get { return new DelegateCommand(Edit); }
        }

        //Edit func
        private void Edit()
        {
            enableEdit();
        }

        //CancelEditCommand
        public ICommand CancelEditCommand
        {
            get { return new DelegateCommand(CancelEdit); }
        }

        //CancelEdit func
        private void CancelEdit()
        {
            disableEdit();
        }

        
        public ICommand ExportDataToPdfCommand
        {
            get { return new DelegateCommand(exportPersonalDataToPdf); }
        }
        private void exportPersonalDataToPdf()
        {
            string formFile = "./Resources/Docs/personal_data.pdf";
            MergeDocument document = new MergeDocument(formFile);



            document.Form.Fields["firstName"].Value =User.FirstName;
            document.Form.Fields["secondName"].Value =User.SecondName;
            document.Form.Fields["lastName"].Value = User.LastName;
            document.Form.Fields["id"].Value = User.UserId.ToString();
            document.Form.Fields["pass"].Value = User.UserPassword;
            document.Form.Fields["trafficPoliceman"].Value = (User.IsTrafficPoliceman == true) ? "Да" : "Не";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF File|*.pdf;";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream fileStream = File.Create(saveFileDialog.FileName))
                {
                    document.Draw(fileStream);
                }
            }
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

        //IsPswdBoxVisible property
        private bool _isPswdBoxVisible;
        public bool IsPswdBoxVisible
        {
            get { return _isPswdBoxVisible; }
            set
            {
                _isPswdBoxVisible = value;
                RaisePropertyChangedEvent("IsPswdBoxVisible");
            }
        }

        //IsPswdTxtBoxVisible property
        private bool _isPswdTxtBoxVisible;
        public bool IsPswdTxtBoxVisible
        {
            get { return _isPswdTxtBoxVisible; }
            set
            {
                _isPswdTxtBoxVisible = value;
                RaisePropertyChangedEvent("IsPswdTxtBoxVisible");
            }
        }

        //IsPswdTxtBoxEnabled property
        private bool _isPswdTxtBoxEnabled;
        public bool IsPswdTxtBoxEnabled
        {
            get { return _isPswdTxtBoxEnabled; }
            set
            {
                _isPswdTxtBoxEnabled = value;
                RaisePropertyChangedEvent("IsPswdTxtBoxEnabled");
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


        private void enableEdit()
        {
            IsFirstNameTxtBoxEnabled = true;
            IsSecondNameTxtBoxEnabled = true;
            IsLastNameTxtBoxEnabled = true;
            IsCheckBoxTrafficPolicemanEnabled = true;

            IsCanceEditBtnVisible = true;
            IsSaveEditBtnVisible = true;

            IsEditBtnEnabled = false;

            IsPswdTxtBoxEnabled = true;

            showPassword();
        }

        private void disableEdit()
        {
            IsFirstNameTxtBoxEnabled = false;
            IsSecondNameTxtBoxEnabled = false;
            IsLastNameTxtBoxEnabled = false;
            IsCheckBoxTrafficPolicemanEnabled = false;

            IsEditBtnEnabled = true;

            IsCanceEditBtnVisible = false;
            IsSaveEditBtnVisible = false;

            IsPswdTxtBoxEnabled = false;

            hidePassword();
           
        }

        public ICommand UpdateUserCommand
        {
            get { return new DelegateCommand(updateUser); }
        }

        private void updateUser()
        {
            bool check = User.IsTrafficPoliceman;

            if (!uiDataValidation(User.FirstName, User.SecondName, User.LastName, check, User.UserPassword))
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

                //Receiving REST response
                checkResponse = service.UpdateUser(User);

                DispatchService.Invoke(() =>
                {
                    DispatchService.Invoke(() =>
                    {
                        stopLoading();
                    });

                    switch (checkResponse)
                    {
                        case 1:
                            MessageBox.Show("Възникна проблем при свързването с базата данни ", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            break;
                        case 2:
                           MessageBox.Show( "Неуспешно обновяване на потребител", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            break;
                        case 0:
                           MessageBox.Show("Данните за потребителя бяха успешно обновени", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            LoggedUserGreeting = String.Format("Здравейте, {0} {1}", User.FirstName, User.LastName);
                            disableEdit();
                            break;

                    }
                });
            });

        }

        private bool uiDataValidation(string firstN, string secondN, string lastN, bool isTraffic, string pass)
        {
            //Name validation
            string firstNameValidation = InputValidator.validateName(firstN);
            if (firstNameValidation != null)
            {
                MessageBox.Show(firstNameValidation, "Грешка в името",MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string secondNameValidation = InputValidator.validateName(secondN);
            if (secondNameValidation != null)
            {
                MessageBox.Show(secondNameValidation, "Грешка в презимето", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string lastNameValidation = InputValidator.validateName(lastN);
            if (lastNameValidation != null)
            {
                MessageBox.Show(lastNameValidation, "Грешка във фамилията", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            string passValidation = InputValidator.validatePass(pass);
            if (passValidation != null)
            {
                MessageBox.Show(passValidation, "Грешка при парола", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;

        }

        public ICommand HidePassCommand
        {
            get { return new DelegateCommand(hidePassword); }
        }
        private void hidePassword()
        {
            IsPswdTxtBoxVisible = false;
            IsPswdBoxVisible = true;
        }


        public ICommand ShowPassCommand
        {
            get { return new DelegateCommand(showPassword); }
        }
        private void showPassword()
        {
            IsPswdTxtBoxVisible = true;
            IsPswdBoxVisible = false;

            //txtBoxVisiblePassword.Text = pswdBoxPassword.Password;

        }

    }
}
