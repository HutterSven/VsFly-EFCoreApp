using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreApp2021
{
    public class Booking
    {

        [Key]
        public int BookingID { get; set; }
        [ForeignKey("FlightNo")]
        public int FlightNo { get; set; }
        [ForeignKey("PassengerID")]
        public int PassengerID { get; set; }
        public double Price { get; set; }

    }
}
