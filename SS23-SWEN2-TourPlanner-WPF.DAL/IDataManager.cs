using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.DAL
{
    public interface IDataManager
    {
        Tour AddTour(Tour t);

        void EditTour(Tour t);

        void EditTourLog(TourLog tourLog);

        void AddTourLog(Tour tour, TourLog tourLog);
        void DeleteTour(Tour tour);
        void DeleteTourLog(Tour tour, TourLog tourLog);
        IEnumerable<Tour> GetTours();
        IEnumerable<TourLog> GetTourLogs();
    }
}
