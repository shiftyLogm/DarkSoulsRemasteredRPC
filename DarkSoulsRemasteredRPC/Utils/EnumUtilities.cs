using DarkSoulsRemasteredRPC.Enums;
using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace DarkSoulsRemasteredRPC.Utils
{
    public static class EnumUtilities
    {
        public static string GetEnumMemberValueById<TEnum>(int id) where TEnum : Enum
        {
            var enumValue = (TEnum)(object)id;
            var memberInfo = typeof(TEnum).GetMember(enumValue.ToString()).FirstOrDefault();
            var enumMemberAttr = memberInfo.GetCustomAttribute<EnumMemberAttribute>();
            return enumMemberAttr.Value;
        }

        public static T GetEnumValueById<T>(int id) where T : Enum => (T)(object)id;
    }
}
