using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TgFramework.VisualModel
{
    public abstract class LayoutSettingsBase
    {
        #region Private Members

        private ILayoutFactory _factory;

        #endregion

        #region Properties

        private ILayoutFactory Factory
        {
            get
            {
                if (_factory == null)
                {
                    _factory = EditorFactory.Instance.GetLayoutFactory(this);
                }

                return _factory;
            }
        }

        public PropertyContainer PropertyContainer { get; internal set; }

        #endregion

        #region Methods

        public UIElement CreateLayout()
        {
            return Factory.CreateLayout(this);
        }

        public void RefreshLayout(EditFieldBase[] fields)
        {
            if (fields == null)
            {
                throw new ArgumentNullException("fields");
            }

            Factory.RefreshLayout(fields);
        }

        #endregion
    }
}
