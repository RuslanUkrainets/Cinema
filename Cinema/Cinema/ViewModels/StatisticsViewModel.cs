using Cinema.Infrastructure;
using CinemaLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.ViewModels
{
    class StatisticsViewModel : BaseNotifyPropertyChanged
    {
        private DateTime selectedDate;
        private DataView dataView;
        private CinemaManager cinemaManager = new CinemaManager();

        public DataView DataView
        {
            get => dataView;
            set
            {
                dataView = value;
                OnNotifyPropertyChanged();
            }
        }
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                FillDataTable();
                OnNotifyPropertyChanged();
            }
        }

        private void FillDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.AddRange(new DataColumn[] { new DataColumn("UserId"), new DataColumn("User Login"), new DataColumn("Per Day"), new DataColumn("AVG per month"), new DataColumn("Total") });
            var users = cinemaManager.GetAllUsers();
            foreach (var user in users)
            {
                table.LoadDataRow(new object[] { 
                    user.UserId, 
                    user.Login, 
                    cinemaManager.StatisticsPerDay(user, SelectedDate), 
                    cinemaManager.StatisticsAvgPerMonth(user, SelectedDate),  
                    cinemaManager.StatisticsTotal(user)
                }, true);
            }
            DataView = table.DefaultView;
        }
    }
}
