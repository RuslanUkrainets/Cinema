using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLibrary.Repositories
{
    class CinemaRoomRepository
    {
        private CinemaContext db = new CinemaContext();

        public bool LazyLoad
        {
            get => db.Configuration.LazyLoadingEnabled;
            set => db.Configuration.LazyLoadingEnabled = value;
        }
        public DbQuery<CinemaRoom> GetAll() => db.CinemaRooms;   
        public CinemaRoom GetCinemaRoom(int id) => db.CinemaRooms.Find(id);
        public void AddOrUpadate(CinemaRoom room) => db.CinemaRooms.AddOrUpdate(room);
        public void Remove(CinemaRoom room) => db.CinemaRooms.Remove(room);
        public DbEntityEntry<CinemaRoom> GetEntry(CinemaRoom room) => db.Entry(room);
        public void Save() => db.SaveChanges();
        ~CinemaRoomRepository() => db.Dispose();
    }
}
