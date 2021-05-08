using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLibrary
{
    public class Seat
    {
        public int SeatId { get; set; }
        [Required]
        public int PerformanceId { get; set; }
        [Required]
        public int Number { get; set; }
        public int? UserId { get; set; }
        public bool IsFree { get; set; } = true;
    }
}
