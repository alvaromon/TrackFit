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
            BitmapImage image = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Images\" + ApplicationServices.User.XMLFile.SelectSingleNode(@"User/ProfilePicture").InnerText.Trim(), UriKind.Absolute));
            Profile_Page_Profile_Image.Source = image;
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
       
        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            this.CheckBox1.IsChecked = this.CheckBox2.IsChecked = this.CheckBox3.IsChecked = this.CheckBox4.IsChecked = this.CheckBox5.IsChecked =
                this.CheckBox6.IsChecked = this.CheckBox7.IsChecked = this.CheckBox8.IsChecked = false;
        }
        
        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Great Job!!! Your Meal Plan was Created");
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Meal Item Added");
        }
    }
}
