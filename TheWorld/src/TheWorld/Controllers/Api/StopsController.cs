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
    [Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private IWorldRepository _repo;

        public StopsController(IWorldRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            //http://localhost:9701/api/trips/US%20Trip/stops
            try
            {
                var trip = _repo.GetTripByName(tripName,User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(x => x.Order).ToList()));
            }
            catch (Exception ex)
            {
                //Log error.
            }

            return BadRequest("Failed to get data");
        }

        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody] StopViewModel modelStop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(modelStop);
                    _repo.AddStop(tripName, User.Identity.Name, newStop);


                    if (await _repo.SaveChangesAsync())
                    {
                        return Created($"/api/trips/{tripName}/stops/{newStop.Name}",
                            Mapper.Map<StopViewModel>(newStop));
                    }
                }
                else
                {
                    return BadRequest("Failed to Save stop to database");
                }
            }
            catch (Exception ex)
            {
                //log error
            }

            return BadRequest("Failed to save a new stop");
        }

    }
}
