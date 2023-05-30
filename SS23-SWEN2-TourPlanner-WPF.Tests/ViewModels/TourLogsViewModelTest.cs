using Moq;
using SS23_SWEN2_TourPlanner_WPF.BL;
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
    }
}
