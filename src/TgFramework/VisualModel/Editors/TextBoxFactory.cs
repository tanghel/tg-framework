using System.Windows;
using System.Windows.Controls;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Editors
{
    public class TextBoxFactory : IEditorFactory<TextField>
    {
        #region IEditorFactory Interface Implementation

        public DependencyProperty EditProperty
        {
            get { return TextBox.TextProperty; }
        }

        public UIElement CreateElement(FieldBase field)
        {
            return new TextBox();
        }

        #endregion
    }
}