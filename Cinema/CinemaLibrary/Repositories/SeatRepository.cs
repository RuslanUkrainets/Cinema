using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLibrary.Repositories
{
    class SeatRepository
    {
        private CinemaContext db = new CinemaContext();

        public bool LazyLoad
        {
            get => db.Configuration.LazyLoadingEnabled;
            set => db.Configuration.LazyLoadingEnabled = value;
        }
        public DbQuery<Seat> GetAll() => db.Seats;
        public Seat GetSeat(int id) => db.Seats.Find(id);
        public void AddOrUpadate(Seat seat) => db.Seats.AddOrUpdate(seat);
        public void Remove(Seat seat) => db.Seats.Remove(seat);
        public DbEntityEntry<Seat> GetEntry(Seat seat) => db.Entry(seat);
        public void Save() => db.SaveChanges();
        ~SeatRepository() => db.Dispose();
    }
}
