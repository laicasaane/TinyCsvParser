namespace TinyCsvParser.TypeConverter
{
    public class NullableUserTypeConverter<TTargetType> : UserTypeConverterBase<TTargetType?>
        where TTargetType : struct
    {
        private readonly UserTypeConverter<TTargetType> internalConverter;

        public NullableUserTypeConverter(UserTypeConverter<TTargetType> internalConverter)
            : base(internalConverter.ConstructorArgs, null)
        {
            this.internalConverter = internalConverter;
        }

        public override bool TryConvert(in ReadOnlySpan<string> values, out TTargetType? result)
        {
            if (this.internalConverter.TryConvert(values, out var innerResult))
            {
                result = innerResult;
                return true;
            }

            result = default;
            return false;
        }
    }
}
