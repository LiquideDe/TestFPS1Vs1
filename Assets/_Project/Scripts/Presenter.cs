using System;
using System.Collections.Generic;
using System.Linq;

namespace FPS
{
    public class Presenter
    {
        private BattleCube[] _battleCubes;
        private View _view;
        private RuleStartStopGame _ruleStartStopGame;
        private PlayerController _playerController;
        private List<Bullet> _bullets = new List<Bullet>();
        private List<string> _stringActions = new List<string>();
        private int _aiWin = 0, _playerWin = 0;
        private bool _isBattleStart = false;

        public Presenter(View view, RuleStartStopGame ruleStartStopGame, BattleCube[] battleCubes, PlayerController playerController)
        {
            _view = view;
            _battleCubes = new BattleCube[battleCubes.Length];
            _battleCubes = battleCubes;
            _playerController = playerController;
            _ruleStartStopGame = ruleStartStopGame;
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
                _stringActions.Add(textAction + "\n");
                if (_stringActions.Count > 31)
                    _stringActions.RemoveAt(0);

                string textToSend = String.Join(", ", _stringActions.Select(t => t));
                _view.SetActionsText(textToSend);
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
                _playerWin++;
            else
                _aiWin++;

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

