using System.Runtime.CompilerServices;

namespace TinyCsvParser
{
    public static class SpanExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] array)
            => new ReadOnlySpan<T>(array);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] array, int start)
            => new ReadOnlySpan<T>(array, start);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] array, int start, int length)
            => new ReadOnlySpan<T>(array, start, length);
    }
}
