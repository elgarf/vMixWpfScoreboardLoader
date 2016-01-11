using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace vMixWpfScoreboardLoader.Data
{
    public class TeamData : INotifyPropertyChanged, Data.IScoreboardData
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The <see cref="Name" /> property's name.
        /// </summary>
        public const string NamePropertyName = "Name";

        private string _name = "";

        /// <summary>
        /// Sets and gets the Name property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (_name == value)
                {
                    return;
                }

                _name = value;
                RaisePropertyChanged(NamePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Score" /> property's name.
        /// </summary>
        public const string ScorePropertyName = "Score";

        private int _score = 0;

        /// <summary>
        /// Sets and gets the Score property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Score
        {
            get
            {
                return _score;
            }

            set
            {
                if (_score == value)
                {
                    return;
                }

                _score = value;
                RaisePropertyChanged(ScorePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Description" /> property's name.
        /// </summary>
        public const string DescriptionPropertyName = "Description";

        private string _description = "";

        /// <summary>
        /// Sets and gets the Description property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                if (_description == value)
                {
                    return;
                }

                _description = value;
                RaisePropertyChanged(DescriptionPropertyName);
            }
        }

        private void RaisePropertyChanged(string namePropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(namePropertyName));
        }
    }
}
