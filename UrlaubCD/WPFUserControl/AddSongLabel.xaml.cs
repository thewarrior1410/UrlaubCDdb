using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace UrlaubCD.WPFUserControl
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class AddSongLabel : UserControl, INotifyPropertyChanged
    {

        private Song song = new Song();
        public Song Song
        {
            get { return song; }
            set
            {
                if (song != value)
                {
                    song = value;
                    OnPropertyChanged();
                }
            }
        }

        public string s { get; set; }


        public AddSongLabel()
        {
            InitializeComponent();
            DataContext = this.DataContext;
        }

        public void button_add_song(object sender, RoutedEventArgs e)
        {
            this.Song.Song_name = song_inp.Text;
            this.Song.Interpret_name = interpret_inp.Text;
            Console.WriteLine("Song: "+this.Song.Song_name+", Interpret: " + this.Song.Interpret_name);
            // add this Song to the pl in MainWindow songStackPanel; 
            // sync corresponding Playlist in playlistStackPanel and reload songlist (songStackPanel)
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
