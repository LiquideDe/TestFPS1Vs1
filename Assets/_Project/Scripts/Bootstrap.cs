using UnityEngine;
using Zenject;

namespace FPS
{
    public class Bootstrap : MonoBehaviour
    {
        [Inject]
        private void Construct(Mediator mediator) => mediator.ShowMainMenu();

    }
}


