using FlightCodingTest.Filters;
using FlightCodingTest.Models;

namespace FlightCodingTest.Tests.Filters
{
    public class FutureDepartureFilterTests
    {
        [Fact]
        public void IsFlightValid_ReturnFalse_Flights_Departing_Before_Current_Time()
        {
            // Arrange
            var flight = new Flight
            {
                Segments = new List<Segment>
                {
                    new Segment { DepartureDate = DateTime.Now.AddDays(-1), ArrivalDate = DateTime.Now.AddHours(1) }
                }
            };
            var futureDepartureFilter = new FutureDepartureFilter();

            // Act
            var result = futureDepartureFilter.IsFlightValid(flight);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsFlightValid_ReturnTrue_Flights_Departing_After_Current_Time()
        {
            // Arrange
            var flight = new Flight
            {
                Segments = new List<Segment>
                {
                    new Segment { DepartureDate = DateTime.Now.AddDays(1), ArrivalDate = DateTime.Now.AddHours(2) }
                }
            };
            var futureDepartureFilter = new FutureDepartureFilter();

            // Act
            var result = futureDepartureFilter.IsFlightValid(flight);

            // Assert
            Assert.True(result);
        }
    }
}
