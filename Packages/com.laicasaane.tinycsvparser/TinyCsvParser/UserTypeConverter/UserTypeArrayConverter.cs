using System;

namespace TinyCsvParser.TypeConverter
{
    public class UserTypeArrayConverter<TTargetType> : IArrayTypeConverter<TTargetType[]>
    {
        private readonly IUserTypeConverter<TTargetType> internalConverter;

        public UserTypeArrayConverter(IUserTypeConverter<TTargetType> internalConverter)
        {
            this.internalConverter = internalConverter;
        }

        public bool TryConvert(in ReadOnlySpan<string> values, out TTargetType[] result)
        {
            var argCount = this.internalConverter.ConstructorArgs;

            if (argCount <= 0)
            {
                result = Array.Empty<TTargetType>();
                return false;
            }

            var arrayLength = values.Length / argCount;

            if (arrayLength <= 0)
            {
                result = Array.Empty<TTargetType>();
                return false;
            }

            result = new TTargetType[arrayLength];

            for (var i = 0; i < arrayLength; i++)
            {
                var start = i * argCount;

                if (!this.internalConverter.TryConvert(values.Slice(start, argCount), out TTargetType element))
                    return false;

                result[i] = element;
            }

            return true;
        }

        public Type TargetType => typeof(TTargetType[]);
    }
}
