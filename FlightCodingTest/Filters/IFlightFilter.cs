using FlightCodingTest.Models;

namespace FlightCodingTest.Filters
{
    public interface IFlightFilter
    {
        /// <summary>Determines whether the given 'Flight' meets the criteria defined by the filter.</summary>
        /// <param name="flight">The 'Flight' object to be validated by the filter.</param>
        /// <returns>True if the flight meets the filter's criteria and is considered valid; otherwise, false.</returns>
        bool IsFlightValid(Flight flight);
    }
}
