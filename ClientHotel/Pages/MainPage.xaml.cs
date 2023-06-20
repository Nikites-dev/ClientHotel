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
using ClientHotel.Database;

namespace ClientHotel.Pages
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            dateArrive.Text = DateTime.Today.ToString();
            dateDeparture.Text = (DateTime.Today + TimeSpan.FromDays(1)).ToString();
            CheckStatusRoom();
            
            
            
            ShowStatusRoom();
        }

        private void listTemplate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room room = (Room)listTemplate.SelectedItem;

            var reservRoomList = App.Connection.Reservation.Where(x => x.IdRoom == room.IdRoom).ToList();
            var reservRoom = reservRoomList.Where(o => DateTime.Parse(dateArrive.Text) >= o.DateArrival && DateTime.Parse(dateDeparture.Text) <= o.DateDeparture)
                .FirstOrDefault();
            
            if (room.IdStatusRoom == 2)
            {
                NavigationService.Navigate(new ReservationPage(room, dateArrive.Text, dateDeparture.Text));
            }
            else
            {
                NavigationService.Navigate(new RoomPage(reservRoom));
            }
        }

        void ShowStatusRoom()
        {
            List<Room> rooms = App.Connection.Room.ToList();
            List<Room> avaliableRooms = new List<Room>();
            
            foreach (var room in rooms)
            {
                var reservRoomList = App.Connection.Reservation.Where(x => x.IdRoom == room.IdRoom).ToList();
                var reservRoom = reservRoomList.Where(o => DateTime.Parse(dateArrive.Text) >= o.DateArrival && DateTime.Parse(dateDeparture.Text) <= o.DateDeparture)
                    .FirstOrDefault();
                

                if (reservRoom == null)
                {
                    room.IdStatusRoom = 2;
                    avaliableRooms.Add(room);
                }
                else
                {
                    if (DateTime.Parse(dateArrive.Text) >= reservRoom.DateArrival && DateTime.Parse(dateDeparture.Text) <= reservRoom.DateDeparture)
                    {
                        room.IdStatusRoom = 1;
                        avaliableRooms.Add(room);
                    }
                    else
                    {
                        room.IdStatusRoom = 2;
                        avaliableRooms.Add(room);
                    }
                }
            }

            listTemplate.ItemsSource = avaliableRooms;
        }

        void CheckStatusRoom()
        {
            var rooms = App.Connection.Room.ToList();

            foreach (var room in rooms)
            {
                Reservation reservExist = App.Connection.Reservation.Where(x => x.IdRoom == room.IdRoom).FirstOrDefault();

                if (reservExist == null)
                {
                    continue;
                }

                if (reservExist.DateDeparture < DateTime.Now)
                {
                    room.IdStatusRoom = 3;

                    if (reservExist.DateDeparture + TimeSpan.FromHours(1) < DateTime.Now)
                    {
                        room.IdStatusRoom = 2;

                        ReservationUser reservUser = App.Connection.ReservationUser.Where(x => x.IdReservation == reservExist.IdReservation).FirstOrDefault();

                        if (reservUser != null)
                        {
                            App.Connection.ReservationUser.Remove(reservUser);
                            App.Connection.Reservation.Remove(reservExist);
                            
                            App.Connection.SaveChanges();
                        }
                    }
                }
            }
        }

        private void BtnAddClient_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUserPage());
        }

        private void DateArrive_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateArrive.SelectedDate != null)
            {
                var arrDate = dateArrive;
                dateDeparture.Text = (DateTime.Parse(dateArrive.Text) + TimeSpan.FromDays(1)).ToString(); 
                ShowStatusRoom();
            }
        }
    }
}
