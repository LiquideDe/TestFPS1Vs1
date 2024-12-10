using UnityEngine;

namespace FPS
{
    public class Mediator : MonoBehaviour
    {
        [SerializeField] private ViewDeadWindow _canvasAdsPrefab;
        private BattleCube _player;

        public void Initialize(Presenter presenter, BattleCube player)
        {
            presenter.PlayerWin += PlayerWin;
            presenter.PlayerLoose += PlayerLoose;
            _player = player;
        }

        private void PlayerLoose()
        {
            ViewDeadWindow viewDead = Instantiate(_canvasAdsPrefab);
            viewDead.Initialize(_player, "Вы проиграли.");
        }

        private void PlayerWin()
        {
            ViewDeadWindow viewDead = Instantiate(_canvasAdsPrefab);
            viewDead.Initialize(_player, "Вы выиграли!");
        }
    }
}


