using System;

namespace Sander.DroneBattle
{
    public interface IPoolableObject<T>
    {
        public Action<T> OnBackToPool { get; set; }
        public void OnActive();
    }
}
