using System;
using System.Collections.Generic;
using System.Linq;

namespace UrlaubCD.Data
{
    public class Playlist
    {
        public List<Song> Songs { get; set; } = new List<Song>();

        public string Playlist_name { get; set; }

        public Playlist()
        {
            // Playlist_name = "Playlist 1";
            // Songs.Add(new Song("Supper's Ready", "Genesis"));
        }

        public void addSong(Song song)
        {
            Songs.Add(song);
        }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            // Wenn Name und alle Songs gleich sind -> true
            Playlist objPl = obj as Playlist;
            if (this.Playlist_name == objPl.Playlist_name
                && !this.Songs.Except(objPl.Songs).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
