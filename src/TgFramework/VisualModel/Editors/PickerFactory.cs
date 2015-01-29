using System;
using System.Windows;
using System.Windows.Controls;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Editors
{
    public class PickerFactory : IEditorFactory<PickerField>
    {
        #region IEditorFactory Interface Implementation

        public DependencyProperty EditProperty
        {
            get { return ComboBox.SelectedValueProperty; }
        }

        public UIElement CreateElement(FieldBase field)
        {
            var pickerField = field as PickerField;
            if (pickerField == null)
            {
                throw new ArgumentException("field is not of type PickerField");
            }

            return new ComboBox()
            {
                DisplayMemberPath = "Title",
                SelectedValuePath = "Id",
                ItemsSource = pickerField.Items,
            };
        }

        #endregion
    }
}
