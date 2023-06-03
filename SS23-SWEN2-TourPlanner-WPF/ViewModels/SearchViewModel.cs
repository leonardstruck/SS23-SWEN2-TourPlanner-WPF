using SS23_SWEN2_TourPlanner_WPF.BL;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SS23_SWEN2_TourPlanner_WPF.ViewModels
{
    internal class SearchViewModel : BaseViewModel
    {
        private readonly ToursViewModel _toursViewModel;
        private readonly IToursManager _tourManager;
        private readonly Search search;

        private readonly BackgroundWorker _searchWorker;
        public ICommand SearchButtonClick { get; set; }
        public ICommand SearchClearClick { get; set; }

        private string _searchTerm = string.Empty;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set { _searchTerm = value; OnPropertyChanged(SearchTerm); }
        }
        
        private bool _isSearching;
        public bool IsSearching
        {
            get { return _isSearching; }
            set { 
                _isSearching = value;
                OnPropertyChanged(nameof(IsSearching));
                OnPropertyChanged(nameof(UIElementIsEnabled));
            } 
        }

        public bool UIElementIsEnabled
        {
            get { return !_searchWorker.IsBusy; }
        }


        public SearchViewModel(ToursViewModel toursViewModel, IToursManager tourManager)
        {
            _toursViewModel = toursViewModel;
            _searchWorker = new BackgroundWorker();
            _searchWorker.DoWork += SearchWorker_DoWork!;
            _searchWorker.RunWorkerCompleted += SearchWorker_WorkCompleted!;
            search = new Search();
            IsSearching = false;

            SearchButtonClick = new RelayCommand(_ =>
            {
                if (!_searchWorker.IsBusy)
                {
                    _searchWorker.RunWorkerAsync();
                    OnPropertyChanged(nameof(UIElementIsEnabled));
                }
            });

            SearchClearClick = new RelayCommand(_ =>
            {
                if (_searchWorker.IsBusy)
                {
                    _searchWorker.CancelAsync();
                }

                // searching with an empty searchterm shares the behaviour of clearing the results
                SearchTerm = string.Empty;

                _searchWorker.RunWorkerAsync();
                OnPropertyChanged(nameof(UIElementIsEnabled));

                IsSearching = false;
            });
            _tourManager = tourManager;
        }

        private void SearchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (sender is not BackgroundWorker worker)
                return;

            IsSearching = true;

            e.Result = search.SearchTours(_tourManager.GetTours(), SearchTerm);
        }

        private void SearchWorker_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnPropertyChanged(nameof(UIElementIsEnabled));
            _toursViewModel.Tours.Clear();

            if (e.Result is not List<Tour> tours)
                return;

            if (tours.Count == 0)
                return;

            foreach (var tour in tours)
            {
                _toursViewModel.Tours.Add(tour);
            }

            // select first tour as current tour
            _toursViewModel.CurrentTour = _toursViewModel.Tours.First();
        }


    }
}
