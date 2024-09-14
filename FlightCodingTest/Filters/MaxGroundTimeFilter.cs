using FlightCodingTest.Models;

namespace FlightCodingTest.Filters
{
    public class MaxGroundTimeFilter : IFlightFilter
    {
        /// <summary>
        /// The maximum allowable ground time between consecutive flight segments.
        /// The default value is set to 2 hours.
        /// </summary>
        private readonly TimeSpan maxGroundTime = TimeSpan.FromHours(2);

        /// <summary>
        /// Checks whether the given 'Flight' is valid based on the maximum allowable ground time between segments.
        /// A flight is considered valid if no segment has a ground time that exceeds the specified maximum ground time.
        /// </summary>
        /// <param name="flight">The 'Flight' object to validate.</param>
        /// <returns>True if all segments of the flight have ground times between them that are less than or equal to the maximum allowable ground time; otherwise, false</returns>
        public bool IsFlightValid(Flight flight)
        {
            for (int i = 0; i < flight.Segments.Count - 1; i++)
            {
                TimeSpan groundTime = flight.Segments[i + 1].DepartureDate - flight.Segments[i].ArrivalDate;
                if (groundTime > maxGroundTime)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
