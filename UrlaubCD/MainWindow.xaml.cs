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
            
            // Load existing Playlists on Initialization (and provide AddPlLabel)
            loadPlaylistStack();
        }


        // List of Playlists
        List<Playlist> pl_list = new List<Playlist>();


        // Test Button
        private void test(object sender, RoutedEventArgs e)
        {
            // Neues PlaylistLabel mit neuer Playlist
            PlaylistLabel plLabel = new PlaylistLabel();
            
            plLabel.Playlist.Playlist_name = PlaylistStackPanel.Children.Count.ToString();

            

            pl_list.Add(plLabel.Playlist);
            PlaylistStackPanel.Children.Add(plLabel);
        }



        // Load Playlist Panel
        private void loadPlaylistStack()
        {
            // Wenn aktuell eine Playlist ausgewählt ist, sonst null
            PlaylistLabel selected_PL = getSelectedPlL();

            // Remove all Children
            PlaylistStackPanel.Children.RemoveRange(0, PlaylistStackPanel.Children.Count);

            // DataContext auf Liste der Playlists setzen
            PlaylistStackPanel.DataContext = pl_list;

            // Alle Playlists in PlaylistStackPanel anzeigen
            for (int i = 0; i < pl_list.Count; i++)
            {
                PlaylistLabel pL = new PlaylistLabel(pl_list[i]);
                pL.MouseLeftButtonDown += new MouseButtonEventHandler(OnOpenPlaylist);
                PlaylistStackPanel.Children.Add(pL);
            }

            // Wenn vorher eine Playlist ausgewählt war, diese wieder auswählen
            if (selected_PL != null)
            {
                foreach (PlaylistLabel plL in PlaylistStackPanel.Children)
                {
                    if (plL.Playlist.Equals(selected_PL.Playlist))
                    {
                        plL.isSelected = true;
                    }
                }
            }

            appendAddPlLabel(PlaylistStackPanel);
        }

        public void appendAddPlLabel(StackPanel PlaylistStackPanel)
        {
            // Create AddPlLabel
            AddPlLabel aPL = new AddPlLabel();

            Playlist add_pl = new Playlist();
            aPL.DataContext = add_pl;

            // Binding to add_pl (as DataContext) Property: Playlist_name
            Binding add_PName_Binding = new Binding("Playlist_name");

            // Bind to Text of TextBox
            aPL.pl_inp.SetBinding(TextBox.TextProperty, add_PName_Binding);

            aPL.add_Button.Click += new RoutedEventHandler(addPlaylistButton);

            PlaylistStackPanel.Children.Add(aPL);

        }

        public void addPlaylistButton(object sender, RoutedEventArgs e)
        {
            // Wenn add_Button geklickt wird
            Button add_Button = (Button)sender;

            // Inputfeld (Playlist_name) ist an Playlist (im DataContext) gebunden
            Playlist add_pl = add_Button.DataContext as Playlist;

            pl_list.Add(add_pl);
            loadPlaylistStack();
        }



        // Open the Playlists Songlist when Clicked
        public void OnOpenPlaylist(object sender, RoutedEventArgs e)
        {
            PlaylistLabel plL = sender as PlaylistLabel;
            Playlist pl = plL.Playlist;

            // wenn Playlist bereits geladen ist
            if (pl.Equals(songsStackPanel.DataContext as Playlist))
                return;

            resetSelectionPlaylistPanel();
            plL.isSelected = true;

            loadPlaylistSongs(pl);
        }



        // Load Songlist
        public void loadPlaylistSongs(Playlist pl)
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
            loadPlaylistSongs(pl);
        }




        // Utility Methods
        private PlaylistLabel getSelectedPlL()
        {
            for (int i = 0; i < PlaylistStackPanel.Children.Count - 1; i++)
            {
                PlaylistLabel playlistLabel = PlaylistStackPanel.Children[i] as PlaylistLabel;
                if (playlistLabel.isSelected)
                    return playlistLabel;
            }
            return null;
        }


        public void resetSelectionPlaylistPanel()
        {
            for (int i = 0; i < PlaylistStackPanel.Children.Count-1; i++)
            {
                PlaylistLabel playlistLabel = PlaylistStackPanel.Children[i] as PlaylistLabel;
                playlistLabel.isSelected = false;
            }
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
