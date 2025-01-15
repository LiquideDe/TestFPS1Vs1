using System.Collections;
using UnityEngine;

namespace FPS
{
    public class StateFireOn : StateLookAroundAndCorrectAngle, IState
    {
        private AICube _cube;
        private OrientPoint _player;
        private IStateSwitcher _switcher;
        private bool _isFired= false;
        private int _amountOfShots;

        public StateFireOn(AICube cube, OrientPoint player, IStateSwitcher switcher, int amountOfShots) : base (player, cube)
        {
            _cube = cube;
            _switcher = switcher;
            _player = player;
            _amountOfShots = amountOfShots;
        }

        public override void Enter()
        {
            SetAngleTolerande(5);
            SetTarget(_player);
            SetCorrectAngleOn();
            SetStateFindedPlayer(true);
            base.TargetRightAhead += TargetIsRightAhead;
            base.EnemyIsLost += EnemyLost;
        }        

        public override void Exit()
        {
            base.TargetRightAhead -= TargetIsRightAhead;
            base.EnemyIsLost -= EnemyLost;
        }

        private void TargetIsRightAhead()
        {
            if (_isFired == false) 
            {
                _isFired = true;
                _cube.StartCoroutine(PushBullets());
            }
            
        }

        private IEnumerator PushBullets()
        {
            if (IsOrientPointCanReached(_player, TypesBlock.Player))
                for (int i = 0; i < _amountOfShots; i++)
                {
                    _cube.Fire();
                    yield return new WaitForSeconds(0.3f);
                }
            yield return new WaitForSeconds(0.3f);
            _isFired = false;
            _switcher.SwitchState<StateEvasion>();
        }

        private void EnemyLost() => _switcher.SwitchState<StatePatrol>();
        
    }
}


