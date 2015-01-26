using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TgFramework.Core
{
    public static class CoreFrameworkExtensions
    {
        /// <summary>
        /// Returns the formatted string for the given value and arguments.
        /// </summary>
        /// <param name="value">The string value to format.</param>
        /// <param name="arguments">The arguments used for formatting.</param>
        /// <returns></returns>
        public static string FormatString(this string value, params object[] arguments)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            return string.Format(value, arguments);
        }

        /// <summary>
        /// Invokes the provided event handler using the given event sender and arguments.
        /// </summary>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The event arguments.</param>
        public static void Invoke(this EventHandler eventHandler, object sender, EventArgs args)
        {
            var handler = eventHandler;
            if (handler != null)
                handler.Invoke(sender, args);
        }

        /// <summary>
        /// Invokes the provided event handler using the given event sender and arguments.
        /// </summary>
        /// <typeparam name="TArgs">The type of the arguments.</typeparam>
        /// <param name="eventHandler">The event handler.</param>
        /// <param name="sender">The event sender.</param>
        /// <param name="args">The event arguments.</param>
        public static void Invoke<TArgs>(this EventHandler<TArgs> eventHandler, object sender, TArgs args) where TArgs : EventArgs
        {
            var handler = eventHandler;
            if (handler != null)
                handler.Invoke(sender, args);
        }
        
        private static string GetHashValue(this string value, HashAlgorithm algorithm)
        {
            var data = Encoding.UTF8.GetBytes(value);

            var hash = algorithm.ComputeHash(data);

            return new string(hash.SelectMany(x => x.ToString("X2").ToArray()).ToArray());
        }

        public static string ToSha1(this string value)
        {
            return value.GetHashValue(new SHA1Managed());
        }

        public static string ToSha512(this string value)
        {
            return value.GetHashValue(new SHA512Managed());
        }

        public static string ToMd5(this string value)
        {
            return value.GetHashValue(new MD5CryptoServiceProvider());
        }

        /// <summary>
        /// Converts an object to DateTime
        /// </summary>
        /// <param name="obj">The object to be converted</param>
        /// <returns>a DateTime representing the converted item. Otherwise throws an exception</returns>
        public static DateTime ToDateTime(this object obj)
        {
            if (obj == null)
                throw new NullReferenceException("ToDateTime does not allow to operate on a null value");

            DateTime result;

            if (!DateTime.TryParse(obj.ToString(), out result))
            {
                throw new Exception("ToDateTime exception: Could not convert object to DateTime");
            }

            return result;
        }

        /// <summary>
        /// Converts an object to int (32-bit)
        /// </summary>
        /// <param name="obj">The object to be converted</param>
        /// <returns>an int representing the converted item. Otherwise throws an exception</returns>
        public static int ToInt(this object obj, int defaultValue)
        {
            return obj.ToNullableInt() ?? defaultValue;
        }

        public static int ToInt(this object obj)
        {
            return obj.ToInt(0);
        }

        public static int? ToNullableInt(this object obj)
        {
            if (obj is int)
            {
                return (int)obj;
            }

            int result;

            if (!Int32.TryParse(obj.ToStringNN(), out result))
            {
                return null;
            }

            return result;
        }

        public static double? ToNullableDouble(this object obj)
        {
            if (obj is double)
            {
                return (double)obj;
            }

            double result;
            if (!double.TryParse(obj.ToStringNN(), out result))
            {
                return null;
            }

            return result;
        }

        public static string ToStringNN(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            return obj.ToString();
        }

        public static Task Continue(this Task task, Action<Task> continuationFunction)
        {
            return task.ContinueWith(continuationFunction, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static Task Continue<T>(this Task<T> task, Action<Task<T>> continuationFunction)
        {
            return task.ContinueWith(continuationFunction, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static bool None<T>(this IEnumerable<T> collection)
        {
            return !collection.Any();
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> collection, T item)
        {
            return collection.Concat(new T[] { item });
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, T item)
        {
            return collection.Except(new T[] { item });
        }

        public static IEnumerable<T> GetEnumItems<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static T If<T>(this T value, Func<T, bool> condition, T valueIfTrue)
        {
            return condition(value) ? valueIfTrue : value;
        }

        public static void ExecuteIf<T>(this T value, Func<T, bool> condition, Action<T> executer)
        {
            if (condition(value))
            {
                executer(value);
            }
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static void ExecuteOnAll<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            foreach (var item in items)
            {
                action(item);
            }
        }

        public static IEnumerable<T> ExecuteOnAllIndexed<T>(this IEnumerable<T> items, Action<T, int> action)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            int index = 0;
            foreach (var item in items)
            {
                action(item, index++);

                yield return item;
            }
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> items)
        {
            if (items == null)
            {
                return new T[] { };
            }

            return items;
        }

        public static void IfNotNull<T>(this T item, params Action<T>[] actions)
            where T : class
        {
            if (item != null)
            {
                foreach (var action in actions)
                {
                    action.Invoke(item);
                }
            }
        }

        public static T To<T>(this object item)
        {
            if (item is T)
            {
                return (T)item;
            }

            return default(T);
        }

#if !NETFX_CORE

        public static void CopyTo<T>(this T entity, T another)
        {
            var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanRead && x.CanWrite);

            foreach (var property in properties)
            {
                property.SetValue(another, property.GetValue(entity, null), null);
            }
        }

        public static T Copy<T>(this object entity) where T : new()
        {
            var result = new T();
            var sourceProperties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanRead && x.CanWrite);
            var targetProperties = result.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(x => x.CanRead && x.CanWrite);

            foreach (var sourceProperty in sourceProperties)
            {
                var targetProperty = targetProperties.FirstOrDefault(x => x.Name == sourceProperty.Name);
                if (targetProperty != null)
                {
                    targetProperty.SetValue(result, sourceProperty.GetValue(entity, null), null);
                }
            }

            return result;
        }

#endif
    }
}
