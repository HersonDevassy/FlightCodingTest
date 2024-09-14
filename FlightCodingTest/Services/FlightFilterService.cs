using FlightCodingTest.Filters;
using FlightCodingTest.Models;

namespace FlightCodingTest.Services
{
    public class FlightFilterService
    {
        /// <summary>
        /// The collection of flight filters used to apply various filtering criteria to flights.
        /// This field holds a list of filters that implement the 'IFlightFilter' interface.
        /// </summary>
        private readonly IEnumerable<IFlightFilter> _filters;

        /// <summary>Initializes a new instance of the class with the specified flight filters.</summary>
        /// <param name="filters">Representing the collection of filters to be applied to the flights.
        /// Each filter in the collection implements the 'IFlightFilter' interface and provides specific filtering criteria.</param>
        public FlightFilterService(IEnumerable<IFlightFilter> filters)
        {
            _filters = filters;
        }

        /// <summary>Filters the provided collection of flights based on the criteria defined by the filters.</summary>
        /// <param name="flights">Containing the flights to be filtered.</param>
        /// <returns>List of flights containing only those flights that meet all the filtering criteria.</returns>
        public IEnumerable<Flight> FilterFlights(IEnumerable<Flight> flights)
        {
            return flights.Where(flight => _filters.All(filter => filter.IsFlightValid(flight)));
        }
    }
}
