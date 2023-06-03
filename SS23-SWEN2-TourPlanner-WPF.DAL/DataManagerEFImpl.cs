using Microsoft.EntityFrameworkCore;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SS23_SWEN2_TourPlanner_WPF.DAL
{
    public class DataManagerEFImpl : IDataManager
    {
        private readonly TourDbContext _context;

        public DataManagerEFImpl()
        {
            try
            {
                _context = new TourDbContext();

                bool recreate = bool.Parse(ConfigurationManager.AppSettings["ResetDatabase"]);
                if (recreate)
                    _context.Database.EnsureDeleted();

                _context.Database.EnsureCreated();

                if (recreate)
                {
                    var Tour1 = new Tour("From Vienna to Berlin")
                    {
                        Description = "This is the description for Tour 1",
                        From = "Vienna",
                        To = "Berlin",
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
                    _context.SaveChanges();
                }
            } catch (Exception e)
            { 
                MessageBox.Show($"Something went wrong: {e.Message}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // Quit Application
                Environment.Exit(1);
            }
        }

        public Tour AddTour(Tour t)
        {
            
            var tour = _context.Tours.Add(t);
            _context.SaveChanges();

            return tour.Entity;
        }

        public void EditTour(Tour t)
        {
            _context.Tours.Update(t);
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

        public void DeleteTourLog(Tour tour, TourLog tourLog)
        {
            var tourFromDB = _context.Tours.Include(t => t.TourLogs).Single(t => t.Id == tour.Id);
            var tourLogFromDB = tourFromDB.TourLogs.SingleOrDefault(tl => tl.Id == tourLog.Id);
            if (tourLogFromDB != null)
            {
                _context.TourLogs.Remove(tourLogFromDB);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Tour> GetTours()
        {
            _context.Tours.Load();
            return _context.Tours;
        }

        public Tour? GetTour(int id)
        {
            // AsNoTracking is required here, since we want to compare a Tour with the state of an actual Tour in the Database
            return _context.Tours.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TourLog> GetTourLogs()
        {
            _context.TourLogs.Load();
            return _context.TourLogs;
        }

        public void EditTourLog(TourLog tourLog)
        {
            _context.TourLogs.Update(tourLog);
            _context.SaveChanges();
        }
    }
}
