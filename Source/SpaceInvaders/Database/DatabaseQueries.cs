using SpaceInvaders.Objects;
using System;
using System.Linq;
using System.Threading;

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
                db.SaveChanges();
                Console.WriteLine($"\n[PARKING DETAILS]\nTraveller: {parking.Traveller}, Starship: {parking.StarShip}, StartTime: {parking.StartTime}");
            }
        }

        public static Parking CheckParking(string name)
        {
            Parking activParking = null;
            using (var db = new MyContext())
            {
                activParking = db.Parkings.SingleOrDefault(x => x.Traveller == name && x.EndTime == null);
            }

            return activParking;
        }

        public static int OccupiedParkings()
        {
            using (var db = new MyContext())
            {
                return db.Parkings.Where(x => x.EndTime == null).Count();
            }
        }

        public static void EndParking(Person person)
        {
            using (var db = new MyContext())
            {
                var endParking = db.Parkings.SingleOrDefault(x => x.Traveller == person.Name && x.EndTime == null);

                if (endParking == null || endParking.EndTime != null)
                {
                    Console.WriteLine("You have no active parkings.");
                    return;
                }
                endParking.EndTime = DateTime.Now;

                var duration = endParking.EndTime - endParking.StartTime;
                if (duration.HasValue)
                {
                    endParking.TotalSum = Convert.ToDecimal(duration.Value.TotalMinutes) * 2; // cost = 2 credits / minute
                }
                db.SaveChanges();

                Console.WriteLine("Calculating price..\n");
                Thread.Sleep(2000);
                Console.WriteLine($"Start time: {endParking.StartTime}\nEnd time: {endParking.EndTime}");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Total price: " + Math.Round(endParking.TotalSum.Value, 2) + " credits\n");
                Console.ResetColor();
                Thread.Sleep(2000);
            }
        }
    }
}
