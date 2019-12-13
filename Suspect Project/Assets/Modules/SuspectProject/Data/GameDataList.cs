using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuspectProject.Data
{
    public partial class Game
    {
        public abstract class DataList : DataEnumerable { }

        public sealed class DataList<T> : DataList, IEnumerable<T>
        {
            private List<T> _value = new List<T>();

            public int Count => _value.Count;

            public T this[int key] => _value[key];

            public void Add(T value)
            {
                if (_readyToAction)
                {
                    _value.Add(value);
                    RegisterChangedPrimitive(this, Action.Type.Add, value);
                }
                else
                {
                    Debug.LogError($"[Error] If you want to change any value of {GetType()}, please use class, inheriting GameActionBase");
                }
            }

            public bool Remove(T value)
            {
                if (_readyToAction)
                {
                    if (_value.Contains(value))
                    {
                        RegisterChangedPrimitive(this, Action.Type.Remove, value);
                    }

                    return _value.Remove(value);
                }
                else
                {
                    Debug.LogError($"[Error] If you want to change any value of {GetType()}, please use class, inheriting GameActionBase");

                    return false;
                }
            }
            public void Clear()
            {
                if (_readyToAction)
                {
                    foreach (var item in _value)
                    {
                        RegisterChangedPrimitive(this, Action.Type.Remove, item);
                    }

                    _value.Clear();
                }
                else
                {
                    Debug.LogError($"[Error] If you want to change any value of {GetType()}, please use class, inheriting GameActionBase");
                }
            }

            public IEnumerator<T> GetEnumerator()
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