using Cinema.Infrastructure;
using CinemaLibrary;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    class EditPerformanceViewModel : BaseNotifyPropertyChanged, IWasCreated
    {
        private Performance performance;
        private CinemaManager cinemaManager;

        private List<CinemaRoom> cinemaRooms;

        public event WasCreated Notify;

        public List<CinemaRoom> CinemaRooms
        {
            get => cinemaRooms;
            set
            {
                cinemaRooms = value;
                OnNotifyPropertyChanged();
            }
        }
        public CinemaRoom SelectedCinemaRoom { get; set; }

        public Performance Performance
        {
            get => performance;
            set
            {
                performance = value;
                OnNotifyPropertyChanged();
            }
        }

        public EditPerformanceViewModel()
        {
            RegisterMessage();
            Messenger.Default.Send<WasCreatedMessage>(new WasCreatedMessage { WasCreated = this });
            Notify?.Invoke();
            CinemaRooms = cinemaManager.GetCinemaRooms();
            InitializeCommand();
        }

        private void RegisterMessage()
        {
            Messenger.Default.Register<PerformanceMessage>(this, x => Performance = x.Performance);
            Messenger.Default.Register<CinemaManagerMessage>(this, x => cinemaManager = x.CinemaManager);
        }

        private void InitializeCommand()
        {
            EditCommand = new RelayCommand(x =>
            {
                cinemaManager.EditPerformance(Performance, SelectedCinemaRoom);
                Performance = null;
                CinemaRooms = null;
            });
        }

        public void Unregister()
        {
            Messenger.Default.Unregister(this);
        }

        public ICommand EditCommand { get; private set; }
    }
}
