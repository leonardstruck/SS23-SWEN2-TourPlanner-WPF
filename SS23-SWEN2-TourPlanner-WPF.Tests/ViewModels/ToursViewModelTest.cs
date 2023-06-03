using SS23_SWEN2_TourPlanner_WPF.BL;
using SS23_SWEN2_TourPlanner_WPF.DAL;
using SS23_SWEN2_TourPlanner_WPF.Models;
using SS23_SWEN2_TourPlanner_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Windows;

namespace SS23_SWEN2_TourPlanner_WPF.Tests.ViewModels
{
    [TestFixture]
    public class ToursViewModelTest
    {
        private ToursViewModel _viewModel;
        private Mock<IToursManager> _toursManagerMock;
        private Mock<IMessageBoxService> _messageBoxServiceMock;
        private Mock<IFileDialogService> _fileDialogServiceMock;

        [SetUp]
        public void Setup()
        {
            _toursManagerMock = new Mock<IToursManager>();
            _messageBoxServiceMock = new Mock<IMessageBoxService>();
            _fileDialogServiceMock = new Mock<IFileDialogService>();
            _messageBoxServiceMock.Setup(m => m.Show(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<MessageBoxButton>(), It.IsAny<MessageBoxImage>(), It.IsAny<MessageBoxResult>(), It.IsAny<MessageBoxOptions>())).Returns(MessageBoxResult.Yes);
            _viewModel = new ToursViewModel(_toursManagerMock.Object, _messageBoxServiceMock.Object, _fileDialogServiceMock.Object);
        }

        [Test]
        public void Constructor_ShouldLoadToursFromManager()
        {
            // Arrange
            var expectedTours = new List<Tour>
            {
                new Tour("Tour 1"),
                new Tour("Tour 2")
            };
            _toursManagerMock.Setup(tm => tm.GetTours()).Returns(expectedTours);

            // Act
            _viewModel = new ToursViewModel(_toursManagerMock.Object, _messageBoxServiceMock.Object, _fileDialogServiceMock.Object);

            // Assert
            Assert.That(expectedTours, Has.Count.EqualTo(2));
            foreach (var tour in expectedTours)
            {
                Assert.That(_viewModel.Tours, Does.Contain(tour));
            }

        }

        [Test]
        public void DeleteTourCommand_ShouldRemoveTourFromManager()
        {
            // Arrange
            var tourToDelete = new Tour("Tour 1") { Id = 1 };
            _viewModel.Tours.Add(new Tour("Tour 2") { Id = 2 });
            _viewModel.Tours.Add(tourToDelete);
            _viewModel.Tours.Add(new Tour("Tour 3") { Id = 3 });
            _viewModel.CurrentTour = tourToDelete;

            // Act
            _viewModel.DeleteTourCommand.Execute(null);

            // Assert
            _toursManagerMock.Verify(tm => tm.DeleteTour(tourToDelete), Times.Once);
        }
    }
}
