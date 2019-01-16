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
        public static IList<T> GetValues()
        {
            return Enum.GetValues(typeof(T))
                            .Cast<T>()
                            .ToList();
        }

        public static IList<string> GetNames()
        {
            return typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static IList<string> GetDisplayValues()
        {
            return GetNames().Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        public static IList<T> GetValues(T value)
        {

            return GetValues().Where(val => value.IsSet(val)).ToList();
        }

        public static IList<string> GetNames(T value)
        {
            return typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public).Where(fi => value.IsSet((T)fi.GetValue(null))).Select(fi => fi.Name).ToList();
        }

        public static string GetName(T value)
        {
            return typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public).Where(fi => value.IsSet((T)fi.GetValue(null))).Select(fi => fi.Name).FirstOrDefault();
        }

        public static IList<string> GetDisplayValues(T value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static T ParseFlags(IList<string> value)
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
            foreach (PropertyInfo staticProperty in resourceManagerProvider.GetProperties(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (staticProperty.PropertyType == typeof(System.Resources.ResourceManager))
                {
                    System.Resources.ResourceManager resourceManager = (System.Resources.ResourceManager)staticProperty.GetValue(null, null);
                    return resourceManager.GetString(resourceKey);
                }
            }

            return resourceKey; // Fallback with the key name
        }

        public static string GetDisplayValue(T value)
        {
            var typeInfo = value.GetType().GetTypeInfo();

            //var flagAttribute = typeInfo.GetCustomAttribute<FlagsAttribute>();

            //if (flagAttribute == null)
            //{
                var fieldInfo = value.GetType().GetField(value.ToString());
                if (fieldInfo == null)
                    return value.ToString();

                var descriptionAttributes = fieldInfo.GetCustomAttribute<DisplayAttribute>();
                if (descriptionAttributes == null)
                    return value.ToString();

                if (descriptionAttributes.ResourceType != null)
                    return lookupResource(descriptionAttributes.ResourceType, descriptionAttributes.Name);

                return descriptionAttributes.Name;
            //}

            //return String.Join(", ", GetDisplayValues(value));
        }
    }
}