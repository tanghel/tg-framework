using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TgFramework.VisualModel
{
    public abstract class LayoutManager
    {
        public PropertyContainer PropertyContainer { get; internal set; }

        public abstract FrameworkElement CreateLayout();

        public abstract void RefreshLayout();
    }
}
