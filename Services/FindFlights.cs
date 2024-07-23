using Traveless.Models;

namespace Traveless.Services
{
    public class FindFlights
    {
        private readonly List<FlightsObj> flightList;
        private readonly Dictionary<string, string> airportDict;

        public FindFlights(List<FlightsObj> flightList, Dictionary<string, string> airportDict)
        {
            this.flightList = flightList;
            this.airportDict = airportDict;
        }

        public List<FlightsObj> ValidateAndSearch(string fromInput, string toInput, string dayInput)
        {
            if (fromInput == "Any" || toInput == "Any" || dayInput == "Any" || fromInput == null || toInput == null || dayInput == null)
            {
                return new List<FlightsObj> { new FlightsObj { FlightCode = "Error", Airline = "Please enter all search categories" } };
            }

            string? fromCode = ConvertToThreeLetter(fromInput);
            string? toCode = ConvertToThreeLetter(toInput);

            var results = flightList
                .Where(f => (f.Departure == fromCode || f.Departure == fromInput) &&
                            (f.Arrival == toCode || f.Arrival == toInput) &&
                            f.Day == dayInput)
                .ToList();

            if (results.Count == 0)
            {
                return new List<FlightsObj> { new FlightsObj { FlightCode = "No Match", Airline = "No matching flights found." } };
            }

            return results;
        }

        private string? ConvertToThreeLetter(string input)
        {
            if (input != null)
            {
                if (input.Length == 3 && airportDict.ContainsKey(input) && input != null)
                {
                    return input;
                }

                foreach (var keypair in airportDict)
                {
                    if (keypair.Value.Equals(input, StringComparison.OrdinalIgnoreCase))
                    {
                        return keypair.Key;
                    }
                }
            }
            return input;
        }
    }
}
