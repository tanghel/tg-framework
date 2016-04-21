using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Editors
{
    public class PickerFactory : IEditorFactory<PickerField>
    {
        #region IEditorFactory Interface Implementation

        public DependencyProperty EditProperty
        {
            get { return Selector.SelectedValueProperty; }
        }

        public UIElement CreateElement(FieldBase field)
        {
            var pickerField = field as PickerField;
            if (pickerField == null)
            {
                throw new ArgumentException("field is not of type PickerField");
            }

            var comboBox = new ComboBox()
            {
                DisplayMemberPath = "Title",
                SelectedValuePath = "Id",
                ItemsSource = pickerField.Items
            };

            comboBox.AddValueChanged(ComboBox.SelectedValueProperty, ComboBox_ValueChanged);

            return comboBox;
        }

        #endregion

        #region Event Handlers

        private void ComboBox_ValueChanged(object sender, EventArgs args)
        {
            var dependencyObject = sender as DependencyObject;
            if (dependencyObject != null)
            {
                var editValue = dependencyObject.GetValue(EditProperty);
                if (editValue != null && editValue is Enum)
                {
                    foreach (Enum enumValue in Enum.GetValues(editValue.GetType()))
                    {
                        var id = Convert.ToInt32(enumValue);
                        var title = enumValue.ToString();
                    }
                }
            }
        }

        #endregion
    }
}