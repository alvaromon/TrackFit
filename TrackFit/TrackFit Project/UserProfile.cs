using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TrackFit_Project
{
    public class UserProfile
    {
        #region Member Variables

        private String lastName;
        private String firstName;
        private int age;
        private DateTime birthday;
        private double height;
        private double weight;
        private XmlDocument xmlFile;
        private String path;

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
            this.path = path;
            this.xmlFile = new XmlDocument();
            this.xmlFile.Load(path);
            readProfileInfo();
        }

        #endregion

        #region Properties

        public String FirstName{get; set;}
        public String LastName { get; set; }
        public int Age { get; set; }


        #endregion


        #region Methods

        /// <summary>
        /// Fills in UserProfile Class information.
        /// </summary>
        private void readProfileInfo()
        {
            this.firstName = xmlFile.SelectSingleNode(@"User/FirstName").InnerText.Trim();
            this.lastName = xmlFile.SelectSingleNode(@"User/LastName").InnerText.Trim();
            this.age = Int32.Parse( xmlFile.SelectSingleNode(@"User/Age").InnerText.Trim() );
            this.height = Int32.Parse( xmlFile.SelectSingleNode(@"User/HeightInches").InnerText.Trim() );
            this.weight = Int32.Parse( xmlFile.SelectSingleNode(@"User/Weight").InnerText.Trim() );
        }

        #endregion

    }
}
