using UnityEngine;

namespace Sander.DroneBattle
{
    public class DroneCollectState : IDroneState
    {
        private readonly IDroneController _drone;
        private readonly float _collectTime;

        private float _timeRemaining;

        public DroneCollectState(IDroneController drone, float collectTime)
        {
            _drone = drone;
            _collectTime = collectTime;
        }

        public void OnEnter()
        {
            _drone.StopMove();
            _timeRemaining = _collectTime;
            _drone.TargetResource.OnResourceCollected += HandleResourceCollected;
        }

        public void OnUpdate()
        {
            _timeRemaining -= Time.deltaTime;
            if (_timeRemaining <= 0)
            {
                CollectResource();
            }
        }

        public void OnExit()
        {
            _drone.TargetResource.OnResourceCollected -= HandleResourceCollected;
        }

        private void CollectResource()
        {
            if (_drone.TargetResource != null && _drone.TargetResource.TryCollect())
            {
                _drone.CollectResource();
                _drone.ChangeState<DroneTransportState>();
            }
            else
            {
                _drone.ChangeState<DroneSearchState>();
            }
        }

        private void HandleResourceCollected(Resource resource)
        {
            resource.OnResourceCollected -= HandleResourceCollected;
            _drone.ChangeState<DroneSearchState>();
        }
    }
}
