using UnityEngine;
using UnityEngine.AI;

namespace Sander.DroneBattle
{
    public interface IDroneController
    {
        public Vector3 Position { get;}
        public Resource TargetResource { get; }
        public bool HasResource { get;}
        public float ArriveDistance { get; }

        public void ChangeState<T>() where T : IDroneState;
        public void AddResourceToFabrick();
        public void CollectResource();
        public void SetTargetResource(Resource resource);

        public void MoveTo(Vector3 position);
        public void StopMove();
    }
}
