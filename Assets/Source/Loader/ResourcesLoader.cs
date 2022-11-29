
using System;
using UnityEngine;

namespace Loader
{
    public class ResourcesLoader<T> where T : UnityEngine.Object
    {
        private string _path;
        private T[] _results;
        
        public T[] GetResults() => _results;

        public ResourcesLoader(string path)
        {
            _path = path;
        }

        public T[] LoadAll()
        {
            _results = UnityEngine.Resources.LoadAll<T>(_path);
            if (_results == null || _results.Length == 0)
            {
                Debug.LogError(DebugExtensions.Format(GetType(), $"failed to load any assets at path '{_path}'"));
            }
            return _results;
        }

        public bool GetFirst(out T value)
        {
            value = null;
            if (_results != null && _results.Length > 0)
            {
                value = _results[0];
            }
            return value != null;
        }
    }
}
