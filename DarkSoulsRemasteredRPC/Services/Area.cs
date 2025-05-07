using DarkSoulsRemasteredRPC.Enums;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;

namespace DarkSoulsRemasteredRPC.Services
{
    public static class Area
    {
        public static string GetAreaNameById(int id)
        {
            var enumValue = (AreaId)(object)id;
            var memberInfo = typeof(AreaId).GetMember(enumValue.ToString()).FirstOrDefault();
            var enumMemberAttr = memberInfo.GetCustomAttribute<EnumMemberAttribute>();
            return enumMemberAttr.Value;
        }
    }
}
