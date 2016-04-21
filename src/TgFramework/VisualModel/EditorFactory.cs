using System;
using TgFramework.VisualModel.API;
using TgFramework.VisualModel.Editors;
using TgFramework.VisualModel.Layout;

namespace TgFramework.VisualModel
{
    public class EditorFactory
    {
        #region Ioc container implementation

        private static EditorFactory instance;

        public static EditorFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EditorFactory();

                    instance.RegisterEditor<LabelField, LabelFactory>();
                    instance.RegisterEditor<TextField, TextBoxFactory>();
                    instance.RegisterEditor<PickerField, PickerFactory>();
                    instance.RegisterEditor<ButtonField, ButtonFactory>();
                    instance.RegisterEditor<ProgressBarField, ProgressBarFactory>();

                    instance.RegisterDefaultLayoutSettings<GroupBoxLayoutSettings>();
                    instance.RegisterLayout<StackPanelLayoutSettings, StackPanelLayoutFactory>();
                    instance.RegisterLayout<GroupBoxLayoutSettings, GroupBoxLayoutFactory>();
                }

                return instance;
            }
        }

        #endregion

        #region Public Methods

        public void RegisterDefaultLayoutSettings<T>()
            where T : LayoutSettingsBase
        {
            DependencyResolver.Current.RegisterType<LayoutSettingsBase, T>();
        }

        public void RegisterDefaultEditField<T>()
            where T : FieldBase
        {
            DependencyResolver.Current.RegisterType<FieldBase, T>();
        }

        public LayoutSettingsBase CreateDefaultLayoutSettings()
        {
            return DependencyResolver.Current.Resolve<LayoutSettingsBase>();
        }

        public FieldBase CreateDefaultEditField()
        {
            return DependencyResolver.Current.Resolve<FieldBase>();
        }

        public void RegisterEditor<TService, TImplementation>()
            where TService : FieldBase
            where TImplementation : class, IEditorFactory<TService>
        {
            DependencyResolver.Current.RegisterType<IEditorFactory<TService>, TImplementation>();
        }

        public void RegisterLayout<TService, TImplementation>()
            where TService : LayoutSettingsBase
            where TImplementation : class, ILayoutFactory<TService>
        {
            DependencyResolver.Current.RegisterType<ILayoutFactory<TService>, TImplementation>();
        }

        internal IEditorFactory GetEditorFactory(FieldBase field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }

            var type = typeof (IEditorFactory<>).MakeGenericType(field.GetType());
            var factory = DependencyResolver.Current.Resolve(type) as IEditorFactory;

            if (factory == null)
            {
                throw new InvalidOperationException("Could not create IEditorFactory of type " + field.GetType());
            }

            return factory;
        }

        internal ILayoutFactory GetLayoutFactory(LayoutSettingsBase settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }

            var type = typeof (ILayoutFactory<>).MakeGenericType(settings.GetType());
            var factory = DependencyResolver.Current.Resolve(type) as ILayoutFactory;

            if (factory == null)
            {
                throw new InvalidOperationException("Could not create ILayoutFactory of type " + settings.GetType());
            }

            return factory;
        }

        #endregion
    }
}