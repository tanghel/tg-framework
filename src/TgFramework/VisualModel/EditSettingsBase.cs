using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using TgFramework.Core;

namespace TgFramework.VisualModel
{
    public class EditSettingsBase : DependencyObject
    {
        #region Private Members

        private IEditorFactory _factory;

        private EditField _editField;

        #endregion

        #region Events

        public event EventHandler EditFieldChanged;

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

        public EditField EditField
        {
            get
            {
                return _editField;
            }
            set
            {
                _editField = value;
                CoreFrameworkExtensions.Invoke(this.EditFieldChanged, this, EventArgs.Empty);
            }
        }

        #endregion

        #region Methods

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