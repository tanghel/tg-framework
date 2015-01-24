using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using TgFramework.Core;

namespace Traian.Framework.Wpf.Controls
{
    public class WrapGrid : Grid
    {
        #region Private Members

        private GridLength _rowHeight = new GridLength(1, GridUnitType.Star);

        private GridLength _columnWidth = new GridLength(1, GridUnitType.Star);

        private int _numberOfRows;

        private int _numberOfColumns;

        private double _padding;

        private string _Columns;

        private string _Rows;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register(
            "Columns",
            typeof(string),
            typeof(WrapGrid),
            GetPropertyMetadata(null, (grid, newValue) => grid.SetColumnDefinitions(newValue.ToStringNN())));

        public static readonly DependencyProperty RowsProperty = DependencyProperty.Register(
            "Rows", 
            typeof(string), 
            typeof(WrapGrid), 
            GetPropertyMetadata(null, (grid, newValue) => grid.SetRowDefinitions(newValue.ToStringNN())));

        #endregion

        #region Properties

        public GridLength RowHeight
        {
            get
            {
                return this._rowHeight;
            }
            set
            {
                this._rowHeight = value;
                this.RefreshRows();
            }
        }

        public GridLength ColumnWidth
        {
            get
            {
                return this._columnWidth;
            }
            set
            {
                this._columnWidth = value;
                this.RefreshColumns();
            }
        }

        public int NumberOfRows
        {
            get
            {
                return this._numberOfRows;
            }
            set
            {
                this._numberOfRows = value;
                this.RefreshRows();
            }
        }

        public int NumberOfColumns
        {
            get
            {
                return this._numberOfColumns;
            }
            set
            {
                this._numberOfColumns = value;
                this.RefreshColumns();
            }
        }

        public double Padding
        {
            get
            {
                return this._padding;
            }
            set
            {
                this._padding = value;
                this.RefreshPadding();
            }
        }

        public string Rows
        {
            get { return (string)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public string Columns
        {
            get { return (string)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        #endregion

        #region Private Methods

        private static PropertyMetadata GetPropertyMetadata(object defaultValue, Action<WrapGrid, object> propertyChangedAction)
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
                foreach (var child in this.InternalChildren.OfType<FrameworkElement>())
                {
                    child.Margin = new Thickness(Padding);
                }
            }
        }

        private void RefreshColumns()
        {
            this.ColumnDefinitions.Clear();
            for (int i = 0; i < this.NumberOfColumns; i++)
            {
                var columnDefinition = new ColumnDefinition()
                {
                    Width = this.ColumnWidth
                };

                this.ColumnDefinitions.Add(columnDefinition);
            }
        }

        private void RefreshRows()
        {
            this.RowDefinitions.Clear();
            for (int i = 0; i < this.NumberOfRows; i++)
            {
                var rowDefinition = new RowDefinition()
                {
                    Height = this.RowHeight
                };

                this.RowDefinitions.Add(rowDefinition);
            }
        }

        #endregion

        #region Constructors

        public WrapGrid()
        {

        }

        #endregion

        #region Overrides

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var size = base.ArrangeOverride(arrangeSize);

            this.RowDefinitions.Count.ExecuteIf(x => x == 0, x => NumberOfRows = 1);
            this.ColumnDefinitions.Count.ExecuteIf(x => x == 0, x => NumberOfColumns = 1);

            var rowCount = this.RowDefinitions.Count;
            var columnCount = this.ColumnDefinitions.Count;
            var index = 0;
            foreach (var child in this.InternalChildren.Cast<UIElement>())
            {
                var row = index / columnCount;
                var column = index % columnCount;

                Grid.SetRow(child, row);
                Grid.SetColumn(child, column);

                while (!(row < this.RowDefinitions.Count))
                {
                    var lastRowDefinition = this.RowDefinitions.Last();
                    this.RowDefinitions.Add(new RowDefinition() { Height = lastRowDefinition.Height });
                }

                while (!(column < this.ColumnDefinitions.Count))
                {
                    var lastColumnDefinition = this.ColumnDefinitions.Last();
                    this.ColumnDefinitions.Add(new ColumnDefinition() { Width = lastColumnDefinition.Width });
                }

                index++;
            }

            RefreshPadding();

            return size;
        }

        #endregion
    }
}
