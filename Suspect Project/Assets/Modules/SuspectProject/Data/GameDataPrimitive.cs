using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuspectProject.Data
{
    public partial class Game
    {
        public abstract class DataPrimitive
        {
            public struct Action
            {
                public enum Type { Init, Add, Remove, Set };

                public DataPrimitive primitive;
                public Type type;
                public object[] parameters;
            }

            public static Queue<Action> ObservedChangedPrimitives { get; private set; } = new Queue<Action>();

            public DataPrimitive()
            {
                RegisterChangedPrimitive(this, Action.Type.Init);
            }

            public void RegisterChangedPrimitive(DataPrimitive primitive, Action.Type type, params object[] parameters)
            {
                ObservedChangedPrimitives.Enqueue(new Action
                {
                    primitive = primitive,
                    type = type,
                    parameters = parameters
                });
            }
        }

        public sealed class DataPrimitive<T> : DataPrimitive
        {
            private T _value = default;
            public T value => _value;

            public DataPrimitive() : base()
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

        public abstract class DataEnumerable : DataPrimitive { }

    }
}
