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
    internal class IoData
    {
        public void ExportData(ObservableCollection<Tour> tours)
        {
            
            // Show save file dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    string filePath = saveFileDialog.FileName;

                    // Get data from db
                    IDataManager dataManager = new DataManagerEFImpl();

                    PropertyInfo[] properties = typeof(Tour).GetProperties();
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
                //Get the path of specified file
                string filePath = openFileDialog.FileName;
                //Read the contents of the file into a stream
                var fileStream = openFileDialog.OpenFile();

                using (StreamReader reader = new StreamReader(fileStream))
                {
                    string[] lines = File.ReadAllLines(filePath);

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
                        string[] data = lines[i].Split(',');
                        Tour tour = new Tour("");
                        for (int j = 0; j < data.Length; j++)
                        {
                            PropertyInfo property = properties[j];
                            string value = data[j];
                            if (property.PropertyType == typeof(double))
                            {
                                
                                property.SetValue(tour, double.Parse(value, CultureInfo.InvariantCulture));
                            }
                            else if(property.PropertyType == typeof(List<TourLog>))
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
                                        tour.TourLogs.Add(tl);
                                    }
                                }
                            }
                            else if(property.PropertyType == typeof(BitmapImage))
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
                        tours.Add(tour);
                    }
                    return tours;

                }
            }

            return new List<Tour>();
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
                foreach (object item in listValue)
                {
                    serializedList += item.ToString() + "&&&";
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
