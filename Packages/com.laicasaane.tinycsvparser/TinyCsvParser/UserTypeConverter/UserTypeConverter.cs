namespace TinyCsvParser.TypeConverter
{
    public abstract class UserTypeConverter<TTargetType> : UserTypeConverterBase<TTargetType>
    {
        protected UserTypeConverter(ITypeConverterProvider converterProvider)
               : base(converterProvider)
        {
        }

        protected UserTypeConverter(int constructorParamAmount, ITypeConverterProvider converterProvider)
            : base(constructorParamAmount, converterProvider)
        {
        }

        public sealed override bool TryConvert(in ReadOnlySpan<string> values, out TTargetType result)
        {
            if (values.Length < this.ConstructorArgs)
            {
                result = default;
                return false;
            }

            return InternalConvert(values, out result);
        }

        protected abstract bool InternalConvert(in ReadOnlySpan<string> values, out TTargetType result);
    }

    public abstract class UserTypeConverter<T, TTargetType> : UserTypeConverter<TTargetType>
    {
        public UserTypeConverter(ITypeConverterProvider converterProvider) : base(1, converterProvider)
        {
        }

        protected abstract TTargetType New(T p);

        protected override bool InternalConvert(in ReadOnlySpan<string> values, out TTargetType result)
        {
            if (!values.TryConvert(0, GetConverter<T>(), out var p))
            {
                result = default;
                return false;
            }

            result = New(p);
            return true;
        }
    }

    public abstract class UserTypeConverter<T1, T2, TTargetType> : UserTypeConverter<TTargetType>
    {
        public UserTypeConverter(ITypeConverterProvider converterProvider) : base(2, converterProvider)
        {
        }

        protected abstract TTargetType New(T1 p1, T2 p2);

        protected override bool InternalConvert(in ReadOnlySpan<string> values, out TTargetType result)
        {
            var index = 0;

            if (!values.TryConvert(ref index, GetConverter<T1>(), out var p1) ||
                !values.TryConvert(ref index, GetConverter<T2>(), out var p2))
            {
                result = default;
                return false;
            }

            result = New(p1, p2);
            return true;
        }
    }

    public abstract class UserTypeConverter<T1, T2, T3, TTargetType> : UserTypeConverter<TTargetType>
    {
        public UserTypeConverter(ITypeConverterProvider converterProvider) : base(3, converterProvider)
        {
        }

        protected abstract TTargetType New(T1 p1, T2 p2, T3 p3);

        protected override bool InternalConvert(in ReadOnlySpan<string> values, out TTargetType result)
        {
            var index = 0;

            if (!values.TryConvert(ref index, GetConverter<T1>(), out var p1) ||
                !values.TryConvert(ref index, GetConverter<T2>(), out var p2) ||
                !values.TryConvert(ref index, GetConverter<T3>(), out var p3))
            {
                result = default;
                return false;
            }

            result = New(p1, p2, p3);
            return true;
        }
    }

    public abstract class UserTypeConverter<T1, T2, T3, T4, TTargetType> : UserTypeConverter<TTargetType>
    {
        public UserTypeConverter(ITypeConverterProvider converterProvider) : base(4, converterProvider)
        {
        }

        protected abstract TTargetType New(T1 p1, T2 p2, T3 p3, T4 p4);

        protected override bool InternalConvert(in ReadOnlySpan<string> values, out TTargetType result)
        {
            var index = 0;

            if (!values.TryConvert(ref index, GetConverter<T1>(), out var p1) ||
                !values.TryConvert(ref index, GetConverter<T2>(), out var p2) ||
                !values.TryConvert(ref index, GetConverter<T3>(), out var p3) ||
                !values.TryConvert(ref index, GetConverter<T4>(), out var p4))
            {
                result = default;
                return false;
            }

            result = New(p1, p2, p3, p4);
            return true;
        }
    }

    public abstract class UserTypeConverter<T1, T2, T3, T4, T5, TTargetType> : UserTypeConverter<TTargetType>
    {
        public UserTypeConverter(ITypeConverterProvider converterProvider) : base(5, converterProvider)
        {
        }

        protected abstract TTargetType New(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5);

        protected override bool InternalConvert(in ReadOnlySpan<string> values, out TTargetType result)
        {
            var index = 0;

            if (!values.TryConvert(ref index, GetConverter<T1>(), out var p1) ||
                !values.TryConvert(ref index, GetConverter<T2>(), out var p2) ||
                !values.TryConvert(ref index, GetConverter<T3>(), out var p3) ||
                !values.TryConvert(ref index, GetConverter<T4>(), out var p4) ||
                !values.TryConvert(ref index, GetConverter<T5>(), out var p5))
            {
                result = default;
                return false;
            }

            result = New(p1, p2, p3, p4, p5);
            return true;
        }
    }

    public abstract class UserTypeConverter<T1, T2, T3, T4, T5, T6, TTargetType> : UserTypeConverter<TTargetType>
    {
        public UserTypeConverter(ITypeConverterProvider converterProvider) : base(6, converterProvider)
        {
        }

        protected abstract TTargetType New(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6);

        protected override bool InternalConvert(in ReadOnlySpan<string> values, out TTargetType result)
        {
            var index = 0;

            if (!values.TryConvert(ref index, GetConverter<T1>(), out var p1) ||
                !values.TryConvert(ref index, GetConverter<T2>(), out var p2) ||
                !values.TryConvert(ref index, GetConverter<T3>(), out var p3) ||
                !values.TryConvert(ref index, GetConverter<T4>(), out var p4) ||
                !values.TryConvert(ref index, GetConverter<T5>(), out var p5) ||
                !values.TryConvert(ref index, GetConverter<T6>(), out var p6))
            {
                result = default;
                return false;
            }

            result = New(p1, p2, p3, p4, p5, p6);
            return true;
        }
    }

    public abstract class UserTypeConverter<T1, T2, T3, T4, T5, T6, T7, TTargetType> : UserTypeConverter<TTargetType>
    {
        public UserTypeConverter(ITypeConverterProvider converterProvider) : base(7, converterProvider)
        {
        }

        protected abstract TTargetType New(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7);

        protected override bool InternalConvert(in ReadOnlySpan<string> values, out TTargetType result)
        {
            var index = 0;

            if (!values.TryConvert(ref index, GetConverter<T1>(), out var p1) ||
                !values.TryConvert(ref index, GetConverter<T2>(), out var p2) ||
                !values.TryConvert(ref index, GetConverter<T3>(), out var p3) ||
                !values.TryConvert(ref index, GetConverter<T4>(), out var p4) ||
                !values.TryConvert(ref index, GetConverter<T5>(), out var p5) ||
                !values.TryConvert(ref index, GetConverter<T6>(), out var p6) ||
                !values.TryConvert(ref index, GetConverter<T7>(), out var p7))
            {
                result = default;
                return false;
            }

            result = New(p1, p2, p3, p4, p5, p6, p7);
            return true;
        }
    }

    public abstract class UserTypeConverter<T1, T2, T3, T4, T5, T6, T7, T8, TTargetType> : UserTypeConverter<TTargetType>
    {
        public UserTypeConverter(ITypeConverterProvider converterProvider) : base(8, converterProvider)
        {
        }

        protected abstract TTargetType New(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8);

        protected override bool InternalConvert(in ReadOnlySpan<string> values, out TTargetType result)
        {
            var index = 0;

            if (!values.TryConvert(ref index, GetConverter<T1>(), out var p1) ||
                !values.TryConvert(ref index, GetConverter<T2>(), out var p2) ||
                !values.TryConvert(ref index, GetConverter<T3>(), out var p3) ||
                !values.TryConvert(ref index, GetConverter<T4>(), out var p4) ||
                !values.TryConvert(ref index, GetConverter<T5>(), out var p5) ||
                !values.TryConvert(ref index, GetConverter<T6>(), out var p6) ||
                !values.TryConvert(ref index, GetConverter<T7>(), out var p7) ||
                !values.TryConvert(ref index, GetConverter<T8>(), out var p8))
            {
                result = default;
                return false;
            }

            result = New(p1, p2, p3, p4, p5, p6, p7, p8);
            return true;
        }
    }

    public abstract class UserTypeConverter<T1, T2, T3, T4, T5, T6, T7, T8, T9, TTargetType> : UserTypeConverter<TTargetType>
    {
        public UserTypeConverter(ITypeConverterProvider converterProvider) : base(9, converterProvider)
        {
        }

        protected abstract TTargetType New(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9);

        protected override bool InternalConvert(in ReadOnlySpan<string> values, out TTargetType result)
        {
            var index = 0;

            if (!values.TryConvert(ref index, GetConverter<T1>(), out var p1) ||
                !values.TryConvert(ref index, GetConverter<T2>(), out var p2) ||
                !values.TryConvert(ref index, GetConverter<T3>(), out var p3) ||
                !values.TryConvert(ref index, GetConverter<T4>(), out var p4) ||
                !values.TryConvert(ref index, GetConverter<T5>(), out var p5) ||
                !values.TryConvert(ref index, GetConverter<T6>(), out var p6) ||
                !values.TryConvert(ref index, GetConverter<T7>(), out var p7) ||
                !values.TryConvert(ref index, GetConverter<T8>(), out var p8) ||
                !values.TryConvert(ref index, GetConverter<T9>(), out var p9))
            {
                result = default;
                return false;
            }

            result = New(p1, p2, p3, p4, p5, p6, p7, p8, p9);
            return true;
        }
    }

    public abstract class UserTypeConverter<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TTargetType> : UserTypeConverter<TTargetType>
    {
        public UserTypeConverter(ITypeConverterProvider converterProvider) : base(10, converterProvider)
        {
        }

        protected abstract TTargetType New(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6, T7 p7, T8 p8, T9 p9, T10 p10);

        protected override bool InternalConvert(in ReadOnlySpan<string> values, out TTargetType result)
        {
            var index = 0;

            if (!values.TryConvert(ref index, GetConverter<T1>(), out var p1) ||
                !values.TryConvert(ref index, GetConverter<T2>(), out var p2) ||
                !values.TryConvert(ref index, GetConverter<T3>(), out var p3) ||
                !values.TryConvert(ref index, GetConverter<T4>(), out var p4) ||
                !values.TryConvert(ref index, GetConverter<T5>(), out var p5) ||
                !values.TryConvert(ref index, GetConverter<T6>(), out var p6) ||
                !values.TryConvert(ref index, GetConverter<T7>(), out var p7) ||
                !values.TryConvert(ref index, GetConverter<T8>(), out var p8) ||
                !values.TryConvert(ref index, GetConverter<T9>(), out var p9) ||
                !values.TryConvert(ref index, GetConverter<T10>(), out var p10))
            {
                result = default;
                return false;
            }

            result = New(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
            return true;
        }
    }
}
