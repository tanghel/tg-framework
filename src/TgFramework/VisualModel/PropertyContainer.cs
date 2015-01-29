using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using TgFramework.Core;
using TgFramework.Validation;
using TgFramework.VisualModel.API;

namespace TgFramework.VisualModel
{
    public class PropertyContainer : ContentControl
    {
        #region Dependency Properties

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(PropertyContainer),
                new PropertyMetadata(null));

        #endregion

        #region Constructors

        public PropertyContainer()
        {
            Fields = new ObservableCollection<FieldBase>();

            Loaded += PropertyContainer_Loaded;
            DataContextChanged += OnDataContextChanged;
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
            get { return (string)GetValue(HeaderProperty); }
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

        private void RefreshValidationRules(object dataContext)
        {
            if (dataContext != null)
            {
                foreach (var field in Fields.Where(x => x.Binding != null))
                {
                    var binding = field.Binding;
                    SetValidationRules(binding, dataContext, field);
                }
            }
        }

        private static void SetValidationRules(Binding binding, object dataContext, FieldBase field)
        {
            if (binding == null) throw new ArgumentNullException("binding");
            if (dataContext == null) throw new ArgumentNullException("dataContext");
            if (field == null) throw new ArgumentNullException("field");

            if (binding.Path != null && binding.Path.Path != null)
            {
                var propertyName = binding.Path.Path;
                var dataContextType = dataContext.GetType();
                var propertyInfo = dataContextType.GetProperty(propertyName);
                if (propertyInfo == null)
                {
                    throw new InvalidOperationException("Property {0} not found on type {1}".FormatString(propertyName, dataContextType));
                }

                var validationRules = ValidationRulesExtractor.GetValidationRules(propertyInfo, dataContext);

                binding.ValidationRules.Clear();
                foreach (var rule in validationRules)
                {
                    binding.ValidationRules.Add(rule);
                }
            }
        }
        
        #endregion

        #region Event Handlers

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            RefreshValidationRules(args.NewValue);
        }

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