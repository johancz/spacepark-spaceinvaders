using System.Collections.Generic;


namespace SpaceInvaders.Objects
{
    public class APIResponseStarships
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public List<Starships> Results { get; set; }
    }
}
