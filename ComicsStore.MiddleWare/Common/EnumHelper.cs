using ComicsStore.MiddleWare.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ComicsStore.MiddleWare.Common
{
    public static class EnumHelper<T> where T : Enum
    {
        public static bool IsFlag(T value)
        {
            var typeInfo = value.GetType().GetTypeInfo();

            var flagAttribute = typeInfo.GetCustomAttribute<FlagsAttribute>();

            return (flagAttribute != null);
        }

        public static ICollection<T> GetValues()
        {
            return Enum.GetValues(typeof(T))
                            .Cast<T>()
                            .ToList();
        }

        public static ICollection<string> GetNames()
        {
            return typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static ICollection<string> GetDisplayValues()
        {
            return GetNames().Select(obj => GetDisplayValueOneValue(Parse(obj))).ToList();
        }

        public static ICollection<T> GetValues(T value)
        {
            if (IsFlag(value))
            {
                return GetValues().Where(val => value.IsSet(val)).ToList();
            }

            return new List<T> { value };
        }

        public static ICollection<string> GetNames(T value)
        {
            if (IsFlag(value))
            {
                return typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public).Where(fi => value.IsSet((T)fi.GetValue(null))).Select(fi => fi.Name).ToList();
            }

            return new List<string> { value.ToString() };
        }

        public static string GetName(T value)
        {
            if (IsFlag(value))
            {
                var fields = typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public).Where(fi => value.IsSet((T)fi.GetValue(null))).Select(fi => fi.Name).ToList();
                return string.Join(",", fields);
            }

            return value.ToString();
        }

        public static ICollection<string> GetDisplayValues(T value)
        {
            return GetNames(value).Select(obj => GetDisplayValueOneValue(Parse(obj))).ToList();
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T ParseFlags(ICollection<string> value)
        {
            var result = default(T);

            foreach (var s in value)
            {
                result = result.Set((T)Enum.Parse(typeof(T), s, true));
            }

            return result;
        }

        private static string lookupResource(Type resourceManagerProvider, string resourceKey)
        {
            foreach (var staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    var resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey; // Fallback with the key name
        }

        private static string GetDisplayValueOneValue(T value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
            {
                return value.ToString();
            }

            var descriptionAttributes = fieldInfo.GetCustomAttribute<DisplayAttribute>();
            if (descriptionAttributes == null)
            {
                return value.ToString();
            }

            if (descriptionAttributes.ResourceType != null)
            {
                return lookupResource(descriptionAttributes.ResourceType, descriptionAttributes.Name);
            }

            return descriptionAttributes.Name;
        }

        public static string GetDisplayValue(T value)
        {
            var typeInfo = value.GetType().GetTypeInfo();

            var flagAttribute = typeInfo.GetCustomAttribute<FlagsAttribute>();

            if (!IsFlag(value))
            {
                return GetDisplayValueOneValue(value);
            }

            return String.Join(", ", GetDisplayValues(value));
        }
    }
}