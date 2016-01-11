using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace vMixWpfScoreboardLoader
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class ScoreboardLoader : vMixInterop.vMixWPFUserControl
    {
        SettingsWindow _settings = new SettingsWindow();
        Dictionary<string, int> _teamSet = new Dictionary<string, int>();
        string _titlePath;
        public ScoreboardLoader()
        {
            InitializeComponent();

            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "XAML Title|*.xaml";
            var result = ofd.ShowDialog();
            if (result.HasValue && result.Value)
            {
                using (var fs = File.OpenRead(ofd.FileName))
                {
                    _titlePath = Path.GetDirectoryName(ofd.FileName);
                    XmlDocument document = new XmlDocument();
                    document.Load(fs);
                    for (int i = document.DocumentElement.Attributes.Count - 1; i >= 0; i += -1)
                    {
                        XmlAttribute attribute = document.DocumentElement.Attributes[i];
                        if (attribute.Name == "x:Class")
                        {
                            document.DocumentElement.Attributes.RemoveAt(i);
                        }
                    }

                    using (var ms = new MemoryStream())
                    {
                        document.Save(ms);
                        ms.Position = 0;

                        var control = (FrameworkElement)XamlReader.Load(ms);

                        foreach (var item in control.Resources.Keys)
                            this.Resources.Add(item, control.FindResource(item));
                        control.Resources.Clear();

                        foreach (var item in control.Triggers)
                            this.Triggers.Add(item);
                        control.Triggers.Clear();

                        container.Children.Add(control);

                        if (control is UserControl)
                        {
                            var _c = control as UserControl;
                            ProcessPanel(_c.Content as Panel);
                        }
                    }
                }
            }
        }

        private void ProcessPanel(Panel p)
        {
            var _tag = (string)p.Tag;
            var props = ParseTag(_tag);
            if (props.Count > 0)
            {
                if (props.ContainsKey("Type") && props["Type"] == "Graphics" && props.ContainsKey("Image"))
                    p.Background = new ImageBrush(new BitmapImage(new Uri(Path.Combine(_titlePath, props["Image"]))));

            }
            foreach (FrameworkElement item in p.Children)
            {
                ProcessFrameworkElement(item);
            }
        }

        private void ProcessFrameworkElement(FrameworkElement item)
        {
            if (item is Decorator)
                ProcessFrameworkElement((item as Decorator).Child as FrameworkElement);
            if (item is vMixTitleLibrary.TextBlockDesign || item is TextBlock)
                ProcessTextBlock(item);
            if (!string.IsNullOrEmpty(item.Name))
                RegisterName(item.Name, item);
            if (item is Panel)
                ProcessPanel(item as Panel);
        }

        private void ProcessTextBlock(FrameworkElement item)
        {
            var _tag = (string)item.Tag;
            var props = ParseTag(_tag);
            if (props.Count > 0)
            {
                if (props.ContainsKey("Type"))
                {
                    switch (props["Type"])
                    {
                        case "Team":
                        case "Score":
                            var key = props["Number"];
                            if (!_teamSet.ContainsKey(key))
                            {
                                var _data = new Data.TeamData();

                                if (props.ContainsKey("Type") && props["Type"] == "Team" && props.ContainsKey("Description"))
                                    _data.Description = props["Description"];

                                BindingOperations.SetBinding(item, item is TextBlock ? TextBlock.TextProperty : vMixTitleLibrary.TextBlockDesign.TextProperty, new Binding(props["Type"] == "Team" ? "Name" : "Score") { Source = _data, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
                                _settings.Teams.Add(_data);
                                _teamSet.Add(key, _settings.Teams.Count - 1);
                            }
                            else
                                BindingOperations.SetBinding(item, item is TextBlock ? TextBlock.TextProperty : vMixTitleLibrary.TextBlockDesign.TextProperty, new Binding(props["Type"] == "Team" ? "Name" : "Score") { Source = _settings.Teams[_teamSet[key]], UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });

                            if (props.ContainsKey("Type") && props["Type"] == "Team" && props.ContainsKey("Description"))
                                _settings.Teams[_teamSet[key]].Description = props["Description"];
                            if (props.ContainsKey("Type") && props["Type"] == "Team" && props.ContainsKey("Default"))
                                _settings.Teams[_teamSet[key]].Name = props["Default"];
                            break;
                        case "Stopwatch":
                            var _sdata = new Data.StopwatchData();
                            BindingOperations.SetBinding(item, item is TextBlock ? TextBlock.TextProperty : vMixTitleLibrary.TextBlockDesign.TextProperty, new Binding("Time") { Source = _sdata, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
                            _settings.Teams.Add(_sdata);
                            break;
                    }
                }

            }
        }

        Dictionary<string, string> ParseTag(string tag)
        {
            var result = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(tag))
            {
                var tags = tag.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                foreach (var item in tags)
                {
                    var prop = item.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                    result.Add(prop[0], prop[1]);
                }
            }

            return result;
        }

        public void Close()
        {
            //throw new NotImplementedException();
        }

        public TimeSpan GetDuration()
        {
            return TimeSpan.FromSeconds(1);
            //throw new NotImplementedException();
        }

        public TimeSpan GetPosition()
        {
            return TimeSpan.FromSeconds(0);
            //throw new NotImplementedException();
        }

        public void Load(int width, int height)
        {
            //throw new NotImplementedException();
        }

        public void Pause()
        {
            //throw new NotImplementedException();
        }

        public void Play()
        {
            //throw new NotImplementedException();
        }

        public void SetPosition(TimeSpan position)
        {
            //throw new NotImplementedException();
        }

        public void ShowProperties()
        {
            Dispatcher.Invoke(new Action(delegate
            {
                _settings.Show();
            }));
            //throw new NotImplementedException();
        }
    }
}
