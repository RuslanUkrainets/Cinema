using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLibrary
{
    public class Archive
    {
        [Key]
        public int ArchiveId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public int NumberOfSeats { get; set; }
    }
}