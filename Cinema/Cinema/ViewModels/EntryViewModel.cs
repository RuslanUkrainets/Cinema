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
    class EntryViewModel
    {
        private CinemaManager cinemaManager = new CinemaManager();
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        
        public EntryViewModel()
        {
            InitCommand();
        }

        private void InitCommand()
        {
            LoginCommand = new RelayCommand<ICloseable>(x => 
            {                
                if (cinemaManager.RegisterUser(Name, Lastname, Login, Password, Email))
                {
                    x.Close();
                }
            });
        }

        public RelayCommand<ICloseable> LoginCommand { get; private set; }
        
    }
}
