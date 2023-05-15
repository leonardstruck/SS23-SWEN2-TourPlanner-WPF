using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.Models
{
    public enum Difficulty
    {
        Easy,
        Moderate,
        Challenging
    }

    public class TourLog
    {     
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Comment { get; set; }
        public Difficulty Difficulty { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int Rating { get; set; }
    }
}
