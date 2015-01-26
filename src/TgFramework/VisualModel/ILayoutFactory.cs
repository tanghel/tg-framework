using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TgFramework.VisualModel
{
    public interface ILayoutFactory
    {
        UIElement CreateLayout(LayoutSettingsBase settings);

        void RefreshLayout(EditFieldBase[] fields);
    }

    public interface ILayoutFactory<T> : ILayoutFactory
        where T : LayoutSettingsBase
    {

    }
}
