using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace TrackFit_Project
{
    public class ExercisePlan
    {

        #region Member Variables

        /// <summary>
        /// This is the double array that contians all exercises for the week. 7 columns, one for each day of the week.
        /// </summary>
        public XmlNode[,] _exercises = new XmlNode[7,6];
        public const String _path = @"C:\Users\alvaro\Documents\GitHub\TrackFit\TrackFit\TrackFit Project\bin\Debug\Exercises.xml";
        public UserProfile _user = ApplicationServices.User;
        private static Random rando = new Random();

        #endregion

        #region ctor

        /// <summary>
        /// Standard constructor
        /// </summary>
        public ExercisePlan(  )
        {
            buildPlan();
        }

        #endregion

        #region Properties

        public XmlNode[,] Exercises
        {
            get
            {
                return _exercises;
            }

            set
            {
                _exercises = value;
            }
        }

        #endregion

        #region Methods

        public void buildPlan()
        {
            switch ( ApplicationServices.User.ExerciseGoal )
            {
                case ExerciseGoal.Strength:
                    buildStrengthPlan();
                    _user.PlanCreated = true;
                    break;

                case ExerciseGoal.Tone:
                    buildTonePlan();
                    _user.PlanCreated = true;
                    break;

                case ExerciseGoal.Weight_Loss:
                    buildWeightLossPlan();
                    _user.PlanCreated = true;
                    break;

                default:
                    MessageBox.Show( "Error: User profile Exercise Goal is not set. Please go to the profile settings and select an Exercise Goal" );
                    break;
            }
        }

        /// <summary>
        /// This function will build an exercise plan for a User that wants to be tone.
        /// </summary>
        private void buildTonePlan()
        {
            for (int i = 0; i < 7; i++)
            {
                // Light day, usually wednesday
                if (i == 2)
                {
                    _exercises[i,0] = Pick(ApplicationServices.User.getCardioExercises());
                    _exercises[i,1] = Pick(ApplicationServices.User.getCardioExercises());
                }
                else if (i == 5 || i == 6)
                {
                    _exercises[i,0] = Pick(ApplicationServices.User.getRest());
                }

                _exercises[i,0] = Pick(ApplicationServices.User.getUpperbodyExercises());
                _exercises[i,1] = Pick(ApplicationServices.User.getUpperbodyExercises());
                _exercises[i,2] = Pick(ApplicationServices.User.getLegExercises());
                _exercises[i,3] = Pick(ApplicationServices.User.getLegExercises());
                _exercises[i,4] = Pick(ApplicationServices.User.getCardioExercises());
                _exercises[i,5] = Pick(ApplicationServices.User.getCoreExercises());
            }
        }

        /// <summary>
        /// This function will build an exercise plan for a User that wants to lose weight.
        /// </summary>
        private void buildWeightLossPlan()
        {
            for (int i = 0; i < 7; i++)
            {
                // Upperbody and cardio day
                if (i == 0 || i == 3)
                {
                    _exercises[i, 0] = Pick(ApplicationServices.User.getUpperbodyExercises());
                    _exercises[i, 1] = Pick(ApplicationServices.User.getUpperbodyExercises());
                    _exercises[i, 2] = Pick(ApplicationServices.User.getCardioExercises());
                    _exercises[i, 3] = Pick(ApplicationServices.User.getCardioExercises());
                    _exercises[i, 4] = Pick(ApplicationServices.User.getCardioExercises());
                    _exercises[i, 5] = Pick(ApplicationServices.User.getCoreExercises());
                }
                // Light day, usually wednesday
                else if (i == 2)
                {
                    _exercises[i, 0] = Pick(ApplicationServices.User.getCardioExercises());
                    _exercises[i, 1] = Pick(ApplicationServices.User.getCardioExercises());
                }
                // Legs and Cardio day
                else if (i == 1 || i == 4)
                {
                    _exercises[i, 0] = Pick(ApplicationServices.User.getLegExercises());
                    _exercises[i, 1] = Pick(ApplicationServices.User.getLegExercises());
                    _exercises[i, 2] = Pick(ApplicationServices.User.getCardioExercises());
                    _exercises[i, 3] = Pick(ApplicationServices.User.getCardioExercises());
                    _exercises[i, 4] = Pick(ApplicationServices.User.getCardioExercises());
                    _exercises[i, 5] = Pick(ApplicationServices.User.getCoreExercises());
                }
                else if (i == 5 || i == 6)
                {
                    _exercises[i, 0] = Pick(ApplicationServices.User.getRest());
                }
            }
        }

        /// <summary>
        /// This function will build an exercise plan for a User that wants to gain strength
        /// </summary>
        private void buildStrengthPlan()
        {
            for (int i = 0; i < 7; i++)
            {
                // Upperbody and cardio day
                if (i == 0 || i == 3)
                {
                    _exercises[i, 0] = Pick(ApplicationServices.User.getUpperbodyExercises());
                    _exercises[i, 1] = Pick(ApplicationServices.User.getUpperbodyExercises());
                    _exercises[i, 2] = Pick(ApplicationServices.User.getUpperbodyExercises());
                    _exercises[i, 3] = Pick(ApplicationServices.User.getCoreExercises());
                    _exercises[i, 4] = Pick(ApplicationServices.User.getCoreExercises());
                    _exercises[i, 5] = Pick(ApplicationServices.User.getCardioExercises());
                }
                // Light day, usually wednesday
                else if (i == 2)
                {
                    _exercises[i, 0] = Pick(ApplicationServices.User.getCoreExercises());
                    _exercises[i, 1] = Pick(ApplicationServices.User.getCardioExercises());
                }
                // Legs and Cardio day
                else if (i == 1 || i == 4)
                {
                    _exercises[i, 0] = Pick(ApplicationServices.User.getLegExercises());
                    _exercises[i, 1] = Pick(ApplicationServices.User.getLegExercises());
                    _exercises[i, 2] = Pick(ApplicationServices.User.getLegExercises());
                    _exercises[i, 3] = Pick(ApplicationServices.User.getCoreExercises());
                    _exercises[i, 4] = Pick(ApplicationServices.User.getCoreExercises());
                    _exercises[i, 5] = Pick(ApplicationServices.User.getCardioExercises());
                }
                else if (i == 5 || i == 6)
                {
                    _exercises[i, 0] = Pick(ApplicationServices.User.getRest());
                }
            }
        }

        private XmlNode Pick(List<XmlNode> list)
        { 
            int r = rando.Next(list.Count);

            return list.ElementAt(r);
        }

        public String planToString(int day)
        {
            String planStr = "\u2022 ";

            for (int i = 0; i < 6; i++)
            {
                if (_exercises[day,i] != null)
                {
                    planStr = planStr + _exercises[day, i].Attributes["name"].Value;

                    if (i < 5 && _exercises[day,i+1] != null)
                    {
                        planStr = planStr + "\n\u2022 ";
                    }
                }
            }
            return planStr;
        }

        #endregion
    }
}
