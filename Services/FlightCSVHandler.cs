using Traveless.Models;

namespace Traveless.Services
{
    internal class FlightCSVHandler
    {
        private const string filePath = @"C:\Users\matth\source\repos\Traveless\flights.csv";

        private string GetFilePath()
        {
            return filePath;
        }

        public List<FlightsObj> flightList()
        {
            var result = new List<FlightsObj>();
            var filePath = GetFilePath();

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The CSV file was not found.", filePath);
            }

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var split = line.Split(',');

                if (split.Length == 8)
                {
                    FlightsObj tempObj = new(
                        split[0],
                        split[1],
                        split[2],
                        split[3],
                        split[4],
                        split[5],
                        int.Parse(split[6]),
                        double.Parse(split[7])
                        );
                    result.Add(tempObj);
                }
            }
            return result;
        }
    }
}