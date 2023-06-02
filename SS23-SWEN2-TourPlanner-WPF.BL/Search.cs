using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.BL
{
    public class Search
    {
        public bool FilterTourLogs { get; set; } = false;
        
        public List<Tour> SearchTours(IEnumerable<Tour> tours, string searchTerm)
        {
            // First normalize searchTerm
            searchTerm = searchTerm.Trim().ToLower();

            // if the searchTerm is empty now, return the entire List
            if (string.IsNullOrEmpty(searchTerm)) return tours.ToList();

            return tours.Where(tour =>
            {
                // First search inside TourLogs
                var matchingTourLogs = SearchTourLogs(tour.TourLogs, searchTerm);

                // if there's a match inside the tourlogs, the tour should be considered a match
                if (matchingTourLogs.Count > 0)
                {
                    // if FilterTourLogs is set to true, replace the tourlogs with the matchingTourLogs
                    if (FilterTourLogs)
                    {
                        tour.TourLogs = matchingTourLogs;
                    }
                    return true;
                }

                // Check if there's a match for any Tour member variable
                if (tour.Name.ToLower().Contains(searchTerm)) return true;
                if (tour.Description.ToLower().Contains(searchTerm)) return true;
                if (tour.From.ToLower().Contains(searchTerm)) return true;
                if (tour.To.ToLower().Contains(searchTerm)) return true;

                return false;
            }).ToList();
        }

        private List<TourLog> SearchTourLogs(IEnumerable<TourLog> tourLogs, string searchTerm)
        {
            return tourLogs.Where(tourLog =>
            {
                if (tourLog.Comment.ToLower().Contains(searchTerm)) return true;
                if (tourLog.Difficulty.ToString().ToLower().Contains(searchTerm)) return true;

                return false;
            }).ToList();
        }

    }
}
