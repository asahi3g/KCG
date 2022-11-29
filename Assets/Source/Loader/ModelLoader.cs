using System.Collections.Generic;
using UnityEngine;

namespace Loader
{
    public abstract class ModelLoader<K, V> where V : UnityEngine.Object
    {
        private readonly ResourcesLoader<V> _resourcesLoader;
        private Dictionary<K, V> map;


        public ModelLoader(ResourcesLoader<V> resourcesLoader)
        {
            _resourcesLoader = resourcesLoader;
        }


        public bool Get(K key, out V value)
        {
            value = null;
            if (map != null)
            {
                if (map.ContainsKey(key))
                {
                    value = map[key];
                }
            }

            return value != null;
        }

        public void LoadAll()
        {
            map = new Dictionary<K, V>();
            V[] values = _resourcesLoader.LoadAll();

            OnLoadAll(values, map);

            int loadedCount = values.Length;
            int finalCount = map.Values.Count;
            string countStr = $"{finalCount}/{loadedCount}".Color(loadedCount == finalCount ? Color.green : Color.red);
            Debug.Log(DebugExtensions.Format(GetType(), $"loaded {countStr} prefabs"));
        }

        protected abstract void OnLoadAll(IEnumerable<V> values, Dictionary<K, V> map);
    }
}
