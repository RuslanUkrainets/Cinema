using Cinema.Infrastructure;
using CinemaLibrary;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cinema.ViewModels
{
    class LobbieViewModel : BaseNotifyPropertyChanged, IWasCreated
    {
        private List<Performance> performances;
        private Performance selectPerformance;
        private CinemaManager cinemaManager;

        public event WasCreated Notify;

        public List<Performance> Performances
        {
            get => performances;
            set
            {
                performances = value;
                OnNotifyPropertyChanged();
            }
        }
        public Performance SelectPerformance
        {
            get => selectPerformance;
            set
            {
                selectPerformance = value;
                Messenger.Default.Send<PerformanceMessage>(new PerformanceMessage { Performance = selectPerformance });
            }
        }

        public LobbieViewModel()
        {
            Messenger.Default.Register<CinemaManagerMessage>(this, x => cinemaManager = x.CinemaManager);
            Messenger.Default.Register<PerfomancesListMessage>(this, x => Performances = x.Performances);
            Messenger.Default.Register<RemoveMessage>(this, x => RemoveSelected(x.IsRemove));
            Messenger.Default.Send<WasCreatedMessage>(new WasCreatedMessage { WasCreated = this });
            Notify?.Invoke();
        }

        private void RemoveSelected(bool isRemove)
        {
            if(isRemove == true)
            {
                cinemaManager.RemovePerformance(SelectPerformance);
                SelectPerformance = null;
                Performances = cinemaManager.GetPerformances().ToList();
            }
        }

        public void Unregister()
        {
            Messenger.Default.Unregister(this);
        }
    }
}
