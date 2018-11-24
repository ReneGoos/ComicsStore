using System;
using System.Collections.Generic;
using System.Text;

namespace ComicsStore.MiddleWare.Extensions
{
    public static class EnumExtensions
    {
        public static T Set<T>(this T value, T flags)
            where T : Enum
        {
            Type type = typeof(T);

            // only works with enums
            if (!type.IsEnum) throw new ArgumentException(
                "The type parameter T must be an enum type.");

            // handle each underlying type
            Type numberType = Enum.GetUnderlyingType(type);

            if (numberType.Equals(typeof(int)))
            {
                return BoxSet<T, int>(value, flags, (a, b) => a | b);
            }
            else if (numberType.Equals(typeof(sbyte)))
            {
                return BoxSet<T,sbyte>(value, flags, (a, b) => (sbyte)(a | b));
            }
            else if (numberType.Equals(typeof(byte)))
            {
                return BoxSet<T,byte>(value, flags, (a, b) => (byte)(a | b));
            }
            else if (numberType.Equals(typeof(short)))
            {
                return BoxSet<T,short>(value, flags, (a, b) => (short)(a | b));
            }
            else if (numberType.Equals(typeof(ushort)))
            {
                return BoxSet<T,ushort>(value, flags, (a, b) => (ushort)(a | b));
            }
            else if (numberType.Equals(typeof(uint)))
            {
                return BoxSet<T,uint>(value, flags, (a, b) => a | b);
            }
            else if (numberType.Equals(typeof(long)))
            {
                return BoxSet<T,long>(value, flags, (a, b) => a | b);
            }
            else if (numberType.Equals(typeof(ulong)))
            {
                return BoxSet<T,ulong>(value, flags, (a, b) => a | b);
            }
            else if (numberType.Equals(typeof(char)))
            {
                return BoxSet<T,char>(value, flags, (a, b) => (char)(a | b));
            }
            else
            {
                throw new ArgumentException(
                    "Unknown enum underlying type " +
                    numberType.Name + ".");
            }
        }

        public static bool IsSet<T>(this T value, T flags)
            where T : Enum
        {
            Type type = typeof(T);

            // only works with enums
            if (!type.IsEnum) throw new ArgumentException(
                "The type parameter T must be an enum type.");

            // handle each underlying type
            Type numberType = Enum.GetUnderlyingType(type);

            if (numberType.Equals(typeof(int)))
            {
                return BoxIsSet<int>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(sbyte)))
            {
                return BoxIsSet<sbyte>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(byte)))
            {
                return BoxIsSet<byte>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(short)))
            {
                return BoxIsSet<short>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(ushort)))
            {
                return BoxIsSet<ushort>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(uint)))
            {
                return BoxIsSet<uint>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(long)))
            {
                return BoxIsSet<long>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(ulong)))
            {
                return BoxIsSet<ulong>(value, flags, (a, b) => (a & b) == b);
            }
            else if (numberType.Equals(typeof(char)))
            {
                return BoxIsSet<char>(value, flags, (a, b) => (a & b) == b);
            }
            else
            {
                throw new ArgumentException(
                    "Unknown enum underlying type " +
                    numberType.Name + ".");
            }
        }

        /// Helper function for handling the value types. Boxes the
        /// params to object so that the cast can be called on them.
        private static TEnum BoxSet<TEnum, TUl>(object value, object flags,
            Func<TUl, TUl, TUl> op)
        {
            return (TEnum) (object) op((TUl)value, (TUl)flags);
        }

        /// Helper function for handling the value types. Boxes the
        /// params to object so that the cast can be called on them.
        private static bool BoxIsSet<T>(object value, object flags,
            Func<T, T, bool> op)
        {
            return op((T)value, (T)flags);
        }
    }
}
