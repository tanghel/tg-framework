using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TgFramework.VisualModel.Editors
{
    public class StackPanelLayoutManager : LayoutManager
    {
        public StackPanel StackPanel { get; set; }

        public override System.Windows.FrameworkElement CreateLayout()
        {
            return StackPanel = new StackPanel();
        }

        public override void RefreshLayout()
        {
            if (StackPanel == null)
            {
                throw new InvalidOperationException("The content has not yet been created. Please call CreateLayout first.");
            }

            if (PropertyContainer == null)
            {
                throw new InvalidOperationException("No PropertyContainer has been assigned.");
            }

            if (PropertyContainer.Fields == null)
            {
                throw new InvalidOperationException("The PropertyContainer does not have the Fields property initialized.");
            }

            StackPanel.Children.Clear();
            foreach (var field in this.PropertyContainer.Fields)
            {
                if (field != null)
                {
                    StackPanel.Children.Add(field.Editor);
                }
            }
        }
    }
}
