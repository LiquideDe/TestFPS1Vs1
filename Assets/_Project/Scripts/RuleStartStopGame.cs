using UnityEngine;
using UnityEngine.UI;
using System;

namespace FPS
{
    public class RuleStartStopGame : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart, _buttonStop;
        public event Action StartGame, StopGame;

        private void OnEnable()
        {
            _buttonStart.onClick.AddListener(StartPressed);
            _buttonStop.onClick.AddListener(StopPressed);
        }

        private void OnDisable()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonStop.onClick.RemoveAllListeners();
        }

        public void ShowStartButton()
        {
            _buttonStart.gameObject.SetActive(true);
            _buttonStop.gameObject.SetActive(false);
        }

        public void ShowStopButton() 
        {
            _buttonStart.gameObject.SetActive(false);
            _buttonStop.gameObject.SetActive(true);
        }

        private void StopPressed() => StopGame?.Invoke();

        private void StartPressed() => StartGame?.Invoke();
    }
}


