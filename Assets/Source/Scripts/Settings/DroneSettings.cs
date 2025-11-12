using UnityEngine;

namespace Sander.DroneBattle
{
    public class DroneSettings : AbstractSettings
    {
        public float Speed
        {
            get => _speed;
            set => SetValue(ref _speed, value);
        }

        public bool IsDrawTrace
        {
            get => _isDrawTrace;
            set => SetValue(ref _isDrawTrace, value);
        }

        [SerializeField] private float _speed = 5;
        [SerializeField] private bool _isDrawTrace = true;
    }
}
