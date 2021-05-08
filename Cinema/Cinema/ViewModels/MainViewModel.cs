using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Cinema.Infrastructure;
using Cinema.Views;
using CinemaLibrary;
using GalaSoft.MvvmLight.Messaging;

namespace Cinema.ViewModels
{
    delegate void WasCreated();
    class MainViewModel : BaseNotifyPropertyChanged
    {
        private IWasCreated wasCreated;

        private CinemaManager cinemaManager = new CinemaManager();
        private Pages currentPage = Pages.Lobbie;
        private Entry currentEntry = Entry.NotLogined;
        private UserCinema userCinema;
        private Performance performance;

        public Entry CurrentEntry
        {
            get => currentEntry;
            set
            {
                currentEntry = value;
                OnNotifyPropertyChanged();
            }
        }

        public Pages CurrentPage
        {
            get => currentPage;
            set
            {
                wasCreated.Unregister();
                currentPage = value;
                OnNotifyPropertyChanged();
            }
        }

        public MainViewModel()
        {
            RegisterMessage();
        }

        private void SendMessage()
        {
            Messenger.Default.Send<CinemaManagerMessage>(new CinemaManagerMessage { CinemaManager = cinemaManager });
            Messenger.Default.Send<PerfomancesListMessage>(new PerfomancesListMessage { Performances = cinemaManager.GetPerformances().ToList() });
            Messenger.Default.Send<PerformanceMessage>(new PerformanceMessage { Performance = performance });
            Messenger.Default.Send<UserMessage>(new UserMessage { UserCinema = userCinema });
        }

        private void RegisterMessage()
        {
            Messenger.Default.Register<PageMessage>(this, x => CurrentPage = x.Pages);
            Messenger.Default.Register<EntryMessage>(this, x => CurrentEntry = x.Entry);
            Messenger.Default.Register<UserMessage>(this, x => userCinema = x.UserCinema);
            Messenger.Default.Register<PerformanceMessage>(this, x => performance = x.Performance);
            Messenger.Default.Register<WasCreatedMessage>(this, x => { wasCreated = x.WasCreated; wasCreated.Notify += SendMessage;});            
        }
    }
}
