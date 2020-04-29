using System;
using System.Collections.Generic;
using System.Linq;
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

namespace UrlaubCD
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void test(object sender, RoutedEventArgs e)
        {

            PlaylistStackPanel.Children.Add(new Label
            {
                Height = 30,
                Content = PlaylistStackPanel.Children.Count,
                Background = getRandomColor()
            });

        }

        public SolidColorBrush getRandomColor()
        {
            Random r = new Random();
            SolidColorBrush scb = new SolidColorBrush(Color.FromRgb((byte)r.Next(255), (byte)r.Next(255), (byte)r.Next(255)));
            return scb;
        }

    }
}
