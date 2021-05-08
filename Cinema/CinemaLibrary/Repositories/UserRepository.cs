using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLibrary.Repositories
{
    class UserRepository
    {
        private CinemaContext db = new CinemaContext();

        public bool LazyLoad
        {
            get => db.Configuration.LazyLoadingEnabled;
            set => db.Configuration.LazyLoadingEnabled = value;
        }
        public DbQuery<UserCinema> GetAll() => db.Users;
        public UserCinema GetUser(int id) => db.Users.Find(id);
        public void AddOrUpadate(UserCinema user) => db.Users.AddOrUpdate(user);
        public void Remove(UserCinema user) => db.Users.Remove(user);
        public DbEntityEntry<UserCinema> GetEntry(UserCinema user) => db.Entry(user);
        public void Save() => db.SaveChanges();
        ~UserRepository() => db.Dispose();        
    }
}
