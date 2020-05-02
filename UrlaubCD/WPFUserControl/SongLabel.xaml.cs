using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UrlaubCD.Data;

namespace UrlaubCD.WPFUserControl
{
    /// <summary>
    /// Interaktionslogik für SongLabel.xaml
    /// </summary>
    public partial class SongLabel : UserControl
    {

        public Song Song { get; set; }

        public SongLabel(Song s, int num)
        {
            InitializeComponent();
            this.Song = s;
            number.Content = num;
            song.Content = s.Song_name;
            interpret.Content = s.Interpret_name;

        }
    }
}
