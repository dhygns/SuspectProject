using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuspectProject.Data
{
    public partial class Game
    {
        public abstract class DataDictionary : DataEnumerable { }

        public sealed class DataDictionary<TKey, TValue> : DataDictionary, IEnumerable<KeyValuePair<TKey, TValue>>
        {
            private Dictionary<TKey, TValue> _value = new Dictionary<TKey, TValue>();

            public int Count => _value.Count;

            public TValue this[TKey key] => _value[key];

            public void Add(TKey key, TValue value)
            {
                if (_readyToAction)
                {
                    _value.Add(key, value);
                    RegisterChangedPrimitive(this, Action.Type.Add, key, value);
                }
                else
                {
                    Debug.LogError($"[Error] If you want to change any value of {GetType()}, please use class, inheriting GameActionBase");
                }
            }

            public bool Remove(TKey key)
            {
                if (_readyToAction)
                {
                    if (_value.TryGetValue(key, out TValue value))
                    {
                        RegisterChangedPrimitive(this, Action.Type.Remove, key, value);
                    }

                    return _value.Remove(key);
                }
                else
                {
                    Debug.LogError($"[Error] If you want to change any value of {GetType()}, please use class, inheriting GameActionBase");

                    return false;
                }
            }

            public bool TryGetValue(TKey key, out TValue value)
            {
                return _value.TryGetValue(key, out value);
            }

            public void Clear()
            {
                if (_readyToAction)
                {
                    foreach (var kv in _value)
                    {
                        RegisterChangedPrimitive(this, Action.Type.Remove, kv.Key, kv.Value);
                    }

                    _value.Clear();
                }
                else
                {
                    Debug.LogError($"[Error] If you want to change any value of {GetType()}, please use class, inheriting GameActionBase");
                }
            }

            public Dictionary<TKey, TValue>.KeyCollection Keys => _value.Keys;

            public Dictionary<TKey, TValue>.ValueCollection Values => _value.Values;

            public override string ToString()
            {
                string result = $"{GetType().Name} : [\n";

                foreach (var kv in _value)
                {
                    result += $"{kv.Key} : {kv.Value}\n";
                }

                result += "]\n";

                return result;
            }

            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            {
                return _value.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _value.GetEnumerator();
            }

        }
    }
}