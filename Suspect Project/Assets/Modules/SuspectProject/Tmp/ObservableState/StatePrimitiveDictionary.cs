using System;
using System.Collections.Generic;

namespace SuspectProject.ObservableState
{
    public class StatePrimitiveDictionary<TKey, TValue> where TValue : new()
    {
        public enum ActionType { Added, Removed, Updated }

        private Dictionary<TKey, StatePrimitive<TValue>> _dic = new Dictionary<TKey, StatePrimitive<TValue>>();

        public TValue this[TKey key] => _dic[key].value;

        public event Action<StatePrimitive<TValue>, ActionType> onValueChanged;

        public void Add(TKey key, StatePrimitive<TValue> value)
        {
            _dic.Add(key, value);
            value.onValueChanged += OnElementValueChanged;
            onValueChanged?.Invoke(value, ActionType.Added);
        }

        public void Add(TKey key, TValue value)
        {
            Add(key, new StatePrimitive<TValue>(value));
        }

        public TValue AddNew(TKey key)
        {
            TValue item = new TValue();
            Add(key, item);
            return item;
        }

        public bool Remove(TKey key)
        {
            if (_dic.TryGetValue(key, out StatePrimitive<TValue> value))
            {
                value.onValueChanged -= OnElementValueChanged;
                onValueChanged?.Invoke(value, ActionType.Removed);
                return _dic.Remove(key);
            }

            return false;
        }

        private void OnElementValueChanged(StatePrimitive<TValue> element)
        {
            onValueChanged?.Invoke(element, ActionType.Updated);
        }
    }
}

