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
    /// Interaction logic for Main_Page.xaml
    /// </summary>
    public partial class Main_Page : Page
    {
        public int _day = 0;
        public String[] _dayStrings = new String[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        public Main_Page()
        {
            InitializeComponent();
            Exercise_Text_Block.Text = ApplicationServices.Plan.planToString(_day);
            Exercise_tab_left_button.IsEnabled = false;
            Day_of_the_Week_Label.Content = _dayStrings[0];
            Welcome_Banner.Content = $@"Welcome {ApplicationServices.User.FirstName}!";

            try
            {
                BitmapImage image = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Images\" + ApplicationServices.User.XMLFile.SelectSingleNode(@"User/ProfilePicture").InnerText.Trim(), UriKind.Absolute));
                Profile_Image_Exercise.Source = image;
                Profile_Image_Nutrition.Source = image;
                Profile_Image_Statistics.Source = image;
            }
            catch (System.IO.FileNotFoundException)
            {
                BitmapImage image = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Images\stock profile image.jpg", UriKind.Absolute));
                Profile_Image_Exercise.Source = image;
                Profile_Image_Nutrition.Source = image;
                Profile_Image_Statistics.Source = image;
            }
        }

        private void profileButtonClick(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;
            mainWindow.Content = new Profile_Page();
        }

        private void Log_out_hyperlink_Click(object sender, RoutedEventArgs e)
        {
            ApplicationServices.User = null;

            Window mainWindow = Application.Current.MainWindow;
            mainWindow.Content = new Login_Page();
        }

        private void Exercise_tab_Right_button_Click(object sender, RoutedEventArgs e)
        {
            // 0-6 - Monday-Sunday
            _day++;

            if (_day == 6)
            {
                Exercise_tab_Right_button.IsEnabled = false;
                Day_of_the_Week_Label.Content = _dayStrings[_day];
                Exercise_Text_Block.Text = ApplicationServices.Plan.planToString(_day);
            }
            else
            {
                Exercise_tab_Right_button.IsEnabled = true;
                Exercise_tab_left_button.IsEnabled = true;
                Day_of_the_Week_Label.Content = _dayStrings[_day];
                Exercise_Text_Block.Text = ApplicationServices.Plan.planToString(_day);
            }
        }

        public Uri ProfileImage
        {
            get
            {
                BitmapImage image = new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Images\" + ApplicationServices.User.XMLFile.SelectSingleNode(@"User/ProfilePicture").InnerText.Trim(), UriKind.Relative));

                return image.UriSource;
            }
        }

        private void Exercise_tab_left_button_Click(object sender, RoutedEventArgs e)
        {
            // 0-6 - Monday-Sunday
            _day--;

            if (_day == 0)
            {
                Exercise_tab_left_button.IsEnabled = false;
                Day_of_the_Week_Label.Content = _dayStrings[_day];
                Exercise_Text_Block.Text = ApplicationServices.Plan.planToString(_day);
            }
            else
            {
                Exercise_tab_left_button.IsEnabled = true;
                Exercise_tab_Right_button.IsEnabled = true;
                Day_of_the_Week_Label.Content = _dayStrings[_day];
                Exercise_Text_Block.Text = ApplicationServices.Plan.planToString(_day);
            }
        }
    }
}
