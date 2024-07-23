namespace Traveless.Services
{
    public class AirportCSVHandler
    {
        private const string filePath = @"C:\Users\matth\source\repos\Traveless\airports.csv";

        private static string GetFilePath()
        {
            return filePath;
        }

        public Dictionary<string, string> ReadAiportCSVToDict()
        {
            var result = new Dictionary<string, string>();
            var filePath = GetFilePath();

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The CSV file was not found.", filePath);
            }

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                var parts = line.Split(',');

                if (parts.Length == 2)
                {
                    var key = parts[0].Trim();
                    var value = parts[1].Trim();

                    if (!result.ContainsKey(key))
                    {
                        result.Add(key, value);
                    }
                }
            }
            return result;
        }
    }
}
