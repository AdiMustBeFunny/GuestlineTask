using System.Runtime.Serialization;

namespace Guestline.ConsoleApp.Entities
{
    public class Booking
    {
        public string HotelId { get; set; }
        
        public DateTime Arrival { get; set; }
        
        public DateTime Departure { get; set; }
        
        public string RoomType { get; set; }

        public RoomRate RoomRate { get; set; }
    }
}
