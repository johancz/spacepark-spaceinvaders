using SpaceInvaders.API;
using SpaceInvaders.Database;
using SpaceInvaders.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;

namespace SpaceTest
{
    public class UnitTest1
    {

        [Fact]
        public void When_Searching_Contains_SearchWord()
        {
            var result = Fetch.People("Sky");

            Assert.Equal(3, result.Result.Count);
        }

        [Fact]
        public void When_Calling_FetchStarship_Expect_All_Starships_OfType_Result()
        {
            var result = Fetch.Starships();

            Assert.Equal(36, result.Result.Count);
        }

        [Fact]
        public void When_Calling_LukeStarships_Expect_All_LukeStarships()
        {
            var person = Fetch.People("Luke");
            var ships = Fetch.Starships();
            var personalShips = new List<Starships>();

            personalShips = ships.Result.Join(person.Result.Select(x => x.Starships).FirstOrDefault(),
                                                                 s1 => s1.URL, s2 => s2,
                                                                 (s1, s2) => s1).ToList();

            Assert.Equal(2, personalShips.Count);
        }

        [Fact]
        public void When_Inserting_ParkingData_In_Database_Expect_AnakinSkywalker_ParkingPrice_10sec()
        {
             var parking = new Parking()
            {
                Traveller = "Anakin Skywalker",
                StarShip = "Naboo fighter",
                StartTime = DateTime.Now.AddSeconds(-10)
            };
            var person = Fetch.People("Anakin");

            using (var db = new MyContext())
            {
                db.Parkings.Add(parking);
                db.SaveChanges();
            }

            DatabaseQueries.EndParking(person.Result.SingleOrDefault());

            using (var db = new MyContext())
            {
                var result = db.Parkings.Where(x => x.Traveller == parking.Traveller).OrderBy(x => x.EndTime).LastOrDefault();

                Assert.Equal(0.34m, Math.Round(
                    result.TotalSum.Value,2));

                db.Remove(result);
                db.SaveChanges();
            }
        }

        [Fact]
        public void When_Inserting_ParkingData_In_Database_Expect_AnakinSkywalker_ParkingPrice_1h()
        {
            var parking = new Parking()
            {
                Traveller = "Anakin Skywalker",
                StarShip = "Naboo fighter",
                StartTime = DateTime.Now.AddHours(-1)
            };
            var person = Fetch.People("Anakin");

            using (var db = new MyContext())
            {
                db.Parkings.Add(parking);
                db.SaveChanges();
            }

            DatabaseQueries.EndParking(person.Result.SingleOrDefault());

            using (var db = new MyContext())
            {
                var result = db.Parkings.Where(x => x.Traveller == parking.Traveller).OrderBy(x => x.EndTime).LastOrDefault();

                Assert.Equal(120m, 
                    result.TotalSum.Value, 1);

                db.Remove(result);
                db.SaveChanges();
            }
        }

        [Fact]
        public void When_Inserting_ParkingData_In_Database_Expect_AnakinSkywalker_ParkingPrice_1day1h()
        {
            var parking = new Parking()
            {
                Traveller = "Anakin Skywalker",
                StarShip = "Naboo fighter",
                StartTime = DateTime.Now.AddDays(-1).AddHours(-1)
            };
            var person = Fetch.People("Anakin");

            using (var db = new MyContext())
            {
                db.Parkings.Add(parking);
                db.SaveChanges();
            }

            DatabaseQueries.EndParking(person.Result.SingleOrDefault());

            using (var db = new MyContext())
            {
                var result = db.Parkings.Where(x => x.Traveller == parking.Traveller).OrderBy(x => x.EndTime).LastOrDefault();

                Assert.Equal(3000m, 
                    result.TotalSum.Value, 1);

                db.Remove(result);
                db.SaveChanges();
            }
        }


        [Fact]
        public void When_Inserting_ParkingData_In_Database_Expect_LukeSkywalker_Starttime()
        {
            using (var db = new MyContext())
            {
                var parking = new Parking()
                {
                    Traveller = "Anakin Skywalker",
                    StarShip = "X-wing",
                };

                db.Parkings.Add(parking);
                db.SaveChanges();

                Assert.Equal(DateTime.Now.ToString(), parking.StartTime.ToString());

                db.Remove(parking);
                db.SaveChanges();
            }
        }
    }
}
