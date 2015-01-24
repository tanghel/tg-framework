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

                    _Instance.RegisterDefaultLayoutManager<GroupBoxLayoutManager>();

                    _Instance.RegisterEditor<EditSettingsBase, LabelFactory>();
                    _Instance.RegisterEditor<TextEditSettings, TextBoxFactory>();
                    _Instance.RegisterEditor<PickerEditSettings, PickerFactory>();
                }

                return _Instance;
            }
        }

        #endregion

        #region Public Methods

        public void BeginCustomRegistration()
        {
            _Instance = null;
        }
        
        public void RegisterDefaultLayoutManager<T>()
            where T : LayoutManager
        {
            container.Register<LayoutManager, T>();
        }

        public LayoutManager CreateDefaultLayoutManager()
        {
            return container.GetInstance<LayoutManager>();
        }

        public void RegisterEditor<TService, TImplementation>()
            where TService : EditSettingsBase
            where TImplementation : class, IEditorFactory<TService>
        {
            container.Register<IEditorFactory<TService>, TImplementation>();
        }

        internal IEditorFactory GetEditor(EditSettingsBase settings)
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

        #endregion
    }
}
