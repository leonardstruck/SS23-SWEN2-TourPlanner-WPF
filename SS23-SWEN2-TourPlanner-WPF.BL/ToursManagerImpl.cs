using MapQuest;
using SS23_SWEN2_TourPlanner_WPF.DAL;
using SS23_SWEN2_TourPlanner_WPF.Log4Net;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapQuest.DirectionsAPI;

namespace SS23_SWEN2_TourPlanner_WPF.BL
{
    public class ToursManagerImpl : IToursManager
    {
        private static readonly ILoggerWrapper logger = LoggerFactory.GetLogger(typeof(ToursManagerImpl).ToString());
        private readonly IDataManager _dataManager;
        private readonly MapQuestAPI mapQuestAPI;

        public event EventHandler<Tour>? TourChanged;
        public event EventHandler<Tour>? TourAdded;

        public ToursManagerImpl(IDataManager dataManager) {  
            _dataManager = dataManager; 
            var apiKey = ConfigurationManager.ConnectionStrings["MapQuestApiKey"].ConnectionString ?? throw new Exception("MapQuestApiKey not present in Config");
            mapQuestAPI = new MapQuestAPI(apiKey);
        }

        public async Task AddTour(Tour t)
        {
            logger.Debug($"Adding Tour: {t.Name}");
            var addedTour = _dataManager.AddTour(t);
            TourAdded?.Invoke(this, addedTour);

            // Handle API Calls in the background
            await Task.Run(() => HandleAPICalls(addedTour));
        }

        public async Task HandleAPICalls(Tour tour)
        {
            var routeType = tour.TransportType switch
            {
                TourTransportType.Auto => Directions.RouteType.Fastest,
                TourTransportType.Bicycle => Directions.RouteType.Bicycle,
                TourTransportType.Walking => Directions.RouteType.Pedestrian,
                _ => throw new Exception("Failed to determine RouteType"),
            };

            var route = await mapQuestAPI.Directions.GetRoute(new Directions.GetRouteReq { From = tour.From, To = tour.To, RouteType = routeType});
      
            tour.Distance = route.Distance;
            tour.Time = route.Time;

            var map = await mapQuestAPI.StaticMap.GetMap(new MapQuest.StaticMapAPI.GetMapReq { BoundingBox = route.BoundingBox, Height = 600, Width = 800, SessionId = route.SessionId });

            var path = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TourPlanner", "Maps");
            // Ensure that directory for Maps is created
            Directory.CreateDirectory(path);

            tour.Image = Path.Join(path, $"{Guid.NewGuid()}.png");

            // write map to disk
            await map.WriteToFile(tour.Image);

            // update tour data
            EditTour(tour);
        }

        public void EditTour(Tour t)
        {
            logger.Debug($"Edit Tour: {t.Id}");
            _dataManager.EditTour(t);

            TourChanged?.Invoke(this, t);
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

        public void EditTourLog(TourLog currentTourLog)
        {
            _dataManager.EditTourLog(currentTourLog);
        }

        public IEnumerable<TourLog> GetTourLogs()
        {
            return _dataManager.GetTourLogs();
        }
    }
}