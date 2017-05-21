using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TrackFit_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Variables

        private UserProfile user;
        private ExercisePlan exercisePlan;

        #endregion

        #region Properties

        public UserProfile User { get; set; }
        public ExercisePlan Plan { get; set; }

        #endregion




    }
}
