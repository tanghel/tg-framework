using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TgFramework.Controls;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel.Layout
{
    public class GroupBoxLayoutFactory : ILayoutFactory<GroupBoxLayoutSettings>
    {
        #region Methods

        public void CreateField(FieldBase field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }

            //if (field.EditSettings == null)
            //{
            //    throw new ArgumentException("field.EditSetttings cannot be null.");
            //}

            var contentBinding = new Binding("Title")
            {
                Source = field
            };

            var label = new Label();
            label.SetBinding(ContentControl.ContentProperty, contentBinding);

            var editElement = field.Editor;
            var frameworkElement = editElement as FrameworkElement;
            if (frameworkElement != null)
            {
                frameworkElement.Margin = new Thickness(2);
                frameworkElement.RemoveFromParent();
            }

            WrapGrid.Children.Add(label);
            WrapGrid.Children.Add(editElement);
        }

        #endregion

        #region Properties

        public GroupBox GroupBox { get; set; }

        public WrapGrid WrapGrid { get; set; }

        #endregion

        #region ILayoutFactory Interface Implementation

        public UIElement CreateLayout(LayoutSettingsBase settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            if (settings.PropertyContainer == null)
            {
                throw new ArgumentException("settings.PropertyContainer cannot be null");
            }

            var groupBoxSettings = settings as GroupBoxLayoutSettings;
            if (groupBoxSettings == null)
            {
                throw new ArgumentException("settings is not of type GroupBoxLayoutSettings");
            }

            GroupBox = new GroupBox();

            var headerBinding = new Binding("Header")
            {
                Source = settings.PropertyContainer
            };

            GroupBox.SetBinding(HeaderedContentControl.HeaderProperty, headerBinding);

            WrapGrid = new WrapGrid()
            {
                Rows = "Auto",
                Columns = "Auto, *"
            };

            GroupBox.Content = WrapGrid;

            return GroupBox;
        }

        public void RefreshLayout(FieldBase[] fields)
        {
            if (fields == null)
            {
                throw new ArgumentNullException("fields");
            }

            if (WrapGrid == null)
            {
                throw new InvalidOperationException(
                    "The content has not yet been created. Please call CreateLayout first.");
            }

            WrapGrid.Children.Clear();
            foreach (var field in fields)
            {
                if (field != null)
                {
                    CreateField(field);
                }
            }
        }

        #endregion
    }
}