using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void test(object sender, RoutedEventArgs e)
        {
            // Neues PlaylistLabel mit neuer Playlist
            PlaylistLabel plLabel = new PlaylistLabel();
            plLabel.Height = 30;
            
            plLabel.Playlist.Playlist_name = PlaylistStackPanel.Children.Count.ToString();

            plLabel.Background = getRandomColor();
            plLabel.Foreground = Brushes.White;
            plLabel.MouseLeftButtonDown += new MouseButtonEventHandler(OnOpenPlaylist);

            PlaylistStackPanel.Children.Add(plLabel);
        }


        public void OnOpenPlaylist(object sender, RoutedEventArgs e)
        {
            PlaylistLabel plL = sender as PlaylistLabel;
            Playlist pl = plL.Playlist;

            // wenn Playlist bereits geladen ist
            if (plEquals(songsStackPanel.DataContext, pl))
                return;
           
            resetSelectionPlaylistPanel();
            plL.isSelected = true;

            loadPlaylist(pl);
        }

        public void loadPlaylist(Playlist pl)
        {
            // Remove all Children
            songsStackPanel.Children.RemoveRange(0, songsStackPanel.Children.Count);

            // Datacontext auf aktuelle Playlist setzen
            songsStackPanel.DataContext = pl;
            List<Song> songs = pl.Songs;

            // Alle Songs in der Playlist als SongLabel anzeigen
            for (int i = 0; i < songs.Count(); i++)
            {
                SongLabel sL = new SongLabel(songs[i], i + 1);
                songsStackPanel.Children.Add(sL);
            }

            // Add-SongLabel am Ende hinzufügen
            appendAddSongLabel(songsStackPanel);
        }

        private void appendAddSongLabel(StackPanel songsStackPanel)
        {
            // Creates AddSongLabel and Binds Song/Interpret Inputs to add_song variable
            AddSongLabel aSL = new AddSongLabel();

            Song add_song = new Song();
            aSL.DataContext = add_song;

            Binding add_SName_Binding = new Binding("Song_name");
            Binding add_Interp_Binding = new Binding("Interpret_name");

            aSL.song_inp.SetBinding(TextBox.TextProperty, add_SName_Binding);
            aSL.interpret_inp.SetBinding(TextBox.TextProperty, add_Interp_Binding);

            aSL.add_Button.Click += new RoutedEventHandler(addSongButton);

            songsStackPanel.Children.Add(aSL);
        }


        public void resetSelectionPlaylistPanel()
        {
            foreach (PlaylistLabel playlistLabel in PlaylistStackPanel.Children)
            {
                playlistLabel.isSelected = false;
            }
        }

        private bool plEquals(object dataContext, Playlist pl)
        {
            // null if empty or not convertable; Playlist if pl selected
            Playlist currentPl = dataContext as Playlist;
            
            // false if no playlist selected or pl is different
            if (currentPl != pl)
            {
                return false;
            } else
            {
                return true;
            }

        }


        public SolidColorBrush getRandomColor()
        {
            Random r = new Random();
            SolidColorBrush scb = new SolidColorBrush(Color.FromRgb((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255)));
            return scb;
        }


        public void addSongButton(object sender, RoutedEventArgs e)
        {
            // Wenn add_Button geklickt wird
            Button add_Button = (Button)sender;

            // Input Felder sind an Song (im DataContext) gebunden
            Song add_song = add_Button.DataContext as Song;

            // Aktuelle Playlist pl
            Playlist pl = songsStackPanel.DataContext as Playlist;
            if (pl == null)
            {
                Console.WriteLine("Playlist not found");
            }

            // Song hinzufügen und Playlist neu Laden
            pl.Songs.Add(add_song);
            loadPlaylist(pl);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }




    }
}
