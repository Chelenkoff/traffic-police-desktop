using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using TrafficPoliceDesktopApp.ServiceReference1;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Threading;

namespace TrafficPoliceDesktopApp.View
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login 
    {
        const int maxIdLength = 10;
        Service1Client service;

        public MainWindow NextWindow { get; set; }
        public Login()
        {
            service = new Service1Client();
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            //UI validation of ID and PASS
            if (!uiPassAndIdValidation(pswdBoxPass.Password, txtBoxEGN.Text)) return;

            //Creating empty user
            User user = new User();
            //Getting already validated input data
            string egn = txtBoxEGN.Text;
            string pass = pswdBoxPass.Password;

            //Starting new non-blocking-ui task
            Task.Factory.StartNew(() =>
            {
                //startLoading logic
                this.Dispatcher.Invoke((Action)(() => startLoading()));

                //Constructing USER from DB
                user = service.GetUserByIdAndPass(egn, pass);
                

                // invoke user code on the main UI thread
                Dispatcher.Invoke(new Action(() =>
                {
                    //stopLoading logic
                    this.Dispatcher.Invoke((Action)(() => stopLoading()));


                    if (!dbResponseValidation(user)) return;
                   
                    NextWindow = new MainWindow(user);
                    NextWindow.LoginWindow = this;
                    NextWindow.Show();
                    this.Hide();
                }));
            });
   
        }

        private bool uiPassAndIdValidation(string pass,string id)
        {
            //Null or whitespaces
            if (String.IsNullOrWhiteSpace(pass) || String.IsNullOrWhiteSpace(id))
            {
                this.ShowMessageAsync("Внимание", "Не са въведени необходимите данни!");
                return false;
            }
             //Digit check
            else if(!id.All(char.IsDigit))
            {
                this.ShowMessageAsync("Внимание", "Некоректен формат на ЕГН! \nМоже да съдържа само цифри!");
                return false;
            }
            //Id length check
            else if(id.Length != maxIdLength)
            {
                this.ShowMessageAsync("Внимание", "ЕГН трябва да съдържа точно 10 цифри!");
                return false;
            }
            else return true;
        }

        private bool dbResponseValidation(User usr)
        {
            //DB-OK , USER - NOT FOUND
            if (usr != null && usr.UserId == 0)
            {
                this.ShowMessageAsync("Грешка", "Не съществува потребител с тези данни!");
                return false;
            }

            //DB - NOT CONNECTED
            else if (usr == null)
            {
                this.ShowMessageAsync("Грешка", "Проблем с връзката с базата данни.");
                return false;
            }
            else return true;
 
        }

        private void startLoading()
        {
            progressRingLoading.IsActive = true;
            txtBoxEGN.IsEnabled = false;
            pswdBoxPass.IsEnabled = false;
            btnLogin.IsEnabled = false;
        }
        private void stopLoading()
        {
            progressRingLoading.IsActive = false;
            txtBoxEGN.IsEnabled = true;
            pswdBoxPass.IsEnabled = true;
            btnLogin.IsEnabled = true;
        }

        private void LoginWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            service.Close();
            Environment.Exit(0);
        }

    }
}
