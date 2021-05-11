using System;
using System.Collections.Generic;
using System.Text;

namespace MotelBooking
{
    public class RoomType
    {
        public int BedCount { get; }
        public decimal RoomPrice { get; }

        public RoomType(int bedCount, decimal roomPrice)
        {
            this.BedCount = bedCount;
            this.RoomPrice = roomPrice;
        }



    }
}
