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
        public RoomPage(Room room)
        {
            InitializeComponent();

            txtNumRoom.Text = room.Number.ToString();
            txtStatus.Text = room.StatusRoom.Name.ToString();
            txtTypeRoom.Text = room.TypeRoom.Name.ToString();
            txtCountClients.Text = room.CountClients.ToString();
            txtCost.Text = room.Cost.ToString();

            var reserv = GetReservation(room.IdRoom);

            if (reserv == null)
            {
                return;
            }

            dateArrive.Text = reserv.DateArrival.Value.ToString();
            dateDeparture.Text = reserv.DateDeparture.Value.ToString();
            dateArrive.Focusable = false;


            listViewClients.ItemsSource = App.Connection.ReservationUser
                .Where(x => x.IdReservation == reserv.IdReservation).ToList();
        }



        Reservation GetReservation(int roomId)
        {
            return App.Connection.Reservation.Where(x => x.IdRoom == roomId).FirstOrDefault();
        }
    }
}
