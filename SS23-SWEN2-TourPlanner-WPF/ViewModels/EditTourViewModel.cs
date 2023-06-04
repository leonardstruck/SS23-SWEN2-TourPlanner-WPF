using SS23_SWEN2_TourPlanner_WPF.BL;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SS23_SWEN2_TourPlanner_WPF.ViewModels
{
    public class EditTourViewModel : BaseViewModel
    {
        private String _name = "";

        public String Name
        {  
            get { return _name; } 
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private String _description = "";

        public String Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private String _from = "";

        public String From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged();
            }
        }

        private String _to = "";
        public String To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged();
            }
        }


        private TourTransportType _transportType = TourTransportType.Walking;

        public TourTransportType TransportType
        {
            get { return _transportType; }
            set
            {
                _transportType = value;
                OnPropertyChanged();
            }
        }

        public EditTourViewModel(Tour tour, IMessageBoxService messageBoxService) {
            _name = tour.Name;
            _description = tour.Description;
            _from = tour.From;
            _to = tour.To;
            _transportType = tour.TransportType;

            ExecuteCommandAdd = new RelayCommand(param =>
            {
                if (string.IsNullOrWhiteSpace(_from) || string.IsNullOrWhiteSpace(_to))
                {
                    messageBoxService.Show("Please provide both from and to addresses for your tour.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(_name))
                {
                    messageBoxService.Show("Please provide a name for your tour.");
                    return;
                }

                tour.Name = _name;
                tour.Description = _description;
                tour.From = _from;
                tour.To = _to;
                tour.TransportType = _transportType;

                this.EditButtonClicked?.Invoke(this, tour);
            });
        }

        public ICommand ExecuteCommandAdd { get; }

        public event EventHandler<Tour> EditButtonClicked;

        public void ClearFields()
        {
            Name = "";
            Description = "";
            From = "";
            To = "";
            TransportType = TourTransportType.Walking;
        }
    }
}
