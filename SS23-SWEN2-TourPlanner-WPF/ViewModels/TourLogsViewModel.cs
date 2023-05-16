using SS23_SWEN2_TourPlanner_WPF.BL;
using SS23_SWEN2_TourPlanner_WPF.Models;
using SS23_SWEN2_TourPlanner_WPF.Views;
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
    public class TourLogsViewModel : BaseViewModel
    {
        private IToursManager _toursManager;
        public ObservableCollection<TourLog> TourLogs { get; set; } = new ObservableCollection<TourLog>();

        public RelayCommand AddTourLogCommand { get; set; }
        public RelayCommand DeleteTourLogCommand { get; }

        public TourLog? CurrentTourLog
        {
            get { return _currentTourLog; }
            set
            {
                // load tour from toursmanager
                _currentTourLog = value;
                if (_currentTourLog == null)
                    return;

                OnPropertyChanged();
            }
        }

        private TourLog? _currentTourLog;

        public TourLogsViewModel(IToursManager toursManager, Tour tour)
        {
            _toursManager = toursManager;
            tour.TourLogs.ForEach(tourLog =>
            {
                TourLogs.Add(tourLog);
            });

            AddTourLogCommand = new RelayCommand(param =>
            {
                if (App.Current.Services.GetService(typeof(AddTourLogViewModel)) is AddTourLogViewModel addTourLogViewModel)
                {
                    // create Dialog window
                    var dialog = new AddTourLogDialog(addTourLogViewModel);
                    dialog.Show();

                    addTourLogViewModel.AddButtonClicked += (_, tourLog) =>
                    {
                        TourLogs.Add(tourLog);
                        toursManager.AddTourLog(tour, tourLog);
                        dialog.Close();
                    };
                }
            });
            DeleteTourLogCommand = new RelayCommand(_ => { 
                // delete TourLog from db

                // delete Tourlog from observableCollection
            });
        }
    }

}
