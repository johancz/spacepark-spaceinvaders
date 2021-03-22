using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceInvaders.Objects;
using SpaceInvaders.Traveller;

namespace SpaceInvaders.Database
{
    public static class DatabaseQueries
    {
        public static void AddParkingToDB(Person person, Starships ship)
        {
            using (var db = new MyContext())
            {
                var parking = new Parking()
                {
                    Traveller = person.Name,
                    StarShip = ship.Name,
                };

                db.Parkings.Add(parking);
                db.SaveChanges();

                Console.WriteLine("Your parking has started! Enjoy you stay at SpacePark.");
                Console.WriteLine($"Traveller: {parking.Traveller}, Starship: {parking.StarShip}, StartTime: {parking.StartTime}");
            }
        }
    }
}
