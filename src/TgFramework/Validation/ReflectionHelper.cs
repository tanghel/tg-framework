using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TgFramework.Validation
{
    public static class ReflectionHelper
    {
        private static readonly Dictionary<Type, List<PropertyInfo>> propertyInfos
            = new Dictionary<Type, List<PropertyInfo>>();

        public static List<PropertyInfo> GetEditableProperties(Type type)
        {
            if (!propertyInfos.ContainsKey(type))
            {
                var properties = type.GetProperties().Where(p => p.CanRead && p.CanWrite).ToList();
                propertyInfos.Add(type, properties);
            }

            return propertyInfos[type];
        }
    }
}