using System;

namespace TgFramework
{
    public interface IResolver
    {
        object Resolve(Type type);
        T Resolve<T>() where T : class;

        void RegisterType<T1, T2>()
            where T2 : class, T1
            where T1 : class;

        void RegisterType<T1, T2>(T2 instance)
            where T2 : class, T1
            where T1 : class;
    }
}