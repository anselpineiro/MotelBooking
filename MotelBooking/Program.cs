using System;

namespace MotelBooking
{
    class Program
    {
        static void Main(string[] args)
        {

            RunManualTest();
            //RunInvalidDateTest();
            //RunInvalidRoomCount();
            //RunInvalidPetCount();
            //RunValidThreeRoomsOneNightWithPets();
            //RunValidTwoRoomsOneNightWithPets();
            //RunValidOneRoomTwoNightsNoPets();
            //RunValidOneRoomOneNightsHandicap();
        }





        private static void RunInvalidDateTest()
        {
            BookingApp bookingApp = new BookingApp();
            Booking booking = new Booking("Invalid Date Test", 1, 2, false, DateTime.Now.AddDays(-1), DateTime.Now);

            Console.WriteLine(bookingApp.GetBookingReport(booking));
        }


        private static void RunInvalidRoomCount()
        {
            BookingApp bookingApp = new BookingApp();
            Booking booking = new Booking("Invalid Room Count Test", 4, 2, false, DateTime.Now, DateTime.Now.AddDays(1));

            Console.WriteLine(bookingApp.GetBookingReport(booking));
        }

        private static void RunInvalidPetCount()
        {
            BookingApp bookingApp = new BookingApp();
            Booking booking = new Booking("Invalid Pet Count Test", 3, 3, false, DateTime.Now, DateTime.Now.AddDays(1));

            Console.WriteLine(bookingApp.GetBookingReport(booking));
        }

        private static void RunManualTest()
        {
            BookingApp bookingApp = new BookingApp();

            Booking booking = new Booking();

            Console.WriteLine("Enter name on booking:");
            booking.BookingName = Console.ReadLine();

            booking.BookingRoom.TypeOfRoom = GetRoomType(bookingApp);

            booking.NumberOfPets = GetNumberOfPets();
            booking.Handicap = GetHandicapNeeds();

            booking.CheckInDate = GetDate("Enter checkin date (MM/dd/yyyy)");
            booking.CheckOutDate = GetDate("Enter checkout date (MM/dd/yyyy)");

            Console.WriteLine(bookingApp.GetBookingReport(booking));
        }


        private static void RunValidThreeRoomsOneNightWithPets()
        {
            BookingApp bookingApp = new BookingApp();
            Booking booking = new Booking("Valid One Night With Pets Test", 3, 2, false, DateTime.Now, DateTime.Now.AddDays(1));

            Console.WriteLine(bookingApp.GetBookingReport(booking));
        }

        private static void RunValidTwoRoomsOneNightWithPets()
        {
            BookingApp bookingApp = new BookingApp();
            Booking booking = new Booking("Valid Two Rooms One Night With Pets Test", 2, 2, false, DateTime.Now, DateTime.Now.AddDays(1));

            Console.WriteLine(bookingApp.GetBookingReport(booking));
        }

        private static void RunValidOneRoomTwoNightsNoPets()
        {
            BookingApp bookingApp = new BookingApp();
            Booking booking = new Booking("Valid One Room Two Nights No Pets Test", 2, 0, false, DateTime.Now, DateTime.Now.AddDays(2));

            Console.WriteLine(bookingApp.GetBookingReport(booking));
        }


        private static void RunValidOneRoomOneNightsHandicap()
        {
            BookingApp bookingApp = new BookingApp();
            Booking booking = new Booking("Valid Handicap Test", 2, 0, true, DateTime.Now, DateTime.Now.AddDays(2));

            Console.WriteLine(bookingApp.GetBookingReport(booking));
        }
        private static RoomType GetRoomType(BookingApp bookingApp)
        {

            int numberOfRooms = 0;
            Console.WriteLine("Enter number of beds (up to 3):");
            Int32.TryParse(Console.ReadLine(), out numberOfRooms);

            RoomType roomType = null;
            bookingApp.RoomTypes.TryGetValue(numberOfRooms, out roomType);

            return roomType;
        }

        private static int GetNumberOfPets()
        {
            int numberOfPets = 0;
            Console.WriteLine("Enter number of pets:");
            Int32.TryParse(Console.ReadLine(), out numberOfPets);

            return numberOfPets;
        }

        private static bool GetHandicapNeeds()
        {
            Console.WriteLine("Do you need handicap accessibility(y/n)?");

            return Console.ReadLine().Trim().ToLower() == "y";
        }

        private static DateTime GetDate(string message)
        {
            bool validDate = false;
            DateTime dateTime = DateTime.MinValue;
            while (!validDate)
            {
                Console.WriteLine(message);


                if (DateTime.TryParse(Console.ReadLine(), out dateTime))
                    validDate = true;
                else
                    Console.WriteLine("Invalid date format.  Please try again.");
            }

            return dateTime;
        }

    }
}
