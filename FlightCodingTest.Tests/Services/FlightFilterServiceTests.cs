using FlightCodingTest.Filters;
using FlightCodingTest.Models;
using FlightCodingTest.Services;

namespace FlightCodingTest.Tests.Services
{
    public class FlightFilterServiceTests
    {
        [Fact]
        public void Filter_Flights_Based_On_Multiple_Filters()
        {
            // Arrange
            var flight = new Flight
            {
                Segments = new List<Segment>
                {
                    new Segment { DepartureDate = System.DateTime.Now.AddDays(-1), ArrivalDate = System.DateTime.Now.AddHours(1) }
                }
            };

            var filters = new List<IFlightFilter>
            {
                new FutureDepartureFilter(),
                new ArrivalBeforeDepartureFilter()
            };
            var flightFilterService = new FlightFilterService(filters);

            // Act
            var result = flightFilterService.FilterFlights(new List<Flight> { flight });

            // Assert
            Assert.Empty(result);
        }
    }
}
