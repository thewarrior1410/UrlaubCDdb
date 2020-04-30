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

        private Playlist pl = new Playlist();

        public PlaylistLabel()
        {
            InitializeComponent();
            lb.Content = pl.playlist_name;
        }


        private void lb_GotFocus(object sender, RoutedEventArgs e)
        {
            //Wenn Playlist angeklickt wird: Rechts im Fenster Songs alle Songs in der Playlist anzeigen
            //onLoadPlaylist(sender);
        }
    }
}
