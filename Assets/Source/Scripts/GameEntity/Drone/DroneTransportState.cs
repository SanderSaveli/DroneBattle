using UnityEngine;

namespace Sander.DroneBattle
{
    public class DroneTransportState : IDroneState
    {
        private readonly IDroneController _drone;
        private readonly Fabrik _fabrick;

        public DroneTransportState(IDroneController drone, Fabrik fabrick)
        {
            _drone = drone;
            _fabrick = fabrick;
        }

        public void OnEnter()
        {
            _drone.MoveTo(_fabrick.transform.position);
        }

        public void OnUpdate()
        {
            float distance = Vector2.Distance(_drone.Position, _fabrick.transform.position);
            if (distance <= _drone.ArriveDistance)
            {
                _drone.AddResourceToFabrick();
                _drone.ChangeState<DroneSearchState>();
            }
        }

        public void OnExit() { }
    }
}
