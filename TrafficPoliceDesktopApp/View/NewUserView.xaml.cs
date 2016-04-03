using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TrafficPoliceDesktopApp.ServiceReference1;
using TrafficPoliceDesktopApp.Utilities;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Win32;
using System.Windows.Threading;

namespace TrafficPoliceDesktopApp.View
{
    /// <summary>
    /// Interaction logic for NewUserView.xaml
    /// </summary>
    public partial class NewUserView : UserControl
    {
        public MainWindow ParentWindow { get; set; }
        public NewUserView()
        {

            InitializeComponent();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clearFields();
        }

        private void clearFields()
        {
            txtBoxId.Text = "";
            txtBoxVisiblePassword.Text = "";
            txtBoxFirstName.Text = "";
            txtBoxSecondName.Text = "";
            txtBoxLastName.Text = "";
            pswdBoxPassword.Password = "";
            checkBoxIsTrafficPoliceman.IsChecked = false;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {

            string id = txtBoxId.Text;
            string firstName = txtBoxFirstName.Text;
            string secondName = txtBoxSecondName.Text;
            string lastName = txtBoxLastName.Text;
            string pass = pswdBoxPassword.Password;

            if (!uiDataValidation(id, firstName, secondName, lastName, pass))
                return;

            MessageDialogResult msgDialog = await ParentWindow.ShowMessageAsync("Внимание", "Сигурни ли сте, че искате да добавите потребител?", MessageDialogStyle.AffirmativeAndNegative);
            if (msgDialog == MessageDialogResult.Negative)
            {
                return;
            }

            bool checkIsTrafficPoliceman = checkBoxIsTrafficPoliceman.IsChecked == true;

            User userToAdd = new User();
            userToAdd.UserId = Convert.ToInt64(txtBoxId.Text);
            userToAdd.FirstName = txtBoxFirstName.Text;
            userToAdd.SecondName = txtBoxSecondName.Text;
            userToAdd.LastName = txtBoxLastName.Text;
            userToAdd.UserPassword = pswdBoxPassword.Password;
            userToAdd.IsTrafficPoliceman = checkIsTrafficPoliceman;

            int check;
            await Task.Factory.StartNew(() =>
            {
                //startLoading logic
                this.Dispatcher.Invoke((Action)(() => startLoading()));

                //Receiving REST api response
                check = ParentWindow.Service.InsertUser(userToAdd);


                // invoke user code on the main UI thread
                Dispatcher.Invoke(new Action(() =>
                {
                    //stopLoading logic
                    this.Dispatcher.Invoke((Action)(() => stopLoading()));

                    switch(check)
                    {
                        case 1:
                            ParentWindow.ShowMessageAsync("Внимание", "Възникна проблем при свързването с базата данни ", MessageDialogStyle.Affirmative);
                            break;
                        case 2:
                            string message = String.Format("Съществува потребител с ЕГН: {0}", userToAdd.UserId);
                            ParentWindow.ShowMessageAsync("Внимание", message, MessageDialogStyle.Affirmative);
                            break;
                        case 0:
                            ParentWindow.ShowMessageAsync("Внимание", "Потребителят бе успешно добавен", MessageDialogStyle.Affirmative);
                            break;

                    }

                }));
            });
        }

        private bool uiDataValidation(string id, string firstName, string secondName, string lastName, string pass)
        {
            string idValidation = InputValidator.validateId(id);
            //Id validation
            if (idValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка в ЕГН", idValidation);
                return false;
            }
            string passValidation = InputValidator.validatePass(pass);
            if (passValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка в паролата", passValidation);
                return false;
            }
           string firstNameValidation = InputValidator.validateName(firstName);
           if (firstNameValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка в името", firstNameValidation);
                return false;
            }
           string secondNameValidation = InputValidator.validateName(secondName);
           if (secondNameValidation != null)
           {
               ParentWindow.ShowMessageAsync("Грешка в презимето", secondNameValidation);
               return false;
           }
           string lastNameValidation = InputValidator.validateName(lastName);
           if (lastNameValidation != null)
           {
               ParentWindow.ShowMessageAsync("Грешка във фамилията", lastNameValidation);
               return false;
           }
           


            return true;
        }

        private void startLoading()
        {
            progressRingLoading.IsActive = true;

        }
        private void stopLoading()
        {
            progressRingLoading.IsActive = false;

        }












    }
}
