using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel
{
    public class PropertyContainer : ContentControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof (string), typeof (PropertyContainer),
                new PropertyMetadata(null));

        #endregion

        #region Constructors

        public PropertyContainer()
        {
            Fields = new ObservableCollection<FieldBase>();

            Loaded += PropertyContainer_Loaded;
        }

        #endregion

        #region Private Members

        private ObservableCollection<FieldBase> _fields;

        private LayoutSettingsBase _layoutSettings;

        #endregion

        #region Properties

        public LayoutSettingsBase LayoutSettings
        {
            get
            {
                if (_layoutSettings == null)
                {
                    _layoutSettings = EditorFactory.Instance.CreateDefaultLayoutSettings();
                    _layoutSettings.PropertyContainer = this;

                    CreateLayout();
                }

                return _layoutSettings;
            }
            set
            {
                _layoutSettings = value;
                _layoutSettings.PropertyContainer = this;

                CreateLayout();

                if (IsLoaded)
                {
                    RefreshLayout();
                }
            }
        }

        public ObservableCollection<FieldBase> Fields
        {
            get { return _fields; }
            set
            {
                if (_fields != null)
                {
                    _fields.CollectionChanged -= Fields_CollectionChanged;
                }

                _fields = value;

                if (_fields != null)
                {
                    _fields.CollectionChanged += Fields_CollectionChanged;
                }
            }
        }

        public string Header
        {
            get { return (string) GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        #endregion

        #region Private Methods

        private void CreateLayout()
        {
            if (LayoutSettings != null)
            {
                Content = LayoutSettings.CreateLayout();
            }
        }

        private void RefreshLayout()
        {
            if (LayoutSettings != null)
            {
                LayoutSettings.RefreshLayout(Fields.ToArray());
            }
        }

        #endregion

        #region Event Handlers

        private void PropertyContainer_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshLayout();
        }

        private void Fields_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                RefreshLayout();
            }
        }

        #endregion
    }
}