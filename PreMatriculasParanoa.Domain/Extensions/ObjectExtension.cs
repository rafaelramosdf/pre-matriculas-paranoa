using System.Collections.Generic;
using System.Reflection;
using System;
using System.Text.Json;
using System.Collections;

namespace PreMatriculasParanoa.Domain.Extensions
{
    public static class ObjectExtension
    {
        public static T Clone<T>(this T obj) where T : class
        {
            if (obj == null) 
                return null;

            var serializedObj = JsonSerializer.Serialize(obj);
            return JsonSerializer.Deserialize<T>(serializedObj);
        }

        public static bool CompareObjects(this object obj1, object obj2)
        {
            if (obj1 == null || obj2 == null)
                return false;

            Type type1 = obj1.GetType();
            Type type2 = obj2.GetType();

            if (type1 != type2)
                return false;

            PropertyInfo[] properties = type1.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object value1 = property.GetValue(obj1);
                object value2 = property.GetValue(obj2);

                if (value1 == null && value2 == null)
                    continue;

                if ((value1 == null && value2 != null) || (value1 != null && value2 == null))
                    return false;

                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    IList list1 = (IList)value1;
                    IList list2 = (IList)value2;

                    if (list1.Count != list2.Count)
                        return false;

                    for (int i = 0; i < list1.Count; i++)
                    {
                        if (!CompareObjects(list1[i], list2[i]))
                            return false;
                    }
                }
                else if (!property.PropertyType.IsPrimitive && !property.PropertyType.IsValueType && property.PropertyType != typeof(string))
                {
                    if (!CompareObjects(value1, value2))
                        return false;
                }
                else
                {
                    if (!value1.Equals(value2))
                        return false;
                }
            }

            return true;
        }
    }
}
