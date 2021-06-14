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
    public class PassengerController : Controller
    {
        private readonly WWWingsContext _context;

        public PassengerController(WWWingsContext context)
        {
            _context = context;
        }


        // POST: api/Passenger
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PassengerM>> PostPassenger(PassengerM passenger)
        {
            EFCoreApp2021.Passenger passengerTemp = new EFCoreApp2021.Passenger();

            List<EFCoreApp2021.Passenger> allPassengers = _context.PassengerSet.ToList();

            foreach(Passenger p in allPassengers)
            {
                if (passenger.Firstname.Contains(p.Firstname) && passenger.Lastname.Contains(p.Lastname))
                {
                    passenger.PassengerID = p.PassengerID;
                    passengerTemp = p;
                    break;
                }
            }
            if (passengerTemp.Firstname == null)
            {
                passengerTemp =  passenger.ConvertToPassengerEF();
                _context.PassengerSet.Add(passengerTemp);
                await _context.SaveChangesAsync();
                passenger.PassengerID = passengerTemp.PassengerID;
            }                                

            return passenger;
        }


    }
}
