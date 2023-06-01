using SS23_SWEN2_TourPlanner_WPF;
using SS23_SWEN2_TourPlanner_WPF.BL;
using SS23_SWEN2_TourPlanner_WPF.Models;
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
    public class ToursViewModel : BaseViewModel
    {
        private readonly IToursManager toursmanager;
        public TourLogsViewModel? TourLogsVM { get; set; }

        public ObservableCollection<Tour> Tours { get; } = new();
        public RelayCommand CreateTourCommand { get; }
        public RelayCommand DeleteTourCommand { get; }
        public RelayCommand EditTourCommand { get; }
        public RelayCommand ExportReportCommand { get; }
        public RelayCommand ExportSingleReportCommand { get; }
        public RelayCommand ExportDataCommand { get; }

        public Tour? CurrentTour { 
            get { return _currentTour; } 
            set {
                // load tour from toursmanager
                _currentTour = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TourSelected));

                if (_currentTour == null)
                    return;
                

                TourLogsVM = new TourLogsViewModel(toursmanager, _currentTour);
 
                OnPropertyChanged(nameof(TourLogsVM));
            } 
        }

        private Tour? _currentTour;

        private bool _isBusy;
        public bool IsBusy { 
            get => _isBusy;
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
            }
        }

        public Boolean TourSelected
        {
            get { return CurrentTour != null; }
        }

        public ToursViewModel(IToursManager toursManager)
        {
            this.toursmanager = toursManager;
            toursManager.GetTours().ToList().ForEach(t => Tours.Add(t));
            toursManager.GetTourLogs();
            this.CreateTourCommand = new RelayCommand(param =>
                {
                    if (App.Current.Services.GetService(typeof(AddTourViewModel)) is AddTourViewModel addTourViewModel)
                    {
                        var addTourDialog = new AddTourDialog(addTourViewModel);
                        
                        addTourDialog.Show();

                        // listen for addTour Events
                        addTourViewModel.AddButtonClicked += async (_, tour) =>
                        {
                            addTourViewModel.IsEnabled = false;
                            try
                            {
                                tour = await toursManager.AddTour(tour);
                                Tours.Add(tour);
                            }
                            catch
                            {

                            }
                            addTourDialog?.Close();
                        };
                    }
                }
            );
            this.DeleteTourCommand = new RelayCommand(_ =>
            {
                if (TourSelected)
                {
                    var temp = CurrentTour;
                    CurrentTour = null;
                    toursManager.DeleteTour(temp);
                    Tours.Remove(Tours.Where(i => i.Id == temp.Id).Single());
                }
            });

            this.EditTourCommand = new RelayCommand(param =>
            {
                if (CurrentTour == null)
                    return;

                var editTourVM = new EditTourViewModel(CurrentTour);

                var editTourDialog = new EditTourDialog(editTourVM);
                editTourDialog.Show();

                editTourVM.EditButtonClicked += (_, tour) =>
                {
                    toursManager.EditTour(tour);

                    // Replace tour in Tour List with the changed tour
                    // find tour that has the same Id
                    var tourInList = Tours.Single(t => t.Id == tour.Id);
                    if (tourInList == null)
                        return;

                    int index = Tours.IndexOf(tourInList);
                    if (index != -1)
                    {
                        Tours.RemoveAt(index);
                        Tours.Insert(index, tour);
                    }

                    CurrentTour = tour;


                    editTourDialog?.Close();
                };
            });
            this.ExportReportCommand = new RelayCommand(_ =>
            {
                Report report = new Report();
                report.CreateReport(Tours);
            });
            this.ExportSingleReportCommand = new RelayCommand(_ =>
            {
                if (CurrentTour == null)
                    return;

                Report report = new Report();
                report.CreateReport(CurrentTour);
            });
            this.ExportDataCommand = new RelayCommand(_ =>
            {
                toursManager.ExportData();
            });
        }
    }
}
