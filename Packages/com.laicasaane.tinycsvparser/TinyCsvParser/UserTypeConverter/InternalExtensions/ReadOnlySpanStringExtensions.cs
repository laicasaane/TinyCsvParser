namespace TinyCsvParser.TypeConverter
{
    internal static class ReadOnlySpanStringExtensions
    {
        public static bool TryConvert<T>(in this ReadOnlySpan<string> self, int index, ITypeConverter<T> converter, out T result)
            => converter.TryConvert(self[index], out result);

        public static bool TryConvert<T>(in this ReadOnlySpan<string> self, ref int index, ITypeConverter<T> converter, out T result)
        {
            var success = converter.TryConvert(self[index], out result);
            index += 1;

            return success;
        }
    }
}