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
using System.Xml;

namespace TrackFit_Project
{
    /// <summary>
    /// Interaction logic for Create_Account_Page.xaml
    /// </summary>
    public partial class Create_Account_Page : Page
    {
        public Create_Account_Page()
        {
            InitializeComponent();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            Window mainWindow = Application.Current.MainWindow;
            mainWindow.Content = new Login_Page();
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            if ( CheckInputs() )
            {
                // Create profile XML
                CreateProfileXML();

                // Build path string and create and set TrackFit user account
                String path = System.IO.Path.Combine(Environment.CurrentDirectory, $@"User Profiles\{Username_Text_Box.Text}.xml");
                ApplicationServices.User = new UserProfile(path);

                // Create plan for new user
                ApplicationServices.Plan = new ExercisePlan();

                // Load up the Main Page
                Window mainWindow = Application.Current.MainWindow;
                mainWindow.Content = new Main_Page();
            }
        }

        private bool CheckInputs()
        {
            if (Password_Text_Box.Text != null &&
                Username_Text_Box.Text != null &&
                First_Name_Text_Box.Text != null &&
                Last_Name_Text_Box.Text != null &&
                Age_Text_Box.Text != null && Convert.ToInt32(Age_Text_Box.Text) > 0 &&
                Weight_Text_Box.Text != null && Convert.ToInt32(Weight_Text_Box.Text) > 0 &&
                Height_Text_Box.Text != null && Convert.ToInt32(Height_Text_Box.Text) > 0 &&
                Exercise_Level_Combo_Box.Text != null && Exercise_Level_Combo_Box.Text != "Exercise Goal" &&
                Exercise_Level_Combo_Box.Text != null && Exercise_Level_Combo_Box.Text != "Exercise Level")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// This will create a new Profile XML File for a new User according to info in create acct page.
        /// </summary>
        public void CreateProfileXML()
        {
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("User");
            XmlAttribute Attribute = doc.CreateAttribute(string.Empty, "name", string.Empty);
            Attribute.Value = $@"{First_Name_Text_Box.Text}" + " " + $@"{Last_Name_Text_Box.Text}";
            root.Attributes.Append(Attribute);
            doc.AppendChild(root);

            XmlElement Element = doc.CreateElement(string.Empty, "Plan", string.Empty);
            XmlText text = doc.CreateTextNode("False");
            Element.AppendChild(text);
            root.AppendChild(Element);

            Element = doc.CreateElement(string.Empty, "Username", string.Empty);
            text = doc.CreateTextNode($@"{Username_Text_Box.Text}");
            Element.AppendChild(text);
            root.AppendChild(Element);

            Element = doc.CreateElement(string.Empty, "Password", string.Empty);
            text = doc.CreateTextNode($@"{Password_Text_Box.Text}");
            Element.AppendChild(text);
            root.AppendChild(Element);

            if (Exercise_Goal_Combo_Box.SelectedIndex == 1)
            {
                Element = doc.CreateElement(string.Empty, "ExerciseGoal", string.Empty);
                text = doc.CreateTextNode("Weight_Loss");
                Element.AppendChild(text);
                root.AppendChild(Element);
            }
            else if (Exercise_Goal_Combo_Box.SelectedIndex == 2)
            {
                Element = doc.CreateElement(string.Empty, "ExerciseGoal", string.Empty);
                text = doc.CreateTextNode("Strength_Gain");
                Element.AppendChild(text);
                root.AppendChild(Element);
            }
            else
            {
                Element = doc.CreateElement(string.Empty, "ExerciseGoal", string.Empty);
                text = doc.CreateTextNode("Tone");
                Element.AppendChild(text);
                root.AppendChild(Element);
            }

            if (Exercise_Level_Combo_Box.SelectedIndex == 1)
            {
                Element = doc.CreateElement(string.Empty, "ExerciseLevel", string.Empty);
                text = doc.CreateTextNode("0");
                Element.AppendChild(text);
                root.AppendChild(Element);
            }
            else if (Exercise_Level_Combo_Box.SelectedIndex == 2)
            {
                Element = doc.CreateElement(string.Empty, "ExerciseLevel", string.Empty);
                text = doc.CreateTextNode("1");
                Element.AppendChild(text);
                root.AppendChild(Element);
            }
            else
            {
                Element = doc.CreateElement(string.Empty, "ExerciseLevel", string.Empty);
                text = doc.CreateTextNode("2");
                Element.AppendChild(text);
                root.AppendChild(Element);
            }

            Element = doc.CreateElement(string.Empty, "FirstName", string.Empty);
            text = doc.CreateTextNode($@"{First_Name_Text_Box.Text}");
            Element.AppendChild(text);
            root.AppendChild(Element);

            Element = doc.CreateElement(string.Empty, "LastName", string.Empty);
            text = doc.CreateTextNode($@"{Last_Name_Text_Box.Text}");
            Element.AppendChild(text);
            root.AppendChild(Element);

            Element = doc.CreateElement(string.Empty, "Height", string.Empty);
            text = doc.CreateTextNode($@"{Height_Text_Box.Text}");
            Element.AppendChild(text);
            root.AppendChild(Element);

            Element = doc.CreateElement(string.Empty, "Weight", string.Empty);
            text = doc.CreateTextNode($@"{Weight_Text_Box.Text}");
            Element.AppendChild(text);
            root.AppendChild(Element);

            Element = doc.CreateElement(string.Empty, "Age", string.Empty);
            text = doc.CreateTextNode($@"{Age_Text_Box.Text}");
            Element.AppendChild(text);
            root.AppendChild(Element);

            doc.Save(Environment.CurrentDirectory + $@"\User Profiles\{Username_Text_Box.Text}.xml");


        }

    }
}
