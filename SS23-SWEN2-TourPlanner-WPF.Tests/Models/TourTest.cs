using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.Tests.Models
{
    [TestFixture]
    public class TourTest
    {
        [Test]
        public void ImageSource_ShouldReturnBitmapEvenIfImageIsNotPresent()
        {
            // Arrange
            var tour = new Tour("Test");
            Assert.That(tour.Image, Is.Empty);

            // Act
            var bitmap = tour.ImageSource;

            // Assert
            Assert.That(bitmap, Is.Not.Null);

        }
    }
}
