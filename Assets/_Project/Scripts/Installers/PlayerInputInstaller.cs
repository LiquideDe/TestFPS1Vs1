using UnityEngine;
using Zenject;

namespace FPS
{
    public class PlayerInputInstaller : MonoInstaller
    {
        [SerializeField] private InputFromKeyboard _inputFromKeyboard;
        public override void InstallBindings()
        {
            InputFromKeyboard input = Container.InstantiatePrefabForComponent<InputFromKeyboard>(_inputFromKeyboard);
            Container.BindInterfacesAndSelfTo<InputFromKeyboard>().FromInstance(input).AsSingle();
        }
    }
}


