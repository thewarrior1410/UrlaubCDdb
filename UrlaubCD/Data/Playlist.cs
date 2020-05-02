using System;
using System.Collections.Generic;

namespace UrlaubCD.Data
{
    public class Playlist
    {

        public List<Song> Songs { get; set; } = new List<Song>();


        public string Playlist_name { get; set; }

        public Playlist()
        {
            Playlist_name = "Playlist 1";
            Songs.Add(new Song("Supper's Ready", "Genesis"));
        }

        public void addSong(Song song)
        {
            Songs.Add(song);
        }




    }
}
