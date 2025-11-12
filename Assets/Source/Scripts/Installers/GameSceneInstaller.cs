using UnityEngine;
using Zenject;

namespace Sander.DroneBattle
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private DroneSettings _droneSettings;
        [SerializeField] private FractionSettings _fractionSettings;
        [SerializeField] private ResourceSettings _resourceSettings;
        [SerializeField] private ResourceSpawner _resourceSpawner;

        public override void InstallBindings()
        {
            Container.Bind<DroneSettings>().FromInstance(_droneSettings).AsSingle().NonLazy();
            Container.Bind<FractionSettings>().FromInstance(_fractionSettings).AsSingle().NonLazy();
            Container.Bind<ResourceSettings>().FromInstance(_resourceSettings).AsSingle().NonLazy();
            Container.Bind<IResourceBase>().FromInstance(_resourceSpawner).AsSingle().NonLazy();
        }
    }
}
