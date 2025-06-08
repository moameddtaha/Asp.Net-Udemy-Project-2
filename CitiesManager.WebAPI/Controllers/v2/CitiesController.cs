using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.WebAPI.DatabaseContext;
using CitiesManager.WebAPI.Modles;

namespace CitiesManager.WebAPI.Controllers.v2
{
    //[Route("api/[controller]")]
    //[ApiController]
    [ApiVersion("2.0")]
    public class CitiesController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        /// <summary>
        /// To get list of cities (only the city name) from 'cities' table
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/xml")]
        public async Task<ActionResult<IEnumerable<string?>>> GetCities()
        {
            var cities = await _context.Cities
                .OrderBy(c => c.CityName) // Sort cities by name
                .Select(c => c.CityName) // Select only the city names
                .ToListAsync();

            return cities;
        }
    }
}
