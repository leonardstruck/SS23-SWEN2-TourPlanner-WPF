using SS23_SWEN2_TourPlanner.DAL;
using SS23_SWEN2_TourPlanner_WPF.DAL;
using SS23_SWEN2_TourPlanner_WPF.Log4Net;
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
        private static readonly ILoggerWrapper logger = LoggerFactory.GetLogger(typeof(ToursManagerImpl).ToString());
        private readonly IDataManager _dataManager;

        public ToursManagerImpl(IDataManager dataManager) {  _dataManager = dataManager; }

        public async Task<Tour> AddTour(Tour t)
        {
            logger.Debug($"Adding Tour: {t.Name}");
            logger.Debug($"Requesting Map for {t.Name} from {t.From} to {t.To}");
            var map = new Map(t);
            t.Image = await map.CreateMap();
            logger.Debug($"Storing Tour {t.Name}");
            _dataManager.AddTourAsync(t);
            return t;
        }

        public void EditTour(Tour t)
        {
            logger.Debug($"Edit Tour: {t.Id}");
            _dataManager.EditTour(t);
        }

        public IEnumerable<Tour> GetTours()
        {
            return _dataManager.GetTours();
        }

        public void AddTourLog(Tour tour, TourLog tourLog)
        {
            logger.Debug($"Add TourLog to {tour.Id}");
            _dataManager.AddTourLog(tour, tourLog);
        }

        public void DeleteTour(Tour tour)
        {
            logger.Debug($"Delete Tour {tour.Id}");
            // delete images
            if (File.Exists(tour.Image))
            {
                logger.Debug($"Deleted existing Map Image for {tour.Id}");
                File.Delete(tour.Image);
            }
            _dataManager.DeleteTour(tour);
        }

        public void DeleteTourLog(Tour tour, TourLog tourLog)
        {
            logger.Debug($"Delete TourLog {tourLog.Id} from {tour.Id}");
            _dataManager.DeleteTourLog(tour, tourLog);
        }
    }
}