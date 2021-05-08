using Cinema.Infrastructure;
using Cinema.Views;
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
    
    class NotLoginetViewModel
    {
        private UserCinema userCinema;
        private CinemaManager cinemaManager = new CinemaManager();

        public string Login { get; set; }
        public string Password { get; set; }
        public NotLoginetViewModel()
        {
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            LoginCommand = new RelayCommand(x =>
            {
                userCinema = cinemaManager.GetUser(Login, Password);
                if(userCinema != null)
                {
                    if (userCinema.IsManager == false)
                    {
                        Messenger.Default.Send<EntryMessage>(new EntryMessage { Entry = Entry.UserLogin});
                    }
                    else
                    {
                        Messenger.Default.Send<EntryMessage>(new EntryMessage { Entry = Entry.ManagerLogin });
                    }
                    Messenger.Default.Send<UserMessage>(new UserMessage { UserCinema = userCinema });
                }
            });
            RegistrationCommand = new RelayCommand(x => new EntryWindow().ShowDialog());
        }

        public ICommand LoginCommand { get; private set; }
        public ICommand RegistrationCommand { get; private set; }
    }
}
