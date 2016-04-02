﻿using MahApps.Metro.Controls;
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
using MahApps.Metro.Controls.Dialogs;
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
    /// Interaction logic for MyProfileView.xaml
    /// </summary>
    public partial class MyProfileView : UserControl
    {


        public MainWindow ParentWindow { get; set; }

        public MyProfileView()
        {
           
            InitializeComponent();
            
        }



        public void defaultFulfillForm()
        {
            lblUserGreeting.Content = String.Format("Здравейте, {0} {1}", ParentWindow.User.FirstName, ParentWindow.User.LastName);
            txtBoxFirstName.Text = ParentWindow.User.FirstName;
            txtBoxSecondName.Text = ParentWindow.User.SecondName;
            txtBoxLastName.Text = ParentWindow.User.LastName;
            txtBoxId.Text = ParentWindow.User.UserId.ToString();
            pswdBoxPassword.Password = ParentWindow.User.UserPassword;
            checkBoxIsTrafficPoliceman.IsChecked = (ParentWindow.User.IsTrafficPoliceman ? true : false);
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            defaultFulfillForm();
            disableEdit();
        }

        private void btnShowPass_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            showPassword();
        }

        private void btnShowPass_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            hidePassword();
        }

        private void btnShowPass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hidePassword();
        }

        private void hidePassword()
        {
            txtBoxVisiblePassword.Visibility = Visibility.Hidden;
            pswdBoxPassword.Visibility = Visibility.Visible;
        }

        private void showPassword()
        {
            txtBoxVisiblePassword.Visibility = Visibility.Visible;
            pswdBoxPassword.Visibility = Visibility.Hidden;

            txtBoxVisiblePassword.Text = pswdBoxPassword.Password;

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            enableEdit();
        }


        private void enableEdit()
        {
            btnEdit.IsEnabled = false;
            
            btnShowPass.IsEnabled = false;

            //Enabling controls for edit
            txtBoxFirstName.IsEnabled = true;
            txtBoxSecondName.IsEnabled = true;
            txtBoxLastName.IsEnabled = true;
            checkBoxIsTrafficPoliceman.IsEnabled = true;

            pswdBoxPassword.Visibility = Visibility.Hidden;

            txtBoxVisiblePassword.IsEnabled = true;
            txtBoxVisiblePassword.Text = pswdBoxPassword.Password;
            txtBoxVisiblePassword.Visibility = Visibility.Visible;
            
            btnSave.Visibility = Visibility.Visible;
            btnCancelEdit.Visibility = Visibility.Visible;
        }

        private void disableEdit()
        {
            btnEdit.IsEnabled = true;
            btnShowPass.IsEnabled = true;

            //Enabling controls for edit
            txtBoxFirstName.IsEnabled = false;
            txtBoxSecondName.IsEnabled = false;
            txtBoxLastName.IsEnabled = false;
            
            checkBoxIsTrafficPoliceman.IsEnabled = false;

            pswdBoxPassword.Visibility = Visibility.Visible;

            txtBoxVisiblePassword.IsEnabled = false;
            txtBoxVisiblePassword.Visibility = Visibility.Hidden;
            

            btnSave.Visibility = Visibility.Hidden;
            btnCancelEdit.Visibility = Visibility.Hidden;

            
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            
            bool check = checkBoxIsTrafficPoliceman.IsChecked == true;

            if (!uiDataValidation(txtBoxFirstName.Text, txtBoxSecondName.Text, txtBoxLastName.Text, check, txtBoxVisiblePassword.Text))
                return;

            MessageDialogResult msgDialog =await ParentWindow.ShowMessageAsync("Внимание", "Сигурни ли сте, че искате да запазите направените промени?",MessageDialogStyle.AffirmativeAndNegative);
            if (msgDialog == MessageDialogResult.Negative)
            {
                return;
            }

            ParentWindow.User.FirstName = txtBoxFirstName.Text;
            ParentWindow.User.SecondName = txtBoxSecondName.Text;
            ParentWindow.User.LastName = txtBoxLastName.Text;
            ParentWindow.User.UserId = Convert.ToInt64(txtBoxId.Text);
            ParentWindow.User.UserPassword = txtBoxVisiblePassword.Text;
            ParentWindow.User.IsTrafficPoliceman = check;

            ParentWindow.lblLoggedUserName.Content = String.Format("{0} {1}", ParentWindow.User.FirstName, ParentWindow.User.LastName);
            lblUserGreeting.Content = String.Format("Здравейте, {0} {1}", ParentWindow.User.FirstName, ParentWindow.User.LastName);
            disableEdit();

            await Task.Factory.StartNew(() =>
            {
                //startLoading logic
                this.Dispatcher.Invoke((Action)(() => startLoading()));

                //Constructing USER from DB

                ParentWindow.Service.UpdateUser(ParentWindow.User);


                // invoke user code on the main UI thread
                Dispatcher.Invoke(new Action(() =>
                {
                    //stopLoading logic
                    this.Dispatcher.Invoke((Action)(() => stopLoading()));



                    //if (!dbResponseValidation(user)) return;
                   

                    //DB - OK, USER - FOUND
                    //var newForm = new MainWindow(user); //create your new form.
                    //newForm.Show(); //show the new form.
                    //this.Hide(); //only if you want to close the current form.
                }));
            });
            

            
        }

        private void startLoading()
        {
            progressRingLoading.IsActive = true;

        }
        private void stopLoading()
        {
            progressRingLoading.IsActive = false;

        }

        private bool uiDataValidation(string firstN,string secondN, string lastN, bool isTraffic, string pass)
        {
            //Name validation
            string firstNameValidation = InputValidator.validateName(firstN);
            string secondNameValidation = InputValidator.validateName(secondN);
            string lastNameValidation = InputValidator.validateName(lastN);
            string passValidation = InputValidator.validatePass(pass);
            if (firstNameValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка в името", firstNameValidation);
                return false;
            }
            if(secondNameValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка в презимето", secondNameValidation);
                return false;
            }
            if (lastNameValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка във фамилията", lastNameValidation);
                return false;
            }
            if(passValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка при парола", passValidation);
                return false;
            }
            return true;

        }

        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            pswdBoxPassword.Password = txtBoxVisiblePassword.Text;
            disableEdit();
            defaultFulfillForm();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            screenshotAndSaveUserData(); 
        }


        private void screenshotAndSaveUserData()
        {
            pswdBoxPassword.Visibility = Visibility.Hidden;
            txtBoxVisiblePassword.Text = pswdBoxPassword.Password;
            txtBoxVisiblePassword.Visibility = Visibility.Visible;

            Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);

            RenderTargetBitmap renderTargetBitmap =
            new RenderTargetBitmap(471, 467, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(this);
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            pswdBoxPassword.Visibility = Visibility.Visible;
            txtBoxVisiblePassword.Visibility = Visibility.Hidden;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Imagefile|*.png;";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream fileStream = File.Create(saveFileDialog.FileName))
                {
                    pngImage.Save(fileStream);
                }
            }
        }
      

    }
}