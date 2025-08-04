using System;
using System.Linq;
using System.Reflection;

namespace NoMod.Loader
{
    internal static class ServiceProviderExtensions
    {
        public static void FillObjectProperties(this IServiceProvider provider, object obj)
        {
            var setObject = new object[] { null };
            var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties.Where(p => p.GetSetMethod(true) != null))
            {
                var value = provider.GetService(property.PropertyType);
                if (value != null)
                {
                    setObject[0] = value;
                    property.GetSetMethod(true).Invoke(obj, setObject);
                }
            }
        }

        public static void FillObjectFields(this IServiceProvider provider, object obj)
        {
            var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var value = provider.GetService(field.FieldType);
                if (value != null)
                {
                    field.SetValue(obj, value);
                }
            }
        }
    }
}
