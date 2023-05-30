using Moq;
using SS23_SWEN2_TourPlanner_WPF.BL;
using SS23_SWEN2_TourPlanner_WPF.DAL;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.Tests.BL
{
    [TestFixture]
    public class ToursManagerTest
    {
        private IToursManager _toursManager;
        private Mock<IDataManager> _dataManager;

        [SetUp]
        public void Setup()
        {
            _dataManager = new Mock<IDataManager>();
            _toursManager = new ToursManagerImpl(_dataManager.Object);
        }

        [Test]
        public void DeleteTour_AlsoDeletesImage()
        {
            // Arrange
            var tour = new Tour("TestTour") { Image = "existingPath" };
            _dataManager.Setup(m => m.DeleteTour(tour)).Verifiable();
            File.Create(tour.Image).Close();
            Assert.That(File.Exists(tour.Image), Is.True);

            // Act
            _toursManager.DeleteTour(tour);

            // Assert
            _dataManager.Verify(m => m.DeleteTour(tour), Times.Once);
            Assert.That(File.Exists(tour.Image), Is.False);
        }


    }
}
