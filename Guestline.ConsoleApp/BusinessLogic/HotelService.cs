using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Guestline.ConsoleApp.DataImporter;

namespace Guestline.ConsoleApp.BusinessLogic
{
    public class HotelService
    {
        private readonly HotelDataSet hotelData;

        public HotelService(HotelDataSet hotelData)
        {
            this.hotelData = hotelData;
        }

        public int GetRoomAvailabilityCount(string hotelId, string roomType, DateTime arriveDate, DateTime? departureDate)
        {
            var hotel = hotelData.Hotels.FirstOrDefault(h => h.Id == hotelId);

            if (hotel == null)
            {
                throw new ArgumentException($"No hotel with id: {hotelId}");
            }

            var roomCount = hotel.Rooms.Where(r => r.RoomType == roomType).Count();
            var roomTypeBookings = hotelData.Bookings.Where(b => b.HotelId == hotelId && b.RoomType == roomType);
            int bookingsCount;

            if (departureDate == null)
            {
                bookingsCount = roomTypeBookings.Where(b => b.Departure > arriveDate && arriveDate >= b.Arrival).Count();
            }
            else
            {
                bookingsCount = roomTypeBookings.Where(b => b.Departure > arriveDate && departureDate > b.Arrival).Count();
            }

            return roomCount - bookingsCount;
        }
    }
}
