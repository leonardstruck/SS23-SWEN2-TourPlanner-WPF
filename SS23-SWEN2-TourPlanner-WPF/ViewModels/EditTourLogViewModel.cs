using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SS23_SWEN2_TourPlanner_WPF.ViewModels
{
    public class EditTourLogViewModel : BaseViewModel
    {
        private DateTime _dateTime = DateTime.Now;
        private string _comment = "";
        private Difficulty _difficulty;
        private TimeSpan _totalTime;
        private int _rating;

        public ICommand ExecuteCommandEdit { get; }
        public event EventHandler<TourLog> EditButtonClicked;

        public EditTourLogViewModel(TourLog tourLog)
        {
            _dateTime = tourLog.DateTime;
            _comment = tourLog.Comment;
            _difficulty = tourLog.Difficulty;
            _totalTime = tourLog.TotalTime;
            _rating = tourLog.Rating;

            ExecuteCommandEdit = new RelayCommand(param =>
            {
                tourLog.DateTime = _dateTime;
                tourLog.Comment = _comment;
                tourLog.Difficulty = _difficulty;
                tourLog.TotalTime = _totalTime;
                tourLog.Rating = _rating;

                this.EditButtonClicked?.Invoke(this, tourLog);
            });
        }

        public DateTime DateTime
        {
            get { return _dateTime; }
            set 
            { 
                _dateTime = value;
                OnPropertyChanged();
            }
        }

        public string Comment
        { 
            get { return _comment; } 
            set
            {
                _comment = value;
                OnPropertyChanged();
            } 
        }

        public Difficulty Difficulty
        {
            get { return _difficulty; }
            set
            {
                _difficulty = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan TimeSpan
        {
            get { return _totalTime; }
            set
            {
                _totalTime = value;
                OnPropertyChanged();
            }
        }

        public int Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                OnPropertyChanged();
            }
        }
    }
}
