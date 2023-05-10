﻿using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.ViewModels
{
    public class ToursViewModel : BaseViewModel
    {
        public ObservableCollection<Tour> Tours { get; }

        public ToursViewModel()
        {

            Tours = new ObservableCollection<Tour>() { new Tour("Tour 1"), new Tour("Tour 2") };
        }
    }
}
