using System.Windows;

namespace TgFramework.VisualModel.API
{
    public interface ILayoutFactory
    {
        UIElement CreateLayout(LayoutSettingsBase settings);
        void RefreshLayout(FieldBase[] fields);
    }

    public interface ILayoutFactory<T> : ILayoutFactory
        where T : LayoutSettingsBase
    {
    }
}