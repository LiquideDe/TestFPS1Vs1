using Firebase.Analytics;

namespace FPS
{
    public class Analytics
    {
        private RuleStartStopGame _ruleStartStopGame;
        private PlayerCube _playerCube;
        private AICube _aICube;
        private int _amountRounds = 0;
        private int _amountBullets = 0;
        private int _playerWins = 0;
        private int _aiWins = 0;

        public Analytics(RuleStartStopGame ruleStartStopGame, PlayerCube playerCube, AICube aICube)
        {
            _ruleStartStopGame = ruleStartStopGame;
            _playerCube = playerCube;
            _aICube = aICube;
            Subscribe();
        }

        private void Subscribe()
        {
            _ruleStartStopGame.StartGame += StartGame;
            _ruleStartStopGame.StopGame += StopGame;
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


