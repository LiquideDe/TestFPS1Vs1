using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace FPS
{
    public class BattlegroundView : ViewCanDestroy
    {
        [SerializeField] private TextMeshProUGUI _textCounts; 
        [SerializeField] private Button _buttonStart, _buttonStop, _buttonExit;
        public event Action StartGame, StopGame, Exit;

        private void OnEnable()
        {
            _buttonStart.onClick.AddListener(StartPressed);
            _buttonStop.onClick.AddListener(StopPressed);
            _buttonExit.onClick.AddListener(ExitPressed);
        }

        private void OnDisable()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonStop.onClick.RemoveAllListeners();
            _buttonExit.onClick.RemoveAllListeners();
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

        public void HideAllButton()
        {
            _buttonStart.gameObject.SetActive(false);
            _buttonStop.gameObject.SetActive(false);
        }

        private void StopPressed() => StopGame?.Invoke();

        private void StartPressed() => StartGame?.Invoke();

        private void ExitPressed() => Exit?.Invoke();
        public void SetCounts(string text) => _textCounts.text = text;
    }
}

