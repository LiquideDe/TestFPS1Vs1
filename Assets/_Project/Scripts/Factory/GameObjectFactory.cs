using UnityEngine;
using Zenject;

namespace FPS
{
    public class GameObjectFactory
    {
        private PrefabHolder _prefabHolder;
        private DiContainer _diContaner;

        public GameObjectFactory(PrefabHolder prefabHolder, DiContainer diContainer)
        {
            _prefabHolder = prefabHolder;
            _diContaner = diContainer;
        }

        public GameObject Get(TypeScene typeScene)
        {
            GameObject instance = _diContaner.InstantiatePrefab(_prefabHolder.Get(typeScene));
            return instance;
        }
    }
}


