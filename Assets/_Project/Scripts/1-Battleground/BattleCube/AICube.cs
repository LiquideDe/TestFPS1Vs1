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
                base.UpdateManual(); //Не знал как лучше назвать, но UpdateManual звучит не очень явно, наверное стоило ManualUpdate?
            }
        }

        private void OnTriggerEnter(Collider other) => PointIsReached?.Invoke(other.gameObject);

        public void Initialize(List<OrientPoint> orientPoints, OrientPoint player, Vector3 startPosition, ConfigData configData)
        {
            base.Initialize(startPosition, configData);
            _cubeAIStateMachine = new CubeAIStateMachine(this, player, orientPoints, configData.clip);
        }

        public override void StartGame()
        {
            base.StartGame();
            _cubeAIStateMachine.SwitchState<StatePatrol>();
        }
    }
}


