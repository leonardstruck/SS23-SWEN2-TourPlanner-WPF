using Microsoft.EntityFrameworkCore;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.DAL
{
    public class DataManagerEFImpl : IDataManager
    {
        private readonly TourDbContext _context;

        public DataManagerEFImpl()
        {
            _context = new TourDbContext();

            bool recreate = true;
            if (recreate)
                _context.Database.EnsureDeleted();

            _context.Database.EnsureCreated();

            if (recreate)
            {
                var Tour1 = new Tour("Tour 1")
                {
                    Description = "This is the description for Tour 1",
                    From = "Corneliusgasse 4, 1060 Wien",
                    To = "Kaunitzgasse 11, 1060 Wien",
                };
                Tour1.TourLogs.Add(new TourLog
                {
                    Comment = "This is a comment",
                    DateTime = DateTime.Now,
                    Difficulty = Difficulty.Easy,
                    Rating = 5,
                    TotalTime = TimeSpan.FromMinutes(5),
                });
                _context.Tours.Add(Tour1);
                _context.Tours.Add(new Tour("Tour 2"));
                _context.SaveChanges();
            }

        }

        public void AddTour(Tour t)
        {
            _context.Tours.Add(t);
            _context.SaveChanges();
        }

        public void AddTourLog(Tour tour, TourLog tourLog)
        {
            var tourFromDB = _context.Tours.Include(t => t.TourLogs).Single(t => t.Id == tour.Id);
            tourFromDB.TourLogs.Add(tourLog);
            _context.SaveChanges();
        }

        public void DeleteTour(Tour tour)
        {
            _context.Tours.Remove(tour);
            _context.SaveChanges();
        }

        public IEnumerable<Tour> GetTours()
        {
            _context.Tours.Load();
            return _context.Tours;
        }
    }
}
