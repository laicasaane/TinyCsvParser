using System;
using TinyCsvParser.Model;

namespace TinyCsvParser.Mapping
{
    public class CsvRowMapping<TEntity> : ICsvPropertyMapping<TEntity, TokenizedRow>
    {
        private readonly Func<TEntity, TokenizedRow, bool> action;

        [Obsolete("Use the constructor that accepts a Func and return true/false to indicate mapping success.", true)]
        public CsvRowMapping(Action<TEntity, TokenizedRow> action)
        {
            this.action = (entity, value) => {
                try
                {
                    action(entity, value);
                    return true;
                }
                catch
                {
                    return false;
                }
            };
        }

        public CsvRowMapping(Func<TEntity, TokenizedRow, bool> action)
        {
            this.action = action;
        }

        public bool TryMapValue(TEntity entity, TokenizedRow value)
        {
            return this.action(entity, value);
        }
    }
}
