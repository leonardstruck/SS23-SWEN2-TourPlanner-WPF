using Moq;
using SS23_SWEN2_TourPlanner_WPF.BL;
using SS23_SWEN2_TourPlanner_WPF.Models;
using SS23_SWEN2_TourPlanner_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.Tests.ViewModels
{
    [TestFixture]
    public class TourLogsViewModelTest
    {
        private Mock<IToursManager> _toursManagerMock;
        private TourLogsViewModel _viewModel;
        private Tour _tour;

        [SetUp]
        public void Setup()
        {
            _toursManagerMock = new Mock<IToursManager>();
            _tour = new Tour("Test-Tour")
            {
                Id = 1,
            };

            _tour.TourLogs.Add(new TourLog() { Id = 1 });
            _tour.TourLogs.Add(new TourLog() { Id = 2 });

            _viewModel = new TourLogsViewModel(_toursManagerMock.Object, _tour);
        }

        [Test]
        public void DeleteTourLogCommand_ShouldDeleteTourLogFromViewModel()
        {
            // Arrange
            var initialCount = _viewModel.TourLogs.Count;
            var selectedTourLog = _viewModel.TourLogs.Last();
            _viewModel.CurrentTourLog = selectedTourLog;

            // Act
            _viewModel.DeleteTourLogCommand.Execute(null);

            // Assert
            Assert.That(_viewModel.TourLogs, Has.Count.LessThan(initialCount));
            Assert.That(_viewModel.TourLogs, Does.Not.Contain(selectedTourLog));
        }

        [Test]
        public void DeleteTourLogCommand_ShouldDeleteTourLogFromToursManager()
        {
            // Arrange
            var selectedTourLog = _viewModel.TourLogs.Last();
            _viewModel.CurrentTourLog = selectedTourLog;
            _toursManagerMock.Setup(m => m.DeleteTourLog(It.IsAny<Tour>(), selectedTourLog));


            // Act
            _viewModel.DeleteTourLogCommand.Execute(null);

            // Assert
            _toursManagerMock.Verify(m => m.DeleteTourLog(It.IsAny<Tour>(), selectedTourLog));
        }
    }
}
