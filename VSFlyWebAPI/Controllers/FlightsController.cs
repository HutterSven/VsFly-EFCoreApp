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

namespace VSFlyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly WWWingsContext _context;

        public FlightsController(WWWingsContext context)
        {
            _context = context;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FlightM>>> GetFlightSet()
        {
            var flightList = await _context.FlightSet.ToListAsync();
            List<Models.FlightM> listFlightM = new List<FlightM>();
            foreach (Flight f in flightList)
            {
                var fM = f.ConvertToFlightM();
                listFlightM.Add(fM);
            }
            return listFlightM;
        }

        // GET: api/Flights/available
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<FlightM>>> GetAvailableFlightSet()
        {
            var flightList = await _context.FlightSet.ToListAsync();
            var bookingList = await _context.BookingSet.ToListAsync();
            List<Models.FlightM> listFlightM = new List<FlightM>();
            foreach (Flight f in flightList)
            {
                int count = 0;
                foreach (Booking b in bookingList)
                {
                    if (b.FlightNo == f.FlightNo) count++;
                }
                if (f.Seats > count)
                {
                    var fM = f.ConvertToFlightM();
                    listFlightM.Add(fM);
                }
            }
            return listFlightM;
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var flight = await _context.FlightSet.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }


        // PUT: api/Flights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            if (id != flight.FlightNo)
            {
                return BadRequest();
            }

            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Flights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(FlightM flight)
        {
            _context.FlightSet.Add(flight.ConvertToFlightEF());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlight", new { id = flight.FlightNo }, flight);
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var flight = await _context.FlightSet.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            _context.FlightSet.Remove(flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Flights/price/5
        [HttpGet("price/{id}")]
        public async Task<ActionResult<double>> GetPriceOfFlight(int id)
        {
            var bookingList = await _context.BookingSet.ToListAsync();
            var flightList = await _context.FlightSet.ToListAsync();
            List<Booking> listBooking = new List<Booking>();
            Flight flight = new Flight();
            foreach (Booking b in bookingList)
            {
                listBooking.Add(b);
            }
            foreach (Flight f in flightList)
            {
                if (f.FlightNo == id)
                {
                    flight = f;
                }
            }
            return CalcPrice(listBooking, flight);
        }

        private bool FlightExists(int id)
        {
            return _context.FlightSet.Any(e => e.FlightNo == id);
        }

        private double CalcPrice(List<Booking> bookings, Flight flight)
        {
            int count = 0;
            foreach (Booking b in bookings)
            {
                if (b.FlightNo == flight.FlightNo) count++;
            }
            if (count != 0 && count / flight.Seats > .8)
            {
                foreach (Booking b in bookings)
                { 
                    return flight.BasePrice * 1.5;
                }
            }
            if (count != 0 && count / flight.Seats < .2 && (flight.Date.Year == DateTime.Today.Year && flight.Date.Month - DateTime.Today.Month < 2))
            {
                foreach (Booking b in bookings)
                {
                    return flight.BasePrice * .8;
                }
            }
            if (count != 0 &&  count / flight.Seats < .5 && (flight.Date.Year == DateTime.Today.Year && flight.Date.Month - DateTime.Today.Month < 1))
            {
                foreach (Booking b in bookings)
                {
                    return flight.BasePrice * .7;
                }
            }
            return flight.BasePrice;
        }

    }
}
