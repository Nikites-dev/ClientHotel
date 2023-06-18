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
            
            List<Room> rooms = App.Connection.Room.ToList();
            listTemplate.ItemsSource = rooms;
        }

        private void listTemplate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Room room = (Room)listTemplate.SelectedItem;

            if (room.IdStatusRoom == 2)
            {
                NavigationService.Navigate(new ReservationPage(room, dateArrive.Text, dateDeparture.Text));
            }
            else
            {
                NavigationService.Navigate(new RoomPage(room));
            }
        }

        void CheckStatusRoom()
        {
            var rooms = App.Connection.Room.ToList();

            foreach (var room in rooms)
            {
                Reservation reservExist = App.Connection.Reservation.Where(x => x.IdRoom == room.IdRoom).FirstOrDefault();

                if (reservExist == null)
                {
                    return;
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
                        }
                    }

                    App.Connection.SaveChanges();
                }
            }
        }

        private void BtnAddClient_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUserPage());
        }
    }
}
