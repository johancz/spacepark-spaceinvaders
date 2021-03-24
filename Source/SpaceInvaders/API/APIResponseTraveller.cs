using SpaceInvaders.Objects;
using System.Collections.Generic;

namespace SpaceInvaders.API
{
    public class APIResponseTraveller
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public List<Person> Results { get; set; }
    }
}
