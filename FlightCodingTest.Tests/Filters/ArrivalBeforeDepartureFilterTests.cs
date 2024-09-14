using FlightCodingTest.Filters;
using FlightCodingTest.Models;

namespace FlightCodingTest.Tests.Filters
{
    public class ArrivalBeforeDepartureFilterTests
    {
        [Fact]
        public void IsFlightValid_ReturnFalse_AnySegmentArrival_BeforeDeparture()
        {
            // Arrange
            var filter = new ArrivalBeforeDepartureFilter();
            var flight = new Flight
            {
                Segments = new[]
                {
                    new Segment { DepartureDate = DateTime.Now.AddHours(1), ArrivalDate = DateTime.Now.AddHours(-1) }, // Invalid segment
                }
            };

            // Act
            var result = filter.IsFlightValid(flight);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsFlightValid_ReturnTrue_AllSegmentArrivals_AfterDeparture()
        {
            // Arrange
            var filter = new ArrivalBeforeDepartureFilter();
            var flight = new Flight
            {
                Segments = new[]
                {
                    new Segment { DepartureDate = DateTime.Now.AddHours(1), ArrivalDate = DateTime.Now.AddHours(2) },
                    new Segment { DepartureDate = DateTime.Now.AddHours(3), ArrivalDate = DateTime.Now.AddHours(4) }
                }
            };

            // Act
            var result = filter.IsFlightValid(flight);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsFlightValid_ReturnTrue_ArrivalAndDeparture_SameTime()
        {
            // Arrange
            var filter = new ArrivalBeforeDepartureFilter();
            var flight = new Flight
            {
                Segments = new[]
                {
                    new Segment { DepartureDate = DateTime.Now, ArrivalDate = DateTime.Now } // Same time
                }
            };

            // Act
            var result = filter.IsFlightValid(flight);

            // Assert
            Assert.True(result);
        }
    }
}
