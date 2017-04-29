using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
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
            try
            {
                var result = _repo.GetAllTrips();
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(result));
            }
            catch (Exception ex)
            {
                //Loggin

                return BadRequest(ex);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                //Save to the db
                var newTrip = Mapper.Map<Trip>(theTrip);
                _repo.AddTrip(newTrip);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
                else
                {
                    return BadRequest("Failed to save changes in db");
                }
            }
            return BadRequest("Bad Data");
        }
    }
}
