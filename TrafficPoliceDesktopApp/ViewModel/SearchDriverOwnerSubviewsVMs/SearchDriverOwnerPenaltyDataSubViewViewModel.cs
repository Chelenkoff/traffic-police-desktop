using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TrafficPoliceDesktopApp.ServiceReference1;

namespace TrafficPoliceDesktopApp.ViewModel.SearchDriverOwnerSubviewsVMs
{
    public class SearchDriverOwnerPenaltyDataSubViewViewModel : INotifyPropertyChanged
    {

        //Constructor
        public SearchDriverOwnerPenaltyDataSubViewViewModel(DriverOwner drOwner)
        {
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
