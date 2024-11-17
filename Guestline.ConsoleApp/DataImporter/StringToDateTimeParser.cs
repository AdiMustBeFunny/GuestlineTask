namespace Guestline.ConsoleApp.DataImporter
{
    public class StringToDateTimeParser
    {
        private static readonly int standardDateLength = 8;

        public DateTime Parse(string dateString)
        {
            if (dateString.Length == 0 || dateString.Length != standardDateLength)
            {
                throw new ArgumentException($"Date's length was expected to be {standardDateLength}, but was {dateString.Length}. Date: {dateString}");
            }

            bool yearSuccess = int.TryParse(dateString.Substring(0, 4), out int year);
            bool monthSuccess = int.TryParse(dateString.Substring(4, 2), out int month);
            bool daySuccess = int.TryParse(dateString.Substring(6, 2), out int day);

            if (!yearSuccess || !monthSuccess || !daySuccess)
            {
                throw new ArgumentException($"Failed to parse the date. Date: {dateString}");
            }

            return new DateTime(year, month, day);
        }
    }
}
