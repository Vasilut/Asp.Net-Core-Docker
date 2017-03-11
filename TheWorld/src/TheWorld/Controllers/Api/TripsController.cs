using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repo;

        public TripsController(IWorldRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllTrips());
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]Trip theTrip)
        {
            return Ok(true);
        }
    }
}
