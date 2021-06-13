using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCoreApp2021
{
    public class Person
    {
        [Key]
        public int PersonID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
