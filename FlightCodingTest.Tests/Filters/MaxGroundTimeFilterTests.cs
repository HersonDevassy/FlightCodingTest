using FlightCodingTest.Filters;
using FlightCodingTest.Models;

namespace FlightCodingTest.Tests.Filters
{
    public class MaxGroundTimeFilterTests
    {
        [Fact]
        public void IsFlightValid_ReturnFalse_WhenGroundTime_ExceedsTwoHours()
        {
            // Arrange
            var filter = new MaxGroundTimeFilter();
            var flight = new Flight
            {
                Segments = new[]
                {
                    new Segment { DepartureDate = DateTime.Now, ArrivalDate = DateTime.Now.AddHours(1) },
                    new Segment { DepartureDate = DateTime.Now.AddHours(4), ArrivalDate = DateTime.Now.AddHours(5) } // 3 hours ground time
                }
            };

            // Act
            var result = filter.IsFlightValid(flight);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsFlightValid_ReturnTrue_WhenGroundTime_LessThanOrEqualToTwoHours()
        {
            // Arrange
            var filter = new MaxGroundTimeFilter();
            var flight = new Flight
            {
                Segments = new[]
                {
                    new Segment { DepartureDate = DateTime.Now, ArrivalDate = DateTime.Now.AddHours(1) },
                    new Segment { DepartureDate = DateTime.Now.AddHours(2), ArrivalDate = DateTime.Now.AddHours(3) } // 1 hour ground time
                }
            };

            // Act
            var result = filter.IsFlightValid(flight);

            // Assert
            Assert.True(result);
        }
    }
}
