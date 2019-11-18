using System;
using System.Collections.Generic;

namespace SuspectProject.ObservableState
{
    public class StatePrimitive<T>
    {
        public StatePrimitive()
        {
            _value = default;
        }

        public StatePrimitive(T value)
        {
            _value = value;
        }

        public event Action<StatePrimitive<T>> onValueChanged;

        private T _value = default;
        public T value
        {
            get => _value;
            set
            {
                if (!value.Equals(_value))
                {
                    _value = value;
                    onValueChanged.Invoke(this);
                }
            }
        }

    }
}

