using System;

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

        private void GoForward()
        {
            _player.FullForward();
            SomeTextAction?.Invoke("����� ��������� ������");
        }

        private void GoBack()
        {
            _player.FullBack();
            SomeTextAction?.Invoke("����� ��������� �����");
        }

        private void StopMove()
        {
            _player.StopCube();
            SomeTextAction?.Invoke("����� ���������������");
        }

        private void TurnRight()
        {
            _player.RotateRight();
            SomeTextAction?.Invoke("����� ������������ �������");
        }

        private void TurnLeft()
        {
            _player.RotateLeft();
            SomeTextAction?.Invoke("����� ������������ ������");
        }

        private void StopRotate()
        {
            _player.StopRotate();
            SomeTextAction?.Invoke("����� �������� ��������������");
        }

        private void Fire()
        {
            _player.Fire();
            SomeTextAction?.Invoke("����� ������ �����");
        }
    }
}


