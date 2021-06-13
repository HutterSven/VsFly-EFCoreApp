using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EFCoreApp2021
{
    public class Passenger:Person
    {
        [Key]
        public int PassengerID { get; set; }

        // not finished
        // Flight <---- Booking ----> Passenger
        public virtual ICollection<Booking> BookingSet { get; set; }
    }
}
