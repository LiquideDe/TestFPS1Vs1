using System.Collections.Generic;
using UnityEngine;
using System;

namespace FPS
{
    public class AICube : BattleCube
    {        
        public event Action<GameObject> PointIsReached;
        private CubeAIStateMachine _cubeAIStateMachine;

        void Update()
        {
            if (_isGameStarted)
            {
                _cubeAIStateMachine.Update();
            }
        }

        private void OnTriggerEnter(Collider other) => PointIsReached?.Invoke(other.gameObject);

        public void Initialize(List<OrientPoint> orientPoints, OrientPoint player, Vector3 startPosition)
        {
            base.Initialize(startPosition);
            _cubeAIStateMachine = new CubeAIStateMachine(this, player, orientPoints);
        }

        public override void StartGame()
        {
            base.StartGame();
            _cubeAIStateMachine.SwitchState<StatePatrol>();
        }
    }
}


