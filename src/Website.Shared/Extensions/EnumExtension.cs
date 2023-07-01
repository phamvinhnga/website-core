using System.ComponentModel;

namespace Website.Shared.Extensions
{
    public static class EnumExtension
    {
        public static string GetEnumDescription<T>(this T source, int index = 0)
        {
            var field = source?.GetType()?.GetField(source.ToString() ?? string.Empty);
            if (field is null)
            {
                return string.Empty;
            }

            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0 && index < attributes.Length && index > -1)
            {
                return attributes[index].Description;
            }
            return string.Empty;
        }
    }
}
