namespace Guestline.ConsoleApp.ConsoleParameters
{
    public class ConsoleParametersParser
    {
        private const string hotelOption = "--hotels";
        private const string bookingsOption = "--bookings";
        private readonly List<string> _expectedOptions = [hotelOption, bookingsOption];
        public ConsoleAppParameters Parse(string[] args)
        {
            if (args.Length != 4)
            {
                throw new ArgumentException("Incorrect arguments. Please provide provide more options. Example: --hotels ./SampleData/hotels.json --bookings ./SampleData/bookings.json");
            }

            List<string> userOptions = [args[0], args[2]];
            userOptions = userOptions.Distinct().ToList();

            foreach (string expectedOption in _expectedOptions)
            {
                if (!userOptions.Contains(expectedOption))
                {
                    throw new ArgumentException($"Incorrect argument. Got {string.Join(" and ", userOptions)} but expected : {string.Join(" or ", _expectedOptions)}");
                }
            }

            string hotelsFilePath;
            string bookingsFilePath;

            if (args[0] == hotelOption)
            {
                hotelsFilePath = args[1];
                bookingsFilePath = args[3];
            }
            else
            {
                hotelsFilePath = args[3];
                bookingsFilePath = args[1];
            }

            return new ConsoleAppParameters(hotelsFilePath, bookingsFilePath);
        }
    }
}
