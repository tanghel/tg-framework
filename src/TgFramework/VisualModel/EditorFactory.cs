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

                    _Instance.RegisterDefaultEditSettings<TextEditSettings>();
                    _Instance.RegisterEditor<TextEditSettings, TextBoxFactory>();
                    _Instance.RegisterEditor<PickerEditSettings, PickerFactory>();
                    _Instance.RegisterEditor<ButtonSettings, ButtonFactory>();

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

        public void RegisterDefaultEditSettings<T>()
            where T : EditSettingsBase
        {
            container.Register<EditSettingsBase, T>();
        }

        public LayoutSettingsBase CreateDefaultLayoutSettings()
        {
            return container.GetInstance<LayoutSettingsBase>();
        }

        public EditSettingsBase CreateDefaultEditSettings()
        {
            return container.GetInstance<EditSettingsBase>();
        }

        public void RegisterEditor<TService, TImplementation>()
            where TService : EditSettingsBase
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

        internal IEditorFactory GetEditorFactory(EditSettingsBase settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            var type = typeof(IEditorFactory<>).MakeGenericType(settings.GetType());
            var factory = container.GetInstance(type) as IEditorFactory;

            if (factory == null)
            {
                throw new InvalidOperationException("Could not create IEditorFactory of type " + settings.GetType().ToString());
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
