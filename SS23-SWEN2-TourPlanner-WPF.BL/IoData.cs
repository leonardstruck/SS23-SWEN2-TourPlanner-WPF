using iText.Layout;
using log4net.Repository.Hierarchy;
using Microsoft.Win32;
using SS23_SWEN2_TourPlanner_WPF.DAL;
using SS23_SWEN2_TourPlanner_WPF.Log4Net;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Globalization;
using System.Windows.Navigation;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace SS23_SWEN2_TourPlanner_WPF.BL
{
    public class IoData
    {
        private static readonly ILoggerWrapper logger = LoggerFactory.GetLogger(typeof(IoData).ToString());

        public void ExportData(IEnumerable<Tour> tours, string fileName)
        {
            
            
            try
            {
                PropertyInfo[] properties = typeof(Tour).GetProperties();
                StringBuilder csvData = new StringBuilder();

                // Append the header row
                string headerRow = string.Join(",", properties.Select(prop => EscapeValue(prop.Name)));
                csvData.AppendLine(headerRow);

                // Append the data rows
                foreach (Tour data in tours)
                {
                    string dataRow = string.Join(",", properties.Select(p => EscapeValue(ConvertValueToString(p.GetValue(data)))));
                    csvData.AppendLine(dataRow);
                }

                File.WriteAllText(fileName, csvData.ToString());
            }
            catch (Exception e)
            {
                logger.Error($"Failed to export data: {e.Message}");
                throw;
            }
            
        }

        // Helper method to escape a value if it contains a comma
        string EscapeValue(string value)
        {
            if (value.Contains(","))
            {
                // Enclose the value in quotes and double any existing quotes
                value = "\"" + value.Replace("\"", "\"\"") + "\"";
            }
            return value;
        }

        public IEnumerable<Tour> ImportData(string fileName)
        {
            using (FileStream fs = File.OpenRead(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);

                // [0] = header row
                // [1..n] = data rows
                if (lines.Length < 2)
                    return new List<Tour>();

                // check if header row is valid
                PropertyInfo[] properties = typeof(Tour).GetProperties();
                string headerRow = string.Join(",", properties.Select(prop => prop.Name));
                if (lines[0] != headerRow)
                    return new List<Tour>();

                List<Tour> tours = new List<Tour>();
                for (int i = 1; i < lines.Length; i++)
                {
                    Tour tour = new Tour("");
                    tour.ToTour(lines[i]);
                    if (tour.Id != -1)
                        tour.Id = 0;
                        tours.Add(tour);
                }
                return tours;
            }
            

        }

        private string ConvertValueToString(object value)
        {
            if (value is double doubleValue)
            {
                return doubleValue.ToString(CultureInfo.InvariantCulture);
            }
            else if (value is IList listValue)
            {
                // Serialize the list into a comma-separated string
                string serializedList = string.Empty;
                int itemCount = listValue.Count;
                int currentIndex = 0;

                foreach (object item in listValue)
                {
                    serializedList += item.ToString();

                    if (currentIndex != itemCount - 1)
                    {
                        serializedList += "&&&";
                    }

                    currentIndex++;
                }
                return serializedList;
            }
            else
            {
                return value?.ToString();
            }
        }
    }
}
