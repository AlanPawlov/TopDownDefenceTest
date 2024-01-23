using System.Runtime.CompilerServices;

namespace CommonTemplate.GameSetting
{
    public static class StringExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue(this string value)
        {
            return string.IsNullOrEmpty(value) == false;
        }

        public static OptionsData GetCopy(this OptionsData value)
        {
            var copy = new OptionsData(value.Values);
            return copy;
        }
    }
}