using SS23_SWEN2_TourPlanner_WPF.BL;
using SS23_SWEN2_TourPlanner_WPF.ViewModels;
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
using System.Windows.Shapes;

namespace SS23_SWEN2_TourPlanner_WPF.Views
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();
            var toursVM = App.Current.Services.GetService(typeof(ToursViewModel)) as ToursViewModel ?? throw new Exception("Failed to get ToursViewModel");
            var toursManager = App.Current.Services.GetService(typeof(IToursManager)) as IToursManager ?? throw new Exception("Failed to get ToursManager");
            var messageBoxService = App.Current.Services.GetService(typeof(IMessageBoxService)) as IMessageBoxService ?? throw new Exception("Failed to get MessageBoxService");
            if(toursVM != null && toursManager != null)
            {
                this.DataContext = new SearchViewModel(toursVM, toursManager, messageBoxService);
            }
        }
    }
}
