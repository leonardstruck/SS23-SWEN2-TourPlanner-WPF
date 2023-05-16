using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SS23_SWEN2_TourPlanner_WPF.ViewModels
{
    public class AddTourLogViewModel: BaseViewModel
    {
        private DateTime _dateTime = DateTime.Now;
        private string _comment = "";
        private Difficulty _difficulty;
        private TimeSpan _totalTime;
        private int _rating;

        public ICommand ExecuteCommandAdd { get; }
        public event EventHandler<TourLog> AddButtonClicked;

        public AddTourLogViewModel()
        {
            ExecuteCommandAdd = new RelayCommand(param =>
            {
                var tourLog = new TourLog()
                {
                    Comment = _comment,
                    Difficulty = _difficulty,
                    DateTime = _dateTime,
                    Rating = _rating,
                    TotalTime = _totalTime
                };

                this.AddButtonClicked?.Invoke(this, tourLog); 
            });
        }

        public DateTime DateTime { 
            get { return _dateTime; } 
            set { 
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
            set { 
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
