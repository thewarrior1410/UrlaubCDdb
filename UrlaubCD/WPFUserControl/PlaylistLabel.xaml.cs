using System;
using System.Windows;
using System.Windows.Controls;
using UrlaubCD.Data;

namespace UrlaubCD.WPFUserControl
{
    /// <summary>
    /// Interaktionslogik für PlaylistLabel.xaml
    /// </summary>
    public partial class PlaylistLabel : UserControl
    {
        public Playlist Playlist { get; set; }

        public PlaylistLabel()
        {
            InitializeComponent();
            Playlist = new Playlist();
            lb.Content = Playlist.Playlist_name;
        }


        private void lb_GotFocus(object sender, RoutedEventArgs e)
        {
            //Wenn Playlist angeklickt wird: Rechts im Fenster Songs alle Songs in der Playlist anzeigen
            //onLoadPlaylist(sender);
        }
    }
}
