using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TrackFit_Project
{

    #region

    public enum ExerciseLevel { Beginner, Intermediate, Expert };
    public enum ExerciseGoal { Tone, Weight_Loss, Strength_Gain, Maintainance }

    #endregion

    public class UserProfile
    {
        #region Member Variables

        private String _lastName;
        private String _firstName;
        private int _age;
        private DateTime _birthday;
        private double _height;
        private double _weight;
        private ExerciseLevel _exerciseLevel;
        private ExerciseGoal _exerciseGoal;
        private XmlDocument _xmlFile;
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
            readProfileInfo();
        }

        #endregion

        #region Properties

        public String FirstName{ get; set; }
        public String LastName { get; set; }
        public int Age { get; set; }
        public double Height { get; set;  }
        public double Weight { get; set; }
        public ExerciseLevel ExerciseLevel { get; set; }
        public ExerciseGoal ExerciseGoal { get; set; }
        public bool PlanCreated { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Fills in UserProfile Class information.
        /// </summary>
        private void readProfileInfo()
        {
            this._firstName = _xmlFile.SelectSingleNode(@"User/FirstName").InnerText.Trim();
            this._lastName = _xmlFile.SelectSingleNode(@"User/LastName").InnerText.Trim();
            this._age = Int32.Parse( _xmlFile.SelectSingleNode(@"User/Age").InnerText.Trim() );
            this._height = Int32.Parse( _xmlFile.SelectSingleNode(@"User/HeightInches").InnerText.Trim() );
            this._weight = Int32.Parse( _xmlFile.SelectSingleNode(@"User/Weight").InnerText.Trim() );
            this._exerciseLevel= (ExerciseLevel) Int32.Parse( _xmlFile.SelectSingleNode(@"User/ExerciseLevel").InnerText.Trim() );
            this._exerciseGoal = (ExerciseGoal) Int32.Parse( _xmlFile.SelectSingleNode(@"User/ExerciseGoal").InnerText.Trim() );

            if ( _xmlFile.SelectSingleNode(@"User/Plan").InnerText.Trim() == "True" )
            {
                this._planCreated = true;
            }
            else
            {
                this._planCreated = false;
            }
        }

        #endregion

    }
}
