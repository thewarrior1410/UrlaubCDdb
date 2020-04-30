using System;
using System.Collections.Generic;

namespace UrlaubCD.Data
{
    class Playlist
    {

        public List<Song> songs = new List<Song>();
        public String playlist_name { get; set; }

        public Playlist()
        {
            playlist_name = "Playlist 1";
            songs.Add(new Song("Genesis"));

        }

        public void addSong(Song song)
        {
            songs.Add(song);
        }




    }
}
