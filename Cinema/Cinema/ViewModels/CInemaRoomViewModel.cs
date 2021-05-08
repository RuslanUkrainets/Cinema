using Cinema.Infrastructure;
using Cinema.Models;
using Cinema.Views;
using CinemaLibrary;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    class CInemaRoomViewModel : BaseNotifyPropertyChanged, IWasCreated
    {
        private Performance performance;
        private UserCinema userCinema;
        private CinemaManager cinemaManager;

        private List<Seat> seats;
        private Ticket ticket;

        public event WasCreated Notify;
        
        public Seat SelectedSeat { get; set; }

        public List<Seat> SelectedSeats { get; set; } = new List<Seat>();

        public Ticket Ticket
        {
            get => ticket;
            set
            {
                ticket = value;
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
        public CInemaRoomViewModel()
        {
            RegisterMessage();
            Messenger.Default.Send<WasCreatedMessage>(new WasCreatedMessage { WasCreated = this });
            Notify?.Invoke();
            InitializeCommand();
        }

        private void RegisterMessage()
        {
            Messenger.Default.Register<PerformanceMessage>(this, x => { performance = x.Performance; Seats = performance.Seats.ToList(); });
            Messenger.Default.Register<UserMessage>(this, x => userCinema = x.UserCinema);
            Messenger.Default.Register<CinemaManagerMessage>(this, x => cinemaManager = x.CinemaManager);
        }

        private void InitializeCommand()
        {
            ReservCommand = new RelayCommand<IGetSeats>(x =>
            {
                x.GetSeats(SelectedSeats);
                cinemaManager.ReservSeats(userCinema, SelectedSeats);
                Seats = performance.Seats.ToList();
                Ticket = new Ticket
                {
                    Movie = performance.Movie,
                    UserName = userCinema.Name,
                    CinemaRoom = performance.CinemaRoom.Name,
                    DateTime = performance.DateTime.Value,
                    Price = performance.Price.Value * SelectedSeats.Count,
                    Seats = SelectedSeats
                };
                var ticketWindow = new TicketWindow();
                Messenger.Default.Send<TicketMessage>(new TicketMessage { Ticket = ticket });
                ticketWindow.ShowDialog();
            }, x => SelectedSeat != null);
            CencellationComand = new RelayCommand(x =>
            {
                cinemaManager.Cencellation(userCinema, Seats);
                Seats = performance.Seats.ToList();
                Ticket = null;
            });
        }

        public void Unregister()
        {
            Messenger.Default.Unregister(this);
        }

        public RelayCommand<IGetSeats> ReservCommand { get; private set; }
        public ICommand CencellationComand { get; private set; }
    }
}
