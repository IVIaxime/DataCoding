using System.Collections.Generic;

namespace lab8
{
    public class Pair<T1, T2>
    {
        public T1 First { get; set; }

        public T2 Second { get; set; }

        public Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }

        public Pair(KeyValuePair<T1, T2> keyValuePair)
        {
            First = keyValuePair.Key;
            Second = keyValuePair.Value;
        }
    }
}
