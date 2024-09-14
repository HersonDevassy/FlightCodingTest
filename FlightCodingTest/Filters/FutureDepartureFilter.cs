using FlightCodingTest.Models;

namespace FlightCodingTest.Filters
{
    public class FutureDepartureFilter : IFlightFilter
    {
        /// <summary>
        /// Checks whether all segments of the given 'Flight' have departure dates in the future.
        /// A flight is considered valid if all departure dates are after the current date and time.
        /// </summary>
        /// <param name="flight">The 'Flight' object to validate.</param>
        /// <returns>True if all segments in the flight have departure dates that are in the future compared to the current date and time; otherwise, false.</returns>
        public bool IsFlightValid(Flight flight)
        {
            return flight.Segments.All(s => s.DepartureDate > DateTime.Now);
        }
    }
}
