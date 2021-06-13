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
            EFCoreApp2021.Passenger passengerTemp = passenger.ConvertToPassengerEF();
            _context.PassengerSet.Add(passengerTemp);
            await _context.SaveChangesAsync();

            passenger.PassengerID = passengerTemp.PassengerID;

            return passenger;
        }


    }
}
