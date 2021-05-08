using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLibrary
{
    public class UserCinema
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public bool IsManager { get; set; } = false;
    }
}
