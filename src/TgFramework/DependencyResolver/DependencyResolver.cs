using System;

namespace TgFramework
{
    public class DependencyResolver : IResolver
    {
        private static DependencyResolver current;
        private static readonly object locker = new object();
        private static IResolver dependencyResolver;

        private DependencyResolver()
        {
            SetResolver(new SimpleInjectorResolver());
        }

        public static DependencyResolver Current
        {
            get
            {
                lock (locker)
                {
                    return current ?? (current = new DependencyResolver());
                }
            }
        }

        public object Resolve(Type type)
        {
            lock (locker)
            {
                EnsureResolver();
                return dependencyResolver.Resolve(type);
            }
        }

        public T Resolve<T>() where T : class
        {
            lock (locker)
            {
                EnsureResolver();
                return dependencyResolver.Resolve<T>();
            }
        }

        public void RegisterType<T1, T2>()
            where T2 : class, T1
            where T1 : class
        {
            lock (locker)
            {
                EnsureResolver();
                dependencyResolver.RegisterType<T1, T2>();
            }
        }

        public void RegisterType<T1, T2>(T2 instance)
            where T2 : class, T1
            where T1 : class
        {
            lock (locker)
            {
                EnsureResolver();
                dependencyResolver.RegisterType<T1, T2>();
            }
        }

        public static void SetResolver(IResolver resolver)
        {
            lock (locker)
            {
                dependencyResolver = resolver;
            }
        }

        private static void EnsureResolver()
        {
            if (dependencyResolver == null)
            {
                throw new InvalidOperationException("Call SetResolver first");
            }
        }
    }
}