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
        void AddTour(Tour t);

        void AddTourLog(Tour tour, TourLog tourLog);
        void DeleteTour(Tour tour);
        IEnumerable<Tour> GetTours();
    }
}
