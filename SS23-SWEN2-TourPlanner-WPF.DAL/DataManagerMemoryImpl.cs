using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.DAL
{
    public class DataManagerMemoryImpl : IDataManager
    {
        private readonly List<Tour> tours = new();
        private readonly Dictionary<int, List<TourLog>> tourLogs = new();

        public Tour AddTour(Tour t)
        {
            t.Id = tours.Count;
            tours.Add(t);
            return t;
        }

        public void AddTourLog(Tour tour, TourLog tourLog)
        {
            if(tourLogs.ContainsKey(tour.Id))
            {
                tourLogs[tour.Id].Add(tourLog);
            } else
            {
                tourLogs.Add(tour.Id, new List<TourLog> { tourLog });
            }
        }

        public void DeleteTour(Tour tour)
        {
            tours.Remove(tour);
            tourLogs.Remove(tour.Id);
        }

        public void DeleteTourLog(Tour tour, TourLog tourLog)
        {
            if(tourLogs.ContainsKey(tour.Id))
            {
                tourLogs[tour.Id].Remove(tourLog);
            }
        }

        public void EditTour(Tour t)
        {
            var tourInList = tours.Find(tour => t.Id == tour.Id);

            if (tourInList == null)
                return;
            
            // get index of tour
            var index = tours.IndexOf(tourInList);
            if (index == -1) return;

            tours.RemoveAt(index);
            tours.Insert(index, t);
        }

        public void EditTourLog(TourLog tourLog)
        {
            foreach (var tourLogList in tourLogs.Values)
            {
                foreach (var logToUpdate in tourLogList.Where(log => log.Id == tourLog.Id))
                {
                    logToUpdate.DateTime = tourLog.DateTime;
                    logToUpdate.TotalTime = tourLog.TotalTime;
                    logToUpdate.Rating = tourLog.Rating;
                    logToUpdate.Difficulty = tourLog.Difficulty;
                    logToUpdate.Comment = tourLog.Comment;
                }
            }
        }

        public IEnumerable<Tour> GetTours()
        {
            return tours;
        }

        public IEnumerable<TourLog> GetTourLogs()
        {
            // Combine tourLogs into a single list and return it
            return tourLogs.Values.SelectMany(x => x);
        }
    }
}
