using System.Windows;
using System.Windows.Controls;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Editors
{
    public class LabelFactory : IEditorFactory<FieldBase>
    {
        public DependencyProperty EditProperty
        {
            get { return ContentControl.ContentProperty; }
        }

        public UIElement CreateElement(FieldBase field)
        {
            return new Label();
        }
    }
}