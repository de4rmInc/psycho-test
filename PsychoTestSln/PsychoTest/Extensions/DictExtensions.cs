using System.Collections.Generic;

namespace PsychoTest.Extensions
{
    public static class DictExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue valueToAdd)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, valueToAdd);
                return valueToAdd;
            }
            else
                return dictionary[key];
        }
    }
}
