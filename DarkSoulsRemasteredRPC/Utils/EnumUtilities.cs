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

            if (memberInfo is null)
                return enumValue.ToString();

            var enumMemberAttr = memberInfo.GetCustomAttribute<EnumMemberAttribute>();
            return enumMemberAttr?.Value ?? enumValue.ToString();
        }
        
        public static int GetEnumIdByMemberValue<TEnum>(string memberValue) where TEnum : Enum
        {
            var matchingField = typeof(TEnum)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(field =>
                {
                    var attr = field.GetCustomAttribute<EnumMemberAttribute>();
                    return attr != null && attr.Value == memberValue;
                });

            if (matchingField is null)
                throw new ArgumentException();

            return (int)matchingField.GetValue(null)!;
        }
    }
}