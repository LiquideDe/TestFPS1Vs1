using UnityEngine;
using TMPro;

namespace FPS
{
    public class ViewDeadWindow : MonoBehaviour
    {
        [SerializeField] private InterstitialAd _interstitialAd;
        [SerializeField] private RewardAd _rewardAd;
        [SerializeField] private TextMeshProUGUI _textWinLoose;
        private BattleCube _playerCube;

        private void Start()
        {
            _interstitialAd.LoadAd();
            _rewardAd.LoadAd();

            Subscribe();
        }

        public void Initialize(BattleCube playerCube, string textWinLoose)
        {
            _playerCube = playerCube;
            _textWinLoose.text = textWinLoose;
        }

        private void Subscribe()
        {
            _interstitialAd.AdsFinished += DestroyView;
            _rewardAd.AdsFinished += Reward;
        }

        private void Unscribe()
        {
            _interstitialAd.AdsFinished -= DestroyView;
            _rewardAd.AdsFinished -= Reward;
        }

        private void Reward()
        {
            _playerCube.BoostSpeed();
            DestroyView();
        }

        private void DestroyView()
        {
            Unscribe();
            Destroy(gameObject);
        }
    }
}


