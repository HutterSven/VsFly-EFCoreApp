using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EFCoreApp2021
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var ctx = new WWWingsContext();

            var e = ctx.Database.EnsureCreated();

            if (e)
                Console.WriteLine("Database has been created.");
            else
                Console.WriteLine("Database already exists");

            Console.WriteLine("done.");

            // add a Pilot !
            Pilot p = new Pilot { Firstname = "Jean", Salary = 9000 };

            ctx.PilotSet.Add(p);

            ctx.SaveChanges();


            // try to display all the flights
            // entity framework
            // linq
            foreach (Flight flight in ctx.FlightSet) {
                Console.WriteLine("Date: {0} Destination: {1} Seats: {2}", flight.Date, flight.Destination, flight.Seats);
            }

            Console.WriteLine("======================");

            // some records, with criteria
            // LINQ
            var q = from flight in ctx.FlightSet
                    where flight.Seats > 100
                    && flight.Destination.StartsWith("L")
                    select flight;


            foreach (Flight flight in q)
                Console.WriteLine("Date: {0} Destination: {1} Seats: {2}", flight.Date, flight.Destination, flight.Seats);

            Console.WriteLine("======================");

            // another user adds a new flight / modifies a flight

            foreach (Flight flight in q)
                Console.WriteLine("Date: {0} Destination: {1} Seats: {2}", flight.Date, flight.Destination, flight.Seats);

            Console.WriteLine("======================");

            // Lambda
            var l = ctx.FlightSet.Where(f => f.Seats > 100 && f.Destination.StartsWith("L"));

            foreach (Flight flight in l)
                Console.WriteLine("Date: {0} Destination: {1} Seats: {2}", flight.Date, flight.Destination, flight.Seats);

            Console.ReadKey();

            // add a flight !!!
            Flight n = new Flight { Date = DateTime.Now, Departure = "NNN", Destination = "NNN", Seats = 101, PilotId = 1 };

            //n.PilotId = 1;
            //n.Pilot = p;

            ctx.FlightSet.Add(n);

            Flight m;

            for (int i = 0; i <= 10; i++)
            {
                m = new Flight();
                m.BasePrice = 100 * (i + 1);
                m.Date = DateTime.Now;
                m.Departure = "ZRH";
                m.Destination = "BER";
                m.PilotId = 1;
                m.Seats = 100;
                ctx.FlightSet.Add(m);
                ctx.SaveChanges();
            }

            ctx.SaveChanges();

            Console.WriteLine("======================");

            Console.WriteLine(n.FlightNo);

            Console.WriteLine("======================");

            foreach (Flight flight in ctx.FlightSet)
            {
                Console.WriteLine("Date: {0} Destination: {1} Seats: {2}", flight.Date, flight.Destination, flight.Seats);
            }


            // delete the fligntno > 3 (keep only the manually added records)
            Console.WriteLine("======================");


            // it's another query technology : ENTITY SQL !!!
            int r = ctx.Database.ExecuteSqlRaw("DELETE FROM FlightSet WHERE FlightNo > 7");

            Console.WriteLine("Lines: {0}", r);


            // update a flight, Seat ... Seat + 1



            // uppdate Seats+1 for a flight
            Flight updatable = ctx.FlightSet.Find(2);

            if (updatable != null)
            {
                updatable.Seats += 1;

                ctx.SaveChanges();
            }

            Console.WriteLine("======================");


            // same as before, with a JOIN !!!!
            var q4 = from f in ctx.FlightSet.Include(x => x.Pilot)
                     select f;


            foreach (Flight flight in q4) {
                Console.WriteLine("{0} {1} {2} {3}", flight.Date, flight.Departure, flight.Pilot.Firstname, flight.Pilot.Salary);
            }

            // 0. some refactoring as well 

            // 1. FlightSet ... start the query with Pilot ... 
            Console.WriteLine("======================");

            // joins / include !
            var q5 = from pilot in ctx.PilotSet.Include(x => x.FlightAsPilotSet)
                     select pilot;

            foreach (Pilot pilot in q5)
            {
                //ctx.Entry(pilot).Collection(x => x.FlightAsPilotSet).Load();

                Console.WriteLine("{0} {1} {2} {3}", pilot.Firstname, pilot.Salary, pilot.FlightHours, pilot.FlightAsPilotSet.Count());

                foreach (Flight flight in pilot.FlightAsPilotSet)
                    Console.WriteLine("- {0} {1} {2}", flight.Date, flight.Destination, flight.Seats);
            }

            // 2. add many to many ... Booking

            // add Passenger(s)
            Passenger passenger1 = new Passenger { Lastname = "Dubosson", Firstname = "Pierre" };

            Passenger passenger2 = new Passenger { Lastname = "Stuck", Firstname = "Peter" };

            ctx.PassengerSet.Add(passenger1);

            ctx.PassengerSet.Add(passenger2);

            ctx.SaveChanges();

            Passenger passenger;

            for (int i = 0; i <= 10; i++)
            {
                passenger = new Passenger();
                passenger.Firstname = "Hans";
                passenger.Lastname = "Luc";
                ctx.PassengerSet.Add(passenger);
                ctx.SaveChanges();
            }

            Booking booking;

            for (int i = 0; i <= 10; i++)
            {
                booking = new Booking();
                booking.FlightNo = i+1;
                booking.PassengerID = i+1;
                booking.Price = 100;
                ctx.BookingSet.Add(booking);
                ctx.SaveChanges();
            }


            Console.ReadKey();
        }
    }
}
