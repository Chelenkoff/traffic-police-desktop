﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TrafficPoliceDesktopApp.ServiceReference1;
using TrafficPoliceDesktopApp.Utilities;

namespace TrafficPoliceDesktopApp
{
   public class NewUserViewModel : INotifyPropertyChanged
    {
       Service1Client service;

       //Default VM constructor
       public NewUserViewModel()
       {
           service = new Service1Client();
           User = new User();
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
            User.UserId = 0;
            User.FirstName = "";
            User.SecondName = "";
            User.LastName = "";
            User.UserPassword = "";
            User.IsTrafficPoliceman = false;
        }

       private bool uiDataValidation(string id, string firstName, string secondName, string lastName, string pass)
       {
           string idValidation = InputValidator.validateId(id);
           //Id validation
           if (idValidation != null)
           {
               //System.Windows.MessageBox.Show(idValidation,"Грешка в ЕГН");
                MessageBox.Show(idValidation,"Грешка в ЕГН",MessageBoxButton.OK, MessageBoxImage.Error);
                
               return false;
           }
           string passValidation = InputValidator.validatePass(pass);
           if (passValidation != null)
           {
               MessageBox.Show(passValidation, "Грешка в паролата", MessageBoxButton.OK, MessageBoxImage.Error);
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



           return true;
       }

       public ICommand SaveUserCommand
       {
           get { return new DelegateCommand(saveUser); }
       }
       private void saveUser()
       {
           

           if (!uiDataValidation(User.UserId.ToString(),User.FirstName, User.SecondName, User.LastName, User.UserPassword))
               return;



           int check;
           //Starting new non-blocking-ui task
           Task.Factory.StartNew(() =>
           {
               //startLoading();

               DispatchService.Invoke(() =>
               {
                   startLoading();
               });

               //Receiving REST response
               check = service.InsertUser(User);

               DispatchService.Invoke(() =>
               {
                   DispatchService.Invoke(() =>
                   {
                       stopLoading();
                   });

                   switch (check)
                   {
                       case 1:
                           MessageBox.Show("Възникна проблем при свързването с базата данни ", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                           break;
                       case 2:
                           string message = String.Format("Съществува потребител с ЕГН: {0}",User.UserId);
                           MessageBox.Show(message, "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                           break;
                       case 0:
                           MessageBox.Show("Потребителят бе успешно добавен", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                           break;

                   }
               });
           });
       }
    }
}
