using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TrafficPoliceDesktopApp.Utilities;
using WCFDBService;

namespace TrafficPoliceDesktopApp.ViewModel.SearchDriverOwnerSubviewsVMs
{
    class MailServiceWindowViewModel : INotifyPropertyChanged
    {
        private const string KAT_MAIL = "resiloveca94@gmail.com";
        private DriverOwner _drOwner;
        private User _user;
        private MailService _mailer;
        public MailServiceWindowViewModel(DriverOwner drOwner,User usr)
        {
            _drOwner = drOwner;
            _user = usr;
            _mailer = new MailService();

            Title = String.Format("{0} {1} - Контакт", drOwner.FirstName, drOwner.LastName);
            Header = String.Format("Съобщение до {0} {1}", drOwner.FirstName, drOwner.LastName);
            SenderInfo = String.Format("<{0}>\n{1} {2}", KAT_MAIL, usr.FirstName, usr.LastName);
            ReceiverInfo = String.Format("<{0}>\n{1} {2}", drOwner.Email, drOwner.FirstName, drOwner.LastName);
        }

        //Get/Set SenderInfo
        private string _senderInfo;
        public string SenderInfo
        {
            private get { return _senderInfo; }
            set
            {
                _senderInfo = value;
                RaisePropertyChangedEvent("SenderInfo");
            }
        }
        //Get/Set ReceiverInfo
        private string _receiverInfo;
        public string ReceiverInfo
        {
            private get { return _receiverInfo; }
            set
            {
                _receiverInfo = value;
                RaisePropertyChangedEvent("ReceiverInfo");
            }
        }

        //Get/Set Title
        private string _title;
        public string Title
        {
            private get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChangedEvent("Title");
            }
        }

        //Get/Set Header
        private string _header;
        public string Header
        {
            private get { return _header; }
            set
            {
                _header = value;
                RaisePropertyChangedEvent("Header");
            }
        }

        //Get/Set Topic
        private string _topic;
        public string Topic
        {
            private get { return _topic; }
            set
            {
                _topic = value;
                RaisePropertyChangedEvent("Topic");
            }
        }

        //Get/Set Topic
        private string _message;
        public string Message
        {
            private get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChangedEvent("Message");
            }
        }

        public ICommand SendMailCommand
        {
            get { return new DelegateCommand(sendMail); }
        }
        private void sendMail()
        {
            _mailer.AddReceiver(_drOwner.Email);
            _mailer.Sender = KAT_MAIL;
            _mailer.Subject = Topic;
            _mailer.Body = Message;

            bool isMailSend;
            Task.Factory.StartNew(() =>
            {
                //startLoading();

                DispatchService.Invoke(() =>
                {
                    startLoading();
                });

                isMailSend = _mailer.SendEmail();
                stopLoading();

                if (isMailSend)
                {
                    MessageBox.Show("Съобщението е изпратено успешно", "Готово", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    MessageBox.Show("Възникна проблем при изпращане на съобщението", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            });

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

        //RaisePropertyChangedEvent implementation (.net 4.0, for 4.5 we can use CallerMemberName)
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
