using System;
using System.Windows;
using System.Windows.Data;

namespace TgFramework.VisualModel.API
{
    public abstract class FieldBase : DependencyObject
    {
        #region Dependency Properties

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(FieldBase), new PropertyMetadata(null));

        #endregion

        #region Private Members

        private IEditorFactory _factory;
        private Binding _binding;
        private string _fieldName;
        private UIElement _editor;

        #endregion

        #region Properties

        private IEditorFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = EditorFactory.Instance.GetEditorFactory(this);
                }

                return _factory;
            }
        }

        public DependencyProperty EditProperty
        {
            get { return Factory.EditProperty; }
        }

        public Binding Binding
        {
            get { return _binding; }
            set
            {
                _binding = value;
                if (_binding != null)
                {
                    RefreshBinding();
                }
            }
        }

        public string FieldName
        {
            get { return _fieldName; }
            set
            {
                _fieldName = value;
                if (!string.IsNullOrEmpty(value))
                {
                    Binding = new Binding(value);
                }
            }
        }

        public UIElement Editor
        {
            get { return _editor ?? (_editor = CreateElement()); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        #endregion

        #region Methods

        public void RefreshBinding()
        {
            if (Editor != null && Binding != null)
            {
                AttachBinding(Editor, Binding);
            }
        }

        public void AttachBinding(UIElement element, Binding binding)
        {
            if (element == null) throw new ArgumentNullException("element");
            if (binding == null) throw new ArgumentNullException("binding");

            BindingOperations.SetBinding(element, Factory.EditProperty, binding);
        }

        public UIElement CreateElement()
        {
            return Factory.CreateElement(this);
        }

        #endregion
    }
}