using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EFCoreApp2021
{
    public class Booking
    {

        [Key]
        public int BookingID { get; set; }
        public int FlightNo { get; set; }  // declare those keys in WWWxxxxContext
        public int PassengerID { get; set; }
        public double Price { get; set; }
        public virtual Flight Flight { get; set; }
        public virtual Passenger Passenger { get; set; }
    }
}
