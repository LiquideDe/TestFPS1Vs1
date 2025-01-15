using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace FPS
{
    public class MainMenuView : ViewCanDestroy
    {
        [SerializeField] private TextMeshProUGUI _textBestResults;
        [SerializeField] private Button _buttonStartGame, _buttonShowShop, _buttonExit;

        public event Action StartGame;
        public event Action ShowShop;
        public event Action Exit;

        private void OnEnable()
        {
            _buttonStartGame.onClick.AddListener(StartGamePressed);
            _buttonShowShop.onClick.AddListener(ShowShopPressed);
            _buttonExit.onClick.AddListener(ExitPressed);
        }

        private void OnDisable()
        {
            _buttonStartGame.onClick.RemoveAllListeners();
            _buttonShowShop.onClick.RemoveAllListeners();
            _buttonExit.onClick.RemoveAllListeners();
        }

        public void ShowBestGames(float[] bestGames)
        {
            _textBestResults.text = $"Лучшее время: \n";

            for (int i = 0; i < bestGames.Length; i++)
            {
                _textBestResults.text += $"{i + 1} - {ConvertSecondToSecondMinute(bestGames[i])} \n";
            }
        }

        private string ConvertSecondToSecondMinute(float time)
        {
            string text;
            
            int miliSec = (int)(time * 1000);
            int minutes = miliSec / 60000;
            miliSec -= minutes * 60000;
            int seconds = miliSec / 1000;
            miliSec -= seconds * 1000;

            return text = $"{minutes}:{seconds}:{miliSec}";

        }

        private void StartGamePressed()
        {
            StartGame?.Invoke();
        }

        private void ShowShopPressed()
        {
            ShowShop?.Invoke();
        }

        private void ExitPressed()
        {
            Exit?.Invoke();
        }
    }
}

