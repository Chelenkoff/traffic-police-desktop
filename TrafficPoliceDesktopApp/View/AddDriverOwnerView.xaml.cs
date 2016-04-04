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
    /// Interaction logic for AddDriverOwnerView.xaml
    /// </summary>
    public partial class AddDriverOwnerView : UserControl
    {
        public MainWindow ParentWindow { get; set; }
        public AddDriverOwnerView()
        {
            InitializeComponent();
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



        private void UserControl_Initialized(object sender, EventArgs e)
        {
            comboBoxCountries.ItemsSource = getCountryList();
            comboBoxCountries.SelectedItem = "България";
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            clearForm();
        }

       

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string id = txtBoxId.Text;
            string firstName = txtBoxFirstName.Text;
            string secondName = txtBoxSecondName.Text;
            string lastName = txtBoxLastName.Text;
            string sex;
            if (radioBtnMan.IsChecked == true)
                sex = 'М'.ToString();
            else
                sex = 'Ф'.ToString();
            string nationality = comboBoxCountries.SelectedItem.ToString();
            string birthPlace = txtBoxBirthPlace.Text;
            string residence = txtBoxResidence.Text;
            string licenceNum = txtBoxLicenceNum.Text;
            string remainingPts = upDownRemainingPts.Value.ToString();
            string issuedBy = txtBoxIssuedBy.Text;
            string issuedDate = datePickerIssuedDate.SelectedDate.ToString();
            string expiryDate = datePickerExpiryDate.SelectedDate.ToString();

            //needs to be finished
           Categories cat = getCategories();
            

            //if (!uiDataValidation(id, firstName, secondName, lastName, pass))
            //    return;

            MessageDialogResult msgDialog = await ParentWindow.ShowMessageAsync("Внимание", "Сигурни ли сте, че искате да добавите потребител?", MessageDialogStyle.AffirmativeAndNegative);
            if (msgDialog == MessageDialogResult.Negative)
            {
                return;
            }

            /* bool checkIsTrafficPoliceman = checkBoxIsTrafficPoliceman.IsChecked == true;

            User userToAdd = new User();
            userToAdd.UserId = Convert.ToInt64(txtBoxId.Text);
            userToAdd.FirstName = txtBoxFirstName.Text;
            userToAdd.SecondName = txtBoxSecondName.Text;
            userToAdd.LastName = txtBoxLastName.Text;
            userToAdd.UserPassword = pswdBoxPassword.Password;
            userToAdd.IsTrafficPoliceman = checkIsTrafficPoliceman;
            */
            int check;
            await Task.Factory.StartNew(() =>
            {
                //startLoading logic
                this.Dispatcher.Invoke((Action)(() => startLoading()));

                //Receiving REST api response
                //check = ParentWindow.Service.InsertUser(userToAdd);


                // invoke user code on the main UI thread
                Dispatcher.Invoke(new Action(() =>
                {
                    //stopLoading logic
                    this.Dispatcher.Invoke((Action)(() => stopLoading()));
                    check = 2;
                    switch (check)
                    {
                        case 1:
                            ParentWindow.ShowMessageAsync("Внимание", "Възникна проблем при свързването с базата данни ", MessageDialogStyle.Affirmative);
                            break;
                        case 2:
                            //string message = String.Format("Съществува потребител с ЕГН: {0}", userToAdd.UserId);
                            string message = ";";
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

        private void clearForm()
        {
            txtBoxId.Text = "";
            txtBoxFirstName.Text = "";
            txtBoxSecondName.Text = "";
            txtBoxLastName.Text = "";
            comboBoxCountries.SelectedItem = "България";
            txtBoxBirthPlace.Text = "";
            txtBoxResidence.Text = "";
            upDownRemainingPts.Value = 39;
            txtBoxLicenceNum.Text = "";
            txtBoxIssuedBy.Text = "";
            datePickerIssuedDate.SelectedDate = null;
            datePickerExpiryDate.SelectedDate = null;
            clearCategories();

        }

        private void clearCategories()
        {
            datePickerA1AcquiryDate.SelectedDate = null;
            datePickerA1ExpiryDate.SelectedDate = null;
            txtBoxA1Restrictions.Text = "";

            datePickerAAcquiryDate.SelectedDate = null;
            datePickerAExpiryDate.SelectedDate = null;
            txtBoxARestrictions.Text = "";

            datePickerB1AcquiryDate.SelectedDate = null;
            datePickerB1ExpiryDate.SelectedDate = null;
            txtBoxB1Restrictions.Text = "";

            datePickerBAcquiryDate.SelectedDate = null;
            datePickerBExpiryDate.SelectedDate = null;
            txtBoxBRestrictions.Text = "";

            datePickerC1AcquiryDate.SelectedDate = null;
            datePickerC1ExpiryDate.SelectedDate = null;
            txtBoxC1Restrictions.Text = "";

            datePickerCAcquiryDate.SelectedDate = null;
            datePickerCExpiryDate.SelectedDate = null;
            txtBoxCRestrictions.Text = "";

            datePickerD1AcquiryDate.SelectedDate = null;
            datePickerD1ExpiryDate.SelectedDate = null;
            txtBoxD1Restrictions.Text = "";

            datePickerDAcquiryDate.SelectedDate = null;
            datePickerDExpiryDate.SelectedDate = null;
            txtBoxDRestrictions.Text = "";

            datePickerBEAcquiryDate.SelectedDate = null;
            datePickerBEExpiryDate.SelectedDate = null;
            txtBoxBERestrictions.Text = "";

            datePickerC1EAcquiryDate.SelectedDate = null;
            datePickerC1EExpiryDate.SelectedDate = null;
            txtBoxC1ERestrictions.Text = "";

            datePickerCEAcquiryDate.SelectedDate = null;
            datePickerCEExpiryDate.SelectedDate = null;
            txtBoxCERestrictions.Text = "";

            datePickerD1EAcquiryDate.SelectedDate = null;
            datePickerD1EExpiryDate.SelectedDate = null;
            txtBoxD1ERestrictions.Text = "";

            datePickerDEAcquiryDate.SelectedDate = null;
            datePickerDEExpiryDate.SelectedDate = null;
            txtBoxDERestrictions.Text = "";

            datePickerTtbAcquiryDate.SelectedDate = null;
            datePickerTtbExpiryDate.SelectedDate = null;
            txtBoxTtbRestrictions.Text = "";

            datePickerTtmAcquiryDate.SelectedDate = null;
            datePickerTtmExpiryDate.SelectedDate = null;
            txtBoxTtmRestrictions.Text = "";

            datePickerTktAcquiryDate.SelectedDate = null;
            datePickerTktExpiryDate.SelectedDate = null;
            txtBoxTktRestrictions.Text = "";

            datePickerMAcquiryDate.SelectedDate = null;
            datePickerMExpiryDate.SelectedDate = null;
            txtBoxMRestrictions.Text = "";

            
        }

        

        private Categories getCategories()
        {
            string a1AcquiryDate = datePickerA1AcquiryDate.SelectedDate.ToString();
            string a1ExpiryDate = datePickerA1ExpiryDate.SelectedDate.ToString();
            string a1Restrictions = txtBoxA1Restrictions.Text;

            string aAcquiryDate = datePickerAAcquiryDate.SelectedDate.ToString();
            string aExpiryDate = datePickerAExpiryDate.SelectedDate.ToString();
            string aRestrictions = txtBoxARestrictions.Text;

            string b1AcquiryDate = datePickerB1AcquiryDate.SelectedDate.ToString();
            string b1ExpiryDate = datePickerB1ExpiryDate.SelectedDate.ToString();
            string b1Restrictions = txtBoxB1Restrictions.Text;

            string bAcquiryDate = datePickerBAcquiryDate.SelectedDate.ToString();
            string bExpiryDate = datePickerBExpiryDate.SelectedDate.ToString();
            string bRestrictions = txtBoxBRestrictions.Text;

            string c1AcquiryDate = datePickerC1AcquiryDate.SelectedDate.ToString();
            string c1ExpiryDate = datePickerC1ExpiryDate.SelectedDate.ToString();
            string c1Restrictions = txtBoxC1Restrictions.Text;

            string cAcquiryDate = datePickerCAcquiryDate.SelectedDate.ToString();
            string cExpiryDate = datePickerCExpiryDate.SelectedDate.ToString();
            string cRestrictions = txtBoxCRestrictions.Text;

            string d1AcquiryDate = datePickerD1AcquiryDate.SelectedDate.ToString();
            string d1ExpiryDate = datePickerD1ExpiryDate.SelectedDate.ToString();
            string d1Restrictions = txtBoxD1Restrictions.Text;

            string dAcquiryDate = datePickerDAcquiryDate.SelectedDate.ToString();
            string dExpiryDate = datePickerDExpiryDate.SelectedDate.ToString();
            string dRestrictions = txtBoxDRestrictions.Text;

            string beAcquiryDate = datePickerBEAcquiryDate.SelectedDate.ToString();
            string beExpiryDate = datePickerBEExpiryDate.SelectedDate.ToString();
            string beRestrictions = txtBoxBERestrictions.Text;
            ///NEEDS TO BE FINISHED!
            ///
            return new Categories();
        }

    }
}
