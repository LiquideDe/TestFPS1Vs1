using UnityEngine;
using Zenject;

namespace FPS
{
    public class FactoryInstaller : MonoInstaller
    {
        [SerializeField] private PlayerCube _playerCubePrefab;
        [SerializeField] private AICube _aICubePrefab;
        [SerializeField] private PrefabHolder _prefabHolder;

        public override void InstallBindings()
        {
            Container.Bind<PlayerCube>().FromInstance(_playerCubePrefab).AsSingle();
            Container.Bind<AICube>().FromInstance(_aICubePrefab).AsSingle();
            Container.Bind<PrefabHolder>().FromInstance(_prefabHolder).AsSingle();
            BindFactories();
        }

        private void BindFactories()
        {
            Container.Bind<GameObjectFactory>().AsSingle();
            Container.Bind<BattleCubeFactory>().AsSingle();
        }
    }
}


