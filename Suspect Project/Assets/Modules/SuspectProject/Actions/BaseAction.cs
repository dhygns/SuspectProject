using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SuspectProject.Client
{
    public abstract class BaseAction
    {
        private string _actionName { get; set; }

        public BaseAction(string actionName = "")
        {
            _actionName = string.IsNullOrWhiteSpace(actionName) ? GetType().ToString() : actionName;
        }

        public abstract void Execute(ClientState clientState);
    }
}

