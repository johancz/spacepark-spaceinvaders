using System;
using Xunit;
using SpaceInvaders;
using SpaceInvaders.Database;
using SpaceInvaders.API;
using SpaceInvaders.Objects;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using SpaceInvaders.Traveller;
using System.Threading;

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

            //Assert.Equal("X-wing", personalShips.Select(x => x.Name).FirstOrDefault());
            Assert.Equal(2, personalShips.Count);
        }

        [Fact]
        public void When_Inserting_ParkingData_In_Database_Expect_AnakinSkywalker_ParkingPrice()
        {
            var parking = new Parking()
            {
                Traveller = "Anakin Skywalker",
                StarShip = "Naboo fighter",
            };
            var person = Fetch.People("Anakin");

            using (var db = new MyContext())
            {
                db.Parkings.Add(parking);
                db.SaveChanges();
            }

            Thread.Sleep(10000); // Duration between starttime and endtime.
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



        //personalShips.Select(p => p.Name).ToArray()

        // Arrange 

        //using (var db = new MyContext())
        //{
        //    db.Parkings.SingleOrDefault(x => x.Traveller == person.Name && x.EndTime == null);
        //}
    }
}
