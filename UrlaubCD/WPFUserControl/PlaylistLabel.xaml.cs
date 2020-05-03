using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using UrlaubCD.Data;

namespace UrlaubCD.WPFUserControl
{
    /// <summary>
    /// Interaktionslogik für PlaylistLabel.xaml
    /// </summary>
    public partial class PlaylistLabel : UserControl
    {
        public Playlist Playlist { get; set; }

        private bool _isSelected = false;
        public bool isSelected 
        { 
            get { return _isSelected; }
            set 
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnSelectionUpdate(this, EventArgs.Empty);
                }
            }
        }

        public PlaylistLabel()
        {
            InitializeComponent();
            Playlist = new Playlist();

            Binding plName_Binding = new Binding("Playlist_name");
            plName_Binding.Source = Playlist;

            lb.SetBinding(Label.ContentProperty, plName_Binding);

        }


        public event EventHandler SelectionUpdate;
        private void OnSelectionUpdate(object sender, EventArgs e)
        {
            Console.WriteLine("isSeleced Changed for" + ((PlaylistLabel)sender).Playlist.Playlist_name +"to "+isSelected.ToString());
            if (isSelected == true)
            {
                lb.Style = Resources["isSelected"] as Style;
            } else
            {
                lb.Style = Resources["isNOTSelected"] as Style;
            }

            if (SelectionUpdate != null)
            {
                SelectionUpdate(this, EventArgs.Empty);
            }
        }


        /*
        private void lb_GotFocus(object sender, RoutedEventArgs e)
        {
            //Wenn Playlist angeklickt wird: Rechts im Fenster Songs alle Songs in der Playlist anzeigen
            //onLoadPlaylist(sender);
        }
        */

    }
}
