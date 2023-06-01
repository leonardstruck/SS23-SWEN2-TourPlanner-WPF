using SS23_SWEN2_TourPlanner.DAL;
using SS23_SWEN2_TourPlanner_WPF.DAL;
using SS23_SWEN2_TourPlanner_WPF.Log4Net;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ToursManagerImpl(IDataManager dataManager) { _dataManager = dataManager; }

        public async Task<Tour> AddTour(Tour t)
        {
            logger.Debug($"Adding Tour: {t.Name}");
            logger.Debug($"Requesting Map for {t.Name} from {t.From} to {t.To}");
            var map = new Map(t);
            logger.Debug($"Storing Tour {t.Name}");
            Tour temp = await map.CreateMap(); // null wenn von der API nichts zurück kommt
            t.Image = temp.Image;
            t.Distance = temp.Distance;
            t.Time = temp.Time;
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
        public IEnumerable<TourLog> GetTourLogs()
        {
            return _dataManager.GetTourLogs();
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

        public void EditTourLog(TourLog currentTourLog)
        {
            _dataManager.EditTourLog(currentTourLog);
        }

        public void ExportData(ObservableCollection<Tour> tours)
        {
            var io = new IoData();
            io.ExportData(tours);
        }


        public IEnumerable<Tour> ImportData()
        {
            var io = new IoData();
            return io.ImportData();
        }
    }
}