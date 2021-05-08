using CinemaLibrary.Infrastructure;
using System;
using System.Data.Entity;
using System.Linq;

namespace CinemaLibrary
{
    internal class CinemaContext : DbContext
    {
        public CinemaContext()
            : base("CinemaCS")
        {
            Database.SetInitializer(new InitializerDB());
        }
        public virtual DbSet<UserCinema> Users { get; set; }
        public virtual DbSet<Performance> Performances { get; set; }
        public virtual DbSet<CinemaRoom> CinemaRooms { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Archive> Archives { get; set; }
    }
}