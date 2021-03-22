using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace SpaceInvaders
{
    public class Person
    {
        public string Name { get; set; }
        public string Homeworld { get; set; }
        public List<string> Starships { get; set; }
    }
}