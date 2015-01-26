using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TgFramework.Core;
using TgFramework.Data;

namespace TgFramework.VisualModel
{
    public abstract class EditFieldBase : DependencyObject
    {
        #region Private Members

        private IEditorFactory _factory;

        private Binding _Binding;

        private string _FieldName;

        private UIElement _Editor;

        #endregion

        #region Events

        public event ValidatingEventHandler Validating;

        public event ValueChangedEventHandler ValueChanged;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(EditFieldBase), new PropertyMetadata(null));

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
                return _Binding;
            }
            set
            {
                _Binding = value;
                if (_Binding != null)
                {
                    _Binding.ValidationRules.Add(new CancellableValidationRule());
                    RefreshBinding();
                }
            }
        }

        public string FieldName
        {
            get
            {
                return _FieldName;
            }
            set
            {
                _FieldName = value;
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
                if (_Editor == null)
                {
                    _Editor = this.CreateElement();
                }

                return _Editor;
            }
            private set
            {
                _Editor = value;
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

        private bool? ValidatingHandler(UIElement element, object value)
        {
            if (this.Validating != null)
            {
                var args = new ValidatingEventArgs(value)
                {
                    Result = true,
                };

                this.Validating(this, args);

                return args.Result;
            }

            return true;
        }

        private void ValueChangedHandler(UIElement element, object value)
        {
            if (this.ValueChanged != null)
            {
                this.ValueChanged(element, new ValueChangedEventArgs(value));
            }
        }

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

            if (this.Editor != null)
            {
                this.Editor.SetValidatingAction(ValidatingHandler);
                this.Editor.SetValueChangedAction(ValueChangedHandler);
            }
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
