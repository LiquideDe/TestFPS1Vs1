using UnityEngine;
using Zenject;

namespace FPS
{
    public class AudioManagerInstaller : MonoInstaller
    {
        [SerializeField] private AudioManager _audioManagerPrefab;

        public override void InstallBindings()
        {
            AudioManager audioManager = Container.InstantiatePrefabForComponent<AudioManager>(_audioManagerPrefab);
            Container.BindInterfacesAndSelfTo<AudioManager>().FromInstance(audioManager).AsSingle();
        }
    }
}


