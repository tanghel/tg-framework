using System;
using System.Windows;
using System.Windows.Controls;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Layout
{
    public class StackPanelLayoutFactory : ILayoutFactory<StackPanelLayoutSettings>
    {
        public StackPanel StackPanel { get; set; }

        public UIElement CreateLayout(LayoutSettingsBase settings)
        {
            return StackPanel = new StackPanel();
        }

        public void RefreshLayout(FieldBase[] fields)
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
