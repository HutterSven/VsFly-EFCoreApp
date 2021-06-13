using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSFlyWebAPI.Models
{
    public class BookingM
    {
        public int FlightNo { get; set; }  // declare those keys in WWWxxxxContext
        public int PassengerID { get; set; }
        public double Price { get; set; }
    }
}
