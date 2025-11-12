using UnityEngine;

namespace Sander.DroneBattle
{
    public class ResourceSettings : AbstractSettings
    {
        public float SpawnTime
        {
            get => _spawnTime;
            set => SetValue(ref _spawnTime, value);
        }

        [SerializeField] private float _spawnTime = 5;

    }
}
