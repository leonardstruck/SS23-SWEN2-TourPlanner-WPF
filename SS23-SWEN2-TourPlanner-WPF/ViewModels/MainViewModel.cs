using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.ViewModels
{
    public class MainViewModel
    {
        public ToursViewModel ToursVM { get; }
        public MainViewModel(ToursViewModel toursVM)
        {
            ToursVM = toursVM;
        }
    }
}
