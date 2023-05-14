using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.Models
{
    public enum TransportType
    {
        AUTO,
        WALKING,
        BICYCLE
    }

    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }
        public double Distance { get; set; }
        public double Time { get; set; }
        public string Image { get; set; } // path to image
        public List<TourLog> TourLogs { get; } = new();


        public Tour(string Name)
        {
            this.Name = Name;
            this.Description = string.Empty;
            this.From = string.Empty;
            this.To = string.Empty;
            this.TransportType = string.Empty;
            this.Distance = 0;
            this.Time = 0;
            this.Image = string.Empty;
     
        }
    }
}