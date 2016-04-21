using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
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

        public static DependencyPropertyDescriptor GetPropertyDescriptor(this DependencyObject source, DependencyProperty property)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (property == null) throw new ArgumentNullException("property");

            return DependencyPropertyDescriptor.FromProperty(property, source.GetType());
        }

        public static void AddValueChanged(this DependencyObject source, DependencyProperty property, EventHandler handler)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (property == null) throw new ArgumentNullException("property");
            if (handler == null) throw new ArgumentNullException("handler");

            source.GetPropertyDescriptor(property).AddValueChanged(source, handler);
        }

        public static void RemoveValueChanged(this DependencyObject source, DependencyProperty property, EventHandler handler)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (property == null) throw new ArgumentNullException("property");
            if (handler == null) throw new ArgumentNullException("handler");

            source.GetPropertyDescriptor(property).RemoveValueChanged(source, handler);
        }
    }
}