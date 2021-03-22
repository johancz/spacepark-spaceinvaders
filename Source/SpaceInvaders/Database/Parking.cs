using SpaceInvaders.Traveller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Database
{
    class Parking
    {
        public int ID { get; set; }
        public DateTime StartParking { get; set; }

        //IS-UNIQUE
        public string Traveller { get; set; }
        public string StarShip { get; set; }
    }
}
