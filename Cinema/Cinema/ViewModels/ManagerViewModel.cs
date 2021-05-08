using Cinema.Infrastructure;
using CinemaLibrary;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    class ManagerViewModel
    {
        CinemaManager cinemaManager;
        Performance performance;
        List<Performance> performances;

        public ManagerViewModel()
        {
            Messenger.Default.Register<PerformanceMessage>(this, x => performance = x.Performance);
            Messenger.Default.Register<CinemaManagerMessage>(this, x => cinemaManager = x.CinemaManager);
            Messenger.Default.Register<PerfomancesListMessage>(this, x => performances = x.Performances);
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            AddPerfomanceCommand = new RelayCommand(x => Messenger.Default.Send<PageMessage>(new PageMessage { Pages = Pages.AddPerfomance }));
            EditPerformanceCommand = new RelayCommand(x => Messenger.Default.Send<PageMessage>(new PageMessage { Pages = Pages.EditPerformance }), x => performance != null);
            RemovePerformanceCommand = new RelayCommand(x => Messenger.Default.Send<RemoveMessage>(new RemoveMessage { IsRemove = true}), x => performance != null);
            ReservesCommand = new RelayCommand(x => Messenger.Default.Send<PageMessage>(new PageMessage { Pages = Pages.Reserves }),x=> performance != null);
            StatisticsCommand = new RelayCommand(x => Messenger.Default.Send<PageMessage>(new PageMessage { Pages = Pages.Statistics }));
            ToLobbieCommand = new RelayCommand(x => Messenger.Default.Send<PageMessage>(new PageMessage { Pages = Pages.Lobbie }));
            LogoutCommand = new RelayCommand(x =>
            {
                Messenger.Default.Send<PageMessage>(new PageMessage { Pages = Pages.Lobbie });
                Messenger.Default.Send<EntryMessage>(new EntryMessage { Entry = Entry.NotLogined });
                Messenger.Default.Unregister(this);
            });
        }


        public ICommand AddPerfomanceCommand { get; private set; }
        public ICommand EditPerformanceCommand { get; private set; }
        public ICommand RemovePerformanceCommand { get; private set; }
        public ICommand ReservesCommand { get; private set; }
        public ICommand StatisticsCommand { get; private set; }
        public ICommand ToLobbieCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
    }
}
