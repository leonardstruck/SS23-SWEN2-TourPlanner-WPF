using SS23_SWEN2_TourPlanner_WPF.BL;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        
        [SetUp]
        public void Setup()
        {
            _report = new Report();
        }

        [Test]
        public void CreatesPDFFromTours()
        {
            // Arrange
            var tours = new ObservableCollection<Tour>()
            {
                new Tour("Tour 1"),
                new Tour("Tour 2")
            };
            Assert.That(File.Exists(_filename), Is.False);

            // Act
            _report.CreateReport(tours, _filename);

            // Assert
            Assert.That(File.Exists(_filename), Is.True);
        }

        [Test]
        public void CreatesPDFFromTour()
        {
            // Arrange
            var tour = new Tour("Tour");
            Assert.That(File.Exists(_filename), Is.False);

            // Act
            _report.CreateReport(tour, _filename);

            // Assert
            Assert.That(File.Exists(_filename), Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            // Delete Created Report
            File.Delete(_filename);
        }
    }
}
