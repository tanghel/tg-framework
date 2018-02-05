using System;
using SimpleInjector;

namespace TgFramework
{
    public class SimpleInjectorResolver : IResolver
    {
        private readonly Container _container = new Container();

        public SimpleInjectorResolver()
        {
            _container.Options.AllowOverridingRegistrations = true;
        }

        public object Resolve(Type type)
        {
            return _container.GetInstance(type);
        }

        public T Resolve<T>() where T : class
        {
            return _container.GetInstance<T>();
        }

        public void RegisterType<T1, T2>()
            where T2 : class, T1
            where T1 : class
        {
            _container.Register<T1, T2>();
        }

        public void RegisterType<T1, T2>(T2 instance)
            where T2 : class, T1
            where T1 : class
        {
            _container.Register<T1>(() => instance);
        }
    }
}