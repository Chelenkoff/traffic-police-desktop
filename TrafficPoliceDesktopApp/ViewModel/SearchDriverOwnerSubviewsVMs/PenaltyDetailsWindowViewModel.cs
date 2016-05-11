using iTextSharp.text.pdf;
using Microsoft.Maps.MapControl.WPF;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TrafficPoliceDesktopApp.ServiceReference1;
using TrafficPoliceDesktopApp.View;
using WCFDBService;

namespace TrafficPoliceDesktopApp.ViewModel.SearchDriverOwnerSubviewsVMs
{
    public class PenaltyDetailsWindowViewModel : INotifyPropertyChanged
    {
        //data
        private DriverOwner driverOwner;
        private Service1Client service;

        public PenaltyDetailsWindowViewModel(DriverOwner drOwner,Penalty penalty)
        {
            service = new Service1Client();
            _penalty = penalty;
            driverOwner = drOwner;
            _title = String.Format("Нарушение № {0} - {1} {2}", _penalty.PenaltyId, drOwner.FirstName, drOwner.LastName);
            _viewMessage = String.Format("Нарушение на {0} {1}", drOwner.FirstName, drOwner.LastName);

            LocationCenter = new Location(penalty.Latitude,penalty.Longtitude);


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




        //Get/Set LocationCenter
        private Location _locationCenter;
        public Location LocationCenter
        {
            private get { return _locationCenter; }
            set
            {
                _locationCenter = value;
                RaisePropertyChangedEvent("LocationCenter");
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

        //Get/Set Penalty
        private Penalty _penalty;
        public Penalty Penalty
        {
            private get { return _penalty; }
            set
            {
                _penalty = value;
                RaisePropertyChangedEvent("Penalty");
            }
        }

        public ICommand GenerateDisagreementDocumentCommand
        {
            get { return new DelegateCommand(generateDisagreementDocumentCommand); }
        }
        private void generateDisagreementDocumentCommand()
        {
            string formFile = "./Resources/Docs/disagreement_template.pdf";
            PdfReader reader = new PdfReader(formFile);


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF File|*.pdf;";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (PdfStamper stamper = new PdfStamper(reader, new FileStream(saveFileDialog.FileName, FileMode.Create)))
                {
                    AcroFields fields = stamper.AcroFields;

                    // set form fields
                    fields.SetField("driverOwnerNames", String.Format("{0} {1} {2}", driverOwner.FirstName, driverOwner.SecondName, driverOwner.LastName));
                    fields.SetField("driverOwnerId", String.Format("{0}", driverOwner.DriverOwnerId));
                    fields.SetField("driverOwnerResidence", String.Format("{0}", driverOwner.Residence));
                    fields.SetField("penaltyId", String.Format("{0}", Penalty.PenaltyId));
                    fields.SetField("penaltyIssuedDateTime", String.Format("{0}", Penalty.IssuedDateTime.ToShortDateString()));
                    fields.SetField("penaltyId_2", String.Format("{0}", Penalty.PenaltyId));
                    fields.SetField("penaltyIssuedDateTime_2", String.Format("{0}", Penalty.IssuedDateTime.ToShortDateString()));
                    fields.SetField("penaltyDisagreement", String.Format("{0}", Penalty.Disagreement));
                    fields.SetField("penaltyId_3", String.Format("{0}", Penalty.PenaltyId));
                    fields.SetField("penaltyIssuedDateTime_3", String.Format("{0}", Penalty.IssuedDateTime.ToString()));
                    fields.SetField("currentDateTime", String.Format("{0}", DateTime.Now));
                    fields.SetField("driverOwnerNames_2", String.Format("{0} {1} {2}", driverOwner.FirstName, driverOwner.SecondName, driverOwner.LastName));

                    // flatten form fields and close document
                    stamper.FormFlattening = true;
                    stamper.Close();
                }
            }
        }


        public ICommand SendEmailCommand
        {
            get { return new DelegateCommand(sendEmail); }
        }
        private void sendEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("resiloveca94@gmail.com");
                mail.To.Add("sroreg@abv.bg");
                mail.Subject = "Test Mail - 1";
                mail.Body = "Specialno za bat gorgi";

                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment("your attachment file");
                //mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("resiloveca94@gmail.com", "parola");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public ICommand ShowIssuerDetailsCommand
        {
            get { return new DelegateCommand(showIssuerDetailsCommand); }
        }
        private void showIssuerDetailsCommand()
        {

            //Creating empty user
            User user = new User();

            //Starting new non-blocking-ui task
            Task.Factory.StartNew(() =>
            {
                //startLoading();

                DispatchService.Invoke(() =>
                {
                    startLoading();
                });

                //Constructing USER from DB
                user = service.GetReadOnlyUserById(Penalty.IssuerId.ToString());

                DispatchService.Invoke(() =>
                {
                    DispatchService.Invoke(() =>
                    {
                        stopLoading();
                    });

                    //DB response validation
                    if (!dbResponseValidation(user)) return;

                    //DB - OK, USER - FOUND
                    SearchUserWindowViewModel searchUserWindowVM = new SearchUserWindowViewModel(user);
                    SearchUserWindow searchUsrWindow = new SearchUserWindow();
                    searchUsrWindow.DataContext = searchUserWindowVM;
                    searchUsrWindow.Show();
                });
            });
        }

        private void startLoading()
        {
            IsProgressRingActive = true;

        }
        private void stopLoading()
        {
            IsProgressRingActive = false;

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

        //DB Response validation
        private bool dbResponseValidation(User usr)
        {
            //DB-OK , USER - NOT FOUND
            if (usr != null && usr.UserId == 0)
            {
                MessageBox.Show("Не съществува служител с тези данни!", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }

            //DB - NOT CONNECTED
            else if (usr == null)
            {
                MessageBox.Show("Проблем с връзката с базата данни.", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else return true;

        }

    }
}
