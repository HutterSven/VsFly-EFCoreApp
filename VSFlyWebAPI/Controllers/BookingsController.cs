using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFCoreApp2021;
using VSFlyWebAPI.Models;
using VSFlyWebAPI.extensions;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace VSFlyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : Controller
    {
        private readonly WWWingsContext _context;

        public BookingsController(WWWingsContext context)
        {
            _context = context;
        }

        // GET: api/Bookings/flightRevenue/2
        [HttpGet("flightRevenue/{FlightNo}")]
        public async Task<ActionResult<double>> GetTotalPriceOfFlight(int FlightNo)
        {
            var bookingList = await _context.BookingSet.ToListAsync();
            double totalPrice = 0;
            foreach (Booking b in bookingList)
            {
                if (b.FlightNo == FlightNo)
                {
                    totalPrice += b.Price;                
                }
            }
            return totalPrice;
        }

        // GET: api/Bookings/avg/ZRH
        [HttpGet("avg/{dest}")]
        public async Task<ActionResult<double>> GetAveragePriceOfDest(string dest)
        {
            var bookingList = await _context.BookingSet.ToListAsync();
            var flightList = await _context.FlightSet.ToListAsync();
            double avgPrice = 0;
            int bookingsCount = 0;
            foreach (Booking b in bookingList)
            {
                foreach (Flight f in flightList)
                {
                    if (b.FlightNo == f.FlightNo && f.Destination == dest)
                    {
                        avgPrice += b.Price;
                        bookingsCount++;
                    }
                }
                
            }
            avgPrice /= bookingsCount;
            return avgPrice;
        }

        // GET: api/Bookings/byDest/ZRH
        [HttpGet("byDest/{dest}")]
        public async Task<ActionResult<IEnumerable<ByDestM>>> GetByDestSet(string dest)
        {
            var bookingList = await _context.BookingSet.ToListAsync();
            var flightList = await _context.FlightSet.ToListAsync();
            var passengerList = await _context.PassengerSet.ToListAsync();
            List<Models.ByDestM> listByDestM = new List<ByDestM>();
            foreach (Booking b in bookingList)
            {
                foreach (Flight f in flightList)
                {
                    if(b.FlightNo == f.FlightNo && f.Destination == dest)
                    {
                        foreach (Passenger p in passengerList)
                        {
                            if (b.PassengerID == p.PassengerID)
                            {
                                var bDM = b.ConvertToDestM(f, p);
                                listByDestM.Add(bDM);
                            }
                        }
                    }
                }
            }
            return listByDestM;
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookingM>> PostBooking(BookingM booking)
        {
            _context.BookingSet.Add(booking.ConvertToBookingEF());
            await _context.SaveChangesAsync();

            return null;
        }





    }
}
