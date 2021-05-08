using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLibrary
{
    public class Performance
    {
        [Key]
        public int PerformanceId { get; set; }
        [Required]
        public string Movie { get; set; }
        public decimal? Price { get; set; }
        public DateTime? DateTime { get; set; }
        [Required]
        public int CinemaRoomId { get; set; }
        public string ImagePath { get; set; }

        public virtual CinemaRoom CinemaRoom { get; set; }
        public virtual ICollection<Seat> Seats { get; set; } = new HashSet<Seat>();
    }

}
