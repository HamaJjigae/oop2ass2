namespace Traveless.Models
{
    public class FlightSearchModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Day { get; set; }

        public FlightSearchModel()
        {
            From = "Any";
            To = "Any";
            Day = "Any";
        }
    }
}
