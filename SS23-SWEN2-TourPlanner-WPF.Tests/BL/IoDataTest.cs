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
    public class IoDataTest
    {
        private IoData _ioData;
        private string _filename = "test_io.csv";

        [SetUp]
        public void Setup()
        {
            _ioData = new IoData();
        }

        [Test]
        public void CreatesCSVFromTours()
        {
            // Arrange
            var tours = new ObservableCollection<Tour>()
            {
                new Tour("Tour 1"),
                new Tour("Tour 2")
            };
            Assert.That(File.Exists(_filename), Is.False);

            // Act
            _ioData.ExportData(tours, _filename);

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
