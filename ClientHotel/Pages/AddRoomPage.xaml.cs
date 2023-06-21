using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ClientHotel.Database;
using Microsoft.Win32;

namespace ClientHotel.Pages
{
    public partial class AddRoomPage : Page
    {
        public byte[] Image { get; set; }

        public AddRoomPage()
        {
            InitializeComponent();
        }

        private void BtnImage_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;

                OpenFileDialog fileDialog = new OpenFileDialog();

                if (fileDialog.ShowDialog() != null)
                {
                    Image = File.ReadAllBytes(fileDialog.FileName);
                    button.Background = Brushes.LightCyan;
                }
            }
            catch
            {
                MessageBox.Show("image not avaliable!", "Error");
            }
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            var exesRoom = App.Connection.Room.Where(x => x.Number.ToString() == edNumber.Text.ToString()).FirstOrDefault();

            if (exesRoom == null)
            {
                Room room = new Room();
                room.Number = Convert.ToInt32(edNumber.Text);
                room.IdTypeRoom = Convert.ToInt32(edType.Text);
                room.IdStatusRoom = 2;
                room.Cost = Convert.ToInt32(edCost.Text);
                room.Image = Image;
                room.CountClients = Convert.ToInt32(edCountClients.Text);

                App.Connection.Room.Add(room);
                
                App.Connection.SaveChanges();

                MessageBox.Show("succes!");

            }
            else
            {
                MessageBox.Show("room such number already exist");
            }



        }

        private void BtnClients_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}