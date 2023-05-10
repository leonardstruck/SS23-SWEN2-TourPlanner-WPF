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
                _context.Tours.Add(new Tour("Tour 1"));
                _context.Tours.Add(new Tour("Tour 2"));
                _context.SaveChanges();
            }

        }

        public void AddTour(Tour t)
        {
            _context.Tours.Add(t);
            _context.SaveChanges();
        }

        public IEnumerable<Tour> GetTours()
        {
            _context.Tours.Load();
            return _context.Tours;
        }
    }
}
