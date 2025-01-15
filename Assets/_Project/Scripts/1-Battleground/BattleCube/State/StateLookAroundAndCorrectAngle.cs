using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace FPS
{
    public class StateLookAroundAndCorrectAngle : IState
    {
        public enum TypesBlock { Player, Wall }

        public event Action EnemyIsFinded;
        public event Action EnemyIsLost;
        public event Action TargetRightAhead;

        private OrientPoint _player;
        private bool _isPlayerFinded = false;
        private bool _isCorrectAngleOn = false;
        private OrientPoint _currentTargetPoint;
        private int _angleTolerance = 3;
        private AICube _aICube;

        public StateLookAroundAndCorrectAngle(OrientPoint player, AICube aICube)
        {
            _player = player;
            _aICube = aICube;
        }

        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }

        public void Update()
        {
            CorrectAngle();
            FindPlayer();
        }

        public void SetTarget(OrientPoint orientPoint) => _currentTargetPoint = orientPoint;

        public void SetAngleTolerande(int tolerance) => _angleTolerance = tolerance;

        public void SetCorrectAngleOff() => _isCorrectAngleOn = false;
        public void SetCorrectAngleOn() => _isCorrectAngleOn = true;

        public void SetStateFindedPlayer(bool state) => _isPlayerFinded = state;

        public bool IsOrientPointCanReached(OrientPoint point, TypesBlock typesBlock)
        {
            Vector3 direct = point.transform.position - _aICube.transform.position;
            Ray ray = new Ray(_aICube.transform.position, direct);
            //Debug.DrawRay(transform.position, direct, UnityEngine.Color.black, 25);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
                if (typesBlock == TypesBlock.Player)
                {
                    if (hitInfo.transform.gameObject.TryGetComponent<PlayerCube>(out PlayerCube player))
                        return true;
                }
                else if (typesBlock == TypesBlock.Wall)
                    if (hitInfo.transform.gameObject.TryGetComponent<Wall>(out Wall wall))
                        return true;


            return false;
        }

        private void CorrectAngle()
        {
            if (_isCorrectAngleOn)
            {
                Vector3 targetDir = _currentTargetPoint.transform.position - _aICube.transform.position;
                float angle = Vector3.SignedAngle(targetDir, _aICube.transform.forward, _aICube.transform.up);
                Vector3 vector = Vector3.Cross(_aICube.transform.forward, targetDir);

                if (angle > _angleTolerance)
                    _aICube.RotateLeft();
                else if (angle < _angleTolerance * -1)
                    _aICube.RotateRight();
                else
                {
                    _aICube.StopRotate();
                    TargetRightAhead?.Invoke();
                }
            }

        }

        private void FindPlayer()
        {
            if (IsOrientPointCanReached(_player, TypesBlock.Player))
            {
                if (_isPlayerFinded == false)
                {
                    _isPlayerFinded = true;
                    EnemyIsFinded?.Invoke();
                }
            }
            else
            {
                if (_isPlayerFinded)
                {
                    _isPlayerFinded = false;
                    EnemyIsLost?.Invoke();
                }
            }
        }
    }
}

