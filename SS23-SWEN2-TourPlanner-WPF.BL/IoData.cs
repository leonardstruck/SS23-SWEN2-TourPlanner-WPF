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

namespace SS23_SWEN2_TourPlanner_WPF.BL
{
    internal class IoData
    {
        public void ExportData()
        {
            
            // Show save file dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    string filePath = saveFileDialog.FileName;

                    // get data from database
                    IDataManager dataManager = new DataManagerEFImpl();
                    List<Tour> tours = dataManager.GetTours().ToList();

                    // Get the properties of the object type T
                    PropertyInfo[] properties = typeof(Tour).GetProperties();

                    // Create a StringBuilder to store the CSV data
                    StringBuilder csvData = new StringBuilder();

                    // Append the header row
                    string headerRow = string.Join(",", properties.Select(prop => prop.Name));
                    csvData.AppendLine(headerRow);

                    // Append the data rows
                    foreach (Tour data in tours)
                    {
                        string dataRow = string.Join(",", properties.Select(p => ConvertValueToString(p.GetValue(data))));
                        csvData.AppendLine(dataRow);
                    }

                    // Write the CSV data to the file
                    File.WriteAllText(filePath, csvData.ToString());


                    MessageBox.Show("Data export successful.");
                }
                catch (Exception e)
                {
                    MessageBox.Show("Data export failed.");
                }
                finally
                {

                }
            }
                
        }

        public List<Tour> ImportData()
        {
            // ToDo implement

            return new List<Tour>();
        }

        private string ConvertValueToString(object value)
        {
            if (value is double doubleValue)
            {
                return doubleValue.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                return value?.ToString();
            }
        }
    }
}
