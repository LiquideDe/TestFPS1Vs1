using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FPS
{
    public class CubeAIStateMachine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;

        public CubeAIStateMachine(AICube aICube,OrientPoint player, List<OrientPoint> orientPoints)
        {
            _states = new List<IState>()
            {
                new StatePatrol(aICube,player,orientPoints,this ),
                new StateFireOn(aICube, player, this),
                new StateEvasion(aICube, player, this)
            };

            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update()
        {
            if (_currentState != null)
            {
                _currentState.Update();
            }
        }

    }
}


