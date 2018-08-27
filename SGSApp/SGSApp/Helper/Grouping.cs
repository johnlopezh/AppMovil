using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SGSApp.Helper
{
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items) Items.Add(item);
        }

        public K Key { get; set; }
    }
}