using System;
using System.Collections.Generic;
using System.Text;

namespace MotelBooking
{
    public class Booking
    {
        //Would normally store constants like this in the database
        public const decimal _pricePerPet = 20.00M;
        public const int MaxPets = 2;

        


        public string BookingName { get; set; }

        public Room BookingRoom { get; set; }

        private int numberOfPets;
        public int NumberOfPets { get { return numberOfPets; } set { numberOfPets = value;  if (value > 0) { BookingRoom.Level = RoomLevel.GROUND; } } }

        private bool handicap;
        public bool Handicap { get { return handicap; } set { handicap = value; if (value) { BookingRoom.Level = RoomLevel.GROUND; } } }

       
        public DateTime CheckInDate { get; set; }

        public DateTime CheckOutDate { get; set; }

        public bool CheckInDateIsValid { get { return DateIsValid(CheckInDate); } }

        public bool CheckOutDateIsValid { get { return DateIsValid(CheckOutDate) && CheckOutDate.Date > DateTime.Now.Date && CheckOutDate > CheckInDate; } }

        public Booking()
        {
            this.BookingRoom = new Room();
        }

        public Booking(string bookingName, int numberOfRooms, int numberOfPets, bool handicap, DateTime checkInDate, DateTime checkOutDate)
        {
            this.BookingName = bookingName;
            this.BookingRoom = new Room();

            RoomType roomType = null;
            new BookingApp().RoomTypes.TryGetValue(numberOfRooms, out roomType);

            this.BookingRoom.TypeOfRoom = roomType;
            
            this.NumberOfPets = numberOfPets;
            this.Handicap = handicap;
            this.CheckInDate = checkInDate;
            this.CheckOutDate = checkOutDate;

        }


        public Decimal GetTotalPrice()
        {
            //Failsafe to prevent errors from data not set
            if (!IsValid())
                return 0;

            return (NumberOfPets * _pricePerPet) + (Convert.ToDecimal((CheckOutDate - CheckInDate).TotalDays) * BookingRoom.TypeOfRoom.RoomPrice);
        }       

        public bool IsValid()
        {
            return !String.IsNullOrWhiteSpace(BookingName) && BookingRoom != null && BookingRoom.TypeOfRoom != null 
                && CheckInDateIsValid && CheckOutDateIsValid && NumberOfPets <= MaxPets;
        }

        private static bool DateIsValid(DateTime date)
        {
            return date != default(DateTime) && date.Date >= DateTime.Now.Date;
        }


    }
}
