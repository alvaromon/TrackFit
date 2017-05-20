using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        // public UserProfile _user = 

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
            if ()
        }

        #endregion

    }
}
