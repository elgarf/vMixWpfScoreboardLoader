using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace vMixWpfScoreboardLoader
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public ObservableCollection<Data.IScoreboardData> Teams { get; set; }

        public SettingsWindow()
        {
            InitializeComponent();
            DataContext = this;
            Teams = new ObservableCollection<Data.IScoreboardData>();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            (e.Parameter as Data.TeamData).Score += (int)(((Button)e.OriginalSource).Tag);
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void StartStopwatchExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            (e.Parameter as Data.StopwatchData).Start();
        }

        private void StartStopwatchCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(e.Parameter as Data.StopwatchData).Started;
        }

        private void StopStopwatchExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            (e.Parameter as Data.StopwatchData).Stop();
        }

        private void StopStopwatchCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (e.Parameter as Data.StopwatchData).Started;
        }

        private void ResetStopwatchExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            (e.Parameter as Data.StopwatchData).Reset();
        }

        private void ResetStopwatchCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = !(e.Parameter as Data.StopwatchData).Started;
        }
    }
}
