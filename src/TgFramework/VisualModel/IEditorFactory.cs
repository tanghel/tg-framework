using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TgFramework.VisualModel
{
    public interface IEditorFactory
    {
        DependencyProperty EditProperty { get; }

        UIElement CreateElement(EditSettingsBase settings);
    }

    public interface IEditorFactory<T> : IEditorFactory
        where T : EditSettingsBase
    {
    }
}
