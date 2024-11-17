using Guestline.ConsoleApp.Entities;

namespace Guestline.ConsoleApp.DataImporter
{
    public class HotelDataSet
    {
        public HotelDataSet(IEnumerable<Hotel> hotels, IEnumerable<Booking> bookings)
        {
            Hotels = hotels;
            Bookings = bookings;
        }

        public IEnumerable<Hotel> Hotels { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }
    }
}
