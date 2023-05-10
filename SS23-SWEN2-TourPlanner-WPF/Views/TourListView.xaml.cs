﻿using SS23_SWEN2_TourPlanner_WPF.ViewModels;
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
    /// Interaction logic for TourListView.xaml
    /// </summary>
    public partial class TourListView : UserControl
    {
        public TourListView()
        {
            InitializeComponent();
            this.DataContext = App.Current.Services.GetService(typeof(ToursViewModel));
        }
    }
}
