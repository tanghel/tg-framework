using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TgFramework.Core;
using TgFramework.Controls;

namespace TgFramework.VisualModel.Editors
{
    public class GroupBoxLayoutManager : LayoutManager
    {
        #region Properties

        public GroupBox GroupBox { get; private set; }

        public WrapGrid WrapGrid { get; private set; }

        public string Header { get; set; }

        #endregion

        #region LayoutManagerBase Abstract Class Implementation

        public override System.Windows.FrameworkElement CreateLayout()
        {
            this.GroupBox = new GroupBox()
            {
                Header = this.Header
            };

            this.WrapGrid = new WrapGrid()
            {
                Rows = "Auto",
                Columns = "Auto, *"
            };

            this.GroupBox.Content = this.WrapGrid;

            return this.GroupBox;
        }

        public override void RefreshLayout()
        {
            if (WrapGrid == null)
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

            WrapGrid.Children.Clear();
            foreach (var field in this.PropertyContainer.Fields)
            {
                if (field != null)
                {
                    CreateField(field);
                }
            }
        }

        public void CreateField(EditField field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }

            if (field.EditSettings == null)
            {
                throw new ArgumentException("field.EditSetttings cannot be null.");
            }

            var label = new Label() { Content = field.Title ?? field.FieldName.ToStringNN() };
            var editElement = field.Editor;

            WrapGrid.Children.Add(label);
            WrapGrid.Children.Add(editElement);
        }

        #endregion
    }
}
