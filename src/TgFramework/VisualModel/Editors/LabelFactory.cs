using System.Windows;
using System.Windows.Controls;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Editors
{
    public class LabelFactory : IEditorFactory<FieldBase>
    {
        public DependencyProperty EditProperty
        {
            get { return Label.ContentProperty; }
        }

        public UIElement CreateElement(FieldBase field)
        {
            return new Label();
        }
    }
}
