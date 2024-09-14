namespace FlightCodingTest.Models
{
    public class Flight
    {
        /// <summary>
        /// Gets or sets the list of segments for the flight.
        /// Each segment represents a portion of the flight with a departure and arrival time.
        /// </summary>
        public IList<Segment> Segments { get; set; }
    }
}
