using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SS23_SWEN2_TourPlanner_WPF.Models
{
    public enum TourTransportType
    {
        Walking,
        Bicycle,
        Auto
    }

    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public TourTransportType TransportType { get; set; }
        public double Distance { get; set; }
        public TimeSpan Time { get; set; }
        public string Image { get; set; } // path to image
        public List<TourLog> TourLogs { get; set; } = new();

        public Tour(string Name)
        {
            this.Name = Name;
            this.Description = string.Empty;
            this.From = string.Empty;
            this.To = string.Empty;
            this.TransportType = TourTransportType.Walking;
            this.Distance = 0;
            this.Time = new TimeSpan(0);
            this.Image = string.Empty;
            this.TourLogs = new List<TourLog>();
     
        }

        public BitmapImage ImageSource { get
            {
                if(Image == null || Image == "")
                {
                    return new BitmapImage();
                }
                if(File.Exists(Image))
                {
                    var bi = new BitmapImage();
                    bi.BeginInit();
                    bi.CacheOption = BitmapCacheOption.OnLoad;
                    bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
                    bi.UriSource = new Uri(Image);
                    bi.EndInit();
                    return bi;
                }
                return new BitmapImage();
            } 
        }
    }
}