using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace TgFramework.VisualModel.Editors
{
    public class PickerFactory : IEditorFactory<PickerEditSettings>
    {
        #region IEditorFactory Interface Implementation

        public System.Windows.DependencyProperty EditProperty
        {
            get { return ComboBox.SelectedValueProperty; }
        }

        public System.Windows.UIElement CreateElement(EditSettingsBase settings)
        {
            var pickerSettings = settings as PickerEditSettings;
            if (pickerSettings == null)
            {
                throw new ArgumentException("settings is not of type PickerEditSettings");
            }

            return new ComboBox()
            {
                DisplayMemberPath = "Title",
                SelectedValuePath = "Id",
                ItemsSource = pickerSettings.Items,
            };
        }

        #endregion
    }
}
