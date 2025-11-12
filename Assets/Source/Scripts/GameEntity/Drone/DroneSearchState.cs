using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Sander.DroneBattle
{
    public class DroneSearchState : IDroneState
    {
        private readonly IDroneController _drone;
        private readonly IResourceBase _resourceBase;
        private Resource _targetResource;

        public DroneSearchState(IDroneController drone, IResourceBase resourceBase)
        {
            _drone = drone;
            _resourceBase = resourceBase;
        }

        public void OnEnter()
        {
            SetNearestResourceAsTarget();

            _resourceBase.OnNewResourceAdded += CheckNewResource;  
        }

        public void OnUpdate()
        {
            if (_drone.TargetResource == null)
            {
                SetNearestResourceAsTarget();
                return;
            }

            float distance = CheckDistance(_drone.TargetResource.transform.position);
            if (distance <= _drone.ArriveDistance)
                _drone.ChangeState<DroneCollectState>();
        }

        public void OnExit()
        {
            _drone.StopMove();
            _resourceBase.OnNewResourceAdded -= CheckNewResource;
            if (_targetResource != null)
            {
                _targetResource.OnResourceCollected -= HandleTargetResourceCollected;
            }
        }

        private float CheckDistance(Vector2 target) =>
            Vector2.Distance(_drone.Position, target);

        private void CheckNewResource(Resource resource)
        {
            if (_drone.TargetResource == null)
            {
                SetNewTarget(resource);
                return;
            }

            float distanceToCurrResource = CheckDistance(_drone.TargetResource.transform.position);
            float distanceToNewResource = CheckDistance(resource.transform.position);
            if (distanceToNewResource < distanceToCurrResource)
            {
                SetNewTarget(resource);
            }
        }

        private void SetNewTarget(Resource resource)
        {
            if(_targetResource != null)
            {
                _targetResource.OnResourceCollected -= HandleTargetResourceCollected;
            }
            _targetResource = resource;

            _drone.SetTargetResource(_targetResource);
            _drone.MoveTo(resource.transform.position);
            resource.OnResourceCollected += HandleTargetResourceCollected;
        }

        private void HandleTargetResourceCollected(Resource resource)
        {
            if (_targetResource != null)
            {
                _targetResource.OnResourceCollected -= HandleTargetResourceCollected;
            }
            _drone.SetTargetResource(null);
            _targetResource = null;
            SetNearestResourceAsTarget();
        }

        private void SetNearestResourceAsTarget()
        {
            Resource nearestResource = _resourceBase.Resources
                .OrderBy(r => CheckDistance(r.transform.position))
                .FirstOrDefault();

            if (nearestResource != null)
            {
                SetNewTarget(nearestResource);
            }
        }
    }
}
