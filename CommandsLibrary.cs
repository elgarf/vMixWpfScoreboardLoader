using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace vMixWpfScoreboardLoader
{
    public static class CommandsLibrary
    {
        private static RoutedUICommand increaseScore = new RoutedUICommand("IncreaseScore", "IncreaseScore", typeof(FrameworkElement));

        private static RoutedUICommand startStopwatch = new RoutedUICommand("StartStopwatch", "StartStopwatch", typeof(FrameworkElement));
        private static RoutedUICommand stopStopwatch = new RoutedUICommand("StopStopwatch", "StopStopwatch", typeof(FrameworkElement));
        private static RoutedUICommand resetStopwatch = new RoutedUICommand("ResetStopwatch", "ResetStopwatch", typeof(FrameworkElement));

        public static RoutedUICommand IncreaseScore { get { return increaseScore; } }

        public static RoutedUICommand StartStopwatch { get { return startStopwatch; } }
        public static RoutedUICommand StopStopwatch { get { return stopStopwatch; } }
        public static RoutedUICommand ResetStopwatch { get { return resetStopwatch; } }
    }
}
