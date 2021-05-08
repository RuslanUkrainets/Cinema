using CinemaLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    class Ticket
    {
        public string Movie { get; set; }
        public string UserName { get; set; }
        public string CinemaRoom { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
