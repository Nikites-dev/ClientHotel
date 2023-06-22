using ClientHotel.Database;
using System;
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
using ClientHotel.Services;

namespace ClientHotel.Pages
{
    public partial class RoomPage : Page
    {
        public RoomPage(Reservation serevRoom)
        {
            InitializeComponent();

            txtNumRoom.Text = serevRoom.Room.Number.ToString();
            txtStatus.Text = App.Connection.StatusRoom.Where(x => x.IdStatusRoom == serevRoom.Room.IdStatusRoom).FirstOrDefault().Name;
            txtTypeRoom.Text = serevRoom.Room.TypeRoom.Name.ToString();
            txtCountClients.Text = serevRoom.Room.CountClients.ToString();
            txtCost.Text = serevRoom.Room.Cost.ToString();
            imageView.Source = ByteToImage.LoadImage(serevRoom.Room.Image);
           // var reserv = GetReservation(serevRoom.IdReservation);

            if (serevRoom == null)
            {
                return;
            }

            dateArrive.Text = serevRoom.DateArrival.Value.ToShortDateString();
            dateDeparture.Text = serevRoom.DateDeparture.Value.ToShortDateString();
            dateArrive.Focusable = false;


            listViewClients.ItemsSource = App.Connection.ReservationUser
                .Where(x => x.IdReservation == serevRoom.IdReservation).ToList();
        }



        // Reservation GetReservation(int idReservation)
        // {
        //     return App.Connection.Reservation.Where(x => x.IdRoom == roomId).FirstOrDefault();
        // }
    }
}
