using SS23_SWEN2_TourPlanner.DAL;
using SS23_SWEN2_TourPlanner_WPF.DAL;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.BL
{
    public class ToursManagerImpl : IToursManager
    {
        private readonly IDataManager _dataManager;

        public ToursManagerImpl(IDataManager dataManager) {  _dataManager = dataManager; }

        public async Task<Tour> AddTour(Tour t)
        {
            var map = new Map(t);
            Tour temp = await map.CreateMap(); // null wenn von der API nichts zurück kommt
            t.Image = temp.Image;
            t.Distance = temp.Distance;
            t.Time = temp.Time;
            _dataManager.AddTourAsync(t);

            return t;
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
            // delete images
            if (File.Exists(tour.Image))
            {
                File.Delete(tour.Image);
            }
            _dataManager.DeleteTour(tour);
        }

        public void DeleteTourLog(Tour tour, TourLog tourLog)
        {
            _dataManager.DeleteTourLog(tour, tourLog);
        }
    }
}