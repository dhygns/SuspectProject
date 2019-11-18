using System;
using System.Collections.Generic;

namespace SuspectProject.ObservableState
{
    public class StatePrimitiveList<T>
    {
        public enum ActionType { Added, Removed, Updated }
        public T this[int i] => _list[i].value;

        public event Action<StatePrimitive<T>, ActionType> onValueChanged;
        private List<StatePrimitive<T>> _list = new List<StatePrimitive<T>>();

        public void Add(StatePrimitive<T> item)
        {
            _list.Add(item);
            item.onValueChanged += OnElementValueChanged;
            onValueChanged?.Invoke(item, ActionType.Added);
        }

        public void Add(T item)
        {
            Add(new StatePrimitive<T>(item));
        }

        public bool Remove(StatePrimitive<T> item)
        {
            if (_list.Remove(item))
            {
                item.onValueChanged -= OnElementValueChanged;
                onValueChanged?.Invoke(item, ActionType.Removed);
                return true;
            }
            return false;
        }

        public bool Remove(T item)
        {
            return Remove(_list.Find(obj => obj.value.Equals(item)));
        }

        private void OnElementValueChanged(StatePrimitive<T> element)
        {
            onValueChanged?.Invoke(element, ActionType.Updated);
        }
    }
}

