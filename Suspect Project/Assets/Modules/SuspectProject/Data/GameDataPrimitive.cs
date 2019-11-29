using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuspectProject.Data
{
    public abstract class GameDataPrimitive
    {
        public struct Action
        {
            public enum Type { Init, Add, Remove, Set };

            public GameDataPrimitive primitive;
            public Type type;
            public object[] parameters;
        }

        public static Queue<Action> ObservedChangedPrimitives { get; private set; } = new Queue<Action>();

        public GameDataPrimitive()
        {
            RegisterChangedPrimitive(this, Action.Type.Init);
        }

        public void RegisterChangedPrimitive(GameDataPrimitive primitive, Action.Type type, params object[] parameters)
        {
            ObservedChangedPrimitives.Enqueue(new Action
            {
                primitive = primitive,
                type = type,
                parameters = parameters
            });
        }
    }

    public sealed class GameDataPrimitive<T> : GameDataPrimitive
    {
        private T _value = default;
        public T value => _value;

        public GameDataPrimitive() : base()
        {
            if (typeof(T) != typeof(string))
                _value = Activator.CreateInstance<T>();
            else
                _value = (T)Activator.CreateInstance(typeof(string), '\0', 0);
        }

        public void SetValue(T value)
        {
            if (!_value.Equals(value))
            {
                RegisterChangedPrimitive(this, Action.Type.Set);
                _value = value;
            }
        }

        public override string ToString()
        {
            string result = $"{GetType().Name} : [\n";
            result += $"{typeof(T).Name} : {_value}\n";
            result += "]\n";

            return result;
        }
    }

    public sealed class GameDataDictionary<TKey, TValue> : GameDataPrimitive
    {
        private Dictionary<TKey, TValue> _dic = new Dictionary<TKey, TValue>();

        public TValue this[TKey key] => _dic[key];

        public void Add(TKey key, TValue value)
        {
            _dic.Add(key, value);
            RegisterChangedPrimitive(this, Action.Type.Add, key, value);
        }

        public bool Remove(TKey key)
        {
            if (_dic.TryGetValue(key, out TValue value))
            {
                RegisterChangedPrimitive(this, Action.Type.Remove, key, value);
            }

            return _dic.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dic.TryGetValue(key, out value);
        }

        public void Clear()
        {
            foreach (var kv in _dic)
            {
                RegisterChangedPrimitive(this, Action.Type.Remove, kv.Key, kv.Value);
            }

            _dic.Clear();
        }

        public Dictionary<TKey, TValue>.KeyCollection Keys => _dic.Keys;

        public Dictionary<TKey, TValue>.ValueCollection Values => _dic.Values;

        public override string ToString()
        {
            string result = $"{GetType().Name} : [\n";

            foreach (var kv in _dic)
            {
                result += $"{kv.Key} : {kv.Value}\n";
            }

            result += "]\n";

            return result;
        }

    }
}
