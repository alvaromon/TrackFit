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
        private XmlNode[,] _exercises = new XmlNode[7, 6];
        private const String _path = @"C:\Users\alvaro\Documents\GitHub\TrackFit\TrackFit\TrackFit Project\bin\Debug\Exercises.xml";
        private UserProfile _user = ApplicationServices.User;
        private static Random rando = new Random();

        #endregion

        #region ctor

        /// <summary>
        /// Standard constructor
        /// </summary>
        public ExercisePlan()
        {
            if (ApplicationServices.User.PlanCreated && !((DateTime.Now - ApplicationServices.User.StartOfWeek).Days >= 7))
            {
                LoadPlan();
            }
            else
            {
                buildPlan();
            }
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

        /// <summary>
        /// This function will build a plan according to user profile settings
        /// </summary>
        public void buildPlan()
        {
            switch (ApplicationServices.User.ExerciseGoal)
            {
                case ExerciseGoal.Strength:
                    buildStrengthPlan();
                    FindandReplaceDuplicated();
                    ApplicationServices.User.StartOfWeek = DateTime.Now;
                    ApplicationServices.User.PlanCreated = true;
                    WritePlan();
                    break;

                case ExerciseGoal.Tone:
                    buildTonePlan();
                    FindandReplaceDuplicated();
                    ApplicationServices.User.StartOfWeek = DateTime.Now;
                    ApplicationServices.User.PlanCreated = true;
                    WritePlan();
                    break;

                case ExerciseGoal.Weight_Loss:
                    buildWeightLossPlan();
                    FindandReplaceDuplicated();
                    ApplicationServices.User.StartOfWeek = DateTime.Now;
                    ApplicationServices.User.PlanCreated = true;
                    WritePlan();
                    break;

                default:
                    MessageBox.Show("Error: User profile Exercise Goal is not set. Please go to the profile settings and select an Exercise Goal");
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
                // All around work out
                if (i == 0 || i == 1 || i == 3 || i == 4)
                {
                    _exercises[i, 0] = Pick(ApplicationServices.User.getUpperbodyExercises());
                    _exercises[i, 1] = Pick(ApplicationServices.User.getUpperbodyExercises());
                    _exercises[i, 2] = Pick(ApplicationServices.User.getLegExercises());
                    _exercises[i, 3] = Pick(ApplicationServices.User.getLegExercises());
                    _exercises[i, 4] = Pick(ApplicationServices.User.getCardioExercises());
                    _exercises[i, 5] = Pick(ApplicationServices.User.getCoreExercises());
                }
                // Light day, usually wednesday
                else if (i == 2)
                {
                    _exercises[i, 0] = Pick(ApplicationServices.User.getCardioExercises());
                    _exercises[i, 1] = Pick(ApplicationServices.User.getCardioExercises());
                }
                else if (i == 5 || i == 6)
                {
                    _exercises[i, 0] = Pick(ApplicationServices.User.getRest());
                }               
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

        /// <summary>
        /// This function will take in a List of type XmlNode and return a random XmlNode from that list
        /// </summary>
        /// <param name="list"></param>
        /// <returns>Randomly chosen XmlNode.</returns>
        private XmlNode Pick(List<XmlNode> list)
        {
            int r = rando.Next(list.Count);

            return list.ElementAt(r);
        }

        /// <summary>
        /// This function will erase duplicates from the user's exercise plan
        /// </summary>
        private void FindandReplaceDuplicated()
        {

            for (int i = 0; i < 6; i++)
            {
                if (i == 0 || i == 1 || i == 3 || i == 4)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = j+1; k < 6; k++)
                        {
                            if (_exercises[i, j].Attributes["name"].Value == _exercises[i, k].Attributes["name"].Value)
                            {
                                bool replaced = false;

                                switch (_exercises[i, k].Attributes["target"].Value)
                                {

                                    case "upperbody":

                                        while (!replaced)
                                        {
                                            _exercises[i, k] = Pick(ApplicationServices.User.getUpperbodyExercises());

                                            if (_exercises[i, j].Attributes["name"].Value != _exercises[i, k].Attributes["name"].Value)
                                            {
                                                replaced = true;
                                            }
                                        }
                                        break;

                                    case "core":

                                        while (!replaced)
                                        {
                                            _exercises[i, k] = Pick(ApplicationServices.User.getCoreExercises());

                                            if (_exercises[i, j].Attributes["name"].Value != _exercises[i, k].Attributes["name"].Value)
                                            {
                                                replaced = true;
                                            }
                                        }
                                        break;

                                    case "legs":

                                        while (!replaced)
                                        {
                                            _exercises[i, k] = Pick(ApplicationServices.User.getLegExercises());

                                            if (_exercises[i, j].Attributes["name"].Value != _exercises[i, k].Attributes["name"].Value)
                                            {
                                                replaced = true;
                                            }
                                        }
                                        break;

                                    case "cardio":

                                        while (!replaced)
                                        {
                                            _exercises[i, k] = Pick(ApplicationServices.User.getCardioExercises());

                                            if (_exercises[i, j].Attributes["name"].Value != _exercises[i, k].Attributes["name"].Value)
                                            {
                                                replaced = true;
                                            }
                                        }
                                        break;

                                    case "rest":

                                        while (!replaced)
                                        {
                                            _exercises[i, k] = Pick(ApplicationServices.User.getRest());

                                            if (_exercises[i, j].Attributes["name"].Value != _exercises[i, k].Attributes["name"].Value)
                                            {
                                                replaced = true;
                                            }
                                        }
                                        break;

                                    default:
                                        MessageBox.Show("Error: Somehow while replacing duplicated exercises in the User's plan " +
                                                        "an exercise that doesnt match any exercise type was encountered. Please " +
                                                        "rebuild the User's exercise plan by changing exercise goal to something new " +
                                                        "and switching back.");
                                        break;
                                }
                            }
                        }
                    }
                }
                else if (i == 2)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        for (int k = j+1; k < 2; k++)
                        {
                            if (_exercises[i, j].Attributes["name"].Value == _exercises[i, k].Attributes["name"].Value)
                            {
                                bool replaced = false;

                                switch (_exercises[i, k].Attributes["target"].Value)
                                {

                                    case "upperbody":

                                        while (!replaced)
                                        {
                                            _exercises[i, k] = Pick(ApplicationServices.User.getUpperbodyExercises());

                                            if (_exercises[i, j].Attributes["name"].Value != _exercises[i, k].Attributes["name"].Value)
                                            {
                                                replaced = true;
                                            }
                                        }
                                        break;

                                    case "core":

                                        while (!replaced)
                                        {
                                            _exercises[i, k] = Pick(ApplicationServices.User.getCoreExercises());

                                            if (_exercises[i, j].Attributes["name"].Value != _exercises[i, k].Attributes["name"].Value)
                                            {
                                                replaced = true;
                                            }
                                        }
                                        break;

                                    case "legs":

                                        while (!replaced)
                                        {
                                            _exercises[i, k] = Pick(ApplicationServices.User.getLegExercises());

                                            if (_exercises[i, j].Attributes["name"].Value != _exercises[i, k].Attributes["name"].Value)
                                            {
                                                replaced = true;
                                            }
                                        }
                                        break;

                                    case "cardio":

                                        while (!replaced)
                                        {
                                            _exercises[i, k] = Pick(ApplicationServices.User.getCardioExercises());

                                            if (_exercises[i, j].Attributes["name"].Value != _exercises[i, k].Attributes["name"].Value)
                                            {
                                                replaced = true;
                                            }
                                        }
                                        break;

                                    case "rest":

                                        while (!replaced)
                                        {
                                            _exercises[i, k] = Pick(ApplicationServices.User.getRest());

                                            if (_exercises[i, j].Attributes["name"].Value != _exercises[i, k].Attributes["name"].Value)
                                            {
                                                replaced = true;
                                            }
                                        }
                                        break;

                                    default:
                                        MessageBox.Show("Error: Somehow while replacing duplicated exercises in the User's plan " +
                                                        "an exercise that doesnt match any exercise type was encountered. Please " +
                                                        "rebuild the User's exercise plan by changing exercise goal to something new " +
                                                        "and switching back.");
                                        break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// This function ill return a string appropriate for displaying in TrackFit. Bulleted and double-spaced
        /// </summary>
        /// <param name="day"></param>
        /// <returns>Formatted list of exercises</returns>
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
                        planStr = planStr + "\n\n\u2022 ";
                    }
                }
            }
            return planStr;
        }

        /// <summary>
        /// This function will load a plan from the user's profile XML
        /// </summary>
        public void LoadPlan()
        {
            XmlDocument doc = ApplicationServices.User.XMLFile;
            XmlNodeList list = doc.GetElementsByTagName("Workout");
            int nodeNum = 0;

            for (int i = 0; i < 7; i++)
            {
                if (i == 0 || i == 1 || i == 3 || i == 4)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        _exercises[i, j] = list.Item(nodeNum++);
                    }
                }
                else if (i == 2)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        _exercises[i, j] = list.Item(nodeNum++);
                    }
                }
                else
                {
                    _exercises[i, 0] = list.Item(nodeNum++);
                }

            }
        }

        /// <summary>
        /// This function will write the newest built plan to the user's profile XML
        /// </summary>
        public void WritePlan()
        {
            XmlDocument doc = ApplicationServices.User.XMLFile;
            XmlNode ParentNode = doc.CreateElement(string.Empty, "ExercisePlan", string.Empty);

            for (int i = 0; i < 7; i++)
            {
                if (i == 0 || i == 1 || i == 3 || i == 4)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        XmlNode importNode = doc.ImportNode(_exercises[i, j], false);
                        ParentNode.AppendChild(importNode);
                    }
                }
                else if (i == 2)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        XmlNode importNode = doc.ImportNode(_exercises[i, j], false);
                        ParentNode.AppendChild(importNode);
                    }
                }
                else
                {
                    XmlNode importNode = doc.ImportNode(_exercises[i, 0], false);
                    ParentNode.AppendChild(importNode);
                }

            }

            doc.SelectSingleNode(@"User").ReplaceChild(ParentNode, doc.SelectSingleNode(@"User/ExercisePlan"));
            doc.Save(Environment.CurrentDirectory + $@"\User Profiles\{ApplicationServices.User.Username}.xml");
           
        }

        #endregion
    }
}
