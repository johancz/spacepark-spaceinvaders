using SpaceInvaders.Traveller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Database
{
    public class Parking
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [Required]
        public string Traveller { get; set; }
        [Required]
        public string StarShip { get; set; }
        public decimal? TotalSum { get; set; }
    }
}
