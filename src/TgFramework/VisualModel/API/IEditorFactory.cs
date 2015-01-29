using System.Windows;

namespace TgFramework.VisualModel.API
{
    public interface IEditorFactory
    {
        DependencyProperty EditProperty { get; }
        UIElement CreateElement(FieldBase field);
    }

    public interface IEditorFactory<T> : IEditorFactory
        where T : FieldBase
    {
    }
}