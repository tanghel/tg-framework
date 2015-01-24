using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TgFramework.Data
{
    public delegate bool? ValidatingDelegate(UIElement element, object value);

    public delegate void ValueChangedDelegate(UIElement element, object value);

    public static class ValidationExtensions
    {
        // Using a DependencyProperty as the backing store for ItemChanging.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValidatingActionProperty =
            DependencyProperty.RegisterAttached("ValidatingAction", typeof(ValidatingDelegate), typeof(ValidationExtensions), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for ItemChanging.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueChangedActionProperty =
            DependencyProperty.RegisterAttached("ValueChangedAction", typeof(ValueChangedDelegate), typeof(ValidationExtensions), new PropertyMetadata(null));

        public static void SetValueChangedAction(this UIElement element, ValueChangedDelegate value)
        {
            element.SetValue(ValueChangedActionProperty, value);
        }

        public static ValueChangedDelegate GetValueChangedAction(this UIElement element)
        {
            return element.GetValue(ValueChangedActionProperty) as ValueChangedDelegate;
        }

        public static void SetValidatingAction(this UIElement element, ValidatingDelegate value)
        {
            element.SetValue(ValidatingActionProperty, value);
        }

        public static ValidatingDelegate GetValidatingAction(this UIElement element)
        {
            return element.GetValue(ValidatingActionProperty) as ValidatingDelegate;
        }
    }
}
