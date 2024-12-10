using System;
using System.Collections.Generic;

namespace FPS
{
    public class Presenter
    {
        public event Action PlayerWin;
        public event Action PlayerLoose;
        private BattleCube[] _battleCubes;
        private View _view;
        private ViewLog _viewLog;
        private RuleStartStopGame _ruleStartStopGame;
        private PlayerController _playerController;
        private List<Bullet> _bullets = new List<Bullet>();
        private int _aiWin = 0, _playerWin = 0;
        private bool _isBattleStart = false;

        public Presenter(View view,ViewLog viewLog, RuleStartStopGame ruleStartStopGame, BattleCube[] battleCubes, PlayerController playerController)
        {
            _view = view;
            _battleCubes = new BattleCube[battleCubes.Length];
            _battleCubes = battleCubes;
            _playerController = playerController;
            _ruleStartStopGame = ruleStartStopGame;
            _viewLog = viewLog;
            Subscribe();
        }

        private void Subscribe()
        {
            _ruleStartStopGame.StartGame += StartGame;
            _ruleStartStopGame.StopGame += StopGame;

            _playerController.SomeTextAction += AddTextAction;

            foreach (BattleCube battleCube in _battleCubes)
            {
                battleCube.BulletInAir += AddBullet;
                battleCube.GetBreakdown += GetBreakdown;
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
            StopGame();
            foreach (Bullet bullet in _bullets)
                if (bullet != null)
                    bullet.DestroyBullet();
            _bullets.Clear();

            if (cube is AICube)
            {
                _playerWin++;
                PlayerWin?.Invoke();
            }
            else
            {
                _aiWin++;
                PlayerLoose?.Invoke();
            }                

            _view.SetCounts($"Игрок {_playerWin}:{_aiWin} Компьютер");
        }

        private void AddBullet(Bullet bullet) => _bullets.Add(bullet);

        private void StopGame()
        {
            foreach (BattleCube battleCube in _battleCubes)
                battleCube.StopGame();

            _isBattleStart = false;
            _ruleStartStopGame.ShowStartButton();
        }

        private void StartGame()
        {
            foreach (BattleCube battleCube in _battleCubes)
                battleCube.StartGame();

            _isBattleStart = true;
            _ruleStartStopGame.ShowStopButton();
        }
    }
}

