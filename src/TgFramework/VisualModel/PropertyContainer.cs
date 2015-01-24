using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Traian.Framework.Wpf.Controls;

namespace TgFramework.VisualModel
{
    public class PropertyContainer : ContentControl
    {
        #region Private Members

        private LayoutManager _layoutManager;

        private ObservableCollection<EditField> _fields;

        #endregion

        #region Properties

        public LayoutManager LayoutManager
        {
            get
            {
                if (_layoutManager == null)
                {
                    LayoutManager = EditorFactory.Instance.CreateDefaultLayoutManager();
                }

                return _layoutManager;
            }
            set
            {
                _layoutManager = value;
                if (_layoutManager != null)
                {
                    _layoutManager.PropertyContainer = this;
                }
                CreateLayout();
                RefreshLayout();
            }
        }


        public ObservableCollection<EditField> Fields
        {
            get
            {
                return _fields;
            }
            set
            {
                if (this._fields != null)
                {
                    this._fields.CollectionChanged -= Fields_CollectionChanged;
                }

                _fields = value;

                if (this._fields != null)
                {
                    this._fields.CollectionChanged += Fields_CollectionChanged;
                }
            }
        }

        #endregion

        #region Private Methods

        private void CreateLayout()
        {
            if (LayoutManager != null)
            {
                this.Content = LayoutManager.CreateLayout();
            }
        }

        private void RefreshLayout()
        {
            if (LayoutManager != null)
            {
                LayoutManager.RefreshLayout();
            }
        }

        #endregion

        #region Constructors

        public PropertyContainer()
        {
            this.Fields = new ObservableCollection<EditField>();

            this.Loaded += PropertyContainer_Loaded;
        }

        #endregion

        #region Event Handlers

        private void PropertyContainer_Loaded(object sender, RoutedEventArgs e)
        {
            this.RefreshLayout();
        }

        private void Fields_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                this.RefreshLayout();
            }
        }

        #endregion
    }
}
