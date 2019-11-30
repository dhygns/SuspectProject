using System;
using UnityEngine;

namespace SuspectProject.Data
{
    public abstract class GameDataBase
    {
        public GameDataBase()
        {
            foreach(var propertyInfo in GetType().GetProperties())
            {
                propertyInfo.SetValue(this, Activator.CreateInstance(propertyInfo.PropertyType));
            }
        }

        public override string ToString()
        {
            string result = $"{GetType().Name} : [\n";

            foreach (var property in GetType().GetProperties())
            {
                var value = property.GetValue(this);
                result += $"{property.Name} : {value?.ToString()}\n";
            }

            result += "]\n";

            return result;
        }
    }
}