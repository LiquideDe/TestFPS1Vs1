using Firebase.Extensions;
using Firebase.RemoteConfig;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;


namespace FPS
{
    public enum BattleCubeTypes
    {
        Player, Ai
    }

    public class BattleCubeFactory 
    {
        private DiContainer _diContaner;
        private PlayerCube _playerCubePrefab;
        private AICube _aiCubePrefab;
        private Battleground _battleground;
        private ConfigData _configData;
        private OrientPoint _player;

        public BattleCubeFactory(DiContainer diContaner, PlayerCube playerCubePrefab, AICube aiCubePrefab, Battleground battleground)
        {
            _diContaner = diContaner;
            _playerCubePrefab = playerCubePrefab;
            _aiCubePrefab = aiCubePrefab;
            _battleground = battleground;
            FetchDataAsync();
        }

        public GameObject Get(BattleCubeTypes type)
        {
            switch (type)
            {
                case BattleCubeTypes.Player:
                    return GetPlayer();

                case BattleCubeTypes.Ai:
                    return GetAi();

                default:
                    throw new ArgumentException($"Неверный тип {type}");
            }
        }

        private GameObject GetPlayer()
        {
            if(_player == null)
            {
                GameObject playerGameObject = _diContaner.InstantiatePrefab(_playerCubePrefab);
                PlayerCube playerCube = playerGameObject.GetComponent<PlayerCube>();
                playerCube.Initialize(_battleground._playerStartPosition.position, _configData);
                _player = playerGameObject.GetComponent<OrientPoint>();
                return playerGameObject;
            }
            else
            {
                return _player.gameObject;
            }
        }

        private GameObject GetAi()
        {
            if (_player == null)
            {
                Debug.LogError($"Сначал надо создать игрока");
                return null;
            }
                
            GameObject aiGameObject = _diContaner.InstantiatePrefab(_aiCubePrefab);
            AICube aICube = aiGameObject.GetComponent<AICube>();
            aICube.Initialize(_battleground._orientPoints,_player,_battleground._aiStartPostition.position, _configData );

            return aiGameObject;
        }

        private Task FetchDataAsync()
        {
            Debug.Log("Fetching data...");
            System.Threading.Tasks.Task fetchTask =
            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
                TimeSpan.FromHours(12)); //Я так понял, тут надо ставить 12 часов или больше. Вопрос, он сохраняется где то у себя json,
                                //что можно будет обращаться только раз в сутки?
            return fetchTask.ContinueWithOnMainThread(FetchComplete);
        }

        private void FetchComplete(Task fetchTask)
        {
            if (!fetchTask.IsCompleted)
            {
                Debug.LogError("Retrieval hasn't finished.");
                return;
            }

            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var info = remoteConfig.Info;
            if (info.LastFetchStatus != LastFetchStatus.Success)
            {
                Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
                return;
            }

            // Fetch successful. Parameter values must be activated to use.
            remoteConfig.ActivateAsync()
              .ContinueWithOnMainThread(
                task => {
                    string configInText = remoteConfig.GetValue("ConfigFPSTest").StringValue;
                    _configData = JsonUtility.FromJson<ConfigData>(configInText);
                });
        }
    }
}

