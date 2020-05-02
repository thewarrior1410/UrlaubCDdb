using System;

namespace UrlaubCD.Data
{
    public class Song
    {
        
        public string Song_name { get; set; }
        public string Interpret_name { get; set; }
        

        public Song()
        {
            Song_name = "";
            Interpret_name = "";
        }

        public Song(string name, string interpret)
        {
            Song_name = name;
            Interpret_name = interpret;
        }

    }
}
