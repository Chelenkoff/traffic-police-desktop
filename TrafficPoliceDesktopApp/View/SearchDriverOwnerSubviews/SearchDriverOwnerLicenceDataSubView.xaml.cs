

using System.Windows;
using System.Windows.Controls;

namespace TrafficPoliceDesktopApp.View.SearchDriverOwnerSubviews
{
    /// <summary>
    /// Interaction logic for SearchDriverOwnerLicenceDataSubView.xaml
    /// </summary>
    public partial class SearchDriverOwnerLicenceDataSubView : UserControl
    {
        public SearchDriverOwnerLicenceDataSubView()
        {
            InitializeComponent();

        }




        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViewerDriverLicenceData.ScrollToTop();
        }
    }
}
