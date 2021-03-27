using System;

namespace TinyCsvParser.TypeConverter
{
    public abstract class UniformUserTypeConverter<T, TTargetType> : UserTypeConverter<TTargetType>
    {
        private readonly T[] resultCache;

        protected UniformUserTypeConverter(ITypeConverterProvider converterProvider)
               : base(converterProvider)
        {
            this.resultCache = new T[this.ConstructorArgs];
        }

        protected UniformUserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
            this.resultCache = new T[this.ConstructorArgs];
        }

        protected abstract TTargetType New(T[] args);

        protected override bool InternalConvert(in ReadOnlySpan<string> values, out TTargetType result)
        {
            var converter = GetConverter<T>();

            for (var i = 0; i < this.resultCache.Length; i++)
            {
                if (!values.TryConvert(i, converter, out this.resultCache[i]))
                {
                    result = default;
                    return false;
                }
            }

            result = New(this.resultCache);
            return true;
        }
    }

    public abstract class BoolUserTypeConverter<TTargetType> : UniformUserTypeConverter<Boolean, TTargetType>
    {
        protected BoolUserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected BoolUserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class ByteUserTypeConverter<TTargetType> : UniformUserTypeConverter<Byte, TTargetType>
    {
        protected ByteUserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected ByteUserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class DateTimeUserTypeConverter<TTargetType> : UniformUserTypeConverter<DateTime, TTargetType>
    {
        protected DateTimeUserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected DateTimeUserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class DecimalUserTypeConverter<TTargetType> : UniformUserTypeConverter<Decimal, TTargetType>
    {
        protected DecimalUserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected DecimalUserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class DoubleUserTypeConverter<TTargetType> : UniformUserTypeConverter<Double, TTargetType>
    {
        protected DoubleUserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected DoubleUserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class GuidUserTypeConverter<TTargetType> : UniformUserTypeConverter<Guid, TTargetType>
    {
        protected GuidUserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected GuidUserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class Int16UserTypeConverter<TTargetType> : UniformUserTypeConverter<Int16, TTargetType>
    {
        protected Int16UserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected Int16UserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class Int32UserTypeConverter<TTargetType> : UniformUserTypeConverter<Int32, TTargetType>
    {
        protected Int32UserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected Int32UserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class Int64UserTypeConverter<TTargetType> : UniformUserTypeConverter<Int64, TTargetType>
    {
        protected Int64UserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected Int64UserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class SByteUserTypeConverter<TTargetType> : UniformUserTypeConverter<SByte, TTargetType>
    {
        protected SByteUserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected SByteUserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class SingleUserTypeConverter<TTargetType> : UniformUserTypeConverter<Single, TTargetType>
    {
        protected SingleUserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected SingleUserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class StringUserTypeConverter<TTargetType> : UniformUserTypeConverter<String, TTargetType>
    {
        protected StringUserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected StringUserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class TimeSpanUserTypeConverter<TTargetType> : UniformUserTypeConverter<TimeSpan, TTargetType>
    {
        protected TimeSpanUserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected TimeSpanUserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class UInt16UserTypeConverter<TTargetType> : UniformUserTypeConverter<UInt16, TTargetType>
    {
        protected UInt16UserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected UInt16UserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class UInt32UserTypeConverter<TTargetType> : UniformUserTypeConverter<UInt32, TTargetType>
    {
        protected UInt32UserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected UInt32UserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }

    public abstract class UInt64UserTypeConverter<TTargetType> : UniformUserTypeConverter<UInt64, TTargetType>
    {
        protected UInt64UserTypeConverter(ITypeConverterProvider converterProvider)
                  : base(converterProvider)
        {
        }

        protected UInt64UserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }
    }
}