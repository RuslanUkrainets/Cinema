using Cinema.Infrastructure;
using CinemaLibrary;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    class UserCInemaViewModel
    {
        private Performance performance;
        public UserCInemaViewModel()
        {
            Messenger.Default.Register<PerformanceMessage>(this, x => performance = x.Performance);
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            BuyTicketsCommand = new RelayCommand(x => Messenger.Default.Send<PageMessage>(new PageMessage { Pages = Pages.CinemaRoom }), x => performance != null);

            ToLobbieCommand = new RelayCommand(x => Messenger.Default.Send<PageMessage>(new PageMessage { Pages = Pages.Lobbie }));

            LogoutCommand = new RelayCommand(x =>
            {
                Messenger.Default.Send<PageMessage>(new PageMessage { Pages = Pages.Lobbie });
                Messenger.Default.Send<EntryMessage>(new EntryMessage { Entry = Entry.NotLogined });
                Messenger.Default.Unregister(this);
            });
        }

        public ICommand BuyTicketsCommand { get; private set; }
        public ICommand ToLobbieCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
    }
}
