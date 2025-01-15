using UnityEngine;
using Zenject;

namespace FPS
{
    public class BattlegroundInstaller : MonoInstaller
    {
        [SerializeField] private Battleground battlegroundPrefab;
        public override void InstallBindings()
        {
            Battleground battleground = Container.InstantiatePrefabForComponent<Battleground>(battlegroundPrefab);
            Container.BindInterfacesAndSelfTo<Battleground>().FromInstance(battleground).AsSingle();
        }
    }
}

