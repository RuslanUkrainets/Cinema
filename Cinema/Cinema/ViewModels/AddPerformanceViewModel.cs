using Cinema.Infrastructure;
using CinemaLibrary;
using Microsoft.Win32;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cinema.ViewModels
{    
    class AddPerformanceViewModel : BaseNotifyPropertyChanged
    {
        private CinemaManager cinemaManager = new CinemaManager();
        private Performance performance = new Performance() { DateTime = DateTime.Now };

        public Performance Performance
        {
            get => performance;
            set
            {
                performance = value;
                OnNotifyPropertyChanged();
            }
        }

        public List<CinemaRoom> CinemaRooms { get; set; }
        public CinemaRoom SelectedCinemaRoom { get; set; }

        public AddPerformanceViewModel()
        {
            CinemaRooms = cinemaManager.GetCinemaRooms();
            InitializeCommand();
        }

        private void InitializeCommand()
        {
            AddCommand = new RelayCommand(x => 
            {
                cinemaManager.AddPerformance(Performance, SelectedCinemaRoom);
                Performance = new Performance() { DateTime = DateTime.Now };
            });
        }

        public ICommand AddCommand { get; private set; }
    }
}
