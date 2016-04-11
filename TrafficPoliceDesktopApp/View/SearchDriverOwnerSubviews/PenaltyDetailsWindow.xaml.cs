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
using TrafficPoliceDesktopApp.ServiceReference1;
using ceTe.DynamicPDF.Merger;
using System.IO;
using Microsoft.Win32;
using iTextSharp.text.pdf;

namespace TrafficPoliceDesktopApp.View.SearchDriverOwnerSubviews
{
    /// <summary>
    /// Interaction logic for PenaltyDetailsWindow.xaml
    /// </summary>
    public partial class PenaltyDetailsWindow : MetroWindow
    {
        public Penalty Penalty { get; set; }
        public DriverOwner DriverOwner { get; set; }
        public Service1Client ServiceReference { get; set; }
        public PenaltyDetailsWindow(Penalty pen, DriverOwner drOwner, Service1Client servRef)
        {
            InitializeComponent();

            Penalty = pen;
            DriverOwner = drOwner;
            ServiceReference = servRef;

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            this.Title = String.Format("Нарушение № {0} - {1} {2}",Penalty.PenaltyId, DriverOwner.FirstName, DriverOwner.LastName);

            initPenaltyDetails();

        }

        private void initPenaltyDetails()
        {
            lblViewMessage.Content = String.Format("Нарушение на {0} {1}", DriverOwner.FirstName, DriverOwner.LastName);

            txtBoxPenaltyId.Text = Penalty.PenaltyId.ToString();

            datePickeIssuedDate.SelectedDate = Penalty.IssuedDateTime;
            datePickerHappenedDate.SelectedDate = Penalty.HappenedDateTime;

            txtBoxIssuerId.Text = Penalty.IssuerId.ToString();
            txtBoxLocation.Text = Penalty.Location;
            txtBoxDescription.Text = Penalty.Description;
            txtBoxDisagreement.Text = Penalty.Disagreement;
        }

        private void btnGenerateDisagreementTemplate_Click(object sender, RoutedEventArgs e)
        {
            fillPDFForm();
        }

        private void fillPDFForm()
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
                    fields.SetField("driverOwnerNames", String.Format("{0} {1} {2}", DriverOwner.FirstName, DriverOwner.SecondName, DriverOwner.LastName));
                    fields.SetField("driverOwnerId", String.Format("{0}", DriverOwner.DriverOwnerId));
                    fields.SetField("driverOwnerResidence", String.Format("{0}", DriverOwner.Residence));
                    fields.SetField("penaltyId", String.Format("{0}", Penalty.PenaltyId));
                    fields.SetField("penaltyIssuedDateTime", String.Format("{0}", Penalty.IssuedDateTime.ToShortDateString()));
                    fields.SetField("penaltyId_2", String.Format("{0}", Penalty.PenaltyId));
                    fields.SetField("penaltyIssuedDateTime_2", String.Format("{0}", Penalty.IssuedDateTime.ToShortDateString()));
                    fields.SetField("penaltyDisagreement", String.Format("{0}", Penalty.Disagreement));
                    fields.SetField("penaltyId_3", String.Format("{0}", Penalty.PenaltyId));
                    fields.SetField("penaltyIssuedDateTime_3", String.Format("{0}", Penalty.IssuedDateTime.ToString()));
                    fields.SetField("currentDateTime", String.Format("{0}", DateTime.Now));
                    fields.SetField("driverOwnerNames_2", String.Format("{0} {1} {2}", DriverOwner.FirstName, DriverOwner.SecondName, DriverOwner.LastName));

                    // flatten form fields and close document
                    stamper.FormFlattening = true;
                    stamper.Close();
                }
            }

        }

    }
}
