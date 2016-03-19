using System.Collections.Generic;

namespace PsychoTest.Extensions
{
    public static class ListExtensions
    {
        public static TValue GetOrSet<TValue>(this IList<TValue> list, int index, TValue valueToSet)
        {
            if (index >= list.Count)
            {
                list.Add(valueToSet);
                return valueToSet;
            }

            var value = list[index];
            if (value == null)
            {
                list[index] = valueToSet;
                return valueToSet;
            }
            else
                return value;
        }
    }
}