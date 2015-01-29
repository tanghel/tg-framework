using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Editors
{
    public class ButtonFactory : IEditorFactory<ButtonField>
    {
        #region IEditorFactory Interface Implementation

        public DependencyProperty EditProperty
        {
            get { return ButtonBase.CommandProperty; }
        }

        public UIElement CreateElement(FieldBase field)
        {
            var button = new Button();

            button.SetBinding(ContentControl.ContentProperty, new Binding("Content") {Source = field});

            return button;
        }

        #endregion
    }
}