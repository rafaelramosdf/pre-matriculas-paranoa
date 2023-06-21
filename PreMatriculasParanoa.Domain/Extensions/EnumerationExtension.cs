using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace PreMatriculasParanoa.Domain.Extensions
{
    public static class EnumerationExtension
    {
        public static string EnumDescription(this Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo.Length > 0)
            {
                var attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }

        public static IEnumerable<string> EnumNames<T>()
        {
            return (from object value in Enum.GetValues(typeof(T)) select Enum.GetName(typeof(T), value)).ToList();
        }

        public static IEnumerable<string> EnumDescriptions<T>()
        {
            var attributes = typeof(T).GetMembers()
                .SelectMany(member => member.GetCustomAttributes(typeof(DescriptionAttribute), true).Cast<DescriptionAttribute>())
                .ToList();

            return attributes.Select(x => x.Description);
        }

        /// <summary>
        /// Converte um Array de Enum em uma string com os valores (HashCode) separados por "," (Vírgula).
        /// </summary>
        /// <returns>Ex.: "1, 2, 3, 4" ...</returns>
        public static string EnumCodesToStringArray<T>(this IEnumerable<T> en) where T : Enum
        {
            var list = new List<string>();
            foreach (var item in en)
                list.Add(item.GetHashCode().ToString());
            return string.Join(",", list);
        }

        public static bool EnumHas<T>(this Enum type, T value)
        {
            try
            {
                return (((int)(object)type & (int)(object)value) == (int)(object)value);
            }
            catch
            {
                return false;
            }
        }

        public static bool EnumIs<T>(this Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        public static T EnumAdd<T>(this Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(string.Format("Não foi possível adicionar o valor '{0}'.", typeof(T).Name), ex);
            }
        }

        public static T EnumRemove<T>(this Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format("Náo foi possivel remover o valor '{0}'.", typeof(T).Name), ex);
            }
        }

        public static List<T> OrdenarLista<T>(this List<T> lista, string campo)
        {
            if (!string.IsNullOrEmpty(campo))
            {
                var propertyInfo = typeof(T).GetProperty(campo);

                if (propertyInfo != null)
                    lista = lista.OrderBy(x => propertyInfo.GetValue(x)).ToList();
            }

            return lista;
        }

        public static IEnumerable<Enum> GetFlags(this Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value;
        }

        public static string ToDescription<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            return (attributes != null && attributes.Length > 0) ? attributes[0].Description : source.ToString();
        }

        /// <summary>
        /// Converte uma descrição de enumerador em um valor do tipo "T" informado.
        /// </summary>
        /// <typeparam name="T">Tipo de retorno (saída)</typeparam>
        /// <param name="description">Texto "description" do enum</param>
        /// <returns>Retorna o valor convertido</returns>
        public static T DescriptionTo<T>(this string description)
        {
            var fis = typeof(T).GetFields();

            foreach (var fi in fis)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0 && attributes[0].Description == description)
                    return (T)Enum.Parse(typeof(T), fi.Name);
            }

            return default(T);
        }
    }
}