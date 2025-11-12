using System;
using UnityEngine;

namespace Sander.DroneBattle
{
    public abstract class AbstractSettings : MonoBehaviour
    {
        public Action OnUpdate;

        protected void SetValue<T>(ref T field, T value)
        {
            if (!Equals(field, value))
            {
                field = value;
                OnUpdate?.Invoke();
            }
        }

        private void OnValidate()
        {
            OnUpdate?.Invoke();
        }
    }
}
