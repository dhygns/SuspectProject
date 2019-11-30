using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuspectProject.Data
{
    public partial class Game
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
                if (_readyToAction)
                {
                    if (!_value.Equals(value))
                    {
                        RegisterChangedPrimitive(this, Action.Type.Set);
                        _value = value;
                    }
                }
                else
                {
                    Debug.LogError($"[Error] If you want to change any value of {GetType()}, please use class, inheriting GameActionBase");
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

        public abstract class GameDataEnumerable : GameDataPrimitive
        {
        }


        public sealed class GameDataDictionary<TKey, TValue> : GameDataEnumerable, IEnumerable<KeyValuePair<TKey, TValue>>
        {
            private Dictionary<TKey, TValue> _value = new Dictionary<TKey, TValue>();

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
