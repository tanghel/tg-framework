using System;
using System.Windows;

namespace TgFramework.VisualModel.API
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

        public void RefreshLayout(FieldBase[] fields)
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
