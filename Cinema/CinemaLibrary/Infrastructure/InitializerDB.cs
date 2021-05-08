using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaLibrary.Infrastructure
{
    class InitializerDB : CreateDatabaseIfNotExists<CinemaContext>
    {
        protected override void Seed(CinemaContext context)
        {
            base.Seed(context);
            context.Users.Add(new UserCinema
            {
                Name = "admin",
                Lastname = "admin",
                Login = "admin",
                Password = CinemaHash.HashPass("admin"),
                Email = "admin@gmail.com",
                IsManager = true
            });
            context.Users.Add(new UserCinema
            {
                Name = "user",
                Lastname = "user",
                Login = "user",
                Password = CinemaHash.HashPass("user"),
                Email = "user@gmail.com",
                IsManager = false
            });
            context.Users.Add(new UserCinema
            {
                Name = "user1",
                Lastname = "user1",
                Login = "user1",
                Password = CinemaHash.HashPass("user1"),
                Email = "user1@gmail.com",
                IsManager = false
            });
            context.CinemaRooms.Add(new CinemaRoom
            {
                Name = "Big Boss",
                NumberOfSeats = 60
            });
            context.CinemaRooms.Add(new CinemaRoom
            {
                Name = "Red Dragon",
                NumberOfSeats = 50
            });

            context.Performances.Add(new Performance
            {
                Movie = "Max Payne",
                Price = 120,
                DateTime = new DateTime(2021, 04, 20, 13, 00, 00),
                CinemaRoomId = 1,
                ImagePath = "https://mir-s3-cdn-cf.behance.net/project_modules/max_1200/ed769241053573.579756d8d5460.jpg"
            });
            context.Performances.Add(new Performance
            {
                Movie = "Iron Man",
                Price = 80,
                DateTime = new DateTime(2021, 04, 19, 17, 00, 00),
                CinemaRoomId = 2,
                ImagePath = "https://media.comicbook.com/2017/10/iron-man-movie-poster-marvel-cinematic-universe-1038878.jpg"
            });
            context.Performances.Add(new Performance
            {
                Movie = "Avengers",
                Price = 50,
                DateTime = new DateTime(2021, 04, 19, 17, 00, 00),
                CinemaRoomId = 1,
                ImagePath = "https://media.comicbook.com/2017/10/the-avengers-movie-poster-marvel-cinematic-universe-1038892.jpg"
            });
            context.Performances.Add(new Performance
            {
                Movie = "Justice League",
                Price = 120,
                DateTime = new DateTime(2021, 04, 19, 20, 20, 00),
                CinemaRoomId = 1,
                ImagePath = "https://3.bp.blogspot.com/-F7sEZhOR_VA/WgqWgq0UeqI/AAAAAAAAheU/SRg0JRIzPG4fimrwMJ0aiDr2hNZnhqLEgCEwYBhgL/s1600/Justice-League-HD-%2B%25281%2529.jpg"
            });
            context.Performances.Add(new Performance
            {
                Movie = "Green Lanters",
                Price = 82,
                DateTime = new DateTime(2021, 04, 20, 13, 20, 00),
                CinemaRoomId = 2,
                ImagePath = "https://static.squarespace.com/static/51b3dc8ee4b051b96ceb10de/51ce6099e4b0d911b4489b79/51ce61cce4b0d911b44a1842/1301728865227/1000w/Green%20Lanter%20Official%20Poster.jpg"
            });
            context.Performances.Add(new Performance
            {
                Movie = "Star Wars: The Last Jedi",
                Price = 67,
                DateTime = new DateTime(2021, 04, 20, 16, 10, 00),
                CinemaRoomId = 2,
                ImagePath = "https://mypostercollection.com/wp-content/uploads/2018/06/Star-Wars-Episode-8-2017-Printable-Poster-MyPosterCollection.com-5-691x1024.jpg"
            });
            context.SaveChanges();

            foreach (var performance in context.Performances)
            {
                var room = context.CinemaRooms.FirstOrDefault(x => x.CinemaRoomId == performance.CinemaRoomId);
                for (int i = 1; i <= room.NumberOfSeats; i++)
                {
                    Seat seat = new Seat();
                    seat.Number = i;
                    seat.PerformanceId = performance.PerformanceId;
                    performance.Seats.Add(seat);
                    context.Seats.Add(seat);
                }
            }
        }
    }
}
