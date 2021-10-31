using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coding.DictionaryExtension
{
    public static class DictionaryShell
    {
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> source, Dictionary<TKey, TValue> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("Collection is null");

            foreach (var item in collection)
                source.Add(item.Key, item.Value);
        }

        public static void RenameKey<TKey, TValue>(this Dictionary<TKey, TValue> dic,
                                      TKey fromKey, TKey toKey)
        {
            TValue value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }

        /// <summary>
        /// Добавляет элемент в словарь, сортируя его по значению
        /// </summary>
        public static void SortedAdd<TKey, TValue>(this Dictionary<TKey, TValue> dic,
                                      TKey key, TValue value)
        {
            dic.Add(key, value);
            dic = dic.OrderBy(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        /// Сортирует словарь по неубыванию значений
        /// </summary>
        public static Dictionary<TKey, TValue> SortUp<TKey, TValue>(this Dictionary<TKey, TValue> dic)
        {
            return dic.OrderBy(x => x.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        /// <summary>
        /// Сортирует словарь по невозрастанию значений
        /// </summary>
        public static Dictionary<TKey, TValue> SortDown<TKey, TValue>(this Dictionary<TKey, TValue> dic)
        {
            return dic.OrderBy(x => x.Value).Reverse().ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public static void AddToValue<TKey>(this Dictionary<TKey, string> dic, string value)
        {
            for (int i = 0; i < dic.Count; ++i)
                dic[dic.ElementAt(i).Key] = value + dic[dic.ElementAt(i).Key];
        }

        public static TKey GetZeroesValue<TKey>(this Dictionary<TKey, string> dic)
        {
            foreach (var item in dic)
            {
                if (item.Value == new string('0', item.Value.Length))
                    return item.Key;
            }

            throw new ArgumentException("Нулевой элемент не найден");
        }
    }
}
