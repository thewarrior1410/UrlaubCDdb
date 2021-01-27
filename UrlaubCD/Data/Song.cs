using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UrlaubCD.Data
{
    public class Song : INotifyPropertyChanged
    {

        private int track_number;
        public int Track_number
        {
            get { return track_number; }
            set
            {
                track_number = value;
                OnPropertyChanged("Track_Number");
            }
        }

        private string song_name;
        public string Song_name 
        {
            get { return song_name; }
            set
            {
                song_name = value;
                OnPropertyChanged("Song_Name");
            }
        }

        private string interpret_name;
        public string Interpret_name 
        {
            get { return interpret_name; }
            set
            {
                interpret_name = value;
                OnPropertyChanged("Interpret_Name");
            }
        }
        

        public Song()
        {
            Track_number = 0;
            Song_name = "";
            Interpret_name = "";
        }

        public Song(int num, string name, string interpret)
        {
            Track_number = num;
            Song_name = name;
            Interpret_name = interpret;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                Console.WriteLine("PropertyCanged!!!");
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }



    }
}
