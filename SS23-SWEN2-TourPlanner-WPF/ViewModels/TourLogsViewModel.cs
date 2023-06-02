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
using System.Windows;
using System.Windows.Input;

namespace SS23_SWEN2_TourPlanner_WPF.ViewModels
{
    public class TourLogsViewModel : BaseViewModel
    {
        private IToursManager _toursManager;
        public ObservableCollection<TourLog> TourLogs { get; set; } = new ObservableCollection<TourLog>();

        public RelayCommand AddTourLogCommand { get; set; }
        public RelayCommand DeleteTourLogCommand { get; }

        public RelayCommand EditTourLogCommand { get; }

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
                if (CurrentTourLog != null)
                {
                    if (MessageBox.Show("Do you really want to delete this Tourlog?",
                    "Delete Tourlog",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        toursManager.DeleteTourLog(tour, CurrentTourLog);
                        TourLogs.Remove(TourLogs.Where(i => i.Id == CurrentTourLog.Id).Single());
                        CurrentTourLog = null;
                    }
                }
            });
            EditTourLogCommand = new RelayCommand(_ =>
            {
                if (CurrentTourLog == null)
                    return;

                var editTourLogVM = new EditTourLogViewModel(CurrentTourLog);

                var editTourLogDialog = new EditTourLogDialog(editTourLogVM);
                editTourLogDialog.Show();

                editTourLogVM.EditButtonClicked += (_, tourLog) => { 
                    toursManager.EditTourLog(tourLog);

                    // Replace tourLog in List with the changed tour
                    // find tourlog that has the same Id
                    var tourLogInList = TourLogs.Single(t => t.Id == tourLog.Id);
                    if (tourLogInList == null)
                        return;

                    int index = TourLogs.IndexOf(tourLogInList);
                    if (index != -1)
                    {
                        TourLogs.RemoveAt(index);
                        TourLogs.Insert(index, tourLog);
                    }

                    CurrentTourLog = tourLog;

                    editTourLogDialog?.Close();
                };
            });
        }
    }

}
