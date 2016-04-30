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
using WCFDBService;

namespace TrafficPoliceDesktopApp
{
   public class NewUserViewModel : INotifyPropertyChanged
    {
       Service1Client service;

       //Default VM constructor
       public NewUserViewModel()
       {
           service = new Service1Client();
            initDefaultUser();
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


        public ICommand Clear
        {
            get { return new DelegateCommand(clear); }
        }
       private void clear()
        {
            initDefaultUser();
        }

        private void initDefaultUser()
        {
            User = new User();
            User.UserId = 0;
            User.FirstName = "";
            User.SecondName = "";
            User.LastName = "";
            User.UserPassword = "";
            User.IsTrafficPoliceman = false;
        }

       private bool uiDataValidation(string firstName, string secondName, string lastName, string pass)
       {


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
            string passValidation = InputValidator.validatePass(pass);
            if (passValidation != null)
            {
                MessageBox.Show(passValidation, "Грешка в паролата", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }



            return true;
       }

       public ICommand SaveUserCommand
       {
           get { return new DelegateCommand(saveUser); }
       }
       private void saveUser()
       {
           

           if (!uiDataValidation(User.FirstName, User.SecondName, User.LastName, User.UserPassword))
               return;



           string dbResponse;
           //Starting new non-blocking-ui task
           Task.Factory.StartNew(() =>
           {
               //startLoading();

               DispatchService.Invoke(() =>
               {
                   startLoading();
               });

               //Receiving REST response
               dbResponse = service.InsertUserAndGetGeneratedId(User);

               DispatchService.Invoke(() =>
               {
                   DispatchService.Invoke(() =>
                   {
                       stopLoading();
                   });

                   switch (dbResponse)
                   {
                       case "DB_NOT_CONNECTED":
                           MessageBox.Show("Възникна проблем при свързването с базата данни ", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                           break;

                       case "QUERY_ERROR":
                           MessageBox.Show("Възникна проблем при добавянето на служителя ", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                           break;
                       default:
                           MessageBox.Show(String.Format("Служителят бе успешно добавен\n Неговият служебен номер е {0}",dbResponse), "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                           break;

                   }
               });
           });
       }
    }
}
