using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TrackFit_Project
{
    public class Exercise
    {
        #region Member Variable

        private String _path;
        private XmlDocument _xmlFile;
        private String _difficulty;
        private String _targetMuscles;
        private String _name;
        private String _status;

        #endregion


        #region ctor

        /// <summary>
        /// default constructor
        /// </summary>
        public Exercise()
        {

        }

        /// <summary>
        /// Standard constructor
        /// </summary>
        /// <param name="path">File path to exercise XML</param>
        public Exercise(String path)
        {

        }

        #endregion

        #region Properties

        public String Name { get; set; }
        public String Difficulty { get; set; }
        public String TargetMuscles { get; set;  }
        public String Status { get; set; }

        #endregion

    }
}
