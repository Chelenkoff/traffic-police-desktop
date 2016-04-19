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

        public ObservableCollection<Penalty> PenaltiesList  { get; set; }

        public SearchDriverOwnerWindow ParentWindow { get; set; }

        public SearchDriverOwnerPenaltyDataSubView()
        {
            InitializeComponent();
        }

        public void initDriverOwnerPenaltyData(SearchDriverOwnerWindow parent)
        {
            
            //Setting parent
            ParentWindow = parent;
            //lblViewMessage.Content = String.Format("Нарушения на {0} {1}", ParentWindow.DriverOwner.FirstName, ParentWindow.DriverOwner.LastName);
        }

        private void dataGridPenalties_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Getting 'Penalty info' on selected row

            if((Penalty)dataGridPenalties.SelectedItem != null)
            {
                //PenaltyDetailsWindow penDetailsWindow = new PenaltyDetailsWindow((Penalty)dataGridPenalties.SelectedItem,ParentWindow.DriverOwner,ParentWindow.ServiceReference);
                //penDetailsWindow.Show();
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


                //checkResponse = ParentWindow.ServiceReference.removePenalty(penToRemove);

                // invoke user code on the main UI thread
                Dispatcher.Invoke(new Action(() =>
                {
                    //stopLoading logic
                    this.Dispatcher.Invoke((Action)(() => stopLoading()));

                    //switch (checkResponse)
                    //{
                    //    case 1:
                    //        ParentWindow.ShowMessageAsync("Внимание", "Възникна проблем при свързването с базата данни ", MessageDialogStyle.Affirmative);
                    //        break;
                    //    case 2:
                    //        ParentWindow.ShowMessageAsync("Внимание", "Неуспешно премахване на нарушение", MessageDialogStyle.Affirmative);
                    //        break;
                    //    case 0:
                    //        //Removing penalty from parent 
                    //        //ParentWindow.DriverOwner.Penalties = ParentWindow.DriverOwner.Penalties.Where(val => val != penToRemove).ToArray();
                    //        //Clearing penalty from table
                    //        PenaltiesList.Remove(penToRemove);

                    //        ParentWindow.ShowMessageAsync("Внимание", "Нарушението бе успешно премахнато", MessageDialogStyle.Affirmative);

                    //        break;

                    //}

                }));
            });
            
        }

        //public bool arePenaltiesAvailable()
        //{
        //    if (ParentWindow.DriverOwner.Penalties.Length == 0)
        //    {
        //        ParentWindow.ShowMessageAsync("Внимание", "Водачът няма актуални нарушения", MessageDialogStyle.Affirmative);
        //        return false;
        //    }
        //    else
        //    {   //Penalties are available
        //        PenaltiesList = new ObservableCollection<Penalty>(ParentWindow.DriverOwner.Penalties);
        //        //Set DataGrid items source 
        //        dataGridPenalties.ItemsSource = PenaltiesList;
        //        return true;
        //    }
        //}

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
