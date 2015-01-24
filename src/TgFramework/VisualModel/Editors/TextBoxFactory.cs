using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace TgFramework.VisualModel.Editors
{
    public class TextBoxFactory : IEditorFactory<TextEditSettings>
    {
        #region IEditorFactory Interface Implementation

        public System.Windows.DependencyProperty EditProperty
        {
            get { return TextBox.TextProperty; }
        }

        public System.Windows.UIElement CreateElement(EditSettingsBase settings)
        {
            return new TextBox();
        }

        #endregion
    }
}
