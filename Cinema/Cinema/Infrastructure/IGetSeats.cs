using CinemaLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure
{
    interface IGetSeats
    {
        void GetSeats(List<Seat> seats);
    }
}
