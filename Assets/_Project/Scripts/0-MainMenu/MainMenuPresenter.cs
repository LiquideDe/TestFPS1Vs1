using UnityEngine;
using System;

namespace FPS
{
    public class MainMenuPresenter 
    {
        public event Action StartGame;
        public event Action ShowShop;
        private MainMenuView _view;
        private IMenuSounds _menuSounds;

        public MainMenuPresenter(MainMenuView view, IMenuSounds sounds)
        {
            _view = view;
            _menuSounds = sounds;
            Subscribe();
            ShowBestResults();
        }        

        private void Subscribe()
        {
            _view.Exit += ExitPressed;
            _view.ShowShop += ShowShopPressed;
            _view.StartGame += StartGamePressed;
        }

        private void Unscribe()
        {
            _view.Exit -= ExitPressed;
            _view.ShowShop -= ShowShopPressed;
            _view.StartGame -= StartGamePressed;
        }        

        private void StartGamePressed()
        {
            _menuSounds.PlayClick();
            StartGame?.Invoke();
            UnscribeAndDestroy();
        }

        private void ShowShopPressed()
        {
            _menuSounds.PlayClick();
            ShowShop?.Invoke();
            UnscribeAndDestroy();
        }

        private void ExitPressed()
        {
            _menuSounds.PlayClick();
            Application.Quit();
        }

        private void UnscribeAndDestroy()
        {
            Unscribe();
            _view.DestroyView();
        }

        private void ShowBestResults()
        {
            TimeResultHolder resultHolder = new TimeResultHolder();
            float[] bestResults = new float[3] {resultHolder.FirstPlace, resultHolder.SecondPlace, resultHolder.ThirdPlace };
            _view.ShowBestGames(bestResults);                
        }
    }
}


