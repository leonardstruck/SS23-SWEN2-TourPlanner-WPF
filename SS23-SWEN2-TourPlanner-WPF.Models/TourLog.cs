using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
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
        public string Comment { get; set; } = string.Empty;
        public Difficulty Difficulty { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int Rating { get; set; }

        public TourLog()
        {
            Id = -1;
        }

        public override string ToString()
        {
            return $"{Id}%%%{DateTime}%%%{Comment}%%%{Difficulty}%%%{TotalTime}%%%{Rating}";
        }

        public void ToTourLog(string tourLogString)
        {
            try
            {
                TourLog temp = new TourLog();
                PropertyInfo[] logProperties = typeof(TourLog).GetProperties();
                string[] logData = tourLogString.Split(new string[] { "%%%" }, StringSplitOptions.None);
                for (int k = 0; k < logData.Length - 1; k++)
                {
                    PropertyInfo logProperty = logProperties[k];
                    var converter = TypeDescriptor.GetConverter(logProperty.PropertyType);
                    var convertedObject = converter.ConvertFromString(logData[k]);
                    logProperty.SetValue(temp, convertedObject);
                }

                // in case an invalide string gets passed
                this.Id = temp.Id;
                this.DateTime = temp.DateTime;
                this.Comment = temp.Comment;
                this.Difficulty = temp.Difficulty;
                this.TotalTime = temp.TotalTime;
                this.Rating = temp.Rating;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    
}
