using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace vMixWpfScoreboardLoader
{
    public class ScoreboardDataTemplateSelector: DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            return (container as FrameworkElement).FindResource(item.GetType().Name) as DataTemplate;//base.SelectTemplate(item, container);
        }
    }
}
