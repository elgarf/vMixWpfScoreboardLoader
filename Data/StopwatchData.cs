using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace vMixWpfScoreboardLoader.Data
{
    public class StopwatchData : INotifyPropertyChanged, Data.IScoreboardData
    {
        TimeSpan _currentTime;

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

        /// <summary>
        /// The <see cref="Started" /> property's name.
        /// </summary>
        public const string StartedPropertyName = "Started";

        private bool _started = false;

        /// <summary>
        /// Sets and gets the Started property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool Started
        {
            get
            {
                return _started;
            }

            set
            {
                if (_started == value)
                {
                    return;
                }

                _started = value;
                RaisePropertyChanged(StartedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Time" /> property's name.
        /// </summary>
        public const string TimePropertyName = "Time";

        private string _time = "";

        /// <summary>
        /// Sets and gets the Time property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Time
        {
            get
            {
                return _time;
            }

            set
            {
                if (_time == value)
                {
                    return;
                }

                _time = value;
                RaisePropertyChanged(TimePropertyName);
            }
        }


        /// <summary>
        /// The <see cref="Duration" /> property's name.
        /// </summary>
        public const string DurationPropertyName = "Duration";

        private TimeSpan _duration = TimeSpan.FromSeconds(0);

        /// <summary>
        /// Sets and gets the Duration property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                return _duration;
            }

            set
            {
                if (_duration == value && !_started)
                {
                    return;
                }

                _duration = value;
                _currentTime = _duration;
                RaisePropertyChanged(DurationPropertyName);

                UpdateTime(_duration);
            }
        }

        /// <summary>
        /// The <see cref="StartAt" /> property's name.
        /// </summary>
        public const string StartAtPropertyName = "StartAt";

        private TimeSpan _startAt = TimeSpan.FromSeconds(0);

        /// <summary>
        /// Sets and gets the StartAt property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public TimeSpan StartAt
        {
            get
            {
                return _startAt;
            }

            set
            {
                if (_startAt == value && !_started)
                {
                    return;
                }

                _startAt = value;
                RaisePropertyChanged(StartAtPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Format" /> property's name.
        /// </summary>
        public const string FormatPropertyName = "Format";

        private string _format = "HH:mm:ss";

        /// <summary>
        /// Sets and gets the Format property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Format
        {
            get
            {
                return _format;
            }

            set
            {
                if (_format == value)
                {
                    return;
                }

                _format = value;
                RaisePropertyChanged(FormatPropertyName);

                UpdateTime(_currentTime);
            }
        }

        /// <summary>
        /// The <see cref="Invert" /> property's name.
        /// </summary>
        public const string InvertPropertyName = "Invert";

        private bool _invert = false;

        /// <summary>
        /// Sets and gets the Invert property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool Invert
        {
            get
            {
                return _invert;
            }

            set
            {
                if (_invert == value)
                {
                    return;
                }

                _invert = value;
                RaisePropertyChanged(InvertPropertyName);
            }
        }

        private void UpdateTime(TimeSpan time)
        {

            var fmt = "{0:" + _format.Replace(":", "\\:") + "}";
            Time = string.Format(fmt, new DateTime(time.Ticks < 0 ? 0 : time.Ticks));
        }

        DispatcherTimer _timer;

        public StopwatchData()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += _timer_Tick;
            UpdateTime(_currentTime);
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (!_invert)
                _currentTime = _currentTime.Subtract(TimeSpan.FromSeconds(1));
            else
                _currentTime = _currentTime.Add(TimeSpan.FromSeconds(1));
            UpdateTime(_currentTime);
            if (_currentTime.TotalSeconds == 0)
            {
                _timer.Stop();
                Started = false;
            }
        }

        private void RaisePropertyChanged(string namePropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(namePropertyName));
        }

        public void Start()
        {
            Started = true;
            _timer.Start();
        }

        public void Stop()
        {
            Started = false;
            _timer.Stop();
        }

        public void Reset()
        {
            _timer.Stop();
            _currentTime = _duration;
            UpdateTime(_currentTime);

        }
    }
}
