﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        public List<string> Maneuvers { get; set; } = new();

        public Tour(string Name)
        {
            //this.Id = -1;
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

        public bool IsChildfriendly()
        {
            // man merke an dass ich keine Ahnung habe was Kinder können...

            if (TransportType == TourTransportType.Auto && TimeSpan.Compare(Time, TimeSpan.FromHours(2)) == 1)
                return false;

            if (TransportType == TourTransportType.Bicycle && TimeSpan.Compare(Time, TimeSpan.FromHours(1)) == 1)
                return false;

            if (TransportType == TourTransportType.Walking && TimeSpan.Compare(Time, TimeSpan.FromMinutes(30)) == 1)
                return false;

            if (GetAverageDifficulty() != Difficulty.Easy)
                return false;

            return true;
        }

        public TimeSpan GetAverageTime()
        {
            if (TourLogs.Count == 0)
                return new TimeSpan();

            var totalTime = new TimeSpan();

            foreach (TourLog tourLog in TourLogs)
            {
                totalTime = totalTime.Add(tourLog.TotalTime);
            }

            return totalTime.Divide(TourLogs.Count);
        }

        public double GetAverageRating()
        {
            if (TourLogs.Count == 0)
                return 0;

            var totalRating = 0;

            foreach (TourLog tourLog in TourLogs)
            {
                totalRating += tourLog.Rating;
            }
            return totalRating / TourLogs.Count;
        }

        public Difficulty GetAverageDifficulty()
        {
            if (TourLogs.Count == 0)
                return 0;

            var totalDifficulty = 0;

            foreach (TourLog tourLog in TourLogs)
            {
                totalDifficulty += (int)tourLog.Difficulty;
            }

            return (Difficulty)(totalDifficulty / TourLogs.Count);
        }
      
        public void ToTour(string line)
        {
            
            try
            {
                Tour tour = new Tour("");
                PropertyInfo[] properties = typeof(Tour).GetProperties();
                List<string> data = SplitIgnoringQuotes(line, ',');
                for (int j = 0; j < data.Count; j++)
                {
                    PropertyInfo property = properties[j];
                    string value = data[j];
                    if (property.PropertyType == typeof(double))
                    {

                        property.SetValue(tour, double.Parse(value, CultureInfo.InvariantCulture));
                    }
                    else if (property.PropertyType == typeof(List<string>))
                    {
                        continue;
                    }
                    else if (property.PropertyType == typeof(List<TourLog>))
                    {
                        // Deserialize the list from the comma-separated string
                        string[] listData = value.Split(new string[] { "&&&" }, StringSplitOptions.None);
                        foreach (string item in listData)
                        {
                            // item ist der String mit allen werten von einem log
                            TourLog tl = new TourLog();
                            tl.ToTourLog(item);
                            
                            if (tl.Id != -1)
                            {
                                tl.Id = 0;
                                tour.TourLogs.Add(tl);
                            }
                        }
                    }
                    else if (property.PropertyType == typeof(BitmapImage))
                    {
                        continue;
                    }
                    else
                    {
                        // value muss noch richtig geparst werden
                        var converter = TypeDescriptor.GetConverter(property.PropertyType);
                        var convertedObject = converter.ConvertFromString(value);
                        property.SetValue(tour, convertedObject);
                    }
                }

                this.Id = tour.Id;
                this.Name = tour.Name;
                this.Description = tour.Description;
                this.From = tour.From;
                this.To = tour.To;
                this.TransportType = tour.TransportType;
                this.Distance = tour.Distance;
                this.Time = tour.Time;
                this.Image = tour.Image;
                this.TourLogs = tour.TourLogs;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        public static List<string> SplitIgnoringQuotes(string input, char delimiter)
        {
            List<string> parts = new List<string>();
            StringBuilder currentPart = new StringBuilder();
            bool insideQuotes = false;

            foreach (char c in input)
            {
                if (c == delimiter && !insideQuotes)
                {
                    parts.Add(currentPart.ToString());
                    currentPart.Clear();
                }
                else if (c == '"')
                {
                    insideQuotes = !insideQuotes;
                }
                else
                {
                    currentPart.Append(c);
                }
            }

            parts.Add(currentPart.ToString());

            return parts;
        }
    }
}