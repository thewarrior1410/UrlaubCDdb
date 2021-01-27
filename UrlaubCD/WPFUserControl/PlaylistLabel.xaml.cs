using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
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

            Height = 30;
            Background = Brushes.DarkGray;
            Foreground = Brushes.White;

            lb.Content = "_";
        }

        public PlaylistLabel(Playlist pl)
        {
            InitializeComponent();
            Playlist = pl;

            Height = 30;
            Background = Brushes.DarkGray;
            Foreground = Brushes.White;

            lb.Content = pl.Playlist_name;
        }


        public SolidColorBrush getRandomColor()
        {
            Random r = new Random();
            SolidColorBrush scb = new SolidColorBrush(Color.FromRgb((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255)));
            return scb;
        }


        public event EventHandler SelectionUpdate;
        private void OnSelectionUpdate(object sender, EventArgs e)
        {
            Console.WriteLine("isSeleced Changed for " + ((PlaylistLabel)sender).Playlist.Playlist_name +" to "+isSelected.ToString());
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

    }
}
