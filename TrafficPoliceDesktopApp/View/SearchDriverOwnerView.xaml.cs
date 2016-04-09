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
    /// Interaction logic for SearchDriverOwnerView.xaml
    /// </summary>
    public partial class SearchDriverOwnerView : UserControl
    {
        public MainWindow ParentWindow { get; set; }
        public SearchDriverOwnerView()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string driverOwnerId = txtBoxId.Text;
            //UI validation
            if (!uiDataValidation(driverOwnerId)) return;

            //Creating empty user
            DriverOwner drOwner = new DriverOwner();


            //Starting new non-blocking-ui task
            Task.Factory.StartNew(() =>
            {
                //startLoading logic
                this.Dispatcher.Invoke((Action)(() => startLoading()));

                //Constructing USER from DB
                drOwner = ParentWindow.Service.GetDriverOwnerById(driverOwnerId);

                // invoke user code on the main UI thread
                Dispatcher.Invoke(new Action(() =>
                {
                    //stopLoading logic
                    this.Dispatcher.Invoke((Action)(() => stopLoading()));

                    //DB response validation
                    if (!dbResponseValidation(drOwner)) return;

                    //DB - OK, USER - FOUND
                    SearchDriverOwnerWindow searchDrOwnerWindow = new SearchDriverOwnerWindow(drOwner);
                    searchDrOwnerWindow.Show();

                }));
            });

        }


        private bool dbResponseValidation(DriverOwner drOwner)
        {
            //DB-OK , USER - NOT FOUND
            if (drOwner != null && drOwner.DriverOwnerId == 0)
            {
                ParentWindow.ShowMessageAsync("Грешка", "Не съществува служител с тези данни!");
                return false;
            }

            //DB - NOT CONNECTED
            else if (drOwner== null)
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
