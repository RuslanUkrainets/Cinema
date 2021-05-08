using Cinema.Infrastructure;
using CinemaLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cinema.Views
{
    /// <summary>
    /// Interaction logic for CinemaRoomControl.xaml
    /// </summary>
    public partial class CinemaRoomControl : UserControl, IGetSeats
    {
        public CinemaRoomControl()
        {
            InitializeComponent();
        }

        public void GetSeats(List<Seat> seats)
        {
            seats.Clear();
            foreach (Seat seat in lb.SelectedItems)
            {
                seats.Add(seat);
            }
            lb.SelectedItems.Clear();
        }
    }
}
