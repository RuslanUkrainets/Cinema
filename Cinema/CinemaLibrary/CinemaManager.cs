using CinemaLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Collections.ObjectModel;
using CinemaLibrary.Repositories;

namespace CinemaLibrary
{
    public class CinemaManager
    {
        SeatRepository seatRepository = new SeatRepository();
        UserRepository userRepository = new UserRepository();
        PerformanceRepository performanceRepository = new PerformanceRepository();
        CinemaRoomRepository cinemaRoomRepository = new CinemaRoomRepository();
        ArchiveRepository archiveRepository = new ArchiveRepository();
        
        public UserCinema GetUser(string login, string password)
        {
            var hashPassword = CinemaHash.HashPass(password);
            return userRepository.GetAll().FirstOrDefault(x => x.Login == login && x.Password == hashPassword);
        }

        public bool RegisterUser(string name, string lastname, string login, string password, string email)
        {
            if (login == string.Empty || password == string.Empty || email == string.Empty) return false;

            userRepository.AddOrUpadate(new UserCinema
            {
                Name = name,
                Lastname = lastname,
                Login = login,
                Password = CinemaHash.HashPass(password),
                Email = email
            });
            userRepository.Save();
            return true;
        }

        public List<CinemaRoom> GetCinemaRooms() => cinemaRoomRepository.GetAll().ToList();

        public IEnumerable<Performance> GetPerformances() => performanceRepository.GetAll();
        
        public void RemovePerformance(Performance performance)
        {
            WriteToArchive(performance);
            performanceRepository.Remove(performance);
            performanceRepository.Save();
        }

        private void WriteToArchive(Performance performance)
        {
            var users = GetUsersList(performance);
            foreach (var user in users)
            {
                var countSeats = performance.Seats.Where(x => x.UserId == user.UserId).Count(x => x.UserId.HasValue);
                Archive archive = new Archive
                {
                    UserId = user.UserId,
                    Sum = performance.Price.Value * countSeats,
                    Date = performance.DateTime,
                    NumberOfSeats = countSeats
                };
                archiveRepository.AddOrUpadate(archive);
            }
            archiveRepository.Save();
        }

        public void EditPerformance(Performance performance, CinemaRoom room)
        {
            if (room == null)
            {
                room = cinemaRoomRepository.GetAll().FirstOrDefault(x => x.CinemaRoomId == performance.CinemaRoomId);
            }

            RemoveSeats(performance);
            AddSeats(performance, room);
            performance.CinemaRoomId = room.CinemaRoomId;
            performanceRepository.AddOrUpadate(performance);
            performanceRepository.Save();
        }

        public void AddPerformance(Performance performance, CinemaRoom room)
        {
            room.Performances.Add(performance);
            AddSeats(performance, room);
            cinemaRoomRepository.Save();
        }

        private void AddSeats(Performance performance, CinemaRoom room)
        {
            for (int i = 1; i <= room.NumberOfSeats; i++)
            {
                Seat seat = new Seat();
                seat.Number = i;
                seat.PerformanceId = performance.PerformanceId;
                performance.Seats.Add(seat);
            }
            performanceRepository.Save();
        }

        private void RemoveSeats(Performance performance)
        {
            var seats = seatRepository.GetAll().Where(x => x.PerformanceId == performance.PerformanceId);
            foreach (var seat in seats)
            {
                seatRepository.Remove(seat);
            }
            seatRepository.Save();
        }

        public void ReservSeats(UserCinema user, List<Seat> seats)
        {
            foreach (var seat in seats)
            {
                seat.UserId = user.UserId;
                seat.IsFree = false;
                seatRepository.AddOrUpadate(seat);
            }
            seatRepository.Save();
        }

        public void Cencellation(UserCinema user, List<Seat> seats)
        {
            var userSeats = seats.Where(x => x.UserId == user.UserId);
            CleanUpSeats(userSeats);
        }

        public IEnumerable<UserCinema> GetUsersList(Performance performance)
        {
            var usId = performance.Seats.Select(x => x.UserId).Where(x => x.HasValue).Distinct();
            List<UserCinema> userList = new List<UserCinema>();
            foreach (var id in usId)
            {
                userList.Add( userRepository.GetUser(id.Value));
            }
            return userList;
        }

        public void CencellationForAdm(UserCinema user, List<Seat> seats)
        {
            CleanUpSeats(seats);
        }

        private void CleanUpSeats(IEnumerable<Seat> seats)
        {
            foreach (var seat in seats)
            {
                seat.UserId = null;
                seat.IsFree = true;
                seatRepository.AddOrUpadate(seat);
            }
            seatRepository.Save();
        }

        public IEnumerable<Seat> UsersSeats(UserCinema user, Performance performance)
        {
            return performance.Seats.Where(x => x.UserId == user.UserId);
        }

        public IEnumerable<UserCinema> GetAllUsers() => userRepository.GetAll().Where(x => x.IsManager == false);

        public decimal StatisticsPerDay(UserCinema user, DateTime date)
        {
            var userNote = archiveRepository.GetAll().Where(x => x.UserId == user.UserId && DbFunctions.TruncateTime(x.Date) == date.Date).ToList();
            return userNote.Select(x => x.Sum).Sum();
        }
        public decimal StatisticsAvgPerMonth(UserCinema user, DateTime date)
        {
            var userNote = archiveRepository.GetAll().Where(x => x.UserId == user.UserId && DbFunctions.TruncateTime(x.Date).Value.Month == date.Month && DbFunctions.TruncateTime(x.Date).Value.Year == date.Year).ToList();
            var sum = userNote.Select(x => x.Sum);
            return sum.Count() == 0 ? 0 : sum.Average();
        }
        public decimal StatisticsTotal(UserCinema user)
        {
            var userNote = archiveRepository.GetAll().Where(x => x.UserId == user.UserId).ToList();
            var total = userNote.Select(x => x.Sum);
            return total.Sum() == 0 ? 0 : total.Sum();
        }
    }
}
