using SS23_SWEN2_TourPlanner_WPF.BL;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.Tests.BL
{
    [TestFixture]
    public class SearchTest
    {
        private Search _search;
        private List<Tour> _tours;

        private static readonly string TERM_USED_ONLY_IN_DONAUINSEL_TOURLOG = "%TERM_THAT_IS_ONLY_USED_IN_THIS_TOURLOG%";
        private readonly Tour donauinselTour = new("A quick break from Uni - Walking Route to Donauinsel")
        {
            From = "FH Technikum, 1200 Wien",
            To = "Sportinsel, 1210 Wien",
            TourLogs = new()
                    {
                        new TourLog()
                        {
                            Comment = "The walk is not very nice, but as soon as you are on the island it's nice!"
                        },
                        new TourLog()
                        {
                            Comment = TERM_USED_ONLY_IN_DONAUINSEL_TOURLOG
                        }
                    }
        };

        private readonly Tour praterTour = new("A walk across the prater to the Krebsenwasser")
        {
            From = "Praterstern, Wien",
            To = "Krebsenwasser, Wien",
            TourLogs = new()
            {
                new TourLog()
                {
                    Comment = "It was a walk in the park!",
                    Difficulty = Difficulty.Moderate
                }
            }
        };

        [SetUp]
        public void Setup()
        {
            _search = new Search();
            _tours = new List<Tour>()
            {
               donauinselTour,
               praterTour
            };
        }

        [Test]
        public void SearchFor_Gibberish_ShouldNotReturnAnything()
        {
            // Arrange
            var searchTerm = "asdklfksjdlkfjdfslklkjql23o109usdfölknxcy,mnf213lkj";

            // Act
            var result = _search.SearchTours(_tours, searchTerm);

            // Assert
            Assert.That(result, Has.Count.EqualTo(0));
        }

        [Test]
        public void SearchFor_Donauinsel()
        {
            // Arrange
            var searchTerm = "Donauinsel";

            // Act
            var result = _search.SearchTours(_tours, searchTerm);

            // Assert
            Assert.That(result, Does.Contain(donauinselTour));
        }

        [Test]
        public void SearchFor_TermThatIsInsideTourLogButNotInTour()
        {
            // Arrange
            var searchTerm = TERM_USED_ONLY_IN_DONAUINSEL_TOURLOG;

            // Act
            var result = _search.SearchTours(_tours, searchTerm);

            // Assert
            Assert.That(result, Does.Contain(donauinselTour));
        }

        [Test]
        public void SearchFor_Destination_SearchTerm_Is_Lowercase()
        {
            // Arrange
            var searchTerm = donauinselTour.To.ToLower();

            // Act
            var result = _search.SearchTours(_tours, searchTerm);

            // Assert
            Assert.That(result, Does.Contain(donauinselTour));
        }

        [Test]
        public void SearchReturnsEntireListWhenSearchTermIsEmpty()
        {
            // Arrange
            var searchTerm = "       ";

            // Act
            var result = _search.SearchTours(_tours, searchTerm);

            // Assert
            Assert.That(result, Is.EquivalentTo(_tours));
        }

        [Test]
        public void SearchFor_Enum_Difficulty()
        {
            // Arrange
            var searchTerm = "moderate";

            // Act
            var result = _search.SearchTours(_tours, searchTerm);

            // Assert
            Assert.That(result, Does.Contain(praterTour));
        }

        [Test]
        public void SearchFor_SpecificTourLog_FilterTourlogs()
        {
            // Arrange
            var searchTerm = TERM_USED_ONLY_IN_DONAUINSEL_TOURLOG;
            _search.FilterTourLogs = true;

            // Act
            var result = _search.SearchTours(_tours, searchTerm);

            // Assert
            Assert.That(result, Does.Contain(donauinselTour));
            Assert.That(result, Has.Count.EqualTo(1));

            Assert.That(result.First().TourLogs, Has.Count.EqualTo(1));
            Assert.That(result.First().TourLogs.Last().Comment, Is.EqualTo(TERM_USED_ONLY_IN_DONAUINSEL_TOURLOG));
        }
    }
}
