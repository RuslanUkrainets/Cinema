using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLibrary.Repositories
{
    class ArchiveRepository
    {
        CinemaContext db = new CinemaContext();

        public IQueryable<Archive> GetAll() => db.Archives;
        public Archive GetArchive(int id) => db.Archives.Find(id);
        public void AddOrUpadate(Archive archive) => db.Archives.AddOrUpdate(archive);
        public void Remove(Archive archive) => db.Archives.Remove(archive);
        public DbEntityEntry<Archive> GetEntry(Archive archive) => db.Entry(archive);
        public void Save() => db.SaveChanges();
        ~ArchiveRepository() => db.Dispose();
    }
}
