namespace LRU_Cache
{
    public class CacheItem<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }
    }
}