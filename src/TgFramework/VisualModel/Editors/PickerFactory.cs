using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace TgFramework.VisualModel.Editors
{
    public class PickerFactory : IEditorFactory<PickerField>
    {
        #region IEditorFactory Interface Implementation

        public System.Windows.DependencyProperty EditProperty
        {
            get { return ComboBox.SelectedValueProperty; }
        }

        public System.Windows.UIElement CreateElement(EditFieldBase field)
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
