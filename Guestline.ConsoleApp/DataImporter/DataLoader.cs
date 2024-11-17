using Guestline.ConsoleApp.Entities;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Guestline.ConsoleApp.DataImporter
{
    public class DataLoader
    {
        private readonly JsonSerializerOptions _serializeOptions;
        public DataLoader()
        {
            _serializeOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };

            _serializeOptions.Converters.Add(new DateTimeConverterUsingDateTimeParse());
            _serializeOptions.Converters.Add(new JsonStringEnumConverter());
        }

        public HotelDataSet LoadData(string hotelsFilePath, string bookingsFilePath)
        {
            IEnumerable<Hotel> hotels = ReadJsonArray<Hotel>(hotelsFilePath);
            IEnumerable<Booking> bookings = ReadJsonArray<Booking>(bookingsFilePath);

            return new(hotels, bookings);
        }

        private IEnumerable<TData> ReadJsonArray<TData>(string filePath)
        {
            IEnumerable<TData> data;
            var fileExists = File.Exists(filePath);

            if (!fileExists)
            {
                throw new ArgumentException($"File doesnt exist. Path: {filePath}");
            }

            try
            {
                var json = File.ReadAllText(filePath);
                data = JsonSerializer.Deserialize<IEnumerable<TData>>(json, _serializeOptions) ?? [];
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to process json file. Path = {filePath}, Exception Message = {ex.Message}");
            }

            return data;
        }
    }
}
