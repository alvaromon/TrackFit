using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

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
            Window mainWindow = System.Windows.Application.Current.MainWindow;
            mainWindow.Content = new Login_Page();
        }

        private void Create_Button_Click(object sender, RoutedEventArgs e)
        {
            if ( CheckInputs() )
            {
                // Create profile XML
                CreateProfileXML();

                // Build path string and create and set TrackFit user account
                String path = Path.Combine(Environment.CurrentDirectory, $@"User Profiles\{Username_Text_Box.Text}.xml");
                ApplicationServices.User = new UserProfile(path);

                // Create plan for new user
                ApplicationServices.Plan = new ExercisePlan();
                ApplicationServices.User.PlanCreated = true;

                // Load up the Main Page
                Window mainWindow = System.Windows.Application.Current.MainWindow;
                mainWindow.Content = new Main_Page();
            }
        }

        private void HighlightEmptyFields()
        {
            Empty_Field_Warning_Text.Visibility = Visibility.Visible;

            Object[] Arr = new Object[7] 
            {
                Password_Text_Box,
                Username_Text_Box,
                First_Name_Text_Box,
                Last_Name_Text_Box,
                Age_Text_Box,
                Weight_Text_Box,
                Height_Text_Box,
               
            };

            Object[] ComboBoxArr = new Object[2]
            {
                Exercise_Goal_Combo_Box,
                Exercise_Level_Combo_Box
            };

            foreach (System.Windows.Controls.TextBox obj in Arr)
            {
                if (obj.Text == "")
                {
                    obj.BorderBrush = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    obj.BorderBrush = System.Windows.Media.Brushes.LightGray;
                }
            }

            foreach (System.Windows.Controls.ComboBox obj in ComboBoxArr)
            {
                if (obj.Text == "Exercise Goal" || obj.Text == "Exercise Level")
                {
                    obj.Foreground = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    obj.Foreground = System.Windows.Media.Brushes.Black;
                }
            }

        }

        private bool CheckInputs()
        {
            if (Password_Text_Box.Text != "" &&
                Username_Text_Box.Text != "" &&
                First_Name_Text_Box.Text != "" &&
                Last_Name_Text_Box.Text != "" &&
                Age_Text_Box.Text != "" && Convert.ToInt32(Age_Text_Box.Text) > 0 &&
                Weight_Text_Box.Text != "" && Convert.ToInt32(Weight_Text_Box.Text) > 0 &&
                Height_Text_Box.Text != "" && Convert.ToInt32(Height_Text_Box.Text) > 0 &&
                Exercise_Level_Combo_Box.Text != "" && Exercise_Level_Combo_Box.Text != "Exercise Goal" &&
                Exercise_Level_Combo_Box.Text != "" && Exercise_Level_Combo_Box.Text != "Exercise Level")
            {
                return true;
            }
            else
            {
                HighlightEmptyFields();
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

            Element = doc.CreateElement(string.Empty, "StartOfWeek", string.Empty);
            text = doc.CreateTextNode(DateTime.Now.ToLongDateString());
            Element.AppendChild(text);
            root.AppendChild(Element);

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

            if (Profile_Photo_Text_Box.Text != "")
            {
                Element = doc.CreateElement(string.Empty, "ProfilePicture", string.Empty);
                text = doc.CreateTextNode($@"{Profile_Photo_Text_Box.Text}");
                Element.AppendChild(text);
                root.AppendChild(Element);
            }
            else
            {
                Element = doc.CreateElement(string.Empty, "ProfilePicture", string.Empty);
                text = doc.CreateTextNode(@"stock profile image.jpg");
                Element.AppendChild(text);
                root.AppendChild(Element);
            }

            doc.Save(Environment.CurrentDirectory + $@"\User Profiles\{Username_Text_Box.Text}.xml");
        }

        private void Browse_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Images (*.BMP;*.JPG;*.GIF;*PNG)|*.BMP;*.JPG;*.PNG";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fullPath = dialog.FileName;
                string fileOnlyName = Path.GetFileName(fullPath);

                if (!File.Exists(Environment.CurrentDirectory + $@"\Images\{fileOnlyName}"))
                {
                    File.Copy(fullPath, Environment.CurrentDirectory + $@"\Images\{fileOnlyName}");
                    Profile_Photo_Text_Box.Text = fileOnlyName;
                }
                else
                {
                    int counter= 0;
                    string fileName = Environment.CurrentDirectory + $@"\Images\{fileOnlyName}";

                    while (File.Exists(fileName))
                    {
                        fileName = Environment.CurrentDirectory + $@"\Images\{++counter}{fileOnlyName}";
                    }

                    File.Copy(fullPath, fileName);
                    Profile_Photo_Text_Box.Text = $"{counter}" + fileOnlyName;
                }
            }
        }
    }
}
