using UnityEngine;

namespace FPS
{
    public class Mediator 
    {
        private GameObjectFactory _gameObjectFactory;
        private AudioManager _audioManager;
        private InputFromKeyboard _inputFromKeyboard;
        private BattleCubeFactory _battleCubeFactory;
        private BattlegroundPresenter _presenterBattleground;

        public Mediator(GameObjectFactory gameObjectFactory, AudioManager audioManager, InputFromKeyboard inputFromKeyboard, BattleCubeFactory battleCubeFactory)
        {
            _gameObjectFactory = gameObjectFactory;
            _audioManager = audioManager;
            _inputFromKeyboard = inputFromKeyboard;
            _battleCubeFactory = battleCubeFactory;
        }

        public void ShowMainMenu()
        {
            MainMenuView view = _gameObjectFactory.Get(TypeScene.MainMenu).GetComponent<MainMenuView>();
            MainMenuPresenter presenter = new MainMenuPresenter(view, _audioManager);
            presenter.StartGame += ShowBattleground;
            presenter.ShowShop += ShowShop;
        }

        private void ShowShop()
        {
            ShopView view = _gameObjectFactory.Get( TypeScene.Shop ).GetComponent<ShopView>();
            ShopPresenter presenter = new ShopPresenter(_audioManager, view);
            presenter.Close += ShowMainMenu;
        }

        private void ShowBattleground()
        {
            GameObject gameObject = _gameObjectFactory.Get(TypeScene.BattlegroundView);
            BattlegroundView view = gameObject.GetComponent<BattlegroundView>();
            ViewLog viewLog = gameObject.GetComponent<ViewLog>();
            TimeCounter timeCounter = _gameObjectFactory.Get(TypeScene.Timer).GetComponent<TimeCounter>();

            BattleCube[] battleCubes = new BattleCube[2];
            battleCubes[0] = _battleCubeFactory.Get(BattleCubeTypes.Player).GetComponent<BattleCube>();
            battleCubes[1] = _battleCubeFactory.Get(BattleCubeTypes.Ai).GetComponent<BattleCube>();

            _presenterBattleground = new BattlegroundPresenter(view, viewLog, battleCubes, _inputFromKeyboard, timeCounter, _audioManager);
            _presenterBattleground.RoundEnded += ShowAdvertising;
            _presenterBattleground.ReturnToMainmenu += ShowMainMenu;
            _presenterBattleground.GameEnded += ShowGameOver;

            Analytics analytics = new Analytics(view, battleCubes[0], battleCubes[1]);
        }        

        private void ShowAdvertising(string text)
        {
            ViewDeadWindow deadWindow = _gameObjectFactory.Get(TypeScene.AdvertisingMenu).GetComponent<ViewDeadWindow>();
            deadWindow.GetReward += _presenterBattleground.RewardPlayer;
            deadWindow.AdClosed += _presenterBattleground.ShowStartButton;
            deadWindow.Initialize(text, _audioManager);
        }

        private void ShowGameOver(string text, bool isPlayerWin)
        {
            GameOverView view = _gameObjectFactory.Get( TypeScene.GameOverPanel ).GetComponent<GameOverView>();
            GameOverPresenter presenter = new GameOverPresenter(view, text, _audioManager, isPlayerWin);
            presenter.ExitMainMenu += ReturnToMainmenu;
            presenter.PlayAgain += PlayAgain;
        }

        private void ReturnToMainmenu()
        {
            _presenterBattleground.StopPlaying();
            ShowMainMenu();
        }

        private void PlayAgain()
        {
            _presenterBattleground.PlayAgain();
        }


    }
}


