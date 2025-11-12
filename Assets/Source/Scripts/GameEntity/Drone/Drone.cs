using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Sander.DroneBattle
{
    public class Drone : MonoBehaviour, IHasCollider, IPoolableObject<Drone>, IDroneController
    {
        public bool HasResource { get; private set; }
        public Resource TargetResource { get; private set; }
        public float ArriveDistance => _arriveDistance;
        public Collider2D Collider => _collider;
        public Action<Drone> OnBackToPool { get; set; }
        public Action<Type> OnChangeState { get; set; }
        public Vector3 Position => transform.position;

        [SerializeField] private float _collectTime = 2f;
        [SerializeField] private float _arriveDistance = 0.2f;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Collider2D _collider;

        private Fabrik _fabrick;
        private IResourceBase _resourceSpawner;
        private DroneSettings _settings;

        private FSM<IDroneState> _fsm;

        [Inject]
        public void Construct(DroneSettings droneSettings, IResourceBase resourceSpawner)
        {
            _settings = droneSettings;
            _resourceSpawner = resourceSpawner;
        }

        private void Start()
        {
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void OnEnable()
        {
            _settings.OnUpdate += SetSettings;
            SetSettings();
        }

        private void OnDisable()
        {
            _settings.OnUpdate -= SetSettings;
            if(_fsm != null)
            {
                _fsm.ActiveState.OnExit();
            }
        }

        private void Update()
        {
            _fsm.Update();
        }

        public void Constuct(Fabrik fabrick)
        {
            _fabrick = fabrick;
            IniFSM();
        }

        public void ChangeState<T>() where T : IDroneState
        {
            _fsm.ChangeState<T>();
        }

        public void AddResourceToFabrick()
        {
            if (HasResource)
            {
                _fabrick.AddResource();
                HasResource = false;
            }
        }

        public void CollectResource()
        {
            HasResource = true;
            TargetResource = null;
        }

        public void SetTargetResource(Resource resource)
        {
            if (!HasResource)
            {
                TargetResource = resource;
            }
            else
            {
                Debug.LogWarning("Resource Already Collected");
            }
        }

        public void OnActive()
        {
            HasResource = false;
            IniFSM();
        }

        private void IniFSM()
        {
            if(_fsm != null) { _fsm.ActiveState.OnExit(); }

            _fsm = new FSM<IDroneState>();
            _fsm.OnActiveStateChanged += HandleFSMStateChange;

            _fsm.AddOrUpdateState(new DroneSearchState(this, _resourceSpawner));
            _fsm.AddOrUpdateState(new DroneCollectState(this, _collectTime));
            _fsm.AddOrUpdateState(new DroneTransportState(this, _fabrick));

            _fsm.ChangeState<DroneSearchState>();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _arriveDistance);
        }

        private void HandleFSMStateChange(Type type)
        {
            OnChangeState?.Invoke(type);
        }

        private void SetSettings()
        {
            _agent.speed = _settings.Speed;
        }

        public void MoveTo(Vector3 position)
        {
            _agent.SetDestination(position);
        }

        public void StopMove()
        {
            _agent.ResetPath();
        }
    }
}
