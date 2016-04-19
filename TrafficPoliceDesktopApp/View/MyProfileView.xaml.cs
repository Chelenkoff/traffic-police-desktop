using MahApps.Metro.Controls;
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
using MahApps.Metro.Controls.Dialogs;
using System.Windows.Shapes;
using TrafficPoliceDesktopApp.ServiceReference1;
using TrafficPoliceDesktopApp.Utilities;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Diagnostics;
using iTextSharp.text.pdf;
using ceTe.DynamicPDF.Merger;
using System.Windows.Interactivity;


namespace TrafficPoliceDesktopApp.View
{
    /// <summary>
    /// Interaction logic for MyProfileView.xaml
    /// </summary>
    public partial class MyProfileView : UserControl
    {

        public MyProfileView()
        {
           
            InitializeComponent();
            
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            screenshotAndSaveUserData(); 
        }


        private void screenshotAndSaveUserData()
        {
            pswdBoxPassword.Visibility = Visibility.Hidden;
            txtBoxVisiblePassword.Text = pswdBoxPassword.Password;
            txtBoxVisiblePassword.Visibility = Visibility.Visible;

            Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);

            RenderTargetBitmap renderTargetBitmap =
            new RenderTargetBitmap(471, 467, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(this);
            PngBitmapEncoder pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            pswdBoxPassword.Visibility = Visibility.Visible;
            txtBoxVisiblePassword.Visibility = Visibility.Hidden;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Imagefile|*.png;";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (Stream fileStream = File.Create(saveFileDialog.FileName))
                {
                    pngImage.Save(fileStream);
                }
            }
        }




    }
}
