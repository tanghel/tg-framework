using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TgFramework
{
    public static class FrameworkExtensions
    {
        public static void RemoveFromParent(this FrameworkElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            var panel = element.Parent as Panel;
            if (panel != null)
            {
                panel.Children.Remove(element);
            }
        }
    }
}
