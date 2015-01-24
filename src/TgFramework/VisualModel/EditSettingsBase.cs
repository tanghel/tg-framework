using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TgFramework.VisualModel
{
    public class EditSettingsBase
    {
        private IEditorFactory _factory;

        private IEditorFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = EditorFactory.Instance.GetEditor(this);
                }

                return _factory;
            }
        }

        public EditField EditField { get; set; }

        public EditSettingsBase()
        {

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
    }
}