using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TrackFit_Project
{

    #region Enums

    public enum ExerciseLevel { Beginner, Intermediate, Expert };
    public enum ExerciseGoal { Tone, Weight_Loss, Strength }

    #endregion

    public class UserProfile
    {
        #region Member Variables

        private String _firstName;
        private String _lastName;
        private int _age;
        private double _height;
        private double _weight;
        private ExerciseLevel _exerciseLevel;
        private ExerciseGoal _exerciseGoal;
        private XmlDocument _xmlFile;
        private XmlDocument _xmlExerciseFile;
        private DateTime _startOfWeek;
        private String _path;
        private bool _planCreated;

        #endregion

        #region ctor

        /// <summary>
        /// Default constructor
        /// </summary>
        public UserProfile()
        {

        }

        /// <summary>
        /// Standard constructor
        /// </summary>
        /// <param name="path">Path to the users profile .xml file, post login verification.</param>
        public UserProfile(String path)
        {
            this._path = path;

            this._xmlFile = new XmlDocument();

            this._xmlFile.Load(path);

            this._xmlExerciseFile = new XmlDocument();
            this._xmlExerciseFile.Load(Path.Combine(Environment.CurrentDirectory, $@"Exercises\{goalString()}.xml"));

            readProfileInfo();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Property used to get or set the user's first name
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                _firstName = value;
            }
        }

        public DateTime StartOfWeek
        {
            get
            {
                return _startOfWeek;
            }

            set
            {
                _startOfWeek = value;

                XmlElement Element = _xmlFile.CreateElement(string.Empty, "StartOfWeek", string.Empty);
                XmlText text = _xmlFile.CreateTextNode(value.ToLongDateString());
                Element.AppendChild(text);

                _xmlFile.SelectSingleNode(@"User/StartOfWeek").InnerText = value.ToLongDateString();
                _xmlFile.SelectSingleNode(@"User").ReplaceChild(Element, _xmlFile.SelectSingleNode(@"User/StartOfWeek"));
            }
        }
        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
            }
        }

        public int Age { get; set; }
        public double Height { get; set;  }
        public double Weight { get; set; }
        public XmlDocument XMLFile
        {
            get
            {
                return _xmlFile;
            }
            set
            {
                _xmlFile = value;
            }
        }
        public ExerciseLevel ExerciseLevel
        {
            get
            {
                return _exerciseLevel;
            }
            set
            {
                _exerciseLevel = value;
            }
        }
        public ExerciseGoal ExerciseGoal
        {
            get
            {
                return _exerciseGoal;
            }

            set
            {
                _exerciseGoal = value;
            }
        }
        public bool PlanCreated
        {
            get
            {
                return _planCreated;
            }

            set
            {
                _planCreated = value;

                XmlElement Element = _xmlFile.CreateElement(string.Empty, "Plan", string.Empty);
                XmlText text = _xmlFile.CreateTextNode(value.ToString());
                Element.AppendChild(text);

                _xmlFile.SelectSingleNode(@"User/Plan").InnerText = value.ToString();
                _xmlFile.SelectSingleNode(@"User").ReplaceChild(Element, _xmlFile.SelectSingleNode(@"User/Plan"));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fills in UserProfile Class information.
        /// </summary>
        private void readProfileInfo()
        {
            this._startOfWeek = Convert.ToDateTime(_xmlFile.SelectSingleNode(@"User/StartOfWeek").InnerText);
            this._firstName = _xmlFile.SelectSingleNode(@"User/FirstName").InnerText.Trim();
            this._lastName = _xmlFile.SelectSingleNode(@"User/LastName").InnerText.Trim();
            this._age = Int32.Parse( _xmlFile.SelectSingleNode(@"User/Age").InnerText.Trim() );
            this._height = Int32.Parse( _xmlFile.SelectSingleNode(@"User/Height").InnerText.Trim() );
            this._weight = Int32.Parse( _xmlFile.SelectSingleNode(@"User/Weight").InnerText.Trim() );
            this._exerciseLevel= (ExerciseLevel) Int32.Parse( _xmlFile.SelectSingleNode(@"User/ExerciseLevel").InnerText.Trim() );

            if (_xmlFile.SelectSingleNode(@"User/ExerciseGoal").InnerText.Trim() == "Weight_Loss")
            {
                this._exerciseGoal = ExerciseGoal.Weight_Loss;
            }
            else if (_xmlFile.SelectSingleNode(@"User/ExerciseGoal").InnerText.Trim() == "Strength_Gain")
            {
                this._exerciseGoal = ExerciseGoal.Strength;
            }
            else
            {
                this._exerciseGoal = ExerciseGoal.Tone;
            }

            if ( _xmlFile.SelectSingleNode(@"User/Plan").InnerText.Trim() == "True" )
            {
                this._planCreated = true;
            }
            else
            {
                this._planCreated = false;
            }
        }

        /// <summary>
        ///  This will return a List of all the cardio exercises in the users exercise goal and exercise level
        /// </summary>
        public List<XmlNode> getCardioExercises()
        {
            XmlDocument nodeList = ApplicationServices.User._xmlExerciseFile;
            List<XmlNode> exerciseList = new List<XmlNode>();

            nodeList = _xmlExerciseFile;

            foreach ( XmlNode node in nodeList.SelectNodes( $@"Exercises/{_exerciseLevel}/Workout" ) )
            {
                if ( node.Attributes["target"].Value == "cardio")
                {
                    exerciseList.Add(node);
                }
            }

            return exerciseList;
        }

        /// <summary>
        ///  This will return a List of all the upperbody exercises in the users exercise goal and exercise level
        /// </summary>
        public List<XmlNode> getUpperbodyExercises()
        {
            XmlDocument nodeList = ApplicationServices.User._xmlExerciseFile;
            List<XmlNode> exerciseList = new List<XmlNode>();

            nodeList = _xmlExerciseFile;

            foreach (XmlNode node in nodeList.SelectNodes($@"Exercises/{_exerciseLevel}/Workout"))
            {
                if (node.Attributes["target"].Value == "upperbody")
                {
                    exerciseList.Add(node);
                }
            }

            return exerciseList;
        }

        /// <summary>
        ///  This will return a List of all the leg exercises in the users exercise goal and exercise level
        /// </summary>
        public List<XmlNode> getLegExercises()
        {
            XmlDocument nodeList = ApplicationServices.User._xmlExerciseFile;
            List<XmlNode> exerciseList = new List<XmlNode>();

            nodeList = _xmlExerciseFile;

            foreach (XmlNode node in nodeList.SelectNodes($@"Exercises/{_exerciseLevel}/Workout"))
            {
                if (node.Attributes["target"].Value == "legs")
                {
                    exerciseList.Add(node);
                }
            }

            return exerciseList;
        }

        /// <summary>
        ///  This will return a List of all the cardio exercises in the users exercise goal and exercise level
        /// </summary>
        public List<XmlNode> getCoreExercises()
        {
            XmlDocument nodeList = ApplicationServices.User._xmlExerciseFile;
            List<XmlNode> exerciseList = new List<XmlNode>();

            nodeList = _xmlExerciseFile;

            foreach (XmlNode node in nodeList.SelectNodes($@"Exercises/{_exerciseLevel}/Workout"))
            {
                if (node.Attributes["target"].Value == "core")
                {
                    exerciseList.Add(node);
                }
            }

            return exerciseList;
        }

        /// <summary>
        ///  This will return a List with a Rest Day Node
        /// </summary>
        public List<XmlNode> getRest()
        {
            XmlDocument nodeList = ApplicationServices.User._xmlExerciseFile;
            List<XmlNode> exerciseList = new List<XmlNode>();

            nodeList = _xmlExerciseFile;

            foreach (XmlNode node in nodeList.SelectNodes($@"Exercises/{_exerciseLevel}/Workout"))
            {
                if (node.Attributes["target"].Value == "rest")
                {
                    exerciseList.Add(node);
                }
            }

            return exerciseList;
        }

        /// <summary>
        /// This function will return a string containing the current user's exercise goal.
        /// </summary>
        /// <returns>The user's exervcise goal is returned</returns>
        public String goalString()
        {
            if (_xmlFile.SelectSingleNode(@"User/ExerciseGoal").InnerText.Trim() == "Weight_Loss")
            {
                return "Weight_Loss";
            }
            else if (_xmlFile.SelectSingleNode(@"User/ExerciseGoal").InnerText.Trim() == "Strength")
            {
                return "Strength";
            }
            else
            {
                return "Tone";
            }
        }

        #endregion

    }
}
