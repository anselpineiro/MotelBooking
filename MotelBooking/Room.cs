using System;
using System.Collections.Generic;
using System.Text;

namespace MotelBooking
{
    public class Room
    {
        public RoomType TypeOfRoom { get; set; }
        public RoomLevel Level { get; set; }

        public Room()
        {
            //Default to upper level
            this.Level = RoomLevel.UPPER;
        }
    }
}
