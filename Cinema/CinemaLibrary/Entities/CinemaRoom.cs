using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLibrary
{
    public class CinemaRoom
    {
        [Key]
        public int CinemaRoomId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int NumberOfSeats { get; set; }

        public virtual ICollection<Performance> Performances { get; set; } = new HashSet<Performance>();

    }
}
