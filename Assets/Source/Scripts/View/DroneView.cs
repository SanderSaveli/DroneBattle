using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Sander.DroneBattle
{
    public class DroneView : MonoBehaviour
    {
        [SerializeField] private float _rotationOffset = -90f;
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private TrailRenderer _trailRenderer;
        [SerializeField] private NavMeshAgent _agent;

        private DroneSettings _settings;

        [Inject]
        public void Construct(DroneSettings settins)
        {
            _settings = settins;
        }

        private void OnEnable()
        {
            _settings.OnUpdate += UpdateSettings;
            UpdateSettings();
        }

        private void OnDisable()
        {
            _settings.OnUpdate -= UpdateSettings;
        }

        private void Update()
        {
            RotateToMoveDirection();
        }

        private void RotateToMoveDirection()
        {
            var velocity = _agent.velocity;
            if (velocity.sqrMagnitude < 0.001f)
                return;

            float targetAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg + _rotationOffset;
            float currentAngle = transform.eulerAngles.z;
            float newAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.deltaTime * _rotationSpeed);

            transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
        }

        private void UpdateSettings()
        {
            _trailRenderer.enabled = _settings.IsDrawTrace;
        }
    }
}
