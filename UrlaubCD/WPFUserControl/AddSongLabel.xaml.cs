using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using UrlaubCD.Data;

namespace UrlaubCD.WPFUserControl
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class AddSongLabel : UserControl, INotifyPropertyChanged
    {

        /*
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
        */


        public AddSongLabel()
        {
            InitializeComponent();
            DataContext = this.DataContext;
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
