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
using MySql.Data.MySqlClient;
using System.Text;
using System.Data;

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

            // fill pl_list from db
            pl_list = load_pl_list();

            // Load existing Playlists on Initialization (and provide AddPlLabel)
            loadPlaylistStack();
        }


        // List of Playlists
        public List<Playlist> pl_list = new List<Playlist>();


        // Test Button
        private void test(object sender, RoutedEventArgs e)
        {
            // Neues PlaylistLabel mit neuer Playlist
            /*PlaylistLabel plLabel = new PlaylistLabel();
            plLabel.Playlist.Playlist_name = PlaylistStackPanel.Children.Count.ToString();

            pl_list.Add(plLabel.Playlist);
            PlaylistStackPanel.Children.Add(plLabel);*/

        }


        private DataTable getSQLData(String cmd_text, List<string> cNames)
        {
            
            DataTable dt = new DataTable();
            try
            {
                var connstr = "Server=localhost;Uid=root;Pwd=;database=cddb";
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    
                    // load SQL-Command
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = cmd_text;
                        // Execute SQL-Command
                        using (var reader = cmd.ExecuteReader())
                        {
                            var ii = reader.FieldCount;
                            DataColumn dc;
                            DataRow dr;
                            // write all columns to the table
                            for(int i=0; i<cNames.Count(); i++)
                            {
                                dc = new DataColumn();
                                //dc.DataType = System.Type.GetType("System.String");
                                dc.ColumnName = cNames[i];
                                dt.Columns.Add(dc);
                            }

                            // begins new row
                            while (reader.Read())
                            {
                                dr = dt.NewRow();
                                for (int column = 0; column < ii; column++)
                                {
                                    // begins new column
                                    if (reader[column] is DBNull)
                                        dr[cNames[column]] = "null";
                                    else
                                        dr[cNames[column]] = reader[column].ToString();
                                }
                                // save Row
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return dt;
        }



        private List<Playlist> load_pl_list()
        {
            List<Playlist> pl_list_temp = new List<Playlist>();
            
            // SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = 'cddb'
            DataTable pl_names = getSQLData("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_SCHEMA = 'cddb'", new List<string> { "pl_names" });
            
            Console.WriteLine(pl_names.Rows.Count);

            List<string> pl_name_list = pl_names.AsEnumerable().Select(x => x["pl_names"].ToString()).ToList();
            Console.WriteLine("HIHIHIHIHIHI");
            pl_name_list.ForEach(i => Console.Write("{0}\t", i));

            Playlist pl;
            foreach (String pl_name in pl_name_list)
            {
                // ColumnNames (Num/Titel/Artist): "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Osterurlaub 2018'"
                // List of ColumnNames in the Table
                List<string> cNames_list = getSQLData("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + pl_name + "'", new List<string> { "cNames" }).AsEnumerable().Select(x => x["cNames"].ToString()).ToList();
                
                // SELECT * FROM `osterurlaub 2018` ORDER BY `Nummer`
                // Table of Songs in the Playlist (pl_name)
                DataTable plTable = getSQLData("SELECT * FROM `" + pl_name + "` ORDER BY `Nummer`", cNames_list);
                
                // Create new Playlist and fill it with Songs from the db
                pl = new Playlist();
                pl.Playlist_name = pl_name;
                for (int row = 0; row < plTable.Rows.Count; row++)
                {
                    pl.Songs.Add(new Song(Convert.ToInt32(plTable.Rows[row].Field<string>("Nummer")), plTable.Rows[row].Field<string>("Titel"), plTable.Rows[row].Field<string>("Kuenstler")));
                }
                pl_list_temp.Add(pl);
            }
            return pl_list_temp;
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
                SongLabel sL = new SongLabel(songs[i]);
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

            // Track_Number: Letztes Lied in der Playlist
            add_song.Track_number = pl.Songs.Count + 1;

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
