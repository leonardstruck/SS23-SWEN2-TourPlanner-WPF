using SS23_SWEN2_TourPlanner_WPF.DAL;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.BL
{
    public class ToursManagerImpl : IToursManager
    {
        private readonly IDataManager _dataManager;

        public ToursManagerImpl(IDataManager dataManager) {  _dataManager = dataManager; }

        public void AddTour(Tour t)
        {
            _dataManager.AddTour(t);
        }

        public void EditTour(Tour t)
        {
            _dataManager.EditTour(t);
        }

        public IEnumerable<Tour> GetTours()
        {
            return _dataManager.GetTours();
        }

        public void AddTourLog(Tour tour, TourLog tourLog)
        {
            _dataManager.AddTourLog(tour, tourLog);
        }

        public void DeleteTour(Tour tour)
        {
            _dataManager.DeleteTour(tour);
        }
    }
}
