using Cinema.Infrastructure;
using CinemaLibrary;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    class ReservesViewModel : BaseNotifyPropertyChanged, IWasCreated
    {
        public event WasCreated Notify;

        private Performance performance;
        private CinemaManager cinemaManager;

        private List<UserCinema> users;
        private List<Seat> seats;
        private List<Seat> usersSeats;
        private UserCinema selectedUser;

        public UserCinema SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                UsersSeats = cinemaManager.UsersSeats(SelectedUser, performance).ToList();
                OnNotifyPropertyChanged();
            }
        }

        public List<UserCinema> Users
        {
            get => users;
            set
            {
                users = value;
                OnNotifyPropertyChanged();
            }
        }
        public List<Seat> Seats
        {
            get => seats;
            set
            {
                seats = value;
                OnNotifyPropertyChanged();
            }
        }
        public List<Seat> UsersSeats
        {
            get => usersSeats;
            set
            {
                usersSeats = value;
                OnNotifyPropertyChanged();
            }
        }


        public ReservesViewModel()
        {
            RegisterMessage();
            Messenger.Default.Send<WasCreatedMessage>(new WasCreatedMessage { WasCreated = this });
            Notify?.Invoke();
            CencellationCommand = new RelayCommand(x =>
            {                
                cinemaManager.CencellationForAdm(SelectedUser, UsersSeats);
                Seats = performance.Seats.ToList();
                Users = cinemaManager.GetUsersList(performance).ToList();
                UsersSeats = null;
            }, x => SelectedUser != null);
        }

        private void RegisterMessage()
        {
            Messenger.Default.Register<CinemaManagerMessage>(this, x => cinemaManager = x.CinemaManager);
            Messenger.Default.Register<PerformanceMessage>(this, x => { 
                performance = x.Performance;
                Seats = performance.Seats.ToList();
                Users = cinemaManager.GetUsersList(performance).ToList();
            });
        }
        public void Unregister()
        {
            Messenger.Default.Unregister(this);
        }

        public ICommand CencellationCommand { get; private set; }
    }
}
