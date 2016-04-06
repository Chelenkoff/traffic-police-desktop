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
            string birthDate = datePickerBirthDate.SelectedDate.ToString();
            string birthPlace = txtBoxBirthPlace.Text;
            string residence = txtBoxResidence.Text;
            string remainingPts = upDownRemainingPts.Value.ToString();
            string issuedBy = txtBoxIssuedBy.Text;
            string issuedDate = datePickerIssuedDate.SelectedDate.ToString();
            string expiryDate = datePickerExpiryDate.SelectedDate.ToString();

            //needs to be finished



            if (!uiDataValidation(id, firstName, secondName, lastName, birthDate,birthPlace,residence,issuedBy,issuedDate,expiryDate))
                return;

            MessageDialogResult msgDialog = await ParentWindow.ShowMessageAsync("Внимание", "Сигурни ли сте, че искате да добавите потребител?", MessageDialogStyle.AffirmativeAndNegative);
            if (msgDialog == MessageDialogResult.Negative)
            {
                return;
            }

            DriverOwner driverOwner = getDriverOwner();



            int check;
            await Task.Factory.StartNew(() =>
            {
                //startLoading logic
                this.Dispatcher.Invoke((Action)(() => startLoading()));

                //Receiving REST api response
                check = ParentWindow.Service.RegisterDriverOwner(driverOwner);
                

                // invoke user code on the main UI thread
                Dispatcher.Invoke(new Action(() =>
                {
                    //stopLoading logic
                    this.Dispatcher.Invoke((Action)(() => stopLoading()));
                    switch (check)
                    {
                        case 1:
                            ParentWindow.ShowMessageAsync("Внимание", "Възникна проблем при свързването с базата данни ", MessageDialogStyle.Affirmative);
                            break;
                        case 2:
                            
                            ParentWindow.ShowMessageAsync("Внимание", String.Format("Съществува водач с егн: {0}",driverOwner.DriverOwnerId), MessageDialogStyle.Affirmative);
                            break;
                        case 0:
                            ParentWindow.ShowMessageAsync("Внимание", "Водачът бе успешно добавен", MessageDialogStyle.Affirmative);
                            break;

                    }

                }));
            });
        }

        private bool uiDataValidation(string id, string firstName, string secondName, string lastName, string birthDate, string birthPlace, 
                                       string residence, string issuedBy,string issuedDate, string expiryDate)
        {
            string idValidation = InputValidator.validateId(id);
            if (idValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка в ЕГН", idValidation);
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
            string birthDateValidation = InputValidator.validateDate(birthDate);
            if (birthDateValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка в датата на раждане", birthDateValidation);
                return false;
            }
            string birthPlaceValidation = InputValidator.validateBirthPlace(birthPlace);
            if (birthPlaceValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка в месторождението", birthPlaceValidation);
                return false;
            }
            string residenceValidation = InputValidator.validateResidence(residence);
            if (residenceValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка в постоянен адрес", residenceValidation);
                return false;
            }
            string issuerValidation = InputValidator.validateIssuer(issuedBy);
            if (issuerValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка в постоянен адрес", issuerValidation);
                return false;
            }
            string issuedDateValidation = InputValidator.validateIssuedDate(issuedDate);
            if (issuedDateValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка в датата на издаване на документа", issuedDateValidation);
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
            txtBoxIssuedBy.Text = "";
            datePickerIssuedDate.SelectedDate = null;
            datePickerExpiryDate.SelectedDate = null;
            datePickerBirthDate.SelectedDate = null;
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

            Categories cat = new Categories();

            cat.a1AcquiryDate = datePickerA1AcquiryDate.SelectedDate;
            cat.a1ExpiryDate = datePickerA1ExpiryDate.SelectedDate;
            cat.a1Restrictions = txtBoxA1Restrictions.Text;

            cat.aAcquiryDate = datePickerAAcquiryDate.SelectedDate;
            cat.aExpiryDate = datePickerAExpiryDate.SelectedDate;
            cat.aRestrictions = txtBoxARestrictions.Text;


            cat.b1AcquiryDate = datePickerB1AcquiryDate.SelectedDate;
            cat.b1ExpiryDate = datePickerB1ExpiryDate.SelectedDate;
            cat.b1Restrictions = txtBoxB1Restrictions.Text;

            cat.bAcquiryDate = datePickerBAcquiryDate.SelectedDate;
            cat.bExpiryDate = datePickerBExpiryDate.SelectedDate;
            cat.bRestrictions = txtBoxBRestrictions.Text;

            cat.c1AcquiryDate = datePickerC1AcquiryDate.SelectedDate;
            cat.c1ExpiryDate = datePickerC1ExpiryDate.SelectedDate;
            cat.c1Restrictions = txtBoxC1Restrictions.Text;

            cat.cAcquiryDate = datePickerCAcquiryDate.SelectedDate;
            cat.cExpiryDate = datePickerCExpiryDate.SelectedDate;
            cat.cRestrictions = txtBoxCRestrictions.Text;

            cat.d1AcquiryDate = datePickerD1AcquiryDate.SelectedDate;
            cat.d1ExpiryDate = datePickerD1ExpiryDate.SelectedDate;
            cat.d1Restrictions = txtBoxD1Restrictions.Text;

            cat.dAcquiryDate = datePickerDAcquiryDate.SelectedDate;
            cat.dExpiryDate = datePickerDExpiryDate.SelectedDate;
            cat.dRestrictions = txtBoxDRestrictions.Text;

            cat.beAcquiryDate = datePickerBEAcquiryDate.SelectedDate;
            cat.beExpiryDate = datePickerBEExpiryDate.SelectedDate;
            cat.beRestrictions = txtBoxBERestrictions.Text;

            cat.c1eAcquiryDate = datePickerC1EAcquiryDate.SelectedDate;
            cat.c1eExpiryDate = datePickerC1EExpiryDate.SelectedDate;
            cat.c1eRestrictions = txtBoxC1ERestrictions.Text;

            cat.ceAcquiryDate = datePickerCEAcquiryDate.SelectedDate;
            cat.ceExpiryDate = datePickerCEExpiryDate.SelectedDate;
            cat.ceRestrictions = txtBoxCERestrictions.Text;

            cat.d1eAcquiryDate = datePickerD1EAcquiryDate.SelectedDate;
            cat.d1eExpiryDate = datePickerD1EExpiryDate.SelectedDate;
            cat.d1eRestrictions = txtBoxD1ERestrictions.Text;

            cat.deAcquiryDate = datePickerDEAcquiryDate.SelectedDate;
            cat.deExpiryDate = datePickerDEExpiryDate.SelectedDate;
            cat.deRestrictions = txtBoxDERestrictions.Text;

            cat.ttbAcquiryDate = datePickerTtbAcquiryDate.SelectedDate;
            cat.ttbExpiryDate = datePickerTtbExpiryDate.SelectedDate;
            cat.ttbRestrictions = txtBoxTtbRestrictions.Text;

            cat.ttmAcquiryDate = datePickerTtmAcquiryDate.SelectedDate;
            cat.ttmExpiryDate = datePickerTtmExpiryDate.SelectedDate;
            cat.ttmRestrictions = txtBoxTtmRestrictions.Text;

            cat.tktAcquiryDate = datePickerTktAcquiryDate.SelectedDate;
            cat.tktExpiryDate = datePickerTktExpiryDate.SelectedDate;
            cat.tktRestrictions = txtBoxTktRestrictions.Text;

            cat.mAcquiryDate = datePickerMAcquiryDate.SelectedDate;
            cat.mExpiryDate = datePickerMExpiryDate.SelectedDate;
            cat.mRestrictions = txtBoxMRestrictions.Text;

            return cat;
        }
        
        private DriverOwner getDriverOwner()
        {
            DriverOwner driverOwner = new DriverOwner();

            driverOwner.DriverOwnerId = Convert.ToInt64(txtBoxId.Text);
            driverOwner.FirstName = txtBoxFirstName.Text;
            driverOwner.SecondName = txtBoxSecondName.Text;
            driverOwner.LastName = txtBoxLastName.Text;
            driverOwner.Sex = (radioBtnMan.IsChecked == true ? Sex.Man : Sex.Woman);
            driverOwner.Nationality = comboBoxCountries.SelectedItem.ToString();
            driverOwner.BirthDate = datePickerBirthDate.DisplayDate;
            driverOwner.BirthPlace = txtBoxBirthPlace.Text;
            driverOwner.Residence = txtBoxResidence.Text;
            driverOwner.RemainingPts = Convert.ToByte(upDownRemainingPts.Value);
            driverOwner.LicenceIssueDate = datePickerIssuedDate.DisplayDate;
            driverOwner.LicenceExpiryDate = datePickerExpiryDate.SelectedDate ?? datePickerIssuedDate.DisplayDate.AddYears(10);
            driverOwner.LicenceIssuedBy = txtBoxIssuedBy.Text;
            driverOwner.Categories = getCategories();
            return driverOwner;
        }

    }
}
