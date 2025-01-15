using System.Collections;
using UnityEngine;

namespace FPS
{
    public class StateEvasion : StateLookAroundAndCorrectAngle, IState
    {
        private AICube _cube;
        private IStateSwitcher _switcher;

        public StateEvasion(AICube cube, OrientPoint player, IStateSwitcher switcher) : base(player, cube)
        {
            _cube = cube;
            _switcher = switcher;
        }

        public override void Enter()
        {
            SetCorrectAngleOff();
            SetStateFindedPlayer(true);
            base.EnemyIsLost += EnemyLost;
            MoveEvasion();
        }

        public override void Exit()
        {
            base.EnemyIsLost -= EnemyLost;
        }

        private void MoveEvasion()
        {
            System.Random rand = new System.Random();
            int[] moves = new int[rand.Next(1,3)];

            int first = rand.Next(0, 2);
            if (first == 0)
                moves[0] = 1;
            else moves[0] = -1;

            for (int i = 1; i < moves.Length; i++)
            {
                if (moves[i-1] > 0)
                    moves[i] = -1;
                else
                    moves[i] = 1;
            }
            _cube.StartCoroutine(Moving(moves));
        }

        private IEnumerator Moving(int[] directions)
        {
            for(int i = 0; i < directions.Length; i++)
            {
                if (directions[i] > 0)
                    yield return MoveRight();
                else
                    yield return MoveLeft();
            }
            _switcher.SwitchState<StateFireOn>();
        }

        private IEnumerator MoveRight()
        {
            _cube.RotateRight();
            yield return new WaitForSeconds(0.8f);
        }

        private IEnumerator MoveLeft()
        {
            _cube.RotateLeft();
            yield return new WaitForSeconds(0.8f);
        }

        private void EnemyLost() => _switcher.SwitchState<StatePatrol>();
        
    }
}

