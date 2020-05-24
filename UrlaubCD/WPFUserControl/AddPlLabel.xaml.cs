using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace UrlaubCD.WPFUserControl
{
    /// <summary>
    /// Interaktionslogik für AddPlLabel.xaml
    /// </summary>
    public partial class AddPlLabel : UserControl, INotifyPropertyChanged
    {
        public AddPlLabel()
        {
            InitializeComponent();
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
