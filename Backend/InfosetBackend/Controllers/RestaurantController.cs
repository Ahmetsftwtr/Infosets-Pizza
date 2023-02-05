using InfosetBackend.Context;
using InfosetBackend.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace InfosetBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RestaurantController : ControllerBase
    {
        private readonly DatabaseContext _db;

        public RestaurantController(DatabaseContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IEnumerable<Resturants> GetRestaurant(double latitude, double longitude, int maxdistance, int count)
        {
            const double kmsToMilesFactor = 1.609344;

            IEnumerable<Resturants> restaurants = _db.restaurant_branches.ToList();
            return restaurants
                .Where(restaurant =>
                {
                    double theta = longitude - restaurant.Longitude;
                    double distance = 60 * 1.1515 * (180 / Math.PI) * Math.Acos(
                        Math.Sin(latitude * (Math.PI / 180)) * Math.Sin(restaurant.Latitude * (Math.PI / 180)) +
                        Math.Cos(latitude * (Math.PI / 180)) * Math.Cos(restaurant.Latitude * (Math.PI / 180)) * Math.Cos(theta * (Math.PI / 180))
                    );

                    return Math.Round(distance * kmsToMilesFactor, 2) < maxdistance;
                })
                .Take(count)
                .ToList();
        }
    }
}