using SpaceInvaders.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Traveller
{
    class APIResponseTraveller
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public List<Person> Results { get; set; }
    }
}
