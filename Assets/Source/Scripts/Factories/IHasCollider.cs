using UnityEngine;

namespace Sander.DroneBattle
{
    public interface IHasCollider
    {
        public Collider2D Collider { get; }
    }
}
