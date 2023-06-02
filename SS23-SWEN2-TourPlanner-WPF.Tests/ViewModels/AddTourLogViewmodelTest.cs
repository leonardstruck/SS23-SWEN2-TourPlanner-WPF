using Moq;
using SS23_SWEN2_TourPlanner_WPF.Models;
using SS23_SWEN2_TourPlanner_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.Tests.ViewModels
{
    [TestFixture]
    public class AddTourLogViewmodelTest
    {
        private AddTourLogViewModel _viewModel;
        private Mock<EventHandler<TourLog>> _mockHandler = new();

        [SetUp]
        public void Setup()
        {
            _viewModel = new AddTourLogViewModel();
            _viewModel.AddButtonClicked += _mockHandler.Object;
        }

        [Test]
        public void ExecuteCommandAdd_CreatesTourLogFromUIFields()
        {
            // Arrange
            var tourLog = new TourLog()
            {
                DateTime = DateTime.Now,
                Comment = "This is a comment",
                Difficulty = Difficulty.Easy,
                TotalTime = TimeSpan.FromHours(1),
                Rating = 3
            };
            _viewModel.DateTime = tourLog.DateTime;
            _viewModel.Comment = tourLog.Comment;
            _viewModel.Difficulty = tourLog.Difficulty;
            _viewModel.TimeSpan = tourLog.TotalTime;
            _viewModel.Rating = tourLog.Rating;

            var triggered = false;
            _mockHandler.Setup(m => m(It.IsAny<object>(), It.IsAny<TourLog>())).Callback((object sender, TourLog tourLogFromEvent) =>
            {
                triggered = true;
                Assert.Multiple(() =>
                {
                    Assert.That(tourLog.DateTime, Is.EqualTo(tourLogFromEvent.DateTime));
                    Assert.That(tourLog.Comment, Is.EqualTo(tourLogFromEvent.Comment));
                    Assert.That(tourLog.Difficulty, Is.EqualTo(tourLogFromEvent.Difficulty));
                    Assert.That(tourLog.TotalTime, Is.EqualTo(tourLogFromEvent.TotalTime));
                    Assert.That(tourLog.Rating, Is.EqualTo(tourLogFromEvent.Rating));
                });
            });

            // Act
            _viewModel.ExecuteCommandAdd.Execute(_viewModel);

            // Assert
            Assert.That(triggered, Is.True);
        }
    }
}
