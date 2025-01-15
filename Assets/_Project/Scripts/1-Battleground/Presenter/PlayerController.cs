using System;
using UnityEngine;

namespace FPS
{
    public class PlayerController
    {
        public event Action<string> SomeTextAction;
        private BattleCube _player;
        private InputFromKeyboard _input;

        public PlayerController(BattleCube player, InputFromKeyboard input)
        {
            _player = player;
            _input = input;
            Subscribe();
        }

        public void ClosePlayerController()
        {
            Unscribe();
            _player = null;
            _input = null;
        }

        private void Subscribe()
        {
            _input.Forward += GoForward;
            _input.Back += GoBack;
            _input.StopMove += StopMove;
            _input.TurnRight += TurnRight;
            _input.TurnLeft += TurnLeft;
            _input.StopRotate += StopRotate;
            _input.Fire += Fire;
        }

        private void Unscribe()
        {
            _input.Forward -= GoForward;
            _input.Back -= GoBack;
            _input.StopMove -= StopMove;
            _input.TurnRight -= TurnRight;
            _input.TurnLeft -= TurnLeft;
            _input.StopRotate -= StopRotate;
            _input.Fire -= Fire;
        }

        private void GoForward()
        {
            _player.FullForward();
            SomeTextAction?.Invoke("Игрок двигается вперед");
        }

        private void GoBack()
        {
            _player.FullBack();
            SomeTextAction?.Invoke("Игрок двигается назад");
        }

        private void StopMove()
        {
            _player.StopCube();
            SomeTextAction?.Invoke("Игрок останавливается");
        }

        private void TurnRight()
        {
            _player.RotateRight();
            SomeTextAction?.Invoke("Игрок поворачивает направо");
        }

        private void TurnLeft()
        {
            _player.RotateLeft();
            SomeTextAction?.Invoke("Игрок поворачивает налево");
        }

        private void StopRotate()
        {
            _player.StopRotate();
            SomeTextAction?.Invoke("Игрок перестал поворачиваться");
        }

        private void Fire()
        {
            _player.Fire();
            SomeTextAction?.Invoke("Игрок открыл огонь");
        }
    }
}


