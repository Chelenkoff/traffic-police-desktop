using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TrafficPoliceDesktopApp.View.SearchDriverOwnerSubviews
{
    /// <summary>
    /// Interaction logic for SearchDriverOwnerPenaltyDataSubView.xaml
    /// </summary>
    
    public partial class SearchDriverOwnerPenaltyDataSubView : UserControl
    {
        public DriverOwner DriverOwner { get; set; }
        public Service1Client ServiceReference { get; set; }
        public ObservableCollection<Penalty> PenaltiesList  { get; set; }
        public SearchDriverOwnerPenaltyDataSubView()
        {
            InitializeComponent();
        }

        public void initDriverOwnerPenaltyData(DriverOwner drOwner, Service1Client servRef)
        {

            DriverOwner = drOwner;
            ServiceReference = servRef;
            PenaltiesList = new ObservableCollection<Penalty>(DriverOwner.Penalties);

            lblViewMessage.Content = String.Format("Нарушения на {0} {1}", DriverOwner.FirstName, DriverOwner.LastName);


            //Set DataGrid items source 
            //dataGridPenalties.ItemsSource = DriverOwner.Penalties;
            dataGridPenalties.ItemsSource = PenaltiesList;


        }

        private void dataGridPenalties_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Getting 'Penalty info' on selected row

            if((Penalty)dataGridPenalties.SelectedItem != null)
            {
                PenaltyDetailsWindow penDetailsWindow = new PenaltyDetailsWindow((Penalty)dataGridPenalties.SelectedItem,DriverOwner,ServiceReference);
                penDetailsWindow.Show();
            }
        }

        private async void btnDeletePenalty_Click(object sender, RoutedEventArgs e)
        {
            Penalty penToRemove = (Penalty)dataGridPenalties.SelectedItem;

            if (penToRemove == null) return;

            int checkResponse;
            await Task.Factory.StartNew(() =>
            {
                //startLoading logic
                this.Dispatcher.Invoke((Action)(() => startLoading()));

                //Constructing USER from DB


                checkResponse = ServiceReference.removePenalty(penToRemove);


                // invoke user code on the main UI thread
                Dispatcher.Invoke(new Action(() =>
                {
                    //stopLoading logic
                    this.Dispatcher.Invoke((Action)(() => stopLoading()));

                    switch (checkResponse)
                    {
                        case 1:
                            //ParentWindow.ShowMessageAsync("Внимание", "Възникна проблем при свързването с базата данни ", MessageDialogStyle.Affirmative);
                            MessageBox.Show("Възникна проблем при свързването с базата данни ","Внимание", MessageBoxButton.OK);
                            break;
                        case 2:
                            //ParentWindow.ShowMessageAsync("Внимание", "Неуспешно обновяване на потребител", MessageDialogStyle.Affirmative);
                            MessageBox.Show("Неуспешно премахване на нарушение", "Внимание", MessageBoxButton.OK);

                            break;
                        case 0:
                            //ParentWindow.ShowMessageAsync("Внимание", "Данните за потребителя бяха успешно обновени", MessageDialogStyle.Affirmative);
                            MessageBox.Show("Нарушението бе успешно премахнато", "Внимание", MessageBoxButton.OK);
                            PenaltiesList.Remove(penToRemove);

                            break;

                    }

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
        



    }
}
