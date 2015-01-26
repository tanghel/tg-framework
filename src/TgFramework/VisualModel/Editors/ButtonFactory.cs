using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace TgFramework.VisualModel.Editors
{
    public class ButtonFactory : IEditorFactory<ButtonSettings>
    {
        #region IEditorFactory Interface Implementation

        public System.Windows.DependencyProperty EditProperty
        {
            get { return Button.CommandProperty; }
        }

        public System.Windows.UIElement CreateElement(EditSettingsBase settings)
        {
            var button = new Button();

            button.SetBinding(Button.ContentProperty, new Binding("Content") { Source = settings.EditField });

            return button;
        }

        #endregion
    }
}
