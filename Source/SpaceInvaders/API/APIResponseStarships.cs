using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Objects
{
    public class APIResponseStarships
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public List<Starships> Results { get; set; }
    }
}
