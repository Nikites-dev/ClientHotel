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


            Room room = new Room();
            room.Number = 101;
            room.CountClients = 5;

            List<Room> rooms = new List<Room>();
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);
            rooms.Add(room);

            listTemplate.ItemsSource = rooms;
        }
    }
}
