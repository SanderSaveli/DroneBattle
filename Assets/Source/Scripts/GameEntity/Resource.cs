using System;
using UnityEngine;

namespace Sander.DroneBattle
{
    public class Resource : MonoBehaviour, IHasCollider, IPoolableObject<Resource>
    {
        public Collider2D Collider => _collider;
        public Action<Resource> OnBackToPool { get; set; }

        public Action<Resource> OnResourceCollected { get; set; }

        [SerializeField] private Collider2D _collider;

        private bool _isCollected;

        public bool TryCollect()
        {
            if (!_isCollected)
            {
                _isCollected = true;
                OnResourceCollected?.Invoke(this);
                return true;
            }
            return false;
        }

        public void OnActive()
        {
            _isCollected = false;
        }

        public void RemoveFromField()
        {
            OnBackToPool?.Invoke(this);
        }
    }
}
