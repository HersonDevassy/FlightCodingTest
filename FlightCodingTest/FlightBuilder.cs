using FlightCodingTest.Models;

namespace FlightCodingTest
{
    public class FlightBuilder
    {
        /// <summary>Generates a predefined list of test flights for use in filtering operations.</summary>
        /// <returns>A list of 'Flight' objects representing different flight scenarios, each containing one or more segments.</returns>
        public IList<Flight> GetFlights()
        {
            DateTime threeDaysFromNow = DateTime.Now.AddDays(3);

            return new List<Flight>
            {
                CreateFlight(threeDaysFromNow, threeDaysFromNow.AddHours(2)),
                CreateFlight(threeDaysFromNow, threeDaysFromNow.AddHours(2), threeDaysFromNow.AddHours(3), threeDaysFromNow.AddHours(5)),
                CreateFlight(threeDaysFromNow.AddDays(-6), threeDaysFromNow),
                CreateFlight(threeDaysFromNow, threeDaysFromNow.AddHours(-6)),
                CreateFlight(threeDaysFromNow, threeDaysFromNow.AddHours(2), threeDaysFromNow.AddHours(5), threeDaysFromNow.AddHours(6)),
                CreateFlight(threeDaysFromNow, threeDaysFromNow.AddHours(2), threeDaysFromNow.AddHours(3), threeDaysFromNow.AddHours(4), threeDaysFromNow.AddHours(6), threeDaysFromNow.AddHours(7))
            };
        }

        /// <summary>
        /// Creates a 'Flight' object with one or more segments based on the provided dates.
        /// Each pair of dates (departure and arrival) corresponds to a segment of the flight.
        /// </summary>
        /// <param name="dates">A sequence of 'DateTime' values representing departure and arrival times for each flight segment.</param>
        /// <returns>A 'Flight' object containing a list of segments with the provided departure and arrival times.</returns>
        /// <exception cref="ArgumentException">Thrown if the number of provided dates is not even, as each segment requires both a departure and an arrival time.</exception>
        private static Flight CreateFlight(params DateTime[] dates)
        {
            if (dates.Length % 2 != 0)
                throw new ArgumentException("You must pass an even number of dates,", "dates");

            IEnumerable<DateTime> departureDates = dates.Where((date, index) => index % 2 == 0);
            IEnumerable<DateTime> arrivalDates = dates.Where((date, index) => index % 2 == 1);

            List<Segment> segments = departureDates
                .Zip(arrivalDates, (departureDate, arrivalDate) => new Segment { DepartureDate = departureDate, ArrivalDate = arrivalDate })
                .ToList();

            return new Flight { Segments = segments };
        }
    }
}
