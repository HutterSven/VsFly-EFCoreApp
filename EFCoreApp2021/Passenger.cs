using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EFCoreApp2021
{
    public class Passenger
    {
        [Key]
        public int PassengerID { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public virtual ICollection<Booking> BookingSet { get; set; }
    }
}
