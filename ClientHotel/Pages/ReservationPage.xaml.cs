using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ClientHotel.Database;
using ClientHotel.Services;

namespace ClientHotel.Pages
{
    public partial class ReservationPage : Page
    {
        private Room crntRoom = new Room();
        private DateTime crntdateArrive;
        private DateTime crntdateDeparture;
        public ReservationPage()
        {
            InitializeComponent();
        }

        public ReservationPage(Room room, String dateArriveStr, String dateDepartureStr)
        {
            InitializeComponent();
            crntRoom = room;
            crntdateArrive = DateTime.Parse(dateArriveStr);
            crntdateDeparture = DateTime.Parse(dateDepartureStr); 
            
            ShowRoomData(room, dateArriveStr, dateDepartureStr);
        }


        void ShowRoomData(Room room, String dateArriveStr, String dateDepartureStr)
        {

            txtNumRoom.Text = room.Number.ToString();
            txtStatus.Text = App.Connection.StatusRoom.Where(x => x.IdStatusRoom == room.IdStatusRoom).FirstOrDefault()
                .Name;
            txtTypeRoom.Text = App.Connection.TypeRoom.Where(x => x.IdTypeRoom == room.IdTypeRoom).FirstOrDefault().Name;
            txtCountClients.Text = room.CountClients.ToString();
            txtCost.Text = room.Cost.ToString();
            imageView.Source = ByteToImage.LoadImage(room.Image);
          //  var reserv = App.Connection.Reservation.Where(x => x.IdRoom == room.IdRoom).FirstOrDefault();

            dateArrive.Text = dateArriveStr;
            dateDeparture.Text = dateDepartureStr;
        }

        
        
        
        private void ButtonContinueOnClick(object sender, RoutedEventArgs e)
        {
            Reservation reserv = new Reservation();
            reserv.IdRoom = crntRoom.IdRoom;
            reserv.DateArrival = crntdateArrive;
            reserv.DateDeparture = crntdateDeparture;

            NavigationService.Navigate(new ReservationUserPage(reserv, crntRoom));
        }
    }
}