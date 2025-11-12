using UnityEngine;

namespace Sander.DroneBattle
{
    public class FractionSettings : AbstractSettings
    {
        public int DroneCount
        {
            get => _droneCount;
            set => SetValue(ref _droneCount, value);
        }

        [SerializeField] private int _droneCount = 3;
    }
}
