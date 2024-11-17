using Guestline.ConsoleApp.DataImporter;

namespace Guestline.ConsoleApp.UserInputParser
{
    public class InputParser
    {
        private const string AvailabilityCommandName = "Availability";
        private const string AvailabilityCommandTemplate = "Availability(hotelId ,20240901, roomType) or Availability(hotelId, 20240901-20240903, roomType)";
        private const int LineLengthEnforcement = 26;
        private const int MethodOpeningBrackedIndex = 12;
        private readonly StringToDateTimeParser _stringToDateTimeParser = new StringToDateTimeParser();

        public InputData Parse(string line)
        {
            line = line.Replace(" ", "");

            if (!line.StartsWith(AvailabilityCommandName) || line.Length < LineLengthEnforcement)
            {
                throw new ArgumentException($"Unkown command. Expected: {AvailabilityCommandTemplate}");
            }

            if (line[12] != '(')
            {
                throw new ArgumentException($"Error while parsing input. Expected '(' after \"{AvailabilityCommandName}\" but got '{line[12]}'");
            }

            if (!line.EndsWith(")"))
            {
                throw new ArgumentException($"Error while parsing input. Expected ')' at the end of line but got '{line.Last()}'");
            }

            var parametersAsString = line.Substring(MethodOpeningBrackedIndex + 1);
            parametersAsString = parametersAsString.Substring(0, parametersAsString.Length - 1);

            var parametersAsStringList = parametersAsString.Split(',');

            if (parametersAsStringList.Length != 3)
            {
                throw new ArgumentException($"Invalid number of parameters expected 3 but got {parametersAsStringList.Length}");
            }

            string hotelId = parametersAsStringList[0];
            string roomType = parametersAsStringList[2];

            var dateAsString = parametersAsStringList[1];
            var dateAsList = dateAsString.Split('-');

            DateTime arriveDate;
            DateTime? departureDate = default;

            arriveDate = _stringToDateTimeParser.Parse(dateAsList[0]);

            if (dateAsList.Length == 2)
            {
                departureDate = _stringToDateTimeParser.Parse(dateAsList[1]);

                if (arriveDate >= departureDate) 
                {
                    throw new ArgumentException($"Arrive date cannot be greater or equal to departure date. Arrival: {arriveDate}, Departure: {departureDate}");
                }
            }

            return new InputData(hotelId, roomType, arriveDate, departureDate);
        }
    }
}
