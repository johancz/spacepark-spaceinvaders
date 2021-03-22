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
        public static void AddParking(Person person, Starships ship)
        {


            using (var db = new MyContext())
            {
                var parking = new Parking()
                {
                    Traveller = person.Name,
                    StarShip = ship.Name,
                };

                db.Parkings.Add(parking);
                //db.SaveChanges();

                Console.WriteLine($"\n[PARKING DETAILS]\nTraveller: {parking.Traveller}, Starship: {parking.StarShip}, StartTime: {parking.StartTime}");
            }
        }

        public static Parking CheckParking(string name)
        {
            using (var db = new MyContext())
            {
                 var activParking = db.Parkings.SingleOrDefault(x => x.Traveller == name && x.EndTime == null);

                 return activParking;
            }
        }

        public static void EndParking(Person person, Starships ship)
        {

        }
    }
}
