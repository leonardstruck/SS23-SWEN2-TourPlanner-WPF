using SS23_SWEN2_TourPlanner_WPF.BL;
using SS23_SWEN2_TourPlanner_WPF.Models;
using SS23_SWEN2_TourPlanner_WPF.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SS23_SWEN2_TourPlanner_WPF.ViewModels
{
    public class ToursViewModel : BaseViewModel
    {
        public ObservableCollection<Tour> Tours { get; } = new();
        public RelayCommand CreateTourCommand { get; }

        public ToursViewModel(IToursManager toursManager)
        {
            toursManager.GetTours().ToList().ForEach(t =>  Tours.Add(t));
            this.CreateTourCommand = new RelayCommand(param => OpenCreateTourModal());
        }

        private void OpenCreateTourModal()
        {
            var modal = new AddTourDialog();
            modal.Show();
        }
    }
}
