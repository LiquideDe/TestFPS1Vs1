using System;
using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class BattlegroundPresenter 
    {
        public event Action<string> RoundEnded;
        public event Action GameIsStarted;
        public event Action<string, bool> GameEnded;
        public event Action ReturnToMainmenu;
        private BattleCube[] _battleCubes;
        private BattlegroundView _view;
        private ViewLog _viewLog;
        private PlayerController _playerController;
        private List<Bullet> _bullets = new List<Bullet>();
        private TimeCounter _timeCounter;
        private int _aiWin = 0, _playerWin = 0;
        private bool _isBattleStart = false;
        private List<float> _winingTimes = new List<float>();
        private int _roundToWin = 3;
        private IBattlegroundSounds _battlegroundSounds;

        public BattlegroundPresenter(BattlegroundView view,ViewLog viewLog, BattleCube[] battleCubes, 
            InputFromKeyboard inputFromKeyboard, TimeCounter timeCounter, IBattlegroundSounds battlegroundSounds)
        {
            _view = view;
            _battleCubes = battleCubes;
            _viewLog = viewLog;
            _playerController = new PlayerController(_battleCubes[0], inputFromKeyboard);
            _timeCounter = timeCounter;
            _battlegroundSounds = battlegroundSounds;
            Subscribe();            
        }

        public void RewardPlayer()
        {
            _battleCubes[0].BoostSpeed();
            ShowStartButton();
        }

        public void ShowStartButton() => _view.ShowStartButton();

        public void PlayAgain()
        {
            _view.ShowStartButton();
            _aiWin = 0;
            _playerWin = 0;
            _timeCounter.ResetTimer();
            _view.SetCounts($"Игрок {_playerWin}:{_aiWin} Компьютер");
        }

        public void StopPlaying()
        {
            Unscribe();
        }

        private void Subscribe()
        {
            _view.StartGame += StartGame;
            _view.StopGame += StopGame;
            _view.Exit += ExitToMainMenu;

            _playerController.SomeTextAction += AddTextAction;

            foreach (BattleCube battleCube in _battleCubes)
            {
                battleCube.BulletInAir += AddBullet;
                battleCube.GetBreakdown += GetBreakdown;
                battleCube.StopGameForAnimation += StopGame;
                battleCube.StopGameForAnimation += _view.HideAllButton; //Предчувствую я, что так нельзя, а надо отдельно метод в этом классе создать и его подписать.
            }
        }      
        
        private void Unscribe()
        {
            _view.StartGame -= StartGame;
            _view.StopGame -= StopGame;
            _view.Exit -= ExitToMainMenu;
            _view.DestroyView();
            _viewLog.DestroyView();

            _playerController.SomeTextAction -= AddTextAction;
            _playerController.ClosePlayerController();

            _timeCounter.DestroyView();
            foreach (BattleCube battleCube in _battleCubes)
            {
                battleCube.BulletInAir -= AddBullet;
                battleCube.GetBreakdown -= GetBreakdown;
                battleCube.StopGameForAnimation -= StopGame;
                battleCube.StopGameForAnimation -= _view.HideAllButton;
                battleCube.DestroyCube();
            }
        }

        private void AddTextAction(string textAction)
        {
            if (_isBattleStart)
            {
                _viewLog.AddAction(textAction);
            }            
        }        

        private void GetBreakdown(BattleCube cube)
        {
            
            bool isPlayerWinner;
            if (cube is AICube)
                isPlayerWinner = true;
            else
                isPlayerWinner = false;


            if (CheckSummRoundsToEnd(isPlayerWinner))
            {
                if (isPlayerWinner)
                {
                    _winingTimes.Add(_timeCounter.Timer);
                    SaveResults();
                    _playerWin++;
                    _view.HideAllButton();
                    GameEnded?.Invoke($"Поздравляем! Вы выиграли партию со счетом {_playerWin}:{_aiWin} за {_timeCounter.Timer} секунд.", isPlayerWinner);
                }
                else
                {
                    _aiWin++;
                    _view.HideAllButton();
                    GameEnded?.Invoke($"Увы, вы проиграли партию со счетом {_playerWin}:{_aiWin} за {_timeCounter.Timer} секунд.", isPlayerWinner);
                }
            }
            else
            {
                if (isPlayerWinner)
                {
                    _playerWin++;
                    RoundEnded?.Invoke($"Вы выиграли раунд! Счет {_playerWin}:{_aiWin}");
                }
                else
                {
                    _aiWin++;
                    RoundEnded?.Invoke($"Вы проиграли раунд. Счет {_playerWin}:{_aiWin}");
                }

                _view.SetCounts($"Игрок {_playerWin}:{_aiWin} Компьютер");
            }
        }

        private bool CheckSummRoundsToEnd(bool isPlayerWinner)
        {
            if (isPlayerWinner)
            {
                if (_playerWin + 1 == _roundToWin)
                    return true;
            }
            else
            {
                if(_aiWin + 1 == _roundToWin)
                    return true;
            }                

            return false;
        }

        private void AddBullet(Bullet bullet) => _bullets.Add(bullet);

        private void StopGame()
        {
            _battlegroundSounds.StopHelicopter();
            _timeCounter.StopCount();
            _isBattleStart = false;

            foreach (BattleCube battleCube in _battleCubes)
                battleCube.StopGame();

            foreach (Bullet bullet in _bullets)
                if (bullet != null)
                    bullet.DestroyBullet();

            _bullets.Clear();

            _view.ShowStartButton();
        }

        private void StartGame()
        {
            _battlegroundSounds.PlayHelicopter();
            _battlegroundSounds.PlayStartRound();
            foreach (BattleCube battleCube in _battleCubes)
                battleCube.StartGame();

            _timeCounter.StartCount();
            _isBattleStart = true;
            _view.ShowStopButton();
        }

        private void ExitToMainMenu()
        {
            _battlegroundSounds.StopHelicopter();
            SaveResults();
            Unscribe();
            ReturnToMainmenu?.Invoke();
        }

        private void SaveResults()
        {
            if (_winingTimes.Count == 0)
                return;

            TimeResultHolder resultHolder = new TimeResultHolder();
            resultHolder.AddNewResults(_winingTimes);
        }

    }
}

