using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UrlaubCD.WPFUserControl
{
    /// <summary>
    /// Interaktionslogik für SearchBox.xaml
    /// </summary>
    public partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();
        }

        private void onGotFocus(object sender, RoutedEventArgs e)
        {
            if (txtBox.Text == "Search...")
            {
                txtBox.Text = "";
                txtBox.Foreground = Brushes.Black;
            }
        }

        private void onLostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBox.Text == "")
            {
                txtBox.Text = "Search...";
                txtBox.Foreground = Brushes.Gray;
            }

        }

    }
}
