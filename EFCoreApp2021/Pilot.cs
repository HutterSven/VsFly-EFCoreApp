using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreApp2021
{
    public class Pilot
    {
        [Key]
        public int PilotID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public float Salary { get; set; }
        public int? FlightHours { get; set; }

        public virtual ICollection<Flight> FlightAsPilotSet { get; set; }
    }
}
