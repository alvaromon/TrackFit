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

namespace TrackFit_Project
{
    /// <summary>
    /// Interaction logic for Profile_Page.xaml
    /// </summary>
    public partial class Profile_Page : Page
    {
        public Profile_Page()
        {
            InitializeComponent();
        }

        private void exitProfilePage(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;
            mainWindow.Content = new Main_Page();
        }

        private void WeightChart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BMIChart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
