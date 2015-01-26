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
    public class EditField : DependencyObject
    {
        #region Private Members

        private EditSettingsBase _EditSettings;

        private Binding _Binding;

        private string _FieldName;

        private UIElement _Editor;

        #endregion

        #region Events

        public event ValidatingEventHandler Validating;

        public event ValueChangedEventHandler ValueChanged;

        public event EventHandler EditSettingsChanged;

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(EditField), new PropertyMetadata(null));

        #endregion

        #region Properties

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
                    if (_EditSettings != null)
                    {
                        _Editor = _EditSettings.CreateElement();
                    }
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

        public EditSettingsBase EditSettings
        {
            get
            {
                if (_EditSettings == null)
                {
                    EditSettings = EditorFactory.Instance.CreateDefaultEditSettings();
                }

                return _EditSettings;
            }
            set
            {
                _EditSettings = value;
                if (_EditSettings != null)
                {
                    _EditSettings.EditField = this;
                    RefreshEditor();
                }

                CoreFrameworkExtensions.Invoke(this.EditSettingsChanged, this, EventArgs.Empty);
            }
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
            if (EditSettings != null && Editor != null && Binding != null)
            {
                EditSettings.AttachBinding(Editor, Binding);
            }
        }

        public void RefreshEditor()
        {
            this.Editor = EditSettings.CreateElement();

            if (this.Editor != null)
            {
                this.Editor.SetValidatingAction(ValidatingHandler);
                this.Editor.SetValueChangedAction(ValueChangedHandler);
            }
        }

        #endregion
    }

    public class EditField<T> : EditField
        where T : EditSettingsBase, new()
    {
        #region Properties

        public new T EditSettings
        {
            get
            {
                return base.EditSettings as T;
            }
            set
            {
                base.EditSettings = value;
            }
        }

        #endregion

        #region Constructors

        public EditField()
        {
            this.EditSettings = new T();
        }

        #endregion
    }
}
