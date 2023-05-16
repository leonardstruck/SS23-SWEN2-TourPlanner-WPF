using SS23_SWEN2_TourPlanner_WPF.Models;
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
using System.Windows.Shapes;

namespace SS23_SWEN2_TourPlanner_WPF.Windows
{
    /// <summary>
    /// Interaction logic for AddTourLogDialog.xaml
    /// </summary>
    public partial class AddTourLogDialog : Window
    {
        public AddTourLogDialog(AddTourLogViewModel addTourLogViewModel)
        {
            InitializeComponent();
            this.CBDifficulty.ItemsSource = Enum.GetValues(typeof(Difficulty));
            this.DataContext = addTourLogViewModel;
        }
    }
}
