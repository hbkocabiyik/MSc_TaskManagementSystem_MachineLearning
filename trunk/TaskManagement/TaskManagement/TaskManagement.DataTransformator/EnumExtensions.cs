using System;

namespace TaskManagement.DataTransformator
{
    public static class EnumExtensions
    {
        public static T GetEnum<T>(this string value) where T : struct
        {
            var enumValue = value.Replace(" ", string.Empty);

            return (T)Enum.Parse(typeof(T), enumValue);
        }

        public static void CheckValue(this DbManagementRow dbManagementRow, string valueToCheck)
        {
            dbManagementRow.Skills[valueToCheck] = true;
        }
    }
}