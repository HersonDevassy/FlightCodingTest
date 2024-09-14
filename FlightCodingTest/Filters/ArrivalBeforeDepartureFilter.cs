using FlightCodingTest.Models;

namespace FlightCodingTest.Filters
{
    public class ArrivalBeforeDepartureFilter : IFlightFilter
    {
        /// <summary>
        /// Checks whether all segments of the given 'Flight' have valid timings.
        /// A segment is considered valid if the arrival date is on or after the departure date.
        /// </summary>
        /// <param name="flight">The 'Flight' object to validate.</param>
        /// <returns>True if all segments in the flight have an arrival date that is equal to or after the departure date; otherwise, false.</returns>
        public bool IsFlightValid(Flight flight)
        {
            return flight.Segments.All(s => s.ArrivalDate >= s.DepartureDate);
        }
    }
}
