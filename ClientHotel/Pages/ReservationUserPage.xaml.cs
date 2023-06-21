using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClientHotel.Database;

namespace ClientHotel.Pages
{
    public partial class ReservationUserPage : Page
    {
        private Reservation crntReserv = new Reservation();
        private Room crntRoom = new Room();
        private List<User> avaliableUsers = new List<User>();  
        private List<User> crntChooseUsers = new List<User>();  
        private List<User> users = new List<User>();  
        
        public ReservationUserPage(Reservation reservation, Room room)
        {
            InitializeComponent();
            crntReserv = reservation;
            crntRoom = room;

            users = App.Connection.User.ToList();

            foreach (var user in users)
            {
                var reservUser = App.Connection.ReservationUser.Where(x => x.IdUser == user.IdUser).FirstOrDefault();

                if (reservUser == null)
                {
                    avaliableUsers.Add(user);
                }
            }

            listViewUsers.ItemsSource = avaliableUsers.ToList();
            // listViewUsers.ItemsSource = App.Connection.User.Where(x => x.IdUser != App.Connection.ReservationUser.).ToList();

            txtNumber.Text = crntRoom.Number.ToString();
            txtCountClients.Text = crntRoom.CountClients.ToString();
            txtCrntCount.Text = 0.ToString();
        }

        private void ListViewUsers_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (crntChooseUsers.Count == crntRoom.CountClients)
            {
                MessageBox.Show("номер заполнен!");
                return;
            }


            if (listViewUsers.SelectedItem != null)
            {
                var user = listViewUsers.SelectedItem as User;
                crntChooseUsers.Add(user);
                avaliableUsers.Remove(user);

                listViewUsers.ItemsSource = avaliableUsers.ToList();
                listViewChooseUsers.ItemsSource = crntChooseUsers.ToList();
            }

            txtCrntCount.Text = crntChooseUsers.Count.ToString();
        }

        private void BtnClients_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUserPage(crntReserv, crntRoom));
        }

        private void ListViewChooseUsers_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewChooseUsers.SelectedItem != null)
            {
                var userChoose = listViewChooseUsers.SelectedItem as User;
                avaliableUsers.Add(userChoose);
                crntChooseUsers.Remove(userChoose);

                listViewUsers.ItemsSource = avaliableUsers.ToList();
                listViewChooseUsers.ItemsSource = crntChooseUsers.ToList();
                
                txtCrntCount.Text = crntChooseUsers.Count.ToString();
            }

           
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rsltMessageBox = MessageBox.Show("Забронировать номер?", "Бронирование", MessageBoxButton.YesNo, MessageBoxImage.Question);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:

                    try
                    {
                        App.Connection.Reservation.Add(crntReserv);
                        App.Connection.SaveChanges();
                        crntRoom.IdStatusRoom = 1;

                        foreach (var user in crntChooseUsers)
                        {
                            ReservationUser userReserv = new ReservationUser();
                            userReserv.IdReservation = App.Connection.Reservation.Where(x => x.Room.Number == crntRoom.Number && x.DateArrival == crntReserv.DateArrival && x.DateDeparture == x.DateDeparture).FirstOrDefault().IdReservation;
                            userReserv.IdUser = App.Connection.User.Where(x => x.IdUser == user.IdUser).FirstOrDefault().IdUser;

                            App.Connection.ReservationUser.Add(userReserv);
                            
                            App.Connection.SaveChanges();
                        }

                        MessageBox.Show("Забронировано!");
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                    }
                    
                    break;

                case MessageBoxResult.No:
                    /* ... */
                    break;
            }
        }
    }
}