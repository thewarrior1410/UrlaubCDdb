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
using UrlaubCD.Data;
using UrlaubCD.WPFUserControl;

namespace UrlaubCD
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void test(object sender, RoutedEventArgs e)
        {

            PlaylistLabel plLabel = new PlaylistLabel();
            plLabel.Height = 30;
            // plLabel.Content = PlaylistStackPanel.Children.Count;
            plLabel.Background = getRandomColor();
            plLabel.Foreground = Brushes.White;
            plLabel.MouseLeftButtonDown += new MouseButtonEventHandler(onLoadPlaylist);

            PlaylistStackPanel.Children.Add(plLabel);
        }

        public SolidColorBrush getRandomColor()
        {
            Random r = new Random();
            SolidColorBrush scb = new SolidColorBrush(Color.FromRgb((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255)));
            return scb;
        }

        public void onLoadPlaylist(object sender, RoutedEventArgs e)
        {
            Playlist pl = ((PlaylistLabel)sender).Playlist;

            // Keine Playlist ausgewählt
            if (songsStackPanel.DataContext == null)
            {
                
            }
            // Playlist ändert sich nicht
            else if (songsStackPanel.DataContext.Equals(pl))
            {
                return;
            }

            // Datacontext auf aktuelle Playlist setzen
            songsStackPanel.DataContext = pl;
            List<Song> songs = pl.Songs;

            for (int i = 0; i < songs.Count(); i++) {

                SongLabel sL = new SongLabel(songs[i], i+1);
                songsStackPanel.Children.Add(sL);

            }


            AddSongLabel aSL = new AddSongLabel();
            //aSL.add_Button.Click += new RoutedEventHandler(button_add_song);

            songsStackPanel.Children.Add(aSL);

        }

        

    }
}
