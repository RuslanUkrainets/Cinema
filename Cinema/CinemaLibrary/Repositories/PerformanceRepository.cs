using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLibrary.Repositories
{
    class PerformanceRepository
    {
        private CinemaContext db = new CinemaContext();

        public bool LazyLoad
        {
            get => db.Configuration.LazyLoadingEnabled;
            set => db.Configuration.LazyLoadingEnabled = value;
        }
        public DbQuery<Performance> GetAll() => db.Performances;
        public Performance GetPerformance(int id) => db.Performances.Find(id);
        public void AddOrUpadate(Performance performance) => db.Performances.AddOrUpdate(performance);
        public void Remove(Performance performance) => db.Performances.Remove(performance);
        public DbEntityEntry<Performance> GetEntry(Performance performance) => db.Entry(performance);
        public void Save() => db.SaveChanges();
        ~PerformanceRepository() => db.Dispose();    
    }
}
