using Guestline.ConsoleApp.BusinessLogic;
using Guestline.ConsoleApp.ConsoleParameters;
using Guestline.ConsoleApp.DataImporter;
using Guestline.ConsoleApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guestline.Tests
{
    public class HotelServiceTests
    {
        private readonly HotelService _service;
        private readonly HotelDataSet _hotelDataSet;

        public HotelServiceTests()
        {
            _hotelDataSet = new HotelDataSet([
                    new Hotel(){
                        Id = "H1",
                        Name = "Hotel California",
                        RoomTypes = [
                            new RoomType(){
                                Code = "SGL",
                                Description = "Single Room",
                                Amenities = [],
                                Features = [],
                            },
                            new RoomType(){
                                Code = "DBL",
                                Description = "Double Room",
                                Amenities = [],
                                Features = [],
                            },
                        ],
                        Rooms = [
                            new Room(){
                                RoomType = "SGL",
                                RoomId = "101"
                            },
                            new Room(){
                                RoomType = "SGL",
                                RoomId = "102"
                            },
                            new Room(){
                                RoomType = "DBL",
                                RoomId = "201"
                            },
                            new Room(){
                                RoomType = "DBL",
                                RoomId = "202"
                            },
                        ]
                    }
                ],
                [
                    new Booking(){
                        HotelId = "H1",
                        Arrival = new DateTime(2024,9,1),
                        Departure = new DateTime(2024,9,3),
                        RoomType = "DBL",
                        RoomRate = RoomRate.Prepaid
                    },
                    new Booking(){
                        HotelId = "H1",
                        Arrival = new DateTime(2024,9,1),
                        Departure = new DateTime(2024,9,3),
                        RoomType = "DBL",
                        RoomRate = RoomRate.Prepaid
                    },
                    new Booking(){
                        HotelId = "H1",
                        Arrival = new DateTime(2024,9,1),
                        Departure = new DateTime(2024,9,3),
                        RoomType = "DBL",
                        RoomRate = RoomRate.Prepaid
                    },
                    new Booking(){
                        HotelId = "H1",
                        Arrival = new DateTime(2024,9,2),
                        Departure = new DateTime(2024,9,5),
                        RoomType = "SGL",
                        RoomRate = RoomRate.Standard
                    },
                ]);

            _service = new(_hotelDataSet);
        }

        public static IEnumerable<object[]> RoomTypeOverbookedData
        {
            get
            {
                yield return new object[] { "H1", "DBL", new DateTime(2024, 9, 2), null };
                yield return new object[] { "H1", "DBL", new DateTime(2024, 8, 30), new DateTime(2024, 9, 2) };
                yield return new object[] { "H1", "DBL", new DateTime(2024, 9, 2), new DateTime(2024, 9, 4) };
            }
        }

        [Theory]
        [MemberData(nameof(RoomTypeOverbookedData), MemberType = typeof(HotelServiceTests))]
        public void GetRoomAvailabilityCount_RoomTypeOverbooked_ShouldReturnNegativeNumber(string hotelId, string roomType, DateTime arrival, DateTime? departure)
        {
            //act
            var availabilityCount = _service.GetRoomAvailabilityCount(hotelId, roomType, arrival, departure);

            //assert
            Assert.True(availabilityCount < 0);
        }

        public static IEnumerable<object[]> RoomTypeAvailableData
        {
            get
            {
                yield return new object[] { "H1", "DBL", new DateTime(2024, 9, 10), null };
                yield return new object[] { "H1", "DBL", new DateTime(2024, 9, 3), null };
                yield return new object[] { "H1", "DBL", new DateTime(2024, 9, 3), new DateTime(2024, 9, 5) };
            }
        }

        [Theory]
        [MemberData(nameof(RoomTypeAvailableData), MemberType = typeof(HotelServiceTests))]
        public void GetRoomAvailabilityCount_RoomTypeFullyAvailable_ShouldReturnNumberEqualToRoomCountWithGivenType(string hotelId, string roomType, DateTime arrival, DateTime? departure)
        {
            //arrange
            var roomCountWithGivenType = _hotelDataSet.Hotels.First(h => h.Id == hotelId).Rooms.Where(r => r.RoomType == roomType).Count();

            //act
            var availabilityCount = _service.GetRoomAvailabilityCount(hotelId, roomType, arrival, departure);

            //assert
            Assert.True(availabilityCount == roomCountWithGivenType);
        }

    }
}
