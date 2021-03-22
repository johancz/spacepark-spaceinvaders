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
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        //IS-UNIQUE IF END PARKING IS NULL
        public string Traveller { get; set; }
        public string StarShip { get; set; }
        public decimal? TotalSum { get; set; }
    }
}
