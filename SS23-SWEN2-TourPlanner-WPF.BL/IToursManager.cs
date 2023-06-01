using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.BL
{
    public interface IToursManager
    {
        void EditTour(Tour t);
        Task<Tour> AddTour(Tour t);

        void AddTourLog(Tour tour, TourLog tourLog);

        void EditTourLog(TourLog currentTourLog);
        void DeleteTour(Tour tour);
        void DeleteTourLog(Tour tour, TourLog currentTourLog);
        IEnumerable<Tour> GetTours();
        IEnumerable<TourLog> GetTourLogs();
        IEnumerable<Tour> ImportData();
        void ExportData(ObservableCollection<Tour> tours);
    }
}
