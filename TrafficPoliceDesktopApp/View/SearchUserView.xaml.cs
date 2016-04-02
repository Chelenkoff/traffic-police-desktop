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
using MahApps.Metro.Controls.Dialogs;

using TrafficPoliceDesktopApp.ServiceReference1;
using TrafficPoliceDesktopApp.Utilities;

namespace TrafficPoliceDesktopApp.View
{
    /// <summary>
    /// Interaction logic for SearchUserView.xaml
    /// </summary>
    public partial class SearchUserView : UserControl
    {
        public MainWindow ParentWindow { get; set; }
        public SearchUserView()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string userId = txtBoxId.Text;
            //UI validation
            if (!uiDataValidation(userId)) return;

            //Creating empty user
            User user = new User();

            
            //Starting new non-blocking-ui task
            Task.Factory.StartNew(() =>
            {
                //startLoading logic
                this.Dispatcher.Invoke((Action)(() => startLoading()));

                //Constructing USER from DB
                user = ParentWindow.Service.GetReadOnlyUserById(userId);

                // invoke user code on the main UI thread
                Dispatcher.Invoke(new Action(() =>
                {
                    //stopLoading logic
                    this.Dispatcher.Invoke((Action)(() => stopLoading()));

                    //DB response validation
                    if (!dbResponseValidation(user)) return;

                    //DB - OK, USER - FOUND
                    SearchUserWindow searchUsrWindow = new SearchUserWindow(user);
                    searchUsrWindow.Show();

                }));
            });

            
        }



        private  bool dbResponseValidation(User usr)
        {
            //DB-OK , USER - NOT FOUND
            if (usr != null && usr.UserId == 0)
            {
                ParentWindow.ShowMessageAsync("Грешка", "Не съществува служител с тези данни!");
                return false;
            }

            //DB - NOT CONNECTED
            else if (usr == null)
            {
                ParentWindow.ShowMessageAsync("Грешка", "Проблем с връзката с базата данни.");
                return false;
            }
            else return true;

        }


        private bool uiDataValidation(string id)
        {
            //Name validation
            string idValidation = InputValidator.validateId(id);

            if (idValidation != null)
            {
                ParentWindow.ShowMessageAsync("Грешка в ЕГН", idValidation);
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
