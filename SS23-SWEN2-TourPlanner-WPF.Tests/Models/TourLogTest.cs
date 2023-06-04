using SS23_SWEN2_TourPlanner_WPF.Models;


namespace SS23_SWEN2_TourPlanner_WPF.Tests.Models
{
    [TestFixture]
    public class TourLogTest
    {
        [Test]
        public void ToTourLogTest_ValidStringShouldOverwriteTourLog()
        {
            // Arrange
            var log = new TourLog();

            DateTime now = DateTime.Now;

            var temp = log.Clone();


            // Act
            log.ToTourLog($"{3}%%%{now}%%%Test%%%{Difficulty.Moderate}%%%{TimeSpan.FromHours(1)}%%%{5}");

            // Assert

            Assert.That(log, Is.Not.EqualTo(temp));
            //Assert.That(AreEqual(log, temp));

        }

        [Test]
        public void ToTourLogTest_InvalidStringShouldNotOverwriteTourLog()
        {
            // Arrange
            var log = new TourLog();

            DateTime now = DateTime.Now;

            var temp = log.Clone();

            // Act
            // Used the wrong seperator
            log.ToTourLog($"{3}###{now}###Test###{Difficulty.Moderate}###{TimeSpan.FromHours(1)}###{5}");

            // Assert
            Assert.That(log, Is.EqualTo(temp));
            //Assert.That(AreEqual(log, temp));

        }
    }
}
