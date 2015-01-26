using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TgFramework.VisualModel.Editors
{
    public class StackPanelLayoutFactory : ILayoutFactory<StackPanelLayoutSettings>
    {
        public StackPanel StackPanel { get; set; }

        public System.Windows.UIElement CreateLayout(LayoutSettingsBase settings)
        {
            return StackPanel = new StackPanel();
        }

        public void RefreshLayout(EditFieldBase[] fields)
        {
            if (fields == null)
            {
                throw new ArgumentNullException("fields");
            }

            if (StackPanel == null)
            {
                throw new InvalidOperationException("The content has not yet been created. Please call CreateLayout first.");
            }

            StackPanel.Children.Clear();
            foreach (var field in fields)
            {
                if (field != null)
                {
                    var frameworkElement = field.Editor as FrameworkElement;
                    if (frameworkElement != null)
                    {
                        frameworkElement.RemoveFromParent();
                    }

                    StackPanel.Children.Add(field.Editor);
                }
            }
        }
    }
}
