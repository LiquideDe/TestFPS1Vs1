using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System;
using Zenject;

namespace FPS
{
    public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private string _androidAdUnitId = "Interstitial_Android";
        [SerializeField] private string _iOsAdUnitId = "Interstitial_iOS";
        [SerializeField] private Button _buttonInterstitialAd;

        public event Action AdsFinished;
        private string _adUnitId;
        private IMenuSounds _menuSounds;

        [Inject]
        private void Construct(AudioManager audioManager) => _menuSounds = audioManager;

        private void OnEnable() {
            if (PlayerPrefs.GetInt("removeAds") == 1 || Platform.IsPc)
                _buttonInterstitialAd.onClick.AddListener(PassAdversity);
            else
                _buttonInterstitialAd.onClick.AddListener(ShowAd); 

        }

        private void OnDisable() => _buttonInterstitialAd.onClick?.RemoveAllListeners();

        void Awake()
        {
            _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOsAdUnitId
                : _androidAdUnitId;
        }

        public void LoadAd()
        {
            Debug.Log("Loading Ad: " + _adUnitId);
            Advertisement.Load(_adUnitId, this);
        }

        public void ShowAd()
        {
            _menuSounds.PlayClick();
            Advertisement.Show(_adUnitId, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            // Optionally execute code if the Ad Unit successfully loads content.
            Debug.Log($"LoadedAdS");
        }

        public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
        }

        public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
            // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
        }

        public void OnUnityAdsShowStart(string _adUnitId) { }
        public void OnUnityAdsShowClick(string _adUnitId) { }
        public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) 
        { 
            AdsFinished?.Invoke();
            _menuSounds.PlayClick();
        }

        private void PassAdversity() => AdsFinished?.Invoke();
    }
}


