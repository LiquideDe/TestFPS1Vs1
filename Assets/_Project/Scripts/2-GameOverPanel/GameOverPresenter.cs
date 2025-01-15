using UnityEngine;
using System;

namespace FPS
{
    public class GameOverPresenter
    {
        public event Action PlayAgain;
        public event Action ExitMainMenu;

        private GameOverView _view;
        private IMenuSounds _menuSounds;

        public GameOverPresenter(GameOverView view, string text, IMenuSounds menuSounds, bool isPlayerWin)
        {
            _view = view;
            _menuSounds = menuSounds;
            view.SetTextGameOver(text);
            if (isPlayerWin) view.Show(menuSounds.PlayWinGame);
            else view.Show(menuSounds.PlayLooseGame);
            _menuSounds.PlayPopUp();
            Subscribe();            
        }

        private void Subscribe()
        {
            _view.PlayAgain += PlayAgainPressed;
            _view.ExitMainmenu += ExitMainMenuPressed;
            _view.Exit += Exit;
        }

        private void Unscribe()
        {
            _view.PlayAgain -= PlayAgainPressed;
            _view.ExitMainmenu -= ExitMainMenuPressed;
            _view.Exit -= Exit;
        }

        private void PlayAgainPressed()
        {
            _menuSounds.PlayClick();
            PlayAgain?.Invoke();
            ClosePanel();
        }

        private void ExitMainMenuPressed()
        {
            _menuSounds.PlayClick();
            ExitMainMenu?.Invoke();
            ClosePanel();
        }

        private void ClosePanel()
        {
            _menuSounds.PlayCancel();
            Unscribe();
            _view.DestroyView();
        }

        private void Exit()
        {
            _menuSounds.PlayClick();
            Application.Quit(); //¬озможно нельз€ так резко закрывать приложение? 
        }
    }
}


