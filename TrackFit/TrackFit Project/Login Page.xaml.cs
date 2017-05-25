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
using System.IO;
using System.Xml;
//using static TrackFit_Project.MainWindow;

namespace TrackFit_Project
{
    /// <summary>
    /// Interaction logic for Login_Page.xaml
    /// </summary>
    public partial class Login_Page : Page
    {
        public Login_Page()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This event will verify the username and password entered.
        /// </summary>
        /// <param name="sender">Login_Button obj</param>
        /// <param name="e">?notsure?</param>
        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {

            XmlDocument profileDoc = new XmlDocument();
            String userPassword = "";
            String path = System.IO.Path.Combine(Environment.CurrentDirectory, $@"User Profiles\{Username_Text_Box.Text}.xml");

            try
            {
                profileDoc.Load(path);
                userPassword = profileDoc.SelectSingleNode("User/Password").InnerText.Trim();
            }
            catch (Exception)
            {
                showLoginError();
                return;
            }

            if ( Password_Text_Box.Password == userPassword )
            {
                ApplicationServices.User = new UserProfile(path);

                System.Windows.MessageBox.Show(ApplicationServices.User.FirstName );

                Window mainWindow = Application.Current.MainWindow;
                mainWindow.Content = new Main_Page();
            }
            else
            {
                showLoginError();
            }
        }

        public void showLoginError()
        {
            Login_Error.Visibility = Visibility.Visible;
            Username_Text_Box.Clear();
            Password_Text_Box.Clear();
        }
    }
}
