using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        void AddTrip(Trip trip);
        Task<bool> SaveChangesAsync();
        IEnumerable<Stop> GetAllStops();
        Trip GetTripByName(string tripName, string userName);
        void AddStop(string tripName, string userName, Stop newStop);
        IEnumerable<Trip> GetUserTripsWithStops(string name);
    }
}