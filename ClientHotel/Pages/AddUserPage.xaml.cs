using System;
using System.Windows;
using System.Windows.Controls;
using ClientHotel.Database;

namespace ClientHotel.Pages
{
    public partial class AddUserPage : Page
    {
        private Reservation crntReserv = null;
        private Room crntRoom = new Room();
        public AddUserPage()
        {
            InitializeComponent();
        }
        
        public AddUserPage(Reservation reservation, Room room)
        {
            InitializeComponent();
            crntReserv = reservation;
            crntRoom = room;
        }
        

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (edFName.Text == "" || edLName.Text == "" || edPName.Text == "" || edPhone.Text == "" ||
                dateBirth.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            User user = new User();
            user.FName = edFName.Text;
            user.LName = edLName.Text;
            user.PName = edPName.Text;
            user.Phone = edPhone.Text;
            user.DateBirth = DateTime.Parse(dateBirth.Text);

            App.Connection.User.Add(user);
            App.Connection.SaveChanges();

            MessageBox.Show("Сохранено!");
            
            edFName.Text = "";
            edLName.Text = "";
            edPName.Text = "";
            edPhone.Text = "";
            dateBirth.Text = "";
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (crntReserv == null)
            {
                NavigationService.Navigate(new MainPage());
            }
            else
            {
                NavigationService.Navigate(new ReservationUserPage(crntReserv, crntRoom));    
            }

            
        }
    }
}