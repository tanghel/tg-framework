using System;
using System.Windows;
using System.Windows.Data;

namespace TgFramework.VisualModel.API
{
    public abstract class FieldBase : DependencyObject
    {
        #region Private Members

        private IEditorFactory _factory;
        private Binding _binding;
        private string _fieldName;
        private UIElement _editor;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(FieldBase), new PropertyMetadata(null));

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

        public Binding Binding
        {
            get
            {
                return _binding;
            }
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
            get
            {
                return _fieldName;
            }
            set
            {
                _fieldName = value;
                if (!string.IsNullOrEmpty(value))
                {
                    this.Binding = new Binding(value);
                }
            }
        }

        public UIElement Editor
        {
            get
            {
                if (_editor == null)
                {
                    _editor = this.CreateElement();
                }

                return _editor;
            }
            private set
            {
                _editor = value;
                RefreshBinding();
            }
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
                this.AttachBinding(Editor, Binding);
            }
        }

        public void RefreshEditor()
        {
            this.Editor = this.CreateElement();
        }

        public void AttachBinding(UIElement element, Binding binding)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            if (binding == null)
            {
                throw new ArgumentNullException("binding");
            }

            BindingOperations.SetBinding(element, Factory.EditProperty, binding);
        }

        public UIElement CreateElement()
        {
            return Factory.CreateElement(this);
        }

        #endregion
    }
}
