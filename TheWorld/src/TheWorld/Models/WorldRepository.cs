using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;

        public WorldRepository(WorldContext context)
        {
            _context = context;
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            return _context.Trips.ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public IEnumerable<Stop> GetAllStops()
        {
            return _context.Stops.ToList();
        }

        public Trip GetTripByName(string tripName,string userName)
        {
            return _context.Trips
                   .Include(t => t.Stops)
                   .Where(x => x.Name == tripName && x.UserName == userName).FirstOrDefault();
        }

        public void AddStop(string tripName, string userName, Stop newStop)
        {
            var trip = GetTripByName(tripName,userName);
            if(trip != null)
            {
                trip.Stops.Add(newStop);
                _context.Stops.Add(newStop);
            }
        }

        public IEnumerable<Trip> GetUserTripsWithStops(string name)
        {
            try
            {
                return _context.Trips
                    .Include(t => t.Stops)
                    .OrderBy(t => t.Name)
                    .Where(t => t.UserName == name)
                    .ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
