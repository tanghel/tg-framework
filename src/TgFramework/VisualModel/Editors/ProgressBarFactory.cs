using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Editors
{
    public class ProgressBarFactory : IEditorFactory<ProgressBarField>
    {
        public System.Windows.DependencyProperty EditProperty
        {
            get { return ProgressBar.ValueProperty; }
        }

        public System.Windows.UIElement CreateElement(FieldBase field)
        {
            var progressBar = new ProgressBar();

            progressBar.SetBinding(ProgressBar.MinimumProperty, new Binding("Minimum") { Source = field });
            progressBar.SetBinding(ProgressBar.MaximumProperty, new Binding("Maximum") { Source = field });

            return progressBar;
        }
    }
}
