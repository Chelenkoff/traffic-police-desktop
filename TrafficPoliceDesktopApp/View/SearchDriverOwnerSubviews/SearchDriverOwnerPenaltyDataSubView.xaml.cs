

using System.Windows.Controls;

namespace TrafficPoliceDesktopApp.View.SearchDriverOwnerSubviews
{
    /// <summary>
    /// Interaction logic for SearchDriverOwnerPenaltyDataSubView.xaml
    /// </summary>
    
    public partial class SearchDriverOwnerPenaltyDataSubView : UserControl
    {



        public SearchDriverOwnerPenaltyDataSubView()
        {
            InitializeComponent();
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


        



    }
}
