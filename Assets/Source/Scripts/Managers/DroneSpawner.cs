using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Sander.DroneBattle
{
    public class DroneSpawner : MonoBehaviour
    {
        public int DroneCount { get; private set; }
        public IReadOnlyList<Drone> Drones;

        [SerializeField] private AbstractFactory<Drone> _droneFactory;
        [SerializeField] private Fabrik _fabrik;
        [SerializeField] private SpriteRenderer _spawnArea;
        [Space]
        [SerializeField] private Sprite _droneSprite;

        private List<Drone> _drones = new List<Drone>();
        private FractionSettings _settings;

        [Inject]
        public void Construct(FractionSettings droneSettings)
        {
            _settings = droneSettings;
        }

        private void OnEnable()
        {
            _settings.OnUpdate += UpdateSettings;
            UpdateSettings();
        }

        private void OnDestroy()
        {
            _settings.OnUpdate -= UpdateSettings;
        }

        private void ChangeDroneCount(int targetCount)
        {
            if (_drones.Count > targetCount)
            {
                RemoveDrones(_drones.Count - targetCount);
            }
            else if (_drones.Count < targetCount)
            {
                AddDrones(targetCount - _drones.Count);
            }

            DroneCount = targetCount;
        }

        private void RemoveDrones(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _drones[0].OnBackToPool(_drones[0]);
                _drones.RemoveAt(0);
            }
        }

        private void AddDrones(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Drone drone = _droneFactory.SpawnInsideArea(_spawnArea.bounds);
                if(drone.TryGetComponent(out SpriteRenderer spriteRenderer))
                {
                    spriteRenderer.sprite = _droneSprite;
                }
                drone.Constuct(_fabrik);
                _drones.Add(drone);
            }
        }

        private void UpdateSettings()
        {
            ChangeDroneCount(_settings.DroneCount);
        }
    }
}
