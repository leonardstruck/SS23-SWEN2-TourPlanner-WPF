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
    public class AddTourViewModel : BaseViewModel
    {
        private Boolean _isBusy;
        public Boolean IsEnabled
        {
            get => !_isBusy;
            set { 
                _isBusy = !value;
                OnPropertyChanged();
            }
        }

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

        public AddTourViewModel() {
            ExecuteCommandAdd = new RelayCommand(param =>
            {
                var tour = new Tour(_name)
                {
                    Description = _description,
                    From = _from,
                    To = _to,
                    TransportType = _transportType
                };

                this.AddButtonClicked?.Invoke(this, tour);
            });
        }

        public ICommand ExecuteCommandAdd { get; }

        public event EventHandler<Tour> AddButtonClicked;

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
