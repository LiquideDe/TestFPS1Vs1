using Firebase.Analytics;

namespace FPS
{
    public class Analytics
    {
        private BattlegroundView _view;
        private BattleCube _playerCube;
        private BattleCube _aICube;
        private int _amountRounds = 0;
        private int _amountBullets = 0;
        private int _playerWins = 0;
        private int _aiWins = 0;

        public Analytics(BattlegroundView view, BattleCube playerCube, BattleCube aICube)
        {
            _view = view;
            _playerCube = playerCube;
            _aICube = aICube;
            Subscribe();
        }

        private void Subscribe()
        {
            _view.StartGame += StartGame;
            _view.StopGame += StopGame;
            _playerCube.BulletInAir += BulletInAir;
            _playerCube.GetBreakdown += AiWin;
            _aICube.GetBreakdown += PlayerWin;
        }

        private void PlayerWin(BattleCube cube)
        {
            _playerWins++;
            SendAnalytics();
        }

        private void AiWin(BattleCube cube)
        {
            _aiWins++;
            SendAnalytics();
        }

        private void BulletInAir(Bullet bullet) => _amountBullets++;

        private void StartGame() => _amountRounds++;

        private void StopGame() => SendAnalytics();

        private void SendAnalytics()
        {
            FirebaseAnalytics.LogEvent("Round", new Parameter[] {
            new Parameter("round", _amountRounds),
            new Parameter("bulletsWasFired", _amountBullets),
            new Parameter("playerWins", _playerWins ),
            new Parameter("aiWins", _aiWins)
        });
            _amountBullets = 0;
        }
    }
}


