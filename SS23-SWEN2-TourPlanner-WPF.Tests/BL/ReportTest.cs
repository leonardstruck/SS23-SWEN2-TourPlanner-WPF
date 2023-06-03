using SS23_SWEN2_TourPlanner_WPF.BL;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.Tests.BL
{
    [TestFixture]
    public class ReportTest
    {
        private Report _report;
        private string _filename = "test_report.pdf";

        private string _sampleMap;

        private Tour _sampleTour1;
        private Tour _sampleTour2;

        private List<Tour> _sampleTours;
        
        [SetUp]
        public void Setup()
        {
            _report = new Report();

            _sampleMap = Path.Combine(Path.GetFullPath(TestContext.CurrentContext.TestDirectory), "Assets\\SampleMap.png");
            _sampleTour1 = new Tour("Sample Tour 1")
            {
                Description = "This is a Tour Description For Sample Tour 1",
                From = "Beispielgasse 1, 1000 Wien",
                To = "FH Technikum Wien, Höchstädtplatz 6, 1200 Wien",
                Distance = 3,
                TransportType = TourTransportType.Auto,
                Time = TimeSpan.FromMinutes(20),
                Image = _sampleMap,
                TourLogs = new List<TourLog> {
                    new TourLog()
                    {
                        Comment = "This is the first Tourlog",
                        DateTime = DateTime.Now,
                        Difficulty = Difficulty.Moderate,
                        Rating = 4,
                        TotalTime = TimeSpan.FromMinutes(30),
                    },
                    new TourLog()
                    {
                        Comment = "This is the second Tourlog",
                        DateTime = DateTime.Now,
                        Difficulty = Difficulty.Easy,
                        Rating = 5,
                        TotalTime = TimeSpan.FromMinutes(19),
                    }
                }
            };
            _sampleTour2 = new Tour("Sample Tour 2")
            {
                Description = "This is a Tour Description for Sample Tour 2",
                To = "Beispielgasse 1, 1000 Wien",
                From = "FH Technikum Wien, Höchstädtplatz 6, 1200 Wien",
                Distance = 3,
                TransportType = TourTransportType.Bicycle,
                Time = TimeSpan.FromMinutes(30),
                Image = _sampleMap,
                TourLogs = new List<TourLog> {
                    new TourLog()
                    {
                        Comment = "This is the first Tourlog",
                        DateTime = DateTime.Now,
                        Difficulty = Difficulty.Easy,
                        Rating = 4,
                        TotalTime = TimeSpan.FromMinutes(30),
                    },
                    new TourLog()
                    {
                        Comment = "This is the second Tourlog",
                        DateTime = DateTime.Now,
                        Difficulty = Difficulty.Challenging,
                        Rating = 5,
                        TotalTime = TimeSpan.FromMinutes(43),
                    }
                }
            };

            _sampleTours = new List<Tour>()
            {
                _sampleTour1,
                _sampleTour2
            };
        }

        [Test]
        public void CreatesPDFFromTours()
        {
            // Act
            _report.CreateReport(_sampleTours, _filename);

            // Assert
            Assert.That(File.Exists(_filename), Is.True);
        }

        [Test]
        public void CreatesPDFFromTour()
        {
            // Act
            _report.CreateReport(_sampleTour1, _filename);

            // Assert
            Assert.That(File.Exists(_filename), Is.True);
        }

        [Test, Explicit]
        public void CreateSamplePDFFromTour()
        {
            // Arrange
            var targetFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), _filename);

            // Act
            _report.CreateReport(_sampleTour1, targetFile);

            // Assert
            Assert.That(File.Exists(targetFile), Is.True);

            // Open Report
            using Process fileopener = new();

            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + targetFile + "\"";
            fileopener.Start();
        }

        [Test, Explicit]
        public void CreateSamplePDFFromTours()
        {
            // Arrange
            var targetFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), _filename);

            // Act
            _report.CreateReport(_sampleTours, targetFile);

            // Assert
            Assert.That(File.Exists(targetFile), Is.True);

            // Open Report
            using Process fileopener = new();

            fileopener.StartInfo.FileName = "explorer";
            fileopener.StartInfo.Arguments = "\"" + targetFile + "\"";
            fileopener.Start();
        }

        [TearDown]
        public void TearDown()
        {
            // Delete Created Report
            File.Delete(_filename);
        }
    }
}
