using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TrafficPoliceDesktopApp.ServiceReference1;
using TrafficPoliceDesktopApp.View.SearchDriverOwnerSubviews;

namespace TrafficPoliceDesktopApp.ViewModel.SearchDriverOwnerSubviewsVMs
{
    public class SearchDriverOwnerPenaltyDataSubViewViewModel : INotifyPropertyChanged
    {
        Service1Client service;

        //Constructor
        public SearchDriverOwnerPenaltyDataSubViewViewModel(DriverOwner drOwner)
        {
            service = new Service1Client();

            _driverOwner = drOwner;
            _penaltiesList  = new ObservableCollection<Penalty>(_driverOwner.Penalties);

            _viewMessage = String.Format("Нарушения на {0} {1}", _driverOwner.FirstName, _driverOwner.LastName);
        }

        //RaisePropertyChangedEvent implementation (.net 4.0, for 4.5 we can use CallerMemberName)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        //Get/Set ViewMessage
        private string _viewMessage;
        public string ViewMessage
        {
            private get { return _viewMessage; }
            set
            {
                _viewMessage = value;
                RaisePropertyChangedEvent("ViewMessage");
            }
        }

        //Get/Set DriverOwner
        private DriverOwner _driverOwner;
        public DriverOwner DriverOwner
        {
            private get { return _driverOwner; }
            set
            {
                _driverOwner = value;
                RaisePropertyChangedEvent("DriverOwner");
            }
        }

        //Get/Set PenaltiesList
        private ObservableCollection<Penalty> _penaltiesList;
        public ObservableCollection<Penalty> PenaltiesList
        {
            private get { return _penaltiesList; }
            set
            {
                _penaltiesList = value;
                RaisePropertyChangedEvent("PenaltiesList");
            }
        }

        //Get/Set SelectedPenalty
        private Penalty _selectedPenalty;
        public Penalty SelectedPenalty
        {
            private get { return _selectedPenalty; }
            set
            {
                _selectedPenalty = value;
                RaisePropertyChangedEvent("SelectedPenalty");
            }
        }

        public ICommand DeletePenaltyCommand
        {
            get { return new DelegateCommand(deletePenalty); }
        }
        private void deletePenalty()
        {
            if (_selectedPenalty == null) return;

            int checkResponse;

            //Starting new non-blocking-ui task
            Task.Factory.StartNew(() =>
            {
                //startLoading();

                DispatchService.Invoke(() =>
                {
                    startLoading();
                });


                //REST response
                checkResponse = service.removePenalty(_selectedPenalty);

                DispatchService.Invoke(() =>
                {
                    DispatchService.Invoke(() =>
                    {
                        stopLoading();
                    });

                    switch (checkResponse)
                    {
                        case 1:
                            MessageBox.Show("Възникна проблем при свързването с базата данни ", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            break;
                        case 2:
                            MessageBox.Show("Неуспешно премахване на нарушение", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            break;
                        case 0:
                            //Removing penalty from parent 
                            DriverOwner.Penalties = DriverOwner.Penalties.Where(val => val != _selectedPenalty).ToArray();
                            //Clearing penalty from grid
                            if (PenaltiesList.Remove(_selectedPenalty) == true)
                            {
                                MessageBox.Show("Нарушението бе успешно премахнато", "Внимание", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                break;
                            }
                            break;
                    }
                });
            });
        }

        public ICommand ShowPenaltyDetailsCommand
        {
            get { return new DelegateCommand(showPenaltyDetails); }
        }
        private void showPenaltyDetails()
        {
            if (_selectedPenalty != null)
            {
                PenaltyDetailsWindowViewModel penDetailsVM = new PenaltyDetailsWindowViewModel(_driverOwner, _selectedPenalty);
                PenaltyDetailsWindow penDetailsWindow = new PenaltyDetailsWindow();
                penDetailsWindow.DataContext = penDetailsVM;
                penDetailsWindow.Show();
            }

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


        private void startLoading()
        {
            IsProgressRingActive = true;

        }
        private void stopLoading()
        {
            IsProgressRingActive = false;

        }
    }
}
