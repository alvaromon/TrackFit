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
        public Exercise[][] _exercises = new Exercise[7][];
        public const String _path = @"C:\Users\alvaro\Documents\GitHub\TrackFit\TrackFit\TrackFit Project\bin\Debug\Exercises.xml";
        public UserProfile _user = ApplicationServices.User;

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

        #region Methods

        public void buildPlan()
        {
            switch ( ApplicationServices.User.ExerciseGoal )
            {
                case ExerciseGoal.Strength:
                    buildStrengthPlan();
                    break;

                case ExerciseGoal.Tone:
                    buildTonePlan();
                    break;

                case ExerciseGoal.Weight_Loss:
                    buildWeightLossPlan();
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
            getUpperbodyExercise();
            getUpperbodyExercise();
            getLegsExercise();
            getLegsExercise();
            getCardioExercise();
        }

        /// <summary>
        /// This function will build an exercise plan for a User that wants to lose weight.
        /// </summary>
        private void buildWeightLossPlan()
        {
            getUpperbodyExercise();
            getUpperbodyExercise();
            getCardioExercise();
            getCardioExercise();
            getCardioExercise();
        }

        /// <summary>
        /// 
        /// </summary>
        private void buildStrengthPlan()
        {
            getUpperbodyExercise();
            getUpperbodyExercise();
            getUpperbodyExercise();

            getLegsExercise();
            getLegsExercise();
            getLegsExercise();

            getCardioExercise();
        }

        private void getCardioExercise()
        {
            
        }

        private void getLegsExercise()
        {
            throw new NotImplementedException();
        }

        private void getUpperbodyExercise()
        {
            throw new NotImplementedException();
        }



        #endregion

    }
}
