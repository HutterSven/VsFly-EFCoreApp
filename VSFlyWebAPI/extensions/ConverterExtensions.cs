using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSFlyWebAPI.extensions
{
    public static class ConverterExtensions
    {
        public static Models.FlightM ConvertToFlightM (this EFCoreApp2021.Flight f)
        {
            Models.FlightM fM = new Models.FlightM();
            fM.Date = f.Date;
            fM.Departure = f.Departure;
            fM.Destination = f.Destination;
            fM.FlightNo = f.FlightNo;
            return fM;

        }

        public static EFCoreApp2021.Flight ConvertToFlightEF (this Models.FlightM f)
        {
            EFCoreApp2021.Flight fM = new EFCoreApp2021.Flight();
            fM.Date = f.Date;
            fM.Departure = f.Departure;
            fM.Destination = f.Destination;
            fM.FlightNo = f.FlightNo;
            return fM;
        }

        public static EFCoreApp2021.Booking ConvertToBookingEF(this Models.BookingM b)
        {
            EFCoreApp2021.Booking bEF = new EFCoreApp2021.Booking();
            bEF.FlightNo = b.FlightNo;
            bEF.PassengerID = b.PassengerID;
            bEF.Price = b.Price;

            return bEF;
        }

        public static Models.BookingM ConvertToBookingM(this EFCoreApp2021.Booking b)
        {
            Models.BookingM bM = new Models.BookingM();
            bM.FlightNo = b.FlightNo;
            bM.PassengerID = b.PassengerID;
            bM.Price = b.Price;
            return bM;
        }

        public static Models.ByDestM ConvertToDestM(this EFCoreApp2021.Booking b, EFCoreApp2021.Flight f, EFCoreApp2021.Passenger p)
        {
            Models.ByDestM bDM = new Models.ByDestM();
            bDM.Firstname = p.Firstname;
            bDM.Lastname = p.Lastname;
            bDM.FlightNo = f.FlightNo;
            bDM.Price = b.Price;
            return bDM;
        }

        public static Models.PassengerM ConvertToPassengerM(this EFCoreApp2021.Passenger p)
        {
            Models.PassengerM pM = new Models.PassengerM();
            pM.PassengerID = p.PassengerID;
            pM.Firstname = p.Firstname;
            pM.Lastname = p.Lastname;
            return pM;
        }

        public static EFCoreApp2021.Passenger ConvertToPassengerEF(this Models.PassengerM p)
        {
            EFCoreApp2021.Passenger pEF = new EFCoreApp2021.Passenger();
            pEF.PassengerID = p.PassengerID;
            pEF.Firstname = p.Firstname;
            pEF.Lastname = p.Lastname;
            return pEF;
        }
    }
}
