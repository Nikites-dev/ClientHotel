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

namespace ClientHotel.Pages
{
    public partial class RoomPage : Page
    {
        public RoomPage(Reservation serevRoom)
        {
            InitializeComponent();

            txtNumRoom.Text = serevRoom.Room.Number.ToString();
            txtStatus.Text = serevRoom.Room.StatusRoom.Name.ToString();
            txtTypeRoom.Text = serevRoom.Room.TypeRoom.Name.ToString();
            txtCountClients.Text = serevRoom.Room.CountClients.ToString();
            txtCost.Text = serevRoom.Room.Cost.ToString();

           // var reserv = GetReservation(serevRoom.IdReservation);

            if (serevRoom == null)
            {
                return;
            }

            dateArrive.Text = serevRoom.DateArrival.Value.ToString();
            dateDeparture.Text = serevRoom.DateDeparture.Value.ToString();
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
