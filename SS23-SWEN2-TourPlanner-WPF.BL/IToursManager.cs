using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.BL
{
    public interface IToursManager
    {
        event EventHandler<Tour>? TourChanged;
        event EventHandler<Tour>? TourAdded;
        event EventHandler<Tour>? TourRemoved;

        event EventHandler<TourError>? TourError;
        void EditTour(Tour t);
        Task AddTour(Tour t);

        void AddTourLog(Tour tour, TourLog tourLog);

        void EditTourLog(TourLog currentTourLog);
        void DeleteTour(Tour tour);
        void DeleteTourLog(Tour tour, TourLog currentTourLog);
        IEnumerable<Tour> GetTours();
        IEnumerable<TourLog> GetTourLogs();
    }

    public struct TourError
    {
        public Tour Tour;
        public Exception? Exception;
    }
}
