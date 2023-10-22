using System;
using System.Collections.Generic;

namespace LRU_Cache
{
    public class Cache<K, V>
    {
        private readonly int _capacity;
        private readonly LinkedList<CacheItem<K, V>> _trackingList = new();
        private readonly Dictionary<K, LinkedListNode<CacheItem<K, V>>> _map = new();

        public Cache(int capacity)
        {
            if (capacity < 1) throw new ArgumentException("Capacity must be greater than zero.");
            _capacity = capacity;
        }

        public V Get(K key)
        {
            if (!_map.ContainsKey(key)) return default;

            LinkedListNode<CacheItem<K, V>> node = _map[key];

            // Push to the end of the LRU list
            _trackingList.Remove(node);
            _trackingList.AddLast(node);

            return node.Value.Value;
        }

        public Cache<K, V> Put(K key, V value)
        {
            LinkedListNode<CacheItem<K, V>> node;
            if (!_map.ContainsKey(key))
            {
                /*
                 * Evict from the LRU list and the dictionary
                 * if LRU list count touches capacity
                */
                if (_capacity == _trackingList.Count)
                {
                    LinkedListNode<CacheItem<K, V>> first = _trackingList.First;
                    _map.Remove(first.Value.Key);
                    _trackingList.RemoveFirst();
                }

                node = new LinkedListNode<CacheItem<K, V>>(new CacheItem<K, V>
                {
                    Key = key,
                    Value = value
                });
                _map.Add(key, node);
            }
            else
            {
                // If key present in map, then over-write with fresh value
                node = _map[key];
                node.Value.Value = value;
                _trackingList.Remove(node);
            }

            _trackingList.AddLast(node);
            return this;
        }
    }
}