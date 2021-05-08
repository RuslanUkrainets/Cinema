using Cinema.Infrastructure;
using Cinema.Models;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Cinema.ViewModels
{
    class TicketWindowViewModel : BaseNotifyPropertyChanged
    {

        private DrawingVisual visual = new DrawingVisual();
        private BitmapImage bitmap = new BitmapImage();
        private ImageSource imageSource;
        private Ticket ticket;
        public ImageSource ImageSource
        {
            get => imageSource;
            set
            {   
                imageSource = value;
                OnNotifyPropertyChanged();
            }
        }

        [Obsolete]
        public TicketWindowViewModel()
        {
            Messenger.Default.Register<TicketMessage>(this, x => { ticket = x.Ticket; LoadTicketTemplate(); });
            InializerCommand();
        }

        private void InializerCommand()
        {
            SaveCommand = new RelayCommand(Save);
        }

        private void Save(object parameter)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.Filter = "PNG | *.png";
            if (dlg.ShowDialog() == true)
            {
                BitmapEncoder encoder = new PngBitmapEncoder();             
                RenderTargetBitmap render = new RenderTargetBitmap(bitmap.PixelWidth+192, bitmap.PixelHeight+192, 96, 96, PixelFormats.Default);
                render.Render(visual);
                
                encoder.Frames.Add(BitmapFrame.Create(render));
                using (var fs = new FileStream(dlg.FileName,FileMode.Create))
                {
                    encoder.Save(fs);
                }
            }
        }

        [Obsolete]
        private void LoadTicketTemplate()
        {
            var dir = Directory.GetCurrentDirectory();
            try
            {
                bitmap.BeginInit();
                bitmap.UriSource = new Uri($@"{dir}\Images\ticket.jpg", UriKind.Absolute);
                bitmap.EndInit();

                AddDataTicket();
            }
            catch(InvalidOperationException)
            {
            }
        }

        [Obsolete]
        private void AddDataTicket()
        {            
            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawImage(bitmap, new Rect(bitmap.DpiX, bitmap.DpiY, bitmap.Width, bitmap.Height));
                dc.DrawText(new FormattedText(ticket.Movie, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), 24, Brushes.Black),
                    new Point(160, 140));
                dc.DrawText(new FormattedText(ticket.CinemaRoom, CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), 24, Brushes.Black),
                    new Point(160, 240));
                dc.DrawText(new FormattedText($"${ticket.Price}", CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), 24, Brushes.Black),
                    new Point(160, 340));
                dc.DrawText(new FormattedText($"{ticket.DateTime}", CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), 24, Brushes.Black),
                    new Point(260, 340));
                int count = 30;
                foreach (var seat in ticket.Seats)
                {
                    dc.DrawText(new FormattedText($"Seat: {seat.Number}", CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface("Segoe UI"), 24, Brushes.Black),
                    new Point(460, 80 + count));
                    count += 30;
                }
            }            
            var im = new DrawingImage(visual.Drawing);
            ImageSource = im;
        }

        public ICommand SaveCommand { get; private set; }
    }
}
