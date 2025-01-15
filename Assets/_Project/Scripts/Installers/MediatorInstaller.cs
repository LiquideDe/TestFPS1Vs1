using Zenject;


namespace FPS
{
    public class MediatorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Mediator>().AsSingle();
        }
    }
}

