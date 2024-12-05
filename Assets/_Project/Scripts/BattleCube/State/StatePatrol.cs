using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FPS
{
    public class StatePatrol : StateLookAroundAndCorrectAngle, IState
    {
        private AICube _cube;
        private IStateSwitcher _switcher;
        private List<OrientPoint> _orientPoints;
        private OrientPoint _currentTargetPoint;
        public StatePatrol(AICube cube, OrientPoint player, List<OrientPoint> orientPoints, IStateSwitcher switcher) : base (player, cube)
        {
            _cube = cube;
            _switcher = switcher;
            _orientPoints = orientPoints;
        }

        public override void Enter()
        {
            SetCorrectAngleOn();
            SetStateFindedPlayer(false);
            _cube.PointIsReached += PointIsReached;
            base.EnemyIsFinded += EnemyIsFounded;
            FindNewPoint();
        }        

        public override void Exit()
        {
            _cube.PointIsReached -= PointIsReached;
            base.EnemyIsFinded -= EnemyIsFounded;
        }

        private void FindNewPoint()
        {
            List<float> angles = new List<float>();

            foreach (OrientPoint point in _orientPoints)
            {
                Vector3 targetDir = point.transform.position - _cube.transform.position;
                float angle = Vector3.Angle(targetDir, _cube.transform.forward);
                point.Angle = angle;
                point.Distance = Vector3.Distance(_cube.transform.position, point.transform.position);
            }

            List<OrientPoint> SortedList = _orientPoints.OrderBy(o => o.Angle).ToList();
            List<OrientPoint> possiblePoints = new List<OrientPoint>();

            foreach (OrientPoint point in SortedList)
                if (point.Distance < 20 && point != _currentTargetPoint)
                {
                    if (IsOrientPointCanReached(point, TypesBlock.Wall) == false)
                        possiblePoints.Add(point);
                }

            System.Random random = new System.Random();
            _currentTargetPoint = possiblePoints[random.Next(0, possiblePoints.Count / 2)];
            _cube.FullForward();
            SetAngleTolerande(3);
            SetTarget(_currentTargetPoint);
            SetCorrectAngleOn();
        }

        private void PointIsReached(GameObject gameObject)
        {
            if(gameObject.TryGetComponent<OrientPoint>(out OrientPoint orient))
                if(orient == _currentTargetPoint)
                    FindNewPoint();
        }

        private void EnemyIsFounded() => _switcher.SwitchState<StateFireOn>();
    }
}


