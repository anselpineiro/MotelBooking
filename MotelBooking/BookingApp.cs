using System;
using System.Collections.Generic;
using System.Text;

namespace MotelBooking
{
    public class BookingApp
    {
        private Dictionary<int, RoomType> _roomTypes;

        public Dictionary<int, RoomType> RoomTypes => _roomTypes;

        public BookingApp()
        {
            PopulateRoomTypes();
        }

        private void PopulateRoomTypes()
        {
            //Hard coded to avoid need for database.  A proper app would pull this from the database
            _roomTypes = new Dictionary<int, RoomType>();

            _roomTypes.Add(1, new RoomType(1, 50));
            _roomTypes.Add(2, new RoomType(2, 75));
            _roomTypes.Add(3, new RoomType(3, 90));
        }

        public string GetBookingReport(Booking booking)
        {
            return booking.IsValid() ? GenerateValidBookingReport(booking) : GenerateInvalidBookingReport(booking);
        }


        private string GenerateValidBookingReport(Booking booking)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Format("Reserved For: {0}\r\n", booking.BookingName));
            builder.Append(booking.BookingRoom.Level == RoomLevel.GROUND ? "Ground Floor\r\n" : "Uppper Level\r\n");
            builder.Append(string.Format("{0} bed(s)\r\n", booking.BookingRoom.TypeOfRoom.BedCount));
            builder.Append(string.Format("Check In Date: {0}\r\n", booking.CheckInDate.ToString("MMMM dd yyyy")));
            builder.Append(string.Format("Check Out Date: {0}\r\n", booking.CheckOutDate.ToString("MMMM dd yyyy")));
            
            if (booking.NumberOfPets > 0)
                builder.Append(string.Format("Number of Pets: {0}\r\n", booking.NumberOfPets));

            builder.Append(string.Format("Total Price: ${0}\r\n", booking.GetTotalPrice().ToString("#.##")));

            return builder.ToString();
        }

        private string GenerateInvalidBookingReport(Booking booking)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Problems with you booking. See reasons below.\r\n");

            if (String.IsNullOrWhiteSpace(booking.BookingName))
                builder.Append("No name entered on booking.");

            if (booking.NumberOfPets > Booking.MaxPets)
                builder.Append(string.Format("Maximum pets allowed is {0}. Value entered {1}\r\n", Booking.MaxPets, booking.NumberOfPets));

            if (booking.BookingRoom.TypeOfRoom == null)
                builder.Append(string.Format("Invalid number of beds.  Must be between 1 and 3.\r\n"));

            if (!booking.CheckInDateIsValid)
            {
                if (booking.CheckInDate == default(DateTime))
                    builder.Append("A check in date has not been set.\r\n");
                else if (booking.CheckInDate.Date < DateTime.Now.Date)
                    builder.Append("Check in date cannot be before today.\r\n");
                else
                    builder.Append("An unspecified error with the checkin date has occured.\r\n");
            }

            if (!booking.CheckOutDateIsValid)
            {
                if (booking.CheckOutDate == default(DateTime))
                    builder.Append("Check out date has not been set.\r\n");
                else if (booking.CheckOutDate.Date <= DateTime.Now.Date)
                    builder.Append("Check out date must be after tomorrow or later.\r\n");
                else if (booking.CheckOutDate < booking.CheckInDate)
                    builder.Append("Check out date cannot be before the checkin date.\r\n");
                else
                    builder.Append("An unspecified error with the check out date has occured.\r\n");
            }

            return builder.ToString();
        }
    }
}
