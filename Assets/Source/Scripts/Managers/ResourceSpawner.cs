using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Sander.DroneBattle
{
    public class ResourceSpawner : MonoBehaviour, IResourceBase
    {
        public float SpawnTime { get; private set; }
        public IReadOnlyList<Resource> Resources => _resources;
        public Action<Resource> OnNewResourceAdded { get; set; }

        [SerializeField] ResourceFactory _resourceFactory;

        private List<Resource> _resources = new List<Resource>();

        private float _timeSinseLastSpawn;
        private bool _spawning = true;
        private ResourceSettings _settings;

        [Inject]
        public void Construct(ResourceSettings resourceSettings)
        {
            _settings = resourceSettings;
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
            if (!_spawning)
            {
                return;
            }

            _timeSinseLastSpawn += Time.deltaTime;
            if (_timeSinseLastSpawn > SpawnTime)
            {
                SpawnResource();
                _timeSinseLastSpawn = 0;
            }
        }

        public void Activate()
        {
            _spawning = true;
        }

        public void Stop()
        {
            _spawning = false;
        }

        private void SpawnResource()
        {
            Resource resource = _resourceFactory.Spawn();
            _resources.Add(resource);
            resource.OnResourceCollected += RemoveResource;
            OnNewResourceAdded?.Invoke(resource);
        }

        private void RemoveResource(Resource resource)
        {
            resource.OnResourceCollected -= RemoveResource;
            _resources.Remove(resource);
        }

        private void UpdateSettings()
        {
            SpawnTime = _settings.SpawnTime;
        }
    }
}
