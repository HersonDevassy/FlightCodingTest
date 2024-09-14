using FlightCodingTest.Filters;
using FlightCodingTest.Models;
using FlightCodingTest.Services;
using System.Collections.Generic;

namespace FlightCodingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FlightBuilder flightBuilder = new FlightBuilder();
            IList<Flight> flights = flightBuilder.GetFlights();
            LoadFilters(flights);
        }

        /// <summary>
        /// Continuously prompts the user to select filters and applies them to the list of flights. 
        /// After applying the filters, it groups the filtered flights by the number of segments and displays the results.
        /// </summary>
        /// <param name="flights">The list of flights to filter and display.</param>
        private static void LoadFilters(IList<Flight> flights)
        {
            bool exit = false;
            while (!exit)
            {
                List<IFlightFilter> selectedFilters = GetSelectedFilters(out exit);

                // Apply selected filters
                FlightFilterService flightFilterService = new FlightFilterService(selectedFilters);
                IEnumerable<Flight> filteredFlights = flightFilterService.FilterFlights(flights);

                // Group filtered flights by segment count
                IEnumerable<IGrouping<int, Flight>> groupedFlights = filteredFlights.GroupBy(flight => flight.Segments.Count);

                // Display grouped results
                Console.WriteLine("\nFiltered Flights by Segment Count:");
                foreach (IGrouping<int, Flight> group in groupedFlights)
                {
                    int index = 1;
                    Console.WriteLine($"\nFlights with {group.Key} segments:");
                    foreach (IList<Segment> segments in group.Select(x => x.Segments))
                    {
                        Console.WriteLine($"Flight {index}:");
                        foreach (Segment segment in segments)
                        {
                            Console.WriteLine($"    Departure: {segment.DepartureDate}, Arrival: {segment.ArrivalDate}");
                        }
                        index++;
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prompts the user to select filters for filtering flights and returns a list of selected filters.
        /// Allows multiple filter options to be selected. If 'Exit' is selected, the method terminates the filter selection process.
        /// </summary>
        /// <returns>A list of selected filters implementing the 'IFlightFilter' interface, which will be applied to the flights.</returns>
        private static List<IFlightFilter> GetSelectedFilters(out bool exit)
        {
            exit = false;
            List<IFlightFilter> selectedFilters = new List<IFlightFilter>();

            // Display filter options to the user
            Console.WriteLine("Please select filters (enter comma-separated values, eg: 1,2,3):");
            Console.WriteLine("1. Depart before the current date/time");
            Console.WriteLine("2. Have any segment with an arrival date before the departure date");
            Console.WriteLine("3. Spend more than 2 hours on the ground");
            Console.WriteLine("4. Exit");
            string input = Console.ReadLine();

            // Process user's selection
            if (!string.IsNullOrEmpty(input))
            {
                IEnumerable<string> selectedOptions = input.Split(',').Select(option => option.Trim());
                foreach (string option in selectedOptions)
                {
                    switch (option)
                    {
                        case "1":
                            selectedFilters.Add(new FutureDepartureFilter());
                            break;
                        case "2":
                            selectedFilters.Add(new ArrivalBeforeDepartureFilter());
                            break;
                        case "3":
                            selectedFilters.Add(new MaxGroundTimeFilter());
                            break;
                        case "4":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine($"Unknown filter option: {option}");
                            break;
                    }
                }
            }

            if (!exit && selectedFilters.Count == 0)
            {
                GetSelectedFilters(out exit);
            }
            else if (selectedFilters.Count > 0 && exit)
            {
                Console.WriteLine("Invalid entry: Users are not allowed to select 'Exit' in combination with any other filters.\n");
                GetSelectedFilters(out exit);
            }
            return selectedFilters;
        }
    }
}
