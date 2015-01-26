using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgFramework.VisualModel.Editors;

namespace TgFramework.VisualModel
{
    public class EditorFactory
    {
        #region Private Members

        private readonly Container container = new Container();

        #endregion

        #region Ioc container implementation

        private EditorFactory()
        {
            container.Options.AllowOverridingRegistrations = true;
        }

        private static EditorFactory _Instance;

        public static EditorFactory Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new EditorFactory();

                    _Instance.RegisterEditor<TextField, TextBoxFactory>();
                    _Instance.RegisterEditor<PickerField, PickerFactory>();
                    _Instance.RegisterEditor<ButtonField, ButtonFactory>();

                    _Instance.RegisterDefaultLayoutSettings<GroupBoxLayoutSettings>();
                    _Instance.RegisterLayout<StackPanelLayoutSettings, StackPanelLayoutFactory>();
                    _Instance.RegisterLayout<GroupBoxLayoutSettings, GroupBoxLayoutFactory>();
                }

                return _Instance;
            }
        }

        #endregion

        #region Public Methods
        
        public void RegisterDefaultLayoutSettings<T>()
            where T : LayoutSettingsBase
        {
            container.Register<LayoutSettingsBase, T>();
        }

        public void RegisterDefaultEditField<T>()
            where T : EditFieldBase
        {
            container.Register<EditFieldBase, T>();
        }

        public LayoutSettingsBase CreateDefaultLayoutSettings()
        {
            return container.GetInstance<LayoutSettingsBase>();
        }

        public EditFieldBase CreateDefaultEditField()
        {
            return container.GetInstance<EditFieldBase>();
        }

        public void RegisterEditor<TService, TImplementation>()
            where TService : EditFieldBase
            where TImplementation : class, IEditorFactory<TService>
        {
            container.Register<IEditorFactory<TService>, TImplementation>();
        }

        public void RegisterLayout<TService, TImplementation>()
            where TService : LayoutSettingsBase
            where TImplementation : class, ILayoutFactory<TService>
        {
            container.Register<ILayoutFactory<TService>, TImplementation>();
        }

        internal IEditorFactory GetEditorFactory(EditFieldBase field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }

            var type = typeof(IEditorFactory<>).MakeGenericType(field.GetType());
            var factory = container.GetInstance(type) as IEditorFactory;

            if (factory == null)
            {
                throw new InvalidOperationException("Could not create IEditorFactory of type " + field.GetType().ToString());
            }

            return factory;
        }

        internal ILayoutFactory GetLayoutFactory(LayoutSettingsBase settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            var type = typeof(ILayoutFactory<>).MakeGenericType(settings.GetType());
            var factory = container.GetInstance(type) as ILayoutFactory;

            if (factory == null)
            {
                throw new InvalidOperationException("Could not create ILayoutFactory of type " + settings.GetType().ToString());
            }

            return factory;
        }

        #endregion
    }
}
