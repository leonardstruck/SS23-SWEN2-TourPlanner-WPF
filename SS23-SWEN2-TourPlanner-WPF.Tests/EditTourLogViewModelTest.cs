using System;
using NUnit.Framework;
using Moq;
using SS23_SWEN2_TourPlanner_WPF.Models;
using SS23_SWEN2_TourPlanner_WPF.ViewModels;

namespace SS23_SWEN2_TourPlanner_WPF.Tests.ViewModels
{
    public class EditTourLogViewModelTest
    {

        [Test]
        public void EditTourLogViewModelShouldUpdateTourLogOnSave()
        {
            // Arrange
            var tourLog = new TourLog();
            var viewModel = new EditTourLogViewModel(tourLog);

            var dateTime = new DateTime(2021, 12, 31);
            var comment = "This is a comment";
            var difficulty = Difficulty.Moderate;
            var totalTime = new TimeSpan(3, 30, 0);
            var rating = 5;

            viewModel.DateTime = dateTime;
            viewModel.Comment = comment;
            viewModel.Difficulty = difficulty;
            viewModel.TimeSpan = totalTime;
            viewModel.Rating = rating;

            var expectedTourlog = new TourLog()
            {
                DateTime = dateTime,
                Comment = comment,
                Difficulty = difficulty,
                TotalTime = totalTime,
                Rating = rating
            };

            // Assert (other way around because we listen to the event)
            viewModel.EditButtonClicked += (sender, args) =>
            {
                Assert.Multiple(() =>
                    {
                        Assert.That(args.DateTime, Is.EqualTo(expectedTourlog.DateTime));
                        Assert.That(args.Comment, Is.EqualTo(expectedTourlog.Comment));
                        Assert.That(args.Difficulty, Is.EqualTo(expectedTourlog.Difficulty));
                        Assert.That(args.TotalTime, Is.EqualTo(expectedTourlog.TotalTime));
                        Assert.That(args.Rating, Is.EqualTo(expectedTourlog.Rating));
                    });
            };

            // Act
            viewModel.ExecuteCommandEdit.Execute(null);
        }
    }
}
