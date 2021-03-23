using System;
using Xunit;
using SpaceInvaders;
using SpaceInvaders.Database;
using SpaceInvaders.API;
using SpaceInvaders.Objects;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

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
        public void When_Calling_LukeStarships_Expect_All_LukeStarships_OfType_Result()
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


        //personalShips.Select(p => p.Name).ToArray()

        // Arrange 

        //using (var db = new MyContext())
        //{
        //    db.Parkings.SingleOrDefault(x => x.Traveller == person.Name && x.EndTime == null);
        //}
    }
}
