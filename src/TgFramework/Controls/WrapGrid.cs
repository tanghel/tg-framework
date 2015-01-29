using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TgFramework.Core;

namespace TgFramework.Controls
{
    public class WrapGrid : Grid
    {
        #region Private Members

        private double _padding;

        #endregion

        #region Overrides

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var size = base.ArrangeOverride(arrangeSize);

            var rowCount = RowDefinitions.Count;
            var columnCount = ColumnDefinitions.Count;
            var index = 0;
            foreach (var child in InternalChildren.Cast<UIElement>())
            {
                var row = index/columnCount;
                var column = index%columnCount;

                SetRow(child, row);
                SetColumn(child, column);

                while (!(row < RowDefinitions.Count))
                {
                    var lastRowDefinition = RowDefinitions.Last();
                    RowDefinitions.Add(new RowDefinition() {Height = lastRowDefinition.Height});
                }

                while (!(column < ColumnDefinitions.Count))
                {
                    var lastColumnDefinition = ColumnDefinitions.Last();
                    ColumnDefinitions.Add(new ColumnDefinition() {Width = lastColumnDefinition.Width});
                }

                index++;
            }

            RefreshPadding();

            return size;
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(
            "Columns",
            typeof (string),
            typeof (WrapGrid),
            GetPropertyMetadata(null, (grid, newValue) => grid.SetColumnDefinitions(newValue.ToStringNN())));

        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
            "Rows",
            typeof (string),
            typeof (WrapGrid),
            GetPropertyMetadata(null, (grid, newValue) => grid.SetRowDefinitions(newValue.ToStringNN())));

        #endregion

        #region Properties

        public double Padding
        {
            get { return _padding; }
            set
            {
                _padding = value;
                RefreshPadding();
            }
        }

        public string Rows
        {
            get { return (string) GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public string Columns
        {
            get { return (string) GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        #endregion

        #region Private Methods

        private static PropertyMetadata GetPropertyMetadata(object defaultValue,
            Action<WrapGrid, object> propertyChangedAction)
        {
            if (propertyChangedAction == null)
            {
                throw new ArgumentNullException("propertyChangedAction");
            }

            return new PropertyMetadata(defaultValue, (sender, args) =>
            {
                var wrapGrid = sender as WrapGrid;
                if (wrapGrid != null)
                {
                    propertyChangedAction.Invoke(wrapGrid, args.NewValue);
                }
            });
        }

        private void RefreshPadding()
        {
            if (Padding > 0)
            {
                foreach (var child in InternalChildren.OfType<FrameworkElement>())
                {
                    child.Margin = new Thickness(Padding);
                }
            }
        }

        #endregion
    }
}