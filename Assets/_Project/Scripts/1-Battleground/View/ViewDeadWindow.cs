using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

namespace FPS
{
    public class ViewDeadWindow : ViewCanDestroy
    {
        [SerializeField] private InterstitialAd _interstitialAd;
        [SerializeField] private RewardAd _rewardAd;
        [SerializeField] private TextMeshProUGUI _textWinLoose;
        [SerializeField] private CanvasGroup _canvasGroup;
        public event Action GetReward;
        public event Action AdClosed;

        private IMenuSounds _menuSounds;

        private void Start()
        {
            _interstitialAd.LoadAd();
            _rewardAd.LoadAd();

            Subscribe();
        }

        public void Initialize(string textWinLoose, IMenuSounds menuSounds)
        {
            _textWinLoose.text = textWinLoose;
            _menuSounds = menuSounds;
            menuSounds.PlayPopUp();
            Sequence animation = DOTween.Sequence();

            animation.Append(_canvasGroup.DOFade(1, 0.5f).From(0)).
                Append(_interstitialAd.transform.DOScale(1, 0.5f).From(0)).
                Join(_rewardAd.transform.DOScale(1, 0.5f).From(0));

        }

        private void Subscribe()
        {
            _interstitialAd.AdsFinished += CloseAd;
            _rewardAd.AdsFinished += Reward;
        }

        private void Unscribe()
        {
            _interstitialAd.AdsFinished -= CloseAd;
            _rewardAd.AdsFinished -= Reward;
        }

        private void Reward()
        {
            GetReward?.Invoke();
            Unscribe();
            DestroyView();
        }

        private void CloseAd()
        {
            AdClosed?.Invoke();
            Unscribe();
            DestroyView();
        }

    }
}


