using UnityEngine;
using UnityEngine.Advertisements;

namespace FPS
{
    public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        [SerializeField] string _androidGameId;
        [SerializeField] string _iOSGameId;
        [SerializeField] bool _testMode = true;
        private string _gameId;

        void Awake()
        {
            InitializeAds();            
        }

        public void InitializeAds()
        {
            #if UNITY_IOS
            _gameId = _iOSGameId;
            #elif UNITY_ANDROID
            _gameId = _androidGameId;
            #elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
            #endif
            if (!Advertisement.isInitialized && Advertisement.isSupported)
            {
                Advertisement.Initialize(_gameId, _testMode, this);
            }
            else
            {
                Platform.IsPc = true;
            }
        }


        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.LogError($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}

