namespace TinyCsvParser.TypeConverter
{
    public interface IUserTypeConverter<TTargetType> : IArrayTypeConverter<TTargetType>
    {
        int ConstructorArgs { get; }
    }
}
