using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EFCoreApp2021
{
    public class WWWingsContext : DbContext {
        public DbSet<Flight> FlightSet { get; set; }
        public DbSet<Pilot> PilotSet { get; set; }
        public DbSet<Passenger> PassengerSet { get; set; }
        public DbSet<Booking> BookingSet { get; set; }


        // SQL Express
        public static string ConnectionString { get; set; } = @"Server=(localDB)\MSSQLLocalDB;Database=WWWings_2021Step1;Trusted_Connection=True;App=EFCoreApp2021;MultipleActiveResultSets=true";

        public WWWingsContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            builder.UseSqlServer(ConnectionString);

            builder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            // composed key
            builder.Entity<Booking>().HasKey(x => new { x.FlightNo, x.PassengerID });

            // Entity Core 5.0, no more manual mapping !
            // mapping many to many relationship
            builder.Entity<Booking>()
                .HasOne(x => x.Flight)
                .WithMany(x => x.BookingSet)
                .HasForeignKey(x => x.FlightNo)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Booking>()
                .HasOne(x => x.Passenger)
                .WithMany(x => x.BookingSet)
                .HasForeignKey(x => x.PassengerID)
                .OnDelete(DeleteBehavior.Restrict);

        }


    }
}
