using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Editors
{
    public class ButtonFactory : IEditorFactory<ButtonField>
    {
        #region IEditorFactory Interface Implementation

        public DependencyProperty EditProperty
        {
            get { return Button.CommandProperty; }
        }

        public UIElement CreateElement(FieldBase field)
        {
            var button = new Button();

            button.SetBinding(Button.ContentProperty, new Binding("Content") { Source = field });

            return button;
        }

        #endregion
    }
}
